using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowHandler : MonoBehaviour
{
    int lastWidth, lastHeight;

    [SerializeField]
    Text text;

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            text.text = Display.main.systemHeight + "";
        }

        if (Screen.width != lastWidth)
        {
            if (Screen.width > Display.main.systemHeight)
            {
                Screen.SetResolution(Display.main.systemHeight, Display.main.systemHeight, false);
            }
            else
            {
                Screen.SetResolution(Screen.width, Screen.width, false);
            }
        }
        else if (Screen.height != lastHeight)
        {
            Screen.SetResolution(Screen.height, Screen.height, false);
        }

        lastHeight = Screen.height;
        lastWidth = Screen.width;

    }
}
