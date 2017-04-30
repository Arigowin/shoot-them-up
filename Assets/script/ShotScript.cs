using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public int damage = 1;
    public bool isEnemyShot = false;
    public Vector2 speed = new Vector2(10, 10);
    protected bool _paused = false;

    void OnPauseGame()
    {
        _paused = true;
    }

    void OnResumeGame()
    {
        _paused = false;
    }

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void FixedUpdate()
    {
        if (!_paused)
            GetComponent<Rigidbody2D>().velocity = speed;
    }
}