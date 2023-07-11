using System;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    public Animator screenTransitionAnimator;
    private GameManager _gameManager;
    private bool _shouldLoadNextLevel;
    private AudioManager _audioManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartTransition(bool shouldLoadNextLevel)
    {
        _shouldLoadNextLevel = shouldLoadNextLevel;
        screenTransitionAnimator.SetTrigger("FadeOut");     
    }

    public void OnTransitionComplete()
    {
        _audioManager.FadeToNormal(0, 22000.0f, 2);
        
        if (_shouldLoadNextLevel)
        {
            _gameManager.StartNextLevel();
        }
        else
        {
            _gameManager.StartPreviousLevel();
        }  
    }
}
