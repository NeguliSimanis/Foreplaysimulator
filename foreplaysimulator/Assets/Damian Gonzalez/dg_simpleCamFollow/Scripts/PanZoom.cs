using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    public float maxX = 5;
    public float minX = -5;
    public float maxY = 5;
    public float minY = -2;

    float speed = 0.4f;

    Vector3 mousePos;


    private void Start()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mousePos != Camera.main.ScreenToWorldPoint(Input.mousePosition))
        {
            Vector2 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float diffX = newPos.x - mousePos.x;
            float diffY = newPos.y - mousePos.y;


            if (mousePos.x < maxX &&
                mousePos.x > minX &&
                mousePos.y < maxY &&
                mousePos.y > minY)
            Camera.main.transform.position = new Vector3
                    (Mathf.Lerp(transform.position.x, transform.position.x + diffX, Time.deltaTime * speed),
                    Mathf.Lerp(transform.position.y, transform.position.y + diffY, Time.deltaTime * speed),
                    transform.position.z);


            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    
        
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
