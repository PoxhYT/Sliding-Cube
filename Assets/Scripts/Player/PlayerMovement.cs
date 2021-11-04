using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forceSpeed = 500;
    public Text score;

    public Transform endPosition;

    private StartCountdown startCountdown = new StartCountdown();
    private PlayerColission playerColission = new PlayerColission();

    private float fullDistance;

    void Start()
    {
        startCountdown.currentTime = startCountdown.start;
        fullDistance = GetDistance();
    }

    private void Update()
    {
        if(!playerColission.gameEnd)
        {
            startCountdown.ChangeGravityState(rb, score);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startGame();
        GetCurrentProgress();
    }

    public void startGame()
    {
        if (!playerColission.gameEnd)
        {
            if (rb.useGravity)
            {
                rb.useGravity = true;
                AddForceToRigidbody(0, 0, forceSpeed * Time.deltaTime);
                SetupControlls();
            }
        } else
        {
            Debug.Log("HELLO!!!!!!");
        }
    }

    private void AddForceToRigidbody(float valueA, float valueB, float valueC)
    {
        Vector3 force = new Vector3(valueA, valueB, valueC);
        rb.AddForce(force);
    }

    private void SetupControlls()
    {
        if (Input.GetKey("d"))
        {
            rb.AddForce(50 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        else if (Input.GetKey("a"))
        {
            rb.AddForce(-50 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
    }

    private float GetDistance()
    {
        return Vector3.Distance(transform.position, endPosition.position);
    }

    private float GetCurrentProgress()
    {
        if (rb.transform.position.z <= endPosition.position.z)
        {
            float newDistance = GetDistance();
            float progress = Mathf.InverseLerp(fullDistance, 0f, newDistance);
            return progress;
        }
        return 100.0f;
    }
}
