using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Partner : MonoBehaviour
{
    GameManager gameManager;
    public GameObject speechObject;
    public TextMeshProUGUI speechText;

    #region DESIRE MANAGEMENT
    /*
     * Every 6-12 seconds, changes their mind
     * 
     * Wants to be touched by:
     *  -tongue
     *  -lips
     *  -hands
     *  
     *  Wants to be touched on:
     *  - lips
     *  - bosom
     *  - neck
     *  - cheeks
     *  
     *  Doesn't want to be touched on
     *  - EYES
     */
    float desireChangeCooldownMin = 7f;
    float desireChangeCooldownMax = 13f;

    float currPlayerDesireCooldown = 12f;
    float currPartnerDesireCooldown = 6f;

    // BODY PARTS THAT CAN BE TOUCHED
    public BodyPart curPartnerBodyPart; // which body part partner wants to have touched
    List<BodyPart> allowedPartnerBodyParts = new List<BodyPart>();
    List<BodyPart> forbiddenPartnerBodyPart = new List<BodyPart>();

    // BODY PARTS THAT CAN TOUCH
    public BodyPart curPlayerBodyPart; // which body part partner wants to be touched BY
    List<BodyPart> allowedPlayerBodyParts = new List<BodyPart>();
    List<BodyPart> forbiddenPlayerBodyPart = new List<BodyPart>();


    // DESIRE TEXTS
    /// REQUEST PLAYER USE TONGUE  
    string[] useTongueRequests = {
        "Use Tongue",
        "Lick me"
    };
    /// REQUEST TO HAVE LIPS TOUCHED
    string[] touchLipsRequests = {
        "Touch my lips",
        "Now lips",
        "Lips",
    };
    /// REQUEST TO HAVE NECK TOUCHED
    string[] touchNeckRequests = {
        "Touch my neck",
        "Now neck",
        "Neck",
    };
    /// REQUEST TO HAVE BOSOM TOUCHED
    string[] touchBosomRequests = {
        "Touch mah nipel",
        "Touch my boobs",
        "Touch my bosom",
        "Nipple?",
        "Boobs?"
    };
    #endregion

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        InitializePlayerBodyParts();
        InitializePartnerBodyParts();


        SetPlayerBodyPart();
        SetPartnerBodyPart();
    }

    private void InitializePlayerBodyParts()
    {
        allowedPlayerBodyParts.Add(BodyPart.Tongue);
    }

    private void InitializePartnerBodyParts()
    {

        allowedPartnerBodyParts.Add(BodyPart.Lips);
        allowedPartnerBodyParts.Add(BodyPart.Bosom);
        allowedPartnerBodyParts.Add(BodyPart.Neck);

        forbiddenPartnerBodyPart.Add(BodyPart.Eye);
    }

    public void GainPleasure(float baseAmount)
    {
        gameManager.GainPleasure(baseAmount);
        gameManager.audioManager.StartBGMusic();
    }

    private void SetPlayerBodyPart()
    {
        int bodyPartCount = allowedPlayerBodyParts.Count;
        int bodyPartRoll = Random.Range(0, bodyPartCount);
        curPlayerBodyPart = allowedPlayerBodyParts[bodyPartRoll];
        //Debug.Log("player desired body part " + curPlayerBodyPart);
        currPlayerDesireCooldown = Random.Range(desireChangeCooldownMin, desireChangeCooldownMax);
        if (Mathf.Abs(currPlayerDesireCooldown - currPartnerDesireCooldown) < 2f)
            currPlayerDesireCooldown += 3f;
        SetDesireText(isAboutPlayerBodyPart: true);

    }

    private void SetPartnerBodyPart()
    {
        int bodyPartCount = allowedPartnerBodyParts.Count;
        int bodyPartRoll = Random.Range(0, bodyPartCount);
        curPartnerBodyPart = allowedPartnerBodyParts[bodyPartRoll];
        //Debug.Log("partner desired body part " + curPartnerBodyPart);
        currPartnerDesireCooldown = Random.Range(desireChangeCooldownMin, desireChangeCooldownMax);
        if (Mathf.Abs(currPlayerDesireCooldown - currPartnerDesireCooldown) < 2f)
            currPartnerDesireCooldown += 3f;
        SetDesireText();
    }

    private void SetDesireText(bool isAboutPlayerBodyPart = false)
    {
        string desireText = "I want you";
        if (isAboutPlayerBodyPart)
        {
            switch (curPlayerBodyPart)
            {
                case BodyPart.Tongue:
                    desireText = GetUseTongueRequestText();
                    break;
            }
        }
        else
        {
            switch (curPartnerBodyPart)
            {
                case BodyPart.Bosom:
                    desireText = GetTouchBosomText();
                    break;
                case BodyPart.Lips:
                    desireText = GetTouchLipsText();
                    break;
                case BodyPart.Neck:
                    desireText = GetTouchNeckText();
                    break;
            }
        }
        speechText.text = desireText;
        isTempText = false;
    }

    private string GetUseTongueRequestText()
    {
        string requestText = "Use tongue";

        float complexTextRoll = Random.Range(0, 1f);
        if (complexTextRoll > 0.7f)
        {
            string suffixText = "bosom";
            switch (curPartnerBodyPart)
            {
                case BodyPart.Bosom:
                    break;
                case BodyPart.Lips:
                    string[] array = {
                    "Lick 'em",
                    "Clean my face",
                    "Taste me"};
                    int textRoll = Random.Range(0, array.Length);
                    requestText = array[textRoll];
                    break;
                case BodyPart.Face:
                    suffixText = "face";
                    requestText = "Lick my " + suffixText;
                    break;
                case BodyPart.Neck:
                    suffixText = "neck";
                    requestText = "Lick my " + suffixText;
                    break;
            }
           
        }
        else
        {
            int tongueTextRoll = Random.Range(0, useTongueRequests.Length);
            requestText = useTongueRequests[tongueTextRoll];
        }

        return requestText;
    }

    private string GetTouchLipsText()
    {
        string requestText = "Now lips";

        float complexTextRoll = Random.Range(0, 1f);
        if (complexTextRoll > 0.3f)
        {
            switch (curPlayerBodyPart)
            {
                case BodyPart.Tongue:
                    string[] array = {
                    "Lick my face",
                    "Clean my face",
                    "Taste me"};
                    int textRoll = Random.Range(0, array.Length);
                    requestText = array[textRoll];
                    break;
                case BodyPart.Lips:
                    string[] array2 = {
                    "Kiss me",
                    "Kiss?",
                    "Little smooch?"};
                    int textRoll2 = Random.Range(0, array2.Length);
                    requestText = array2[textRoll2];
                    break;
                case BodyPart.Hand:
                    string[] array3 = {
                    "Caress my face",
                    "Slap me"};
                    int textRoll3 = Random.Range(0, array3.Length);
                    requestText = array3[textRoll3];
                    break;
            }
        }
        else
        {
            int lipsTextRoll = Random.Range(0, touchLipsRequests.Length);
            requestText = touchLipsRequests[lipsTextRoll];
        }

        return requestText;
    }

    private string GetTouchBosomText()
    {
        string requestText = "Now tits";

        float complexTextRoll = Random.Range(0, 1f);
        if (complexTextRoll > 0.3f)
        {
            switch (curPlayerBodyPart)
            {
                case BodyPart.Tongue:
                    string[] array = {
                    "Play with my tits",
                    "Taste me",
                    "Lick 'em"};
                    int textRoll = Random.Range(0, array.Length);
                    requestText = array[textRoll];
                    break;
                case BodyPart.Lips:
                    string[] array2 = {
                    "Suck my tits",
                    "Suck 'em"};
                    int textRoll2 = Random.Range(0, array2.Length);
                    requestText = array2[textRoll2];
                    break;
                case BodyPart.Hand:
                    string[] array3 = {
                    "Motorboat",
                    "Squeeze 'em"};
                    int textRoll3 = Random.Range(0, array3.Length);
                    requestText = array3[textRoll3];
                    break;
            }
        }
        else
        {
            int bosomTextRoll = Random.Range(0, touchBosomRequests.Length);
            requestText = touchBosomRequests[bosomTextRoll];
        }

        return requestText;
    }

    private string GetTouchNeckText()
    {
        string requestText = "Now neck";

        float complexTextRoll = Random.Range(0, 1f);
        if (complexTextRoll > 0.4f)
        {
            switch (curPlayerBodyPart)
            {
                case BodyPart.Tongue:
                    string[] array = {
                    "Lick my neck"};
                    int textRoll = Random.Range(0, array.Length);
                    requestText = array[textRoll];
                    break;
                case BodyPart.Lips:
                    string[] array2 = {
                    "Kiss my neck"};
                    int textRoll2 = Random.Range(0, array2.Length);
                    requestText = array2[textRoll2];
                    break;
                case BodyPart.Hand:
                    string[] array3 = {
                    "Choke me",
                    "Caress my neck",
                    "Touch my neck"};
                    int textRoll3 = Random.Range(0, array3.Length);
                    requestText = array3[textRoll3];
                    break;
            }
        }
        else
        {
            int bosomTextRoll = Random.Range(0, touchNeckRequests.Length);
            requestText = touchNeckRequests[bosomTextRoll];
        }

        return requestText;
    }
    public bool isTempText = false;
    string oldString;
    string curString;
    public void DisplayTempText()
    {
        if (isTempText)
            return;
        isTempText = true;
        string currText = speechText.text;
        string[] arr = {
        "Not there",
        "This is hell",
        "What are you doing?",
        "Stop that",
        "Eww stop",
        "No way jose",
        "That's enough",
        "Not good",
        "Try something else"};
        string newText = arr[Random.Range(0, arr.Length)];
        speechText.text = newText;
        curString = newText;
        oldString = currText;
        StartCoroutine(RevertPartnerText());  
    }

    public void InstantReturnOldText()
    {
        if (speechText.text == curString)
        {
            speechText.text = oldString;
        }
        isTempText = false;
    }

    private IEnumerator RevertPartnerText(float revertAfter = 3f)
    {
        yield return new WaitForSeconds(revertAfter);
        InstantReturnOldText();
    }

    private void Update()
    {
        if (!gameManager.isGameStarted)
            return;
        currPartnerDesireCooldown -= Time.deltaTime;
        if (currPartnerDesireCooldown <=0)
        {
            SetPartnerBodyPart();
        }
        currPlayerDesireCooldown -= Time.deltaTime;
        if (currPlayerDesireCooldown <= 0)
        {
            SetPlayerBodyPart();
        }



    }
}
