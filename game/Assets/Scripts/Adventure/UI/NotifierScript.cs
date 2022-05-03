using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifierScript : MonoBehaviour
{
    public float floatSpeed = 50f, floatTime = 1.2f;

    private float timeElapsed;
    private RectTransform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
        myTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.anchoredPosition = new Vector2(myTransform.anchoredPosition.x, myTransform.anchoredPosition.y + floatSpeed * Time.deltaTime);
        timeElapsed += Time.deltaTime;
        if (timeElapsed > floatTime)
        {
            AdventureController.Instance.animationState++;
            Destroy(gameObject);
        }
    }
}
