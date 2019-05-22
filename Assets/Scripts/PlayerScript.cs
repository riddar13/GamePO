using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // 1 - скорость движения
    public Vector2 speed = new Vector2(50, 50);

    // 2 - направление движения
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // 5 - перемещение игрового объекта
        GetComponent<Rigidbody2D>().velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        //столкновение с врагом
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            // смерть врага 
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            damagePlayer = true;
        }

        // повреждения у игрока
        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }

    void Update()
    {
        // 3 - извлечь информацию оси
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // 4 - движение в каждом направлении
        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        // 5 - стрельба
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                weapon.Attack(false);

                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }

        //убедиться, что игрок не выходит за рамки кадра
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, dist)
            ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
           new Vector3(1, 0, dist)
           ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
           new Vector3(0, 0, dist)
           ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
           new Vector3(0, 1, dist)
           ).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z
            );
    }

    void OnDestroy()
    {
        transform.parent.gameObject.AddComponent<GameOverScript>();
    }

}
