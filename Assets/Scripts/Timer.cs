using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{

    public Slider timerSlider;

    public GameObject fill;
    private float gameTime;
    public bool timeractiv;
    private bool stopTimer;
    public bool timerfz;
    private float timed;
    // Start is called before the first frame update
    void Start()
    {
        // timeractiv = false;
        // stopTimer = false;
        timerfz = false;
    }

    public void reset()
    {
        timerfz = false;
        timeractiv = false;
        timerSlider.value = 0;
    }

    public void calltimer(float gt)
    {
        timed = 0;
        gameTime = gt;
        timerSlider.maxValue = gameTime;
        timeractiv = true;
        stopTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeractiv && timerfz == false)
        {
            timed += Time.deltaTime;

            if (timed >= gameTime)
            {
                stopTimer = true;
                timeractiv = false;
            }
            if (stopTimer == false)
            {
                timerSlider.value = timed;
            }
        }
    }

    public void timerFreeze()
    {
        timerfz = true;

    }

    public void timerWarm()
    {
        timerfz = false;
    }

    public void skip()
    {
        if (timeractiv && timerfz == false)
        {
            timerSlider.value = timerSlider.maxValue;
            timeractiv = false;
            stopTimer = true;
            timed = 0;
        }
    }

}
