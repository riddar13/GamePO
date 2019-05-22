﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp = 1;
    public bool isEnemy = true;

    public HealthScript(int hp1, bool isEnemy1)
    {
        hp = hp1;
        isEnemy = isEnemy1;
    }


    public void Damage(int damageCount)
    {
        hp -= damageCount;
        if (hp <= 0)
        {
            //взрыв
            SpecialEffectsHelper.Instance.Explosion(transform.position);

            //звук взрыва
            SoundEffectsHelper.Instance.MakeExplosionSound();

            //смерть
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}
