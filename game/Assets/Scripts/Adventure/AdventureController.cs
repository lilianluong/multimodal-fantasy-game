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
        // InitializeNewEncounter();

        state = AdventureState.MakingEnemy;
        animationState = 6;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEncounterLoop();
    }

    private void HandleKills()
    {
        if (leftCharacterState.IsDead())
        {
            // Player died
            GlobalData.PlayerDied = true;
            uiController.GoToTutorial();
        }
        else if (rightCharacterState.IsDead())
        {
            // Player killed the enemy
            state = AdventureState.MakingEnemy;
        }
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
        else if (state == AdventureState.EnemyTurn)
        {
            // Enemy's turn! Take an action.
            if (animationState == 3)
            {
                state = AdventureState.Waiting;
                animationState = 0;
                return;
            }
            else if (animationState > 0) return;
            HandleEnemyTurn();
        }
        else if (state == AdventureState.MakingEnemy)
        {
            if (animationState == 3)
            {
                uiController.CreateNotifier($"{rightCharacter.Name} was killed", forPlayer: false);
                uiController.HideEnemyImage();
                animationState++;
            }
            else if (animationState == 6)
            {
                InitializeNewEncounter();
                uiController.ShowEnemyImage(Resources.Load<Sprite>(rightCharacter.Name));
                animationState++;
            }
            else if (animationState == 8)
            {
                state = AdventureState.Waiting;
                animationState = 0;
            }
        }
    }

    private void HandlePlayerTurn(PollTurnResponse response)
    {
        Debug.Log("Player turn: " + response.ToString());
        uiController.UpdateTurnTimer(-1f);
        if (response.spokenCommand == "spell tutor")
        {
            uiController.GoToSpellTutor();
            return;
        }
        else if (response.spokenCommand == "tutorial")
        {
            uiController.GoToTutorial();
            return;
        }

        switch (response.spellCast)
        {
            // SPELLS IMPLEMENTED HERE
            case "flame":
                float dealtDamage = rightCharacterState.TakeDamage(response.score * leftCharacter.AttackDamage);
                uiController.CreateNotifier($"{rightCharacter.Name} took {Mathf.RoundToInt(dealtDamage)} damage", forPlayer: false);
                uiController.ColorFlare(1f, 0f, 0f);
                uiController.UpdateSpellLog(new SpellcastInfo("FLAME", response.score, new SpellEffect(SpellEffectType.Damage, dealtDamage)));
                break;
            case "cure":
                float healedAmount = leftCharacterState.Heal(response.score * 30);
                uiController.CreateNotifier($"You healed for {Mathf.RoundToInt(healedAmount)} HP", forPlayer: true);
                uiController.ColorFlare(0.2f, 1f, 0.2f);
                uiController.UpdateSpellLog(new SpellcastInfo("CURE", response.score, new SpellEffect(SpellEffectType.Heal, healedAmount)));
                break;
            default:
                animationState++;  // skip flare
                uiController.CreateNotifier("Your spell fizzled...", forPlayer: true);
                uiController.ResetSpellLog();
                break;
        }
        uiController.GetPlayerHealthBar().SetHealth(leftCharacterState.GetHealth().Item1);
        uiController.GetEnemyHealthBar().SetHealth(rightCharacterState.GetHealth().Item1);
        animationState++;

        HandleKills();
    }

    private void HandleEnemyTurn()
    {
        Debug.Log("Enemy turn!");
        uiController.UpdateTurnToEnemy(rightCharacter.Name);
        double randomNum = Random.value;

        if (randomNum <= 1f)  // note that Random.value is [0f, 1f] INCLUSIVE for some inane reason
        {
            // Attack
            float dealtDamage = leftCharacterState.TakeDamage(rightCharacter.AttackDamage);
            uiController.CreateNotifier($"You took {Mathf.RoundToInt(dealtDamage)} damage", forPlayer: true);
        }
        // add extra cases if we want

        uiController.GetPlayerHealthBar().SetHealth(leftCharacterState.GetHealth().Item1);
        // uiController.GetEnemyHealthBar().SetHealth(rightCharacterState.GetHealth().Item1);
        animationState = 2;

        HandleKills();
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
        uiController.GetEnemyHealthBar().SetName(rightCharacter.Name);
    }

    private void InitializeLeftCharacter()
    {
        // The left character is Player
        leftCharacter = new Player();
    }
}


public enum AdventureState { Waiting, PlayerTurn, EnemyTurn, MakingEnemy };
