using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorUIController : MonoBehaviour
{
    // References
    public GameObject colorFlarePrefab, spellNamePrefab;  // notifierPrefab
    public Transform spellNameContainer;
    // public Transform leftNotificationContainer, rightNotificationContainer;
    public Text spellLogBody, turnText;
    public Button adventureButton, tutorialButton;

    private List<string> spellNames;
    private Dictionary<string, Text> spellNameTexts;
    private string highlightedSpell;

    // Start is called before the first frame update
    void Start()
    {
        adventureButton.onClick.AddListener(GoToAdventure);
        tutorialButton.onClick.AddListener(GoToTutorial);
        ResetSpellLog();

        turnText.text = "";

        spellNames = SpellExamples.GetExampleNames();
        spellNameTexts = new Dictionary<string, Text>();
        int i = 0;
        foreach (string name in spellNames)
        {
            GameObject textObject = Instantiate(spellNamePrefab, spellNameContainer);
            textObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -70f * i);
            Text textText = textObject.GetComponent<Text>();
            spellNameTexts[name] = textText;
            textText.text = name.ToUpper();
            i++;
        }
        highlightedSpell = spellNames[0];
        HighlightSpell(highlightedSpell);
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
        SceneManager.LoadScene("Tutorial");
    }

    public void UpdateWaitForTurn()
    {
        turnText.text = "Get ready...";
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
        int accuracy = Mathf.RoundToInt(score * 100);
        spellLogBody.text = $"Spell Detected: {spellName}\nAccuracy: {accuracy}%";
    }

    public void ResetSpellLog()
    {
        spellLogBody.text = "No spell was cast.";
    }

    public void HighlightSpell(string spellName)
    {
        if (spellNameTexts.ContainsKey(spellName))
        {
            Text highlightedText = spellNameTexts[highlightedSpell];
            highlightedText.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            highlightedText.fontStyle = FontStyle.Normal;
            highlightedSpell = spellName;
            Text newHighlightText = spellNameTexts[spellName];
            newHighlightText.color = new Color(1f, 1f, 1f, 1f);
            newHighlightText.fontStyle = FontStyle.Bold;
        }
    }
}
