using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{

    private bool timerIsRunning = false;
    private float elapsedTime;

    [HideInInspector]
    public string time;
    [HideInInspector]
    public static TimerController instance;
    private TimeSpan timePlaying;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        time = "00:00:00";
        timerIsRunning = false;
    }

    // Update is called once per frame
    public void RunTimer()
    {
        timerIsRunning = true;
        elapsedTime = 0f;
        UpdateTimer();
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    private void UpdateTimer()
    {
        while (timerIsRunning)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string coveredTime = timePlaying.ToString("mm':'ss':'ff");
            time = coveredTime;
        }
    }
}
