using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//CE script définit le déplacement du point rouge à l'intérieur des cercles pour la "sound task".
public class Counter : MonoBehaviour
{
    float radius;
    Vector3[] centers;
    LineRenderer lineRenderer;
    int circleNumber;
    private float minX, maxX, minY, maxY;
    int counter;
    bool ManipulandumPressed = false;
    bool circleDraw;
    float angle_treshold;
    public bool restart;
    // Start is called before the first frame update
    void Start()
    {
        restart = false;
        angle_treshold = 0.01f;
        circleDraw = true;
        counter = -1;
        circleNumber = GameObject.Find("GameManager").GetComponent<Parameters>().circle_Number;
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

        radius = ((maxX - minX) / (circleNumber * 3f));
        centers = new Vector3[circleNumber];
        for (int i = 0; i < circleNumber; i++)
        {
            float gap = (2 * maxX) / (circleNumber + 1);
            centers[i] = new Vector3(minX + gap * (i + 1), CircleDraw.radius, 0);
        }
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.material.color = new Color32(210, 145, 255, 255);
        lineRenderer.positionCount = Convert.ToInt32(2 * Math.PI / angle_treshold); // 2pi / 0.1
        lineRenderer.startWidth = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            Restart();
        }
        if (Input.GetMouseButtonDown(0)) // ICI
        {
            Restart();
        }
        if (Input.GetKeyDown("p"))
        {
            counter += 1;
            circleDraw = false;
        }
        if (((float)Manipulandum_data_aquired.Force_Data[1] > GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensi && !ManipulandumPressed))
        {
            counter += 1;
            ManipulandumPressed = true;
            circleDraw = false;
        }
        if ((float)Manipulandum_data_aquired.Force_Data[1] < GameObject.Find("GameManager").GetComponent<Parameters>().ManipulandumSensiRelease)
        {
            ManipulandumPressed = false;
        }
        if (counter >= circleNumber)
        {
            counter = 0;
        }
        for (int i = 0; i < circleNumber; i++)
        {
            if (counter == i && !circleDraw)
            {
                DrawFilledCircle(radius, centers[i].x, centers[i].y);
                circleDraw = true;
            }
        }
    }
    void DrawFilledCircle(float r, float pos_X, float pos_Y)
    {
        Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < lineRenderer.positionCount; i ++)
        {
            theta += (2.0f * Mathf.PI * angle_treshold);
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);
            x += pos_X;
            y += pos_Y;
            //Debug.Log("posX: " + pos_X + "  posY: " + pos_Y + "   x: " + x + "  y: " + y);
            pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, new Vector2(pos_X, pos_Y));
            lineRenderer.SetPosition(i, pos);
        }
    }
    void Restart()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = Convert.ToInt32(2 * Math.PI / angle_treshold);

        restart = false;
        angle_treshold = 0.01f;
        circleDraw = true;
        counter = -1;
        circleNumber = GameObject.Find("GameManager").GetComponent<Parameters>().circle_Number;
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

        radius = ((maxX - minX) / (circleNumber * 3f));
        centers = new Vector3[circleNumber];
        for (int i = 0; i < circleNumber; i++)
        {
            float gap = (2 * maxX) / (circleNumber + 1);
            centers[i] = new Vector3(minX + gap * (i + 1), CircleDraw.radius, 0);
        }
        lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        lineRenderer.material.color = new Color32(210, 145, 255, 255);
        lineRenderer.positionCount = Convert.ToInt32(2 * Math.PI / angle_treshold); // 2pi / 0.1
    }
}
