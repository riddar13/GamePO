﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    //префаб снаряда для стрельбы
    public Transform shotPrefab;

    //время перезарядки в секундах
    public float shootingRate = 0.25f;

    //перезарядка
    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    //стрельба из другого скрипта
    public void Attack(bool isEnemy)
    {
        if(CanAttack)
        {
            shootCooldown = shootingRate;

            //создайте новый выстрел
            var shotTransform = Instantiate(shotPrefab) as Transform;

            //определите положение 
            shotTransform.position = transform.position;

            //свойство врага
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            //сделайте так, чтобы выстрел всегда был напрален на него
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right;
            }
        }
    }
    //готово ли оружие выпустить снаряд?
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
