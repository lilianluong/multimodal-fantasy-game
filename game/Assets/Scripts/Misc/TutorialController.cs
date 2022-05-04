using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    // References
    public Button adventureButton, spellTutorButton;
    public Text turnText, titleText;
    private ServerManager serverManager;

    private TutorialState state;
    public int animationState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        serverManager = GetComponent<ServerManager>();
        adventureButton.onClick.AddListener(GoToAdventure);
        spellTutorButton.onClick.AddListener(GoToSpellTutor);

        titleText.text = GlobalData.PlayerDied ? "YOU WERE KILLED" : "HOW TO PLAY";
        GlobalData.PlayerDied = false;

        state = TutorialState.Waiting;
        animationState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTutorialLoop();
    }

    private void HandleTutorialLoop()
    {
        if (state == TutorialState.Waiting)
        {
            // Start a new turn
            if (serverManager.CanMakeRequest())
            {
                // Only do stuff if we aren't still waiting for the request to finish
                if (serverManager.StartedTurn)
                {
                    // We just started a new turn
                    state = TutorialState.PlayerTurn;
                    serverManager.StartedTurn = false;
                }
                else
                {
                    // We should send the request to start a turn
                    serverManager.StartTurn(3f);  // how long should tutorial practice turns last for?
                }
            }
        }
        else if (state == TutorialState.PlayerTurn)
        {
            if (animationState == 1)
            {
                state = TutorialState.Waiting;
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
                        UpdateTurnTimer(response.timeRemaining);
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
        UpdateTurnTimer(-1);
        string spokenCommand = response.spokenCommand;
        if (spokenCommand == "adventure")
        {
            GoToAdventure();
            return;
        }
        else if (spokenCommand == "spell tutor")
        {
            GoToSpellTutor();
            return;
        }
        animationState++;
    }

    private void GoToAdventure()
    {
        SceneManager.LoadScene("Adventure");
    }

    private void GoToSpellTutor()
    {
        SceneManager.LoadScene("Tutor");
    }

    private void UpdateTurnTimer(float timeRemaining)
    {
        if (timeRemaining < 0)
        {
            turnText.text = "";
            return;
        }
        int secsRemaining = Mathf.CeilToInt(timeRemaining);
        turnText.text = $"{secsRemaining}";
    }
}


public enum TutorialState { Waiting, PlayerTurn };
