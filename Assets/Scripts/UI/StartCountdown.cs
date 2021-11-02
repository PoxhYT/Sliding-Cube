using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{

    public float start = 3f;
    public float currentTime = 0f;

    public void ChangeGravityState(Rigidbody rigidbody, Text score)
    {
        if (!isReadyToStart())
        {
            rigidbody.useGravity = false;
            score.enabled = true;
            currentTime -= 1 * Time.deltaTime;
            score.text = currentTime.ToString("0");
        }
        else
        {
            score.enabled = false;
            rigidbody.useGravity = true;
        }
    }

    public bool isReadyToStart()
    {
        return currentTime < 0;
    }

}
