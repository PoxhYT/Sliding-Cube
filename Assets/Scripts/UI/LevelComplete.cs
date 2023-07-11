using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<GameManager>().FinishLevel();
        }
    }
}
