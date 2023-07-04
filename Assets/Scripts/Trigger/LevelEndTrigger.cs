using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    public GameObject LevelCompleteUI;

    private void OnTriggerEnter(Collider collider)
    {
        LevelCompleteUI.SetActive(true);
    }
}
