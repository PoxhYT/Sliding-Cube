using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forceSpeed = 500;
    public Text score;

    private StartCountdown startCountdown = new StartCountdown();
    private PlayerColission playerColission = new PlayerColission();

    // Start is called before the first frame update
    void Start()
    {
        startCountdown.currentTime = startCountdown.start;
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
    }

    public void startGame()
    {
        Debug.Log("IsGameDone: " + !playerColission.gameEnd);

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

    private float GetPlayersCurrentSpeed()
    {
        Vector3 playersVelocity = rb.velocity;      
        return playersVelocity.magnitude;
    }
}
