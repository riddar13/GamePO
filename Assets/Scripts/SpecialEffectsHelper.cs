using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    //синглтон
    public static SpecialEffectsHelper Instance;

    public ParticleSystem smokeEffect;
    public ParticleSystem fireEffect;

    void Awake()
    {
        //регистрация синглтона
        if (Instance != null)
        {
            Debug.LogError("Несколько экземпляров SpecialEffectsHelper!");
        }

        Instance = this;
    }

    //создать взрыв в данной точке
    public void Explosion(Vector3 position)
    {
        //дым над водой
        instantiate(smokeEffect, position);

        //огонь в небе
        instantiate(fireEffect, position);
    }

    //создание экземпляра системы частиц из префаба
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
            prefab,
            position,
            Quaternion.identity
            ) as ParticleSystem;

        //убедитесь, что это будет уничтожено
        Destroy(
            newParticleSystem.gameObject,
            newParticleSystem.startLifetime
            );

        return newParticleSystem;
    }
}
