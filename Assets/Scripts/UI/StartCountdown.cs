using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{

    private float start = 5f;
    private float currentTime = 0f;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = start;    
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReadyToStart())
        {
            currentTime -= 1 * Time.deltaTime;
            score.text = currentTime.ToString("0");
        }
    }

    public bool isReadyToStart()
    {
        if (currentTime == 0) return true;
        return false;
    }

}
