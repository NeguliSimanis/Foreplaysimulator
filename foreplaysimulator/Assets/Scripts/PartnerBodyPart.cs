using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBodyPart : MonoBehaviour
{
    public BodyPart bodyPart;
    public Partner partner;
    bool listenToPlayerTouch = false;
    bool detectLick = false;
    float pleasureGainDelay = 0.1f;
    float nextPleasureGainTime;
    bool isGainingPleasure = false;
    float basePleasureAmount = 1f;

    GameManager gameManager;

    // ANIMATION
    [Header("ANIMATION")]
    public Animator bodyPartAnimator;
    public bool hasAnimator;
    bool isAnimating = false;
    private IEnumerator stopAnimationCoroutine;
    bool isStopAnimActive = false;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        stopAnimationCoroutine = StopAnimatingPartAfterDelay(1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBodyPart>() != null)
        {
            // check if the right body part is touched
            detectLick = true;
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
            detectLick = false;
            if (!isStopAnimActive && hasAnimator)
                StartCoroutine(StopAnimatingPartAfterDelay(1f));
        }
    }

    private void Update()
    {

        if (!detectLick)
            return;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            AnimateBodyPartWhenTouched();
            if (!listenToPlayerTouch)
                return;
            if (!isGainingPleasure)
            {
                isGainingPleasure = true;
                nextPleasureGainTime = Time.time + pleasureGainDelay;
            }
            else if (Time.time > nextPleasureGainTime)
            {
                gameManager.AnimateHeart();
                isGainingPleasure = false;
                partner.GainPleasure(basePleasureAmount);
            }

            //float pleasureGainDelay = 0.1f;
            //float nextPleasureGainTime;
            //bool isGainingPleasure = false;

        }
    }

    private IEnumerator StopAnimatingPartAfterDelay(float delay)
    {
        isStopAnimActive = true;
        yield return new WaitForSeconds(delay);
        if (!listenToPlayerTouch)
        {
            bodyPartAnimator.SetBool("isBouncing", false);
            isAnimating = false;
        }
        isStopAnimActive = false;
    }

    private void AnimateBodyPartWhenTouched()
    {
        if (isAnimating)
            return;
        if (!hasAnimator)
            return;
        isAnimating = true;
        bodyPartAnimator.SetBool("isBouncing", true);
    }
}
