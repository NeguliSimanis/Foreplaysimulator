using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBody : MonoBehaviour
{
    public AudioManager audioManager;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBodyPart>() != null
            && Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            PlayerBodyPart playerBodyPart = collision.gameObject.GetComponent<PlayerBodyPart>();
            switch (playerBodyPart.bodyPart)
            {
                case BodyPart.Tongue:
                    audioManager.PlaySFX(SoundType.Lick);
                    break;
                case BodyPart.Hand:
                    audioManager.PlaySFX(SoundType.Kiss);
                    break;
                case BodyPart.Lips:
                    audioManager.PlaySFX(SoundType.Kiss);
                    break;
            }
        }
    }
}
