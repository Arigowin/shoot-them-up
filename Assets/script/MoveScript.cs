using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);
    private Vector2 _movement;
    protected bool _paused = false;

    void OnPauseGame()
    {
        _paused = true;
    }

    void OnResumeGame()
    {
        _paused = false;
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
