using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameManager gameManager = FindAnyObjectByType<GameManager>();
        }
    }
}
