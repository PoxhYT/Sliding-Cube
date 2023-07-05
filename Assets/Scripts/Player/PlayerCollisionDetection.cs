using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public GameObject player;
    public GameObject levelProgressUI;
    private ParticleSystem explosionParticles;
    public ParticleSystem explosionPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            explosionParticles = Instantiate(explosionPrefab, player.transform.position, Quaternion.identity);
            explosionParticles.Play();
            FindObjectOfType<GameManager>().EndGame(player, levelProgressUI);
        } 
    }
}
