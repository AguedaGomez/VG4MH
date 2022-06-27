using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_tilingMovement : MonoBehaviour
{
    public Material rend;

    void Update()
    {
        // Animates main texture scale in a funky way!
        float scaleX = Mathf.Cos(Time.time) * 0.5f + 1;
        float scaleY = Mathf.Sin(Time.time) * 0.5f + 1;
        rend.mainTextureScale = new Vector2(scaleX, scaleY);
    }
}
