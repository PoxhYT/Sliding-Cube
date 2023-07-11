using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private AudioManager _audioManager;
    private ScreenTransition _screenTransition;
    private bool _isAbleToPressSpace;

    private void Start()
    {
        _videoPlayer = GetComponentInChildren<RawImage>().GetComponentInChildren<VideoPlayer>();
        _audioManager = FindObjectOfType<AudioManager>();
        _screenTransition = FindObjectOfType<ScreenTransition>();
    }

    private void Update()
    {
        //_screenTransition.FadeOut();
        if (_videoPlayer)
        {
            switch (_videoPlayer.time)
            {
                case 0:
                    _audioManager.FadeToNormal(2.5f, 22000.0f, 2);
                    break;
                case 3:
                    _isAbleToPressSpace = true;
                    break;    
                case 17:
                    _isAbleToPressSpace = false;
                    _audioManager.FadeToMuffled(0, 300.0f, 2);
                    break;
            }  
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _isAbleToPressSpace)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        _screenTransition.StartTransition(true);
        _audioManager.FadeToMuffled(0, 300.0f, 2);
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameManager>().StartNextLevel();
        _audioManager.FadeToNormal(0, 22000.0f, 2);
    }
}
