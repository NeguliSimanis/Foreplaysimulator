using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public float rotationSpeed;
    private Vector2 direction;

    public Tentacle tentacle;
    public LineRenderer lineRenderer;
    int maxLineSize = 80;

    public Transform tongueStartPos;
    public Vector2 maxDistanceUpRight;
    public Vector2 maxDistanceDownLeft;
    public float moveSpeed;

    private void Start()
    {
        maxDistanceUpRight = new Vector2(tongueStartPos.position.x + (tentacle.length * tentacle.targetDist),
        tongueStartPos.position.y + (tentacle.length * tentacle.targetDist));
        maxDistanceDownLeft = new Vector2(tongueStartPos.position.x - (tentacle.length * tentacle.targetDist),
        tongueStartPos.position.y - (tentacle.length * tentacle.targetDist));
    }

    private void FixedUpdate()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (angle != 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 potentionalPos = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
        if (!IsTongueTooFar(potentionalPos))
        {
            transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
            //tentacle.length = GetTongueLength(potentionalPos);
            //tentacle.SetStuff();
        }
       //ineRenderer.SetPositions
    }

    private int GetTongueLength(Vector2 potentionalPos)
    {
        int length = 5;
        int add = Mathf.RoundToInt(Mathf.Abs(potentionalPos.x - tongueStartPos.position.x)) +
            Mathf.RoundToInt(Mathf.Abs(potentionalPos.y - tongueStartPos.position.y));
        Debug.Log("add " + add);
        length += add;
        return length;
    }

    private bool IsTongueTooFar(Vector2 targetPos)
    {
        bool tooFar = false;
        if (targetPos.x > maxDistanceUpRight.x)
            tooFar = true;
        if (targetPos.y > maxDistanceUpRight.y)
            tooFar = true;
        //if (targetPos.x < maxDistanceDownLeft.x)
        //    tooFar = true;
        //if (targetPos.y < maxDistanceDownLeft.y)
        //    tooFar = true;
        return tooFar;
    }
}
