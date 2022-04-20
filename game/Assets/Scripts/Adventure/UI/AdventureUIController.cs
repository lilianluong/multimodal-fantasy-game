using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdventureUIController : MonoBehaviour
{
    // References
    public HealthBar playerHealthBar, enemyHealthBar;
    public Text spellLogBody;
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
        // TODO: Update the UI's turn timer
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
