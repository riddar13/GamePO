using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RendererExtensions : MonoBehaviour
{
    //скорость прокрутки
    public Vector2 speed = new Vector2(10, 10);

    //направление движения
    public Vector2 direction = new Vector2(-1, 0);

    //движения должны быть применены к камере
    public bool isLinkedToCamera = false;

    //бесконечный фон
    public bool isLooping = false;

    //список детей с рендерером
    private List<Transform> backgroundPart;

    //получаем детей
    void Start()
    {
        //только для бесконечного фона
        if (isLooping)
        {
            //задействовать всех детей слоя с рендерером
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                //добавить только видимых детей
                if (child.GetComponent<Renderer>() != null)
                {
                    backgroundPart.Add(child);
                }

            }
            //сортировка по позиции
            backgroundPart = backgroundPart.OrderBy(
                t => t.position.x
            ).ToList();
        }
    }


    void Update()
    {
        //перемещение 
        Vector3 movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        //перемещение камеры
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        //loop
        if (isLooping)
        {
            //получение первого объекта
            //список упорядочен слева направо
            Transform firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null)
            {
                if (firstChild.position.x < Camera.main.transform.position.x)
                {
                    if (firstChild.GetComponent<Renderer>().isVisible == false)
                    {
                        Transform lastChild = backgroundPart.LastOrDefault();
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                        firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}

