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

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBodyPart>() != null)
        {
            // check if the right body part is touched
            if (bodyPart != partner.curPartnerBodyPart)
            {
                partner.DisplayTempText();
                return;
            }
            partner.InstantReturnOldText();
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
            gameManager.AnimateHeart();
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
