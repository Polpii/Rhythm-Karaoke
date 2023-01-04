using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleDraw : MonoBehaviour
{
    float theta_scale = 0.01f;        //Set lower to add more points
    int size; //Total number of points in circle
    public static float radius;
    LineRenderer lineRenderer;
    Vector3[] centers;
    int circleNumber;
    private float minX, maxX, minY, maxY;
    bool done;
    public bool restart;

    void Start()
    {
        done = false;
        restart = false;
        circleNumber = GameObject.Find("GameManager").GetComponent<Parameters>().circle_Number;        
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

        radius = ((maxX - minX) / (circleNumber * 3f));

        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        centers = new Vector3[circleNumber];
        for(int i = 0; i < circleNumber; i++)
        {
            float gap = (2 * maxX) / (circleNumber + 1);
            centers[i] = new Vector3(minX + gap * (i + 1), radius, 0);
        }
    }

    void Update()
    {
        Vector3 pos;
        if (!done)
        {
            for(int n = 0; n < circleNumber; n++)
            {
                var child = new GameObject();
                child.transform.parent = transform;
                lineRenderer = child.AddComponent<LineRenderer>();
                lineRenderer.material.color = Color.black;
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.05f;
                lineRenderer.positionCount = size;

                float theta = 0f;
                for (int i = 0; i < size; i++)
                {
                    theta += (2.0f * Mathf.PI * theta_scale);
                    float x = radius * Mathf.Cos(theta);
                    float y = radius * Mathf.Sin(theta);
                    x += centers[n].x;
                    y += centers[n].y;
                    pos = new Vector3(x, y, 0);
                    lineRenderer.SetPosition(i, pos);
                }
            }
            done = true;
        }
        if (restart)
        {
            Restart();
        }
    }
    void Restart()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        done = false;
        restart = false;
        circleNumber = GameObject.Find("GameManager").GetComponent<Parameters>().circle_Number;        
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x;
        maxX = topCorner.x;
        minY = bottomCorner.y;
        maxY = topCorner.y;

        radius = ((maxX - minX) / (circleNumber * 3f));
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        centers = new Vector3[circleNumber];
        for (int i = 0; i < circleNumber; i++)
        {
            float gap = (2 * maxX) / (circleNumber + 1);
            centers[i] = new Vector3(minX + gap * (i + 1), radius, 0);
        }
    }
}