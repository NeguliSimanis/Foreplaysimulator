using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// code from https://www.youtube.com/watch?v=9hTnlp9_wX8&ab_channel=Blackthornprod
/// </summary>
public class Tentacle : MonoBehaviour
{
    public Transform tongueStart;
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    public Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    private void Awake()
    {
        SetStuff();
    } 

    public void SetStuff()
    {
        lineRend.positionCount = length;
        Debug.Log("length" + length);
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void FixedUpdate()
    {
        segmentPoses[0] = targetDir.position;

        // target dist smaller = smaller tongue
        // smaller distance from target = smaller dist multiplier

        Vector3 distanceFromFirstElement = targetDir.position - tongueStart.position;
        float distMultiplier = 0.06f * Mathf.Abs(distanceFromFirstElement.x) + Mathf.Abs(distanceFromFirstElement.y);
        if (distMultiplier > 1)
            distMultiplier = 1;



        //for (int i = segmentPoses.Length-2; i > 0; i--)
        //{
        //    segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i + 1] + targetDir.right * targetDist * distMultiplier,
        //        ref segmentV[i], smoothSpeed);
        //}


        for (int i = 1; i < segmentPoses.Length; i++)
        {
                segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist * distMultiplier,
                    ref segmentV[i], smoothSpeed);
        }

        segmentPoses[segmentPoses.Length - 1] = tongueStart.position;

        //for (int i = 1; i < segmentPoses.Length; i++)
        //{
        //    segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDist * distMultiplier,
        //        ref segmentV[i], smoothSpeed);
        //}
        //segmentPoses[segmentPoses.Length - 1] = tongueStart.position;
        lineRend.SetPositions(segmentPoses);
    }
}
