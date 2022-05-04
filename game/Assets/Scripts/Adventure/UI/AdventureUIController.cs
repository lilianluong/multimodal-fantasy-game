using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdventureUIController : MonoBehaviour
{
    // References
    public GameObject notifierPrefab, colorFlarePrefab;
    public Transform leftNotificationContainer, rightNotificationContainer;
    public HealthBar playerHealthBar, enemyHealthBar;
    public Text spellLogBody, turnText;
    public Button spellTutorButton;

    // Start is called before the first frame update
    void Start()
    {
        spellTutorButton.onClick.AddListener(GoToSpellTutor);
        ResetSpellLog();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToSpellTutor()
    {
        SceneManager.LoadScene("SpellTutor");
    }

    public void UpdateTurnTimer(float timeRemaining)
    {
        if (timeRemaining < 0)
        {
            turnText.text = "It's your turn!";
            return;
        }
        int secsRemaining = Mathf.CeilToInt(timeRemaining);
        if (secsRemaining == 1) turnText.text = $"It's your turn!\nCast a spell in {secsRemaining} second...";
        else turnText.text = $"It's your turn!\nCast a spell in {secsRemaining} seconds...";
    }

    public void UpdateTurnToEnemy(string enemyName)
    {
        turnText.text = $"It's {enemyName}'s turn!";
    }

    public void CreateNotifier(string message, bool forPlayer)
    {
        Transform whichContainer = forPlayer ? leftNotificationContainer : rightNotificationContainer;
        GameObject notifier = Instantiate(notifierPrefab, whichContainer);
        notifier.GetComponent<Text>().text = message;
    }

    public void ColorFlare(float r, float g, float b)
    {
        GameObject flare = Instantiate(colorFlarePrefab, transform);
        flare.GetComponent<ColorFlareScript>().SetColor(r, g, b);
    }


    public void UpdateSpellLog(SpellcastInfo? spellcastInfo)
    {
        if (!spellcastInfo.HasValue)
        {
            spellLogBody.text = "No spell was cast.";
            return;
        }
        string spellName = spellcastInfo.Value.Name;
        int accuracy = (int)spellcastInfo.Value.Score;
        SpellEffect spellEffect = spellcastInfo.Value.Effect;
        spellLogBody.text = $"Spell Detected: {spellName}\nAccuracy: {accuracy}%\n{spellEffect.LogString()}";
    }

    public void ResetSpellLog() => UpdateSpellLog(null);

    public HealthBar GetPlayerHealthBar() => playerHealthBar;
    public HealthBar GetEnemyHealthBar() => enemyHealthBar;
}
