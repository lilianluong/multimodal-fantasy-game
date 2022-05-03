using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // References
    public RectTransform foregroundBarTransform;
    public Text nameText, healthText;

    private float totalHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetHealth(float health, float totalHealth)
    {
        this.totalHealth = totalHealth;
        SetHealth(health);
    }

    public void SetHealth(float health)
    {
        foregroundBarTransform.localScale = new Vector3(Mathf.Min(1f, health / totalHealth), 1f, 1f);
        healthText.text = $"{(int)health}/{(int)totalHealth}";
    }


}
