using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorFlareScript : MonoBehaviour
{
    public float halfTime = 0.75f;

    private float timeElapsed;
    private Image myImage;
    private float r = 1f, g = 0f, b = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
        myImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        float brightnessRatio = Mathf.Abs(timeElapsed - halfTime) / halfTime;
        myImage.color = new Color(r, g, b, 1f - brightnessRatio);
        if (timeElapsed > halfTime * 2)
        {
            if (AdventureController.Instance != null) AdventureController.Instance.animationState++;
            if (TutorController.Instance != null) TutorController.Instance.animationState++;
            Destroy(gameObject);
        }
    }

    public void SetColor(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }
}
