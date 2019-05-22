using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private bool hasSpawn;
    private MoveScript moveScript;
    private WeaponScript[] weapons;

    void Awake()
    {
        //получить оружие только один раз
        weapons = GetComponentsInChildren<WeaponScript>();

        //отключить скрипты, чтобы деактивировать объект при отсутствии спавна
        moveScript = GetComponent<MoveScript>();
    }

    //отключить все
    void Start()
    {
        hasSpawn = false;

        //отключить
        //--коллайдеры
        GetComponent<Collider2D>().enabled = false;
        //--перемещение
        moveScript.enabled = false;
        //--стрельбу
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = false;
        }

    }
    void Update()
    {
        //проверить, начался ли спавн врагов
        if (hasSpawn == false)
        {
            if (GetComponent<Renderer>().isVisible)
            {
                Spawn();
            }
        }
        else
        {
            //автоматическая стрельба
            foreach (WeaponScript weapon in weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);

                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }

            //выход за рамки камеры? уничтожить игровой объект
            if (GetComponent<Renderer>().isVisible == false)
            {
                Destroy(gameObject);
            }
        }
    }

    //самоактивация
    private void Spawn()
    {
        hasSpawn = true;

        //включить
        //--коллайдеры
        GetComponent<Collider2D>().enabled = true;
        //--перемещение
        moveScript.enabled = true;
        //--стрельбу
        foreach (WeaponScript weapon in weapons)
        {
            weapon.enabled = true;
        }

    }

}
