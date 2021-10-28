using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LineRenderer : Graphic
{

    public Vector2Int gridSize;
    public float thickness;

    public List<Vector2> points;

    float width;
    float height;
    public float unitWidth;
    public float unitHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / gridSize.x;
        unitHeight = height / gridSize.y;


        //En caso de que sólo haya un punto en la lista se deberá de dibujar también pero de distinta forma
        if (points.Count < 2)
        {
            Vector2 point = points[0];

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = new Vector3(-thickness * 2, 0);
            vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
            vh.AddVert(vertex);

            vertex.position = new Vector3(thickness * 2, 0);
            vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
            vh.AddVert(vertex);

            vertex.position = new Vector3(0, -thickness * 2);
            vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
            vh.AddVert(vertex);

            vertex.position = new Vector3(0, thickness * 2);
            vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
            vh.AddVert(vertex);

            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(1, 2, 3);

            return;
        }

        float angle = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {

            Vector2 point = points[i];
            Vector2 point2 = points[i + 1];

            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i + 1]) + 90f;
            }

            DrawVerticesForPoint(point, point2, angle, vh);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 4;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 1, index + 2, index + 3);
        }
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        //panel resolution go there in place of 9 and 16

        return (float)(Mathf.Atan2(9f * (target.y - me.y), 16f * (target.x - me.x)) * (180 / Mathf.PI));
    }

    void DrawVerticesForPoint(Vector2 point, Vector2 point2, float angle, VertexHelper vh)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point2.x, unitHeight * point2.y);
        vh.AddVert(vertex);
    }
}