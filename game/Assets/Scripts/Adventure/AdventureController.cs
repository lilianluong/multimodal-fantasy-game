using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    // References
    public static AdventureController Instance { get; set; }
    public AdventureUIController uiController;
    private ServerManager serverManager;

    // Combat state
    private Character leftCharacter, rightCharacter;
    private CharacterState leftCharacterState, rightCharacterState;

    private AdventureState state;
    public int animationState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        serverManager = GetComponent<ServerManager>();
        InitializeLeftCharacter();
        InitializeNewEncounter();

        state = AdventureState.Waiting;
        animationState = 0;
        Instance = this;
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
            if (animationState == 3)
            {
                state = AdventureState.EnemyTurn;
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
            if (animationState == 2)
            {
                state = AdventureState.Waiting;
                animationState = 0;
                return;
            }
            else if (animationState > 0) return;
            HandleEnemyTurn();
        }
    }

    private void HandlePlayerTurn(PollTurnResponse response)
    {
        Debug.Log("Player turn: " + response.ToString());
        switch (response.spellCast)
        {
            case "attack":
                float dealtDamage = rightCharacterState.TakeDamage(response.score * leftCharacter.AttackDamage);
                uiController.CreateNotifier($"{rightCharacter.Name} took {(int)dealtDamage} damage", forPlayer: false);
                uiController.ColorFlare(1f, 0f, 0f);
                uiController.UpdateSpellLog(new SpellcastInfo("ATTACK", response.score, new SpellEffect(SpellEffectType.Damage, dealtDamage)));
                break;
            case "heal":
                float healedAmount = leftCharacterState.Heal(response.score * 30);
                uiController.CreateNotifier($"You healed for {(int)healedAmount} HP", forPlayer: true);
                uiController.ColorFlare(0.2f, 1f, 0.2f);
                uiController.UpdateSpellLog(new SpellcastInfo("HEAL", response.score, new SpellEffect(SpellEffectType.Heal, healedAmount)));
                break;
            default:
                break;
        }
        uiController.GetPlayerHealthBar().SetHealth(leftCharacterState.GetHealth().Item1);
        uiController.GetEnemyHealthBar().SetHealth(rightCharacterState.GetHealth().Item1);
        animationState = 1;
    }

    private void HandleEnemyTurn()
    {
        Debug.Log("Enemy turn!");
        double randomNum = Random.value;

        if (randomNum <= 1f)  // note that Random.value is [0f, 1f] INCLUSIVE for some inane reason
        {
            // Attack
            float dealtDamage = leftCharacterState.TakeDamage(rightCharacter.AttackDamage);
            uiController.CreateNotifier($"You took {(int)dealtDamage} damage", forPlayer: true);
        }
        // add extra cases if we want

        uiController.GetPlayerHealthBar().SetHealth(leftCharacterState.GetHealth().Item1);
        // uiController.GetEnemyHealthBar().SetHealth(rightCharacterState.GetHealth().Item1);
        animationState = 1;
    }

    private void InitializeNewEncounter()
    {
        // Choose an enemy
        rightCharacter = new Werewolf();  // Replace with a random enemy (instance of Character)
        // Initialize CharacterState
        leftCharacterState = new CharacterState(leftCharacter);
        rightCharacterState = new CharacterState(rightCharacter);

        // Set health bars
        (float playerHealth, float playerMaxHealth) = leftCharacterState.GetHealth();
        uiController.GetPlayerHealthBar().SetHealth(playerHealth, playerMaxHealth);

        (float enemyHealth, float enemyMaxHealth) = rightCharacterState.GetHealth();
        uiController.GetEnemyHealthBar().SetHealth(enemyHealth, enemyMaxHealth);
    }

    private void InitializeLeftCharacter()
    {
        // The left character is Player
        leftCharacter = new Player();
    }
}


public enum AdventureState { Waiting, PlayerTurn, EnemyTurn };
