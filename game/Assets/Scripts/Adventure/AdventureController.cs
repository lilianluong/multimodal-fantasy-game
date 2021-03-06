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
                    if (response.turnState == 0)
                    {
                        // Waiting for the recording to start, we chill
                        uiController.UpdateWaitForTurn();
                    }
                    else if (response.timeRemaining >= 0)
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
                float flameDamage = rightCharacterState.TakeDamage((response.score + 0.05f) * leftCharacter.AttackDamage);
                uiController.CreateNotifier($"{rightCharacter.Name} took {Mathf.RoundToInt(flameDamage)} damage", forPlayer: false);
                uiController.ColorFlare(1f, 0f, 0f);
                uiController.UpdateSpellLog(new SpellcastInfo("FLAME", response.score, new SpellEffect(SpellEffectType.Damage, flameDamage)));
                break;
            case "cure":
                float curedHeal = leftCharacterState.Heal(response.score * 30);
                uiController.CreateNotifier($"You healed for {Mathf.RoundToInt(curedHeal)} HP", forPlayer: true);
                uiController.ColorFlare(0.2f, 1f, 0.2f);
                uiController.UpdateSpellLog(new SpellcastInfo("CURE", response.score, new SpellEffect(SpellEffectType.Heal, curedHeal)));
                break;
            case "lightning":
                float lightningDamage = rightCharacterState.TakeDamage(response.score * (leftCharacter.AttackDamage + (6 * Random.value - 2)));
                uiController.CreateNotifier($"{rightCharacter.Name} took {Mathf.RoundToInt(lightningDamage)} damage", forPlayer: false);
                uiController.ColorFlare(1f, 1f, 0.5f);
                uiController.UpdateSpellLog(new SpellcastInfo("LIGHTNING", response.score, new SpellEffect(SpellEffectType.Damage, lightningDamage)));
                break;
            case "leech":
                float leechedDamage = rightCharacterState.TakeDamage(response.score * leftCharacter.AttackDamage);
                float leechedHeal = leftCharacterState.Heal(leechedDamage);
                uiController.CreateNotifier($"You leeched {Mathf.RoundToInt(leechedHeal)} HP", forPlayer: true);
                uiController.ColorFlare(0.7f, 0.7f, 0.4f);
                uiController.UpdateSpellLog(new SpellcastInfo("LEECH", response.score, new SpellEffect(SpellEffectType.Heal, leechedHeal)));
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
        float randomNum = Random.value;

        if (randomNum <= 0.8f)  // note that Random.value is [0f, 1f] INCLUSIVE for some inane reason
        {
            // Attack
            float dealtDamage = leftCharacterState.TakeDamage(rightCharacter.AttackDamage * (0.9f + Random.value * 0.5f));
            uiController.CreateNotifier($"You took {Mathf.RoundToInt(dealtDamage)} damage", forPlayer: true);
        }
        else
        {
            // Heal
            float healAmount = rightCharacterState.Heal(rightCharacter.AttackDamage * (0.2f + Random.value));
            uiController.CreateNotifier($"{rightCharacter.Name} healed for {Mathf.RoundToInt(healAmount)} HP", forPlayer: false);
        }

        uiController.GetPlayerHealthBar().SetHealth(leftCharacterState.GetHealth().Item1);
        // uiController.GetEnemyHealthBar().SetHealth(rightCharacterState.GetHealth().Item1);
        animationState = 2;

        HandleKills();
    }

    private void InitializeNewEncounter()
    {
        // Choose an enemy
        rightCharacter = EnemyManager.GetRandomEnemy();
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
