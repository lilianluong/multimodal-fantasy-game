using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    // References
    public AdventureUIController uiController;
    private ServerManager serverManager;

    // Combat state
    private Character leftCharacter, rightCharacter;
    private CharacterState leftCharacterState, rightCharacterState;

    private AdventureState state;
    private bool inAnimation;

    // Start is called before the first frame update
    void Start()
    {
        serverManager = GetComponent<ServerManager>();
        InitializeLeftCharacter();
        InitializeNewEncounter();

        state = AdventureState.Waiting;
        inAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEncounterLoop();
    }

    private void HandleEncounterLoop()
    {
        if (state == AdventureState.Waiting)
        {
            // Start a new turn
            if (serverManager.CanMakeRequest())
            {
                // Only do stuff if we aren't still waiting for the request to finish
                if (serverManager.StartedTurn)
                {
                    // We just started a new turn
                    state = AdventureState.PlayerTurn;
                    serverManager.StartedTurn = false;
                }
                else
                {
                    // We should send the request to start a turn
                    serverManager.StartTurn(rightCharacter.PlayerTurnTime);
                }
            }
        }
        else if (state == AdventureState.PlayerTurn)
        {
            if (inAnimation)
            {
                state = AdventureState.EnemyTurn;
                serverManager.PolledTurn = false;
                return;
            }
            // Player's turn! Just poll repeatedly until we get our result
            if (serverManager.CanMakeRequest())
            {
                // Only do stuff if we aren't still waiting for the request to finish
                if (serverManager.PolledTurn)
                {
                    PollTurnResponse response = serverManager.PollResponse;
                    serverManager.PolledTurn = false;
                    if (response.timeRemaining > 0)
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
        else if (state == AdventureState.EnemyTurn)
        {
            // Enemy's turn! Take an action.
            if (inAnimation)
            {
                state = AdventureState.Waiting;
                return;
            }
            HandleEnemyTurn();
        }
    }

    private void HandlePlayerTurn(PollTurnResponse response)
    {
        Debug.Log("Player turn: " + response.ToString());
        inAnimation = true;
    }

    private void HandleEnemyTurn()
    {
        Debug.Log("Enemy turn!");
        inAnimation = true;
    }

    private void InitializeNewEncounter()
    {
        // Choose an enemy
        rightCharacter = new Player();  // Replace with an enemy (instance of Character)
        // Initialize CharacterState
        leftCharacterState = new CharacterState(leftCharacter);
        rightCharacterState = new CharacterState(rightCharacter);
    }

    private void InitializeLeftCharacter()
    {
        // The left character is Player
        leftCharacter = new Player();
    }
}


public enum AdventureState { Waiting, PlayerTurn, EnemyTurn };
