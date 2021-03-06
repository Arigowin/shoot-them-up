﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(2, 2);
    public Vector2 direction = new Vector2(-1, 0);
    public bool isLinkedToCamera = false;
    public bool isLooping = false;
    private List<Transform> _backgroundPart;
    protected bool _paused = false;

    void OnEnable()
    {
        PauseScript.OnPauseGame += OnPauseGame;
    }

    void OnDisable()
    {
        PauseScript.OnPauseGame -= OnPauseGame;
    }

    void OnPauseGame(bool pause)
    {
        _paused = pause;
    }

    void Start()
    {
        if (isLooping)
        {
            _backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.GetComponent<Renderer>() != null)
                    _backgroundPart.Add(child);
            }

            _backgroundPart = _backgroundPart.OrderBy(t => t.position.y).ToList();
        }
    }

    void Update()
    {
        if (!_paused)
        {
            Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

            movement *= Time.deltaTime;
            transform.Translate(movement);

            if (isLinkedToCamera)
                Camera.main.transform.Translate(movement);

            if (isLooping)
            {
                Transform firstChild = _backgroundPart.FirstOrDefault();

                if (firstChild != null)
                {
                    if (firstChild.position.y < Camera.main.transform.position.y)
                    {
                        if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                        {
                            Transform lastChild = _backgroundPart.LastOrDefault();
                            Vector3 lastPosition = lastChild.transform.position;
                            Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                            firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);

                            _backgroundPart.Remove(firstChild);
                            _backgroundPart.Add(firstChild);
                        }
                    }
                }
            }
        }
    }
}