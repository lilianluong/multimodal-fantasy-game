using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour
{
    public float titleTime = 3f;
    private float timer;

    void Start()
    {
        timer = titleTime;
    }

    void Update()
    {
        if (timer < 0f) SceneManager.LoadScene("Tutorial");
        timer -= Time.deltaTime;
    }
}
