using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    Vector3 mouseWorldPos;
    Vector3 mouseScreenPos;

    private float maxX;
    private float minX;

    bool moveRight = false;
    bool moveLeft = false;

    float cameraMoveSpeed = 3.9f;

    private void Start()
    {
        Vector3 maxRight = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        Vector3 cameraScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 cameraMaxRightPos = new Vector3(cameraScreenPos.x + Screen.width / 3,
            cameraScreenPos.y, cameraScreenPos.z);
        Vector3 cameraMaxLeftPos = new Vector3(cameraScreenPos.x - Screen.width / 3,
            cameraScreenPos.y, cameraScreenPos.z);
        maxX = Camera.main.ScreenToWorldPoint(cameraMaxRightPos).x;
        minX = Camera.main.ScreenToWorldPoint(cameraMaxLeftPos).x;
    }

    private void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseScreenPos = Camera.main.WorldToScreenPoint(mouseWorldPos);

        //Debug.Log(Screen.width + ". Screen pos: " + mouseScreenPos + ". Mouse world: " + mouseWorldPos);

        if (mouseScreenPos.x > Screen.width *0.8 && transform.position.x < maxX)
        {
            moveRight = true;
        }
        else
        {
            moveRight = false;
        }
        if (mouseScreenPos.x < Screen.width * 0.2 && transform.position.x > minX)
        {
            moveLeft = true;
        }
        else
        {
            moveLeft = false;
        }
    }

   
    private void FixedUpdate()
    {
        if (moveRight)
        {
            transform.position = new Vector3(transform.position.x + Time.deltaTime * cameraMoveSpeed,
                transform.position.y, transform.position.z);
        }
        else if (moveLeft)
        {
            transform.position = new Vector3(transform.position.x - Time.deltaTime * cameraMoveSpeed,
               transform.position.y, transform.position.z);
        }
    }
}
