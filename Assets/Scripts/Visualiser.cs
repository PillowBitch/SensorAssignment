using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visualiser : MonoBehaviour
{
    public Image background;

    public Color32 color1 = Color.white;

    public Color32 color2 = Color.black;

    public Color32 color3 = Color.green;

    bool isActive = false;

    float t;

    void Update()
    {

        t = DataWrite.instance.GetProgress();
        Color32 c = Color32.Lerp(color1, color2, t);

        background.color = c;

        if (t == 0)
        {
            background.color = color3;
        }
    }
}
