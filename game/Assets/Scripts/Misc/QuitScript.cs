using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitScript : MonoBehaviour
{
    public float quitTimeout = 2f;

    private float timer = 0f;
    private Color textColor, invisibleColor;
    private Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        textColor = myText.color;
        textColor.a = 1f;
        invisibleColor = textColor;
        invisibleColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool timerOn = timer > 0f;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (timerOn) Application.Quit();
            else timer = quitTimeout;
        }
        if (timerOn) timer -= Time.deltaTime;
        myText.color = (timer > 0f) ? textColor : invisibleColor;
    }
}
