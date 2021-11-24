using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphAnimator : MonoBehaviour
{
    public UI_LineRenderer[] lines;

    public float time = 0.005f;

    private void OnEnable()
    {
        AnimateLines();
    }

    private void AnimateLines()
    {
        foreach(UI_LineRenderer line in lines)
        {
            AnimateLine(line);
        }
    }

    private void AnimateLine(UI_LineRenderer line)
    {
        List<Vector2> points = line.points;

        Animate(line, points);
    }

    private void Animate(UI_LineRenderer line, List<Vector2> points)
    {
        line.points = new List<Vector2>();

        for(int i = 0; i < points.Count; i++)
        {
            int index = i;
            AnimatePoint(line, index, new Vector2(0, 4), points[index]);
        }
    }

    private void AnimatePoint(UI_LineRenderer line, int index, Vector2 start, Vector2 end)
    {
        LeanTween.delayedCall(time * index, () =>
          {
              if(index > 0)
              {
                  start = line.points[index - 1];
                  line.points.Add(start);
              }else
              {
                  line.points.Add(start);
              }

              LeanTween.value(gameObject, (value) =>
               {
                   line.points[index] = value;
                   line.SetVerticesDirty();
               }, start, end, time);
          });
    }
}
