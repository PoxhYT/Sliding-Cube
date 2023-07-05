using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Transform player;
    public Transform platformEnd; // Transform representing the end of the platform
    public TMP_Text scoreText;
    
    private Vector3 platformStart;
    private float totalLength;

    private void Start()
    {
        // Set platformStart to the player's initial position
        platformStart = player.position;

        totalLength = Vector3.Distance(platformStart, platformEnd.position);
    }

    private void Update()
    {
        if (player)
        {
            float distanceTravelled = Vector3.Distance(player.position, platformStart);
            float rawProgress = (distanceTravelled / totalLength) * 100;
            float progress = Mathf.Min(rawProgress, 100);
            scoreText.text = $"{progress:0.00}%";
        }
    }
}