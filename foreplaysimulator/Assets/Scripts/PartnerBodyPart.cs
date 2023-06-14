using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBodyPart : MonoBehaviour
{
    public BodyPart bodyPart;
    public Partner partner;
    bool listenToPlayerTouch = false;
    float pleasureGainDelay = 0.1f;
    float nextPleasureGainTime;
    bool isGainingPleasure = false;
    float basePleasureAmount = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBodyPart>() != null)
        {
            listenToPlayerTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBodyPart>() != null)
        {
            listenToPlayerTouch = false;
        }
    }

    private void Update()
    {
        if (!listenToPlayerTouch)
            return;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Debug.Log("mouse move in nipple " + Time.time);

            if (!isGainingPleasure)
            {
                isGainingPleasure = true;
                nextPleasureGainTime = Time.time + pleasureGainDelay;
            }
            else if (Time.time > nextPleasureGainTime)
            {
                isGainingPleasure = false;
                partner.GainPleasure(basePleasureAmount);
            }

            //float pleasureGainDelay = 0.1f;
            //float nextPleasureGainTime;
            //bool isGainingPleasure = false;

        }
    }
}
