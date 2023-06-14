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
    public Image pleasureBar;
    float pleasure = 0f;

    float pleasureLoseDelay = 1.5f;
    float basePleasureLoseAmount = 1f;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        InvokeRepeating("LosePleasure", 1f, pleasureLoseDelay);
    }

    private void LosePleasure()
    {
        pleasure -= basePleasureLoseAmount;
        if (pleasure < 0)
            pleasure = 0;
    }

    public void GainPleasure(float baseAmount)
    {
        gameManager.GainPleasure(baseAmount);
    }

    private void Update()
    {
        
    }
}
