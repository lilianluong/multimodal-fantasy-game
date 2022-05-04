using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorAnimator : MonoBehaviour
{
    public float scaleMultiplier = 1f;
    public float xOffset = 0f, yOffset = 3f, pauseTime = 0.5f;
    private string currentSpell;

    private List<GestureFrame> exampleFrames;
    private int frameIndex;
    private double animationTime, exampleTime;

    // Start is called before the first frame update
    void Start()
    {
        currentSpell = SpellExamples.GetExampleNames()[0];
        ResetAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        animationTime += Time.deltaTime * 1e6;
        if (animationTime >= exampleTime + pauseTime * 1e6)
        {
            animationTime = 0;
            frameIndex = 0;
            MoveTo(exampleFrames[frameIndex]);
            GetComponent<TrailRenderer>().Clear();
            return;
        }
        if (animationTime >= exampleTime) return;
        while (animationTime > exampleFrames[frameIndex].timestamp)
        {
            frameIndex++;
        }
        MoveTo(exampleFrames[frameIndex]);
    }

    private void ResetAnimation()
    {
        exampleFrames = SpellExamples.GetExample(currentSpell);
        frameIndex = 0;
        animationTime = 0;
        exampleTime = exampleFrames[exampleFrames.Count - 1].timestamp;
        MoveTo(exampleFrames[0]);
        GetComponent<TrailRenderer>().Clear();
    }

    private void MoveTo(GestureFrame frame)
    {
        transform.position = new Vector3(frame.x * scaleMultiplier + xOffset, frame.y * scaleMultiplier + yOffset, 0f);
    }

    public void SwitchToSpell(string spellName)
    {
        if (SpellExamples.GetExampleNames().Contains(spellName))
        {
            currentSpell = spellName;
            ResetAnimation();
            TutorController.Instance.uiController.HighlightSpell(spellName);
        }
    }
}
