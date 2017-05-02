using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    private Vector2 _movement;
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

    void Update()
    {
        if (!_paused)
            _movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
    }

    void FixedUpdate()
    {
        if (!_paused)
            GetComponent<Rigidbody2D>().velocity = _movement;
    }
}
