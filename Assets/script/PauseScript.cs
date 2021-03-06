﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public delegate void PauseEvent(bool pause);

    public static event PauseEvent OnPauseGame;

    private bool _gameOver = false;
    private bool _pause = false;
    private float _timescale = 1;

    void OnEnable()
    {
        PlayerScript.OnPlayerDeath += GameOver;
    }

    void OnDisable()
    {
        PlayerScript.OnPlayerDeath -= GameOver;
    }

    void Update()
    {
        if (_gameOver || (_pause == false && Input.GetKey(KeyCode.Escape)))
            Pause();
    }

    void GameOver()
    {
        _gameOver = true;
    }

    void Pause()
    {
        if (!_pause)
        {
            _timescale = Time.timeScale;
            Time.timeScale = 0;
            if (OnPauseGame != null)
                OnPauseGame(true);
            _pause = true;
        }
    }

    void Resume()
    {
        Time.timeScale = _timescale;

        if (OnPauseGame != null)
            OnPauseGame(false);
        _pause = false;
        _gameOver = false;
    }

    void OnGUI()
    {
        if (_pause || _gameOver)
        {
            const int buttonWidth = 120;
            const int buttonHeight = 60;

            if (_gameOver == false && GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "Continue"))
                Resume();
            if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retry"))
            {
                Resume();
                SceneManager.LoadScene("shoot_them_up", LoadSceneMode.Single);
            }
            if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "Back to menu"))
            {
                Resume();
                SceneManager.LoadScene("shoot_them_up_menu", LoadSceneMode.Single);
            }
        }
    }
}
