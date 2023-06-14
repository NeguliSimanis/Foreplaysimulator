//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class dg_simpleCamFollow : MonoBehaviour
{
    public Vector3 target;
    [Range(1f,40f)] public float laziness = 10f;
    public bool lookAtTarget = true;
    public bool takeOffsetFromInitialPos = true;
    public Vector3 generalOffset;
    Vector3 whereCameraShouldBe;
    bool warningAlreadyShown = false;

    public float maxX;
    public float minX;
    public float minY;
    public float maxY;

    private void Start() {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (takeOffsetFromInitialPos && target != null) generalOffset = transform.position - target;
        
    }

    void FixedUpdate()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (target != null) {
            whereCameraShouldBe = target + generalOffset;

            if (whereCameraShouldBe.x < maxX &&
                whereCameraShouldBe.x > minX &&
                whereCameraShouldBe.y < maxY &&
                whereCameraShouldBe.y > minY)

            transform.position = Vector3.Lerp(transform.position, whereCameraShouldBe, 1 / laziness);

            if (lookAtTarget) transform.LookAt(target);
        } else {
            if (!warningAlreadyShown) {
                Debug.Log("Warning: You should specify a target in the simpleCamFollow script.", gameObject);
                warningAlreadyShown = true;
            }
        }
    }
}
