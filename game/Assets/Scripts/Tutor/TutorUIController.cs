using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorUIController : MonoBehaviour
{
    // References
    public GameObject colorFlarePrefab;  // notifierPrefab
    // public Transform leftNotificationContainer, rightNotificationContainer;
    public Text spellLogBody, turnText;
    public Button adventureButton, tutorialButton;

    // Start is called before the first frame update
    void Start()
    {
        adventureButton.onClick.AddListener(GoToAdventure);
        tutorialButton.onClick.AddListener(GoToTutorial);
        ResetSpellLog();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToAdventure()
    {
        SceneManager.LoadScene("Adventure");
    }

    public void GoToTutorial()
    {
        // SceneManager.LoadScene("Tutorial");
    }

    public void UpdateTurnTimer(float timeRemaining)
    {
        if (timeRemaining < 0)
        {
            turnText.text = "";
            return;
        }
        int secsRemaining = Mathf.CeilToInt(timeRemaining);
        if (secsRemaining == 1) turnText.text = $"Cast a spell in {secsRemaining} second...";
        else turnText.text = $"Cast a spell in {secsRemaining} seconds...";
    }

    // public void CreateNotifier(string message, bool forPlayer)
    // {
    //     Transform whichContainer = forPlayer ? leftNotificationContainer : rightNotificationContainer;
    //     GameObject notifier = Instantiate(notifierPrefab, whichContainer);
    //     notifier.GetComponent<Text>().text = message;
    // }

    public void ColorFlare(float r, float g, float b)
    {
        GameObject flare = Instantiate(colorFlarePrefab, transform);
        flare.GetComponent<ColorFlareScript>().SetColor(r, g, b);
    }

    public void UpdateSpellLog(string spellName, float score)
    {
        int accuracy = (int)score;
        spellLogBody.text = $"Spell Detected: {spellName}\nAccuracy: {accuracy}%";
    }

    public void ResetSpellLog()
    {
        spellLogBody.text = "No spell was cast.";
    }
}
