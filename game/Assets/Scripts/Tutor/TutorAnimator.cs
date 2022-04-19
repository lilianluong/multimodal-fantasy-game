using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorAnimator : MonoBehaviour
{
    public float scaleMultiplier = 1f;
    public string exampleName;

    private List<GestureFrame> exampleFrames;
    private int frameIndex;
    private double animationTime, exampleTime;

    // Start is called before the first frame update
    void Start()
    {
        exampleFrames = SpellExamples.GetExample(exampleName);
        frameIndex = 0;
        animationTime = 0;
        exampleTime = exampleFrames[exampleFrames.Count - 1].timestamp;
        MoveTo(exampleFrames[0]);
        GetComponent<TrailRenderer>().Clear();
    }

    // Update is called once per frame
    void Update()
    {
        animationTime += Time.deltaTime * 1e6;
        if (animationTime >= exampleTime)
        {
            animationTime = 0;
            frameIndex = 0;
            MoveTo(exampleFrames[frameIndex]);
            GetComponent<TrailRenderer>().Clear();
            return;
        }
        while (animationTime > exampleFrames[frameIndex].timestamp)
        {
            frameIndex++;
        }
        MoveTo(exampleFrames[frameIndex]);
    }

    private void MoveTo(GestureFrame frame)
    {
        transform.position = new Vector3((frame.x - 0.5f) * scaleMultiplier, (frame.y - 0.5f) * scaleMultiplier, 0f);
    }
}
