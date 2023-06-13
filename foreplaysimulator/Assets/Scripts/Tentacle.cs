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

    private void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    } 

    private void FixedUpdate()
    {
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length-1; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + targetDir.right * targetDist,
                ref segmentV[i], smoothSpeed);
        }
        segmentPoses[segmentPoses.Length - 1] = tongueStart.position;
        lineRend.SetPositions(segmentPoses);
    }
}
