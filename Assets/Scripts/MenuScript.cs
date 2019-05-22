using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    void OnGUI()
    {
        const int buttonWidth = 84;
        const int buttonHeight = 60;

        //определяем место кнопки на экране
        Rect buttonRect = new Rect(
            Screen.width / 2 - (buttonWidth / 2),
            (2 * Screen.height / 3) - (buttonHeight / 2),
            buttonWidth,
            buttonHeight
            );

        //нарисовать кнопку
        if(GUI.Button(buttonRect, "START"))
        {
            Application.LoadLevel("Stage1");
        }
    }
}
