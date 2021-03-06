﻿using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform shotPrefab;
    public float shootingRate = 0.25f;
    public bool isEnemy = false;
    public Vector2 speed = new Vector2(0, 10);
    private float _shootCooldown;
    protected bool _paused = false;

    public bool CanAttack
    {
        get
        {
            return _shootCooldown <= 0f;
        }
    }

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
        _shootCooldown = 0f;
    }

    void Update()
    {
        if (!_paused)
        {
            if (isEnemy == false)
            {
                if (Input.GetKey(KeyCode.Space))
                    Attack();
            }

            if (_shootCooldown > 0)
                _shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (CanAttack)
        {
            Transform shotTransform = Instantiate(shotPrefab) as Transform;
            Vector2 position = transform.position;

            _shootCooldown = shootingRate;
            if (isEnemy)
            {
                SoundEffectsHelper.Instance.MakeEnemyShotSound();
                position.y = position.y - 0.5f;
            }
            else
            {
                SoundEffectsHelper.Instance.MakePlayerShotSound();
                position.y = position.y + 0.5f;
            }
            shotTransform.position = position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            shot.speed = speed;
            if (shot != null)
                shot.isEnemyShot = isEnemy;
        }
    }
}