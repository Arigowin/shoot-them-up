using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public int spawnCount;
    public GameObject minion;
    private float _rate = 1f;
    private float _cooldown;
    protected bool _paused = false;

    public bool CanSpawn
    {
        get
        {
            return _cooldown <= 0f;
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
        _cooldown = 0f;
    }

    void FixedUpdate()
    {
        if (!_paused)
        {
            if (_cooldown > 0)
                _cooldown -= Time.deltaTime;
            else
                Spawn();
        }
    }

    void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();

            if (spawnPosition != Vector3.zero)
                Instantiate(minion, spawnPosition, new Quaternion());
        }
        _cooldown = _rate;
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3();
        float startTime = Time.realtimeSinceStartup;
        bool ok = false;

        while (ok == false)
        {
            Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector2 spawnPositionRaw = Random.insideUnitCircle * stageDimensions.x;

            spawnPosition = new Vector3(spawnPositionRaw.x, (stageDimensions.y) + 5, spawnPositionRaw.y);
            ok = !Physics.CheckSphere(spawnPosition, 0.75f);

            if (Time.realtimeSinceStartup - startTime > 0.5f)
            {
                Debug.Log("Time out placing Minion!");
                return Vector3.zero;
            }
        }
        return spawnPosition;
    }
}