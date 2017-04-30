using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp = 1;

    void OnTriggerEnter2D(Collider2D collider)
    {
        bool ShotEnemy = false;
        ShotScript shot = collider.gameObject.GetComponent<ShotScript>();
        if (shot)
        {
            if ((tag == "Enemy" && shot.isEnemyShot == true) || (shot.isEnemyShot == false && tag == "Player"))
                ShotEnemy = true;
            if (ShotEnemy == false)
            {
                hp -= shot.damage;
                Destroy(shot.gameObject);
                if (hp <= 0)
                {
                    if (tag == "Enemy")
                    {
                        ScoreScript score = GUIText.FindObjectOfType < ScoreScript>();
                        if (score)
                            score.ScoreAdd();
                    }
                    SpecialEffectsHelper.Instance.Explosion(transform.position);
                    SoundEffectsHelper.Instance.MakeExplosionSound();
                    Destroy(gameObject);
                }
            }
        }
        else if (tag != "Enemy")
        {
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();
            Destroy(gameObject);
        }
    }
}