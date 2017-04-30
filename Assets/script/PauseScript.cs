using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private bool _pause = false;
    private bool _gameOver = false;
    private float _timescale = 1;

    void OnEnable()
    {
        EventManager.StartListening("Pause", Pause);
        EventManager.StartListening("GameOver", GameOver);
    }

    void OnDisable()
    {
        EventManager.StopListening("Pause", Pause);
        EventManager.StopListening("GameOver", GameOver);
    }

    void GameOver()
    {
        _gameOver = true;
        Pause();
    }

    void Pause()
    {
        if (!_pause)
        {
            _timescale = Time.timeScale;
            Time.timeScale = 0;
            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            _pause = true;
        }
    }

    void Resume()
    {
        Time.timeScale = _timescale;
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
            go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        _pause = false;
    }

    void OnGUI()
    {
        if (_pause)
        {
            const int buttonWidth = 120;
            const int buttonHeight = 60;

            if (_gameOver == false && GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (1 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "Continue"))
                Resume();
            if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "Retry"))
            {
                Resume();
                Destroy(EventManager.Instance);
                SceneManager.LoadScene("shoot_them_up", LoadSceneMode.Single);
            }
            if (GUI.Button(new Rect(Screen.width / 2 - (buttonWidth / 2), (3 * Screen.height / 4) - (buttonHeight / 2), buttonWidth, buttonHeight), "Back to menu"))
            {
                Resume();
                Destroy(EventManager.Instance);
                SceneManager.LoadScene("shoot_them_up_menu", LoadSceneMode.Single);
            }
        }
    }
}
