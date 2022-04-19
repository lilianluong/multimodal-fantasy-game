using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // References
    public RectTransform foregroundBarTransform;
    public Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        SetHealth(27f, 100f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(float health, float totalHealth)
    {
        foregroundBarTransform.localScale = new Vector3(Mathf.Min(1f, health / totalHealth), 1f, 1f);
        healthText.text = $"{(int)health}/{(int)totalHealth}";
    }
}
