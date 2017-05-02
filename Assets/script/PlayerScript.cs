using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public delegate void PlayerEvent();

    public static event PlayerEvent OnPlayerDeath;

    public Vector2 speed = new Vector2(10, 10);
    private Vector2 _default_speed;
    private Vector2 _movement;
    private int[] _lvl = { 0, 2, 4, 6 };
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
        _default_speed = speed;

        WeaponScript[] weapons = GetComponentsInChildren<WeaponScript>();
        foreach (WeaponScript weapon in weapons)
            weapon.enabled = false;
    }

    void Update()
    {
        if (!_paused)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                Left();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                Right();
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Up();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                Down();

            // key Up
            if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                    Reset();
            }

            // Special Key
            if (Input.GetKeyDown(KeyCode.LeftShift))
                Shift();
            if (Input.GetKeyUp(KeyCode.LeftShift))
                ShiftUp();
            

            var dist = (transform.position - Camera.main.transform.position).z;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), Mathf.Clamp(transform.position.y, topBorder, bottomBorder), transform.position.z);

            ActiveWeapon();
        }
    }

    void ActiveWeapon()
    {
        LevelScript lvlScript = GetComponentInParent<LevelScript>();
        if (lvlScript.level <= 3)
        {
            WeaponScript[] weapons = GetComponentsInChildren<WeaponScript>();

            int nb = _lvl[lvlScript.level];

            for (int i = 0; i <= nb && i < 6; i++)
                weapons[i].enabled = true;
        }
    }

    void Left()
    {
        Vector2 direction = new Vector2(-1, 0);
        Move(direction);
    }

    void Right()
    {
        Vector2 direction = new Vector2(1, 0);
        Move(direction);
    }

    void Up()
    {
        Vector2 direction = new Vector2(0, 1);
        Move(direction);
    }

    void Down()
    {
        Vector2 direction = new Vector2(0, -1);
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 vel = GetComponent<Rigidbody2D>().velocity;
        _movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        GetComponent<Rigidbody2D>().velocity = _movement;
    }

    void Reset()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void Shift()
    {
        speed = new Vector2(speed.x / 2, speed.y / 2);
    }

    void ShiftUp()
    {
        speed = _default_speed;
        Reset();
    }

    void OnDestroy()
    {
        if (OnPlayerDeath != null)
            OnPlayerDeath();
    }
}
