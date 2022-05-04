using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorController : MonoBehaviour
{
    // References
    public static TutorController Instance { get; set; }
    public TutorUIController uiController;
    private ServerManager serverManager;
    public TutorAnimator tutorAnimator;

    private TutorState state;
    public int animationState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        serverManager = GetComponent<ServerManager>();

        state = TutorState.Waiting;
        animationState = 0;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTutorLoop();
    }

    private void HandleTutorLoop()
    {
        if (state == TutorState.Waiting)
        {
            // Start a new turn
            if (serverManager.CanMakeRequest())
            {
                // Only do stuff if we aren't still waiting for the request to finish
                if (serverManager.StartedTurn)
                {
                    // We just started a new turn
                    state = TutorState.PlayerTurn;
                    serverManager.StartedTurn = false;
                }
                else
                {
                    // We should send the request to start a turn
                    serverManager.StartTurn(3f);  // how long should spell practice turns last for?
                }
            }
        }
        else if (state == TutorState.PlayerTurn)
        {
            if (animationState == 2)
            {
                state = TutorState.Waiting;
                serverManager.PolledTurn = false;
                animationState = 0;
                return;
            }
            else if (animationState > 0) return;
            // Player's turn! Just poll repeatedly until we get our result
            if (serverManager.CanMakeRequest())
            {
                // Only do stuff if we aren't still waiting for the request to finish
                if (serverManager.PolledTurn)
                {
                    PollTurnResponse response = serverManager.PollResponse;
                    serverManager.PolledTurn = false;
                    if (response.timeRemaining >= 0)
                    {
                        // Turn is still going, let's update the timer and wait
                        uiController.UpdateTurnTimer(response.timeRemaining);
                    }
                    else
                    {
                        // We successfully finished our turn and got a result
                        HandlePlayerTurn(response);
                        return;
                    }
                }
                // Whether or not there is value, poll again as long as we didn't finish our turn
                serverManager.PollTurn();
            }
        }
    }

    private void HandlePlayerTurn(PollTurnResponse response)
    {
        Debug.Log("Player turn: " + response.ToString());
        uiController.UpdateTurnTimer(-1f);

        if (response.spokenCommand.Length > 0)
        {
            string spokenCommand = response.spokenCommand;
            if (spokenCommand == "adventure")
            {
                uiController.GoToAdventure();
                return;
            }
            else if (spokenCommand == "tutorial")
            {
                uiController.GoToTutorial();
                return;
            }
            tutorAnimator.SwitchToSpell(spokenCommand);
        }

        switch (response.spellCast)
        {
            // SPELLS IMPLEMENTED HERE
            case "flame":
                uiController.ColorFlare(1f, 0f, 0f);
                uiController.UpdateSpellLog("FLAME", response.score);
                break;
            case "cure":
                uiController.ColorFlare(0.2f, 1f, 0.2f);
                uiController.UpdateSpellLog("CURE", response.score);
                break;
            case "lightning":
                uiController.ColorFlare(1f, 1f, 0.5f);
                uiController.UpdateSpellLog("LIGHTNING", response.score);
                break;
            case "leech":
                uiController.ColorFlare(0.7f, 0.7f, 0.4f);
                uiController.UpdateSpellLog("LEECH", response.score);
                break;
            default:
                animationState++;  // skip flare
                uiController.ResetSpellLog();
                break;
        }
        animationState++;
    }
}


public enum TutorState { Waiting, PlayerTurn };
