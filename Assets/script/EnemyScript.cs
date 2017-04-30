using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private WeaponScript[] _weapons;
    private bool _hasSpawn;
    private MoveScript _moveScript;
    protected bool _paused = false;

    void OnPauseGame()
    {
        _paused = true;
    }

    void OnResumeGame()
    {
        _paused = false;
    }

    void Awake()
    {
        _weapons = GetComponentsInChildren<WeaponScript>();
        _moveScript = GetComponent<MoveScript>();
    }

    void Start()
    {
        _hasSpawn = false;
        GetComponent<Collider2D>().enabled = false;
        _moveScript.enabled = false;
        foreach (WeaponScript weapon in _weapons)
            weapon.enabled = false;
    }

    void Update()
    {
        if (!_paused)
        {
            if (_hasSpawn == false)
            {
                if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
                    Spawn();
            }
            else
            {
                foreach (WeaponScript weapon in _weapons)
                {
                    if (weapon != null && weapon.enabled && weapon.CanAttack)
                    {
                        weapon.isEnemy = true;
                        weapon.Attack();
                    }
                }
                if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
                    Destroy(gameObject);
            }
        }
    }

    private void Spawn()
    {
        _hasSpawn = true;
        GetComponent<Collider2D>().enabled = true;
        _moveScript.enabled = true;
        foreach (WeaponScript weapon in _weapons)
            weapon.enabled = true;
    }
}
