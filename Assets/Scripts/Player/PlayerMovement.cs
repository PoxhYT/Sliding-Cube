using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    
    public Text score;
    public Text progress;

    public GameManager gameManager;

    public Transform endPosition;

    public float forceSpeed = 500;
    private float fullDistance;

    void Start()
    {
        gameManager.SetCountdown();
        fullDistance = gameManager.GetDistance(rb, endPosition);
    }

    private void Update()
    {
        if(!gameManager.GameHasEnded)
        {
            gameManager.ChangeGravityState(rb, score);
        }

        gameManager.SetupObstacle();
        gameManager.UnloadChunck(rb);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startGame();
        progress.text = gameManager.GetCurrentProgress(rb, endPosition, fullDistance).ToString() + "%";

        if (rb.position.y < 0)
        {
            gameManager.EndGame();
        }
    }

    public void startGame()
    {
        if (!gameManager.GameHasEnded)
        {
            if (rb.useGravity)
            {
                rb.useGravity = true;
                AddForceToRigidbody(0, 0, forceSpeed * Time.deltaTime);
                SetupControlls();
            }
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
}
