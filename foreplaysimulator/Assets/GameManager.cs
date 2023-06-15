using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject characterSelectMenu;
    public GameObject partnerSelectMenu;
    public GameObject gameScene;
    public bool isGameStarted = false;
    private bool isGameOver = false;

    public Player player;

    public GameObject tongue;

    // BUST TIMER
    public TextMeshProUGUI bustTimerText;
    float bustTimeRemaining = 120f;
    float bustUpdateSpeed = 0.01f;
    bool timeRunOut = false;

    //PLEASURE
    private float pleasure = 0;
    private float maxPleasure = 150;
    private float basePleasureLoseAmount = 1f;
    float pleasureLoseDelay = 1.2f;
    public Image pleasureBar;
    public Animator heartPumpAnimator;
    public float heartAnimCooldown = 0.35f;
    public float heartAnimCurrCooldown = 0.35f;

    [Header("UI PANELS")]
    public GameObject gameLosePanel;
    public GameObject gameWonPanel;

    private void Start()
    {
        StartGame();
        tongue.SetActive(false);
        return;
        characterSelectMenu.SetActive(true);
        partnerSelectMenu.SetActive(false);
        gameScene.SetActive(false);
    }


    public void SelectCharacter (int characterID)
    {
        Debug.Log("CHARACTER " + characterID + " SELEECTED");
        characterSelectMenu.SetActive(false);
        partnerSelectMenu.SetActive(true);
    }

    public void SelectPartner(int partnerID)
    {
        Debug.Log("Partner " + partnerID + "selected");
        partnerSelectMenu.SetActive(false);
        StartGame();
        
    }

    private void StartGame()
    {
        isGameStarted = true;
        gameScene.SetActive(true);
        InvokeRepeating("UpdateBustTimer", 0, bustUpdateSpeed);
        InvokeRepeating("LosePleasure", 1f, pleasureLoseDelay);
    }

    private void Update()
    {
        if (!isGameStarted)
            return;

        if (isGameOver)
            return;
        heartAnimCurrCooldown -= Time.deltaTime;
        pleasureBar.fillAmount = pleasure / maxPleasure;

        UpdateBustText();
        if (Input.GetKey(KeyCode.A))
        {
            player.OpenMouth();
        }
        else if(player.isMouthOpen)
        {
            player.OpenMouth(false);
        }
    }

    private void UpdateBustText()
    {
        if (timeRunOut)
        {
            if (isGameOver)
                return;
            isGameOver = true;
            bustTimerText.text = "00:00";
            return;
        }
        int secondsInt = (int)(bustTimeRemaining % 60f);
        int minutes = (int)(bustTimeRemaining / 60f);
        string miliseconds = "00";
        string seconds = secondsInt.ToString();
        if (secondsInt < 10)
            seconds = "0" + secondsInt.ToString();
        int miliInt = (int)(10* (bustTimeRemaining - secondsInt));
        if (miliInt < 10)
        {
            miliseconds = "0" + miliInt.ToString();
        }
        else
        {
            miliseconds = miliInt.ToString();
        }
        bustTimerText.text = "0" + minutes.ToString() + ":" + seconds.ToString();

    }

    private void UpdateBustTimer()
    {
        if (isGameOver)
            return;
        bustTimeRemaining -= bustUpdateSpeed;
        if (bustTimeRemaining <= 0)
        {
            if (!timeRunOut)
            {
                timeRunOut = true;
                LoseGame();
            }
        }
    }

    public void GainPleasure(float baseAmount)
    {
        pleasure += baseAmount;
        if (pleasure >= maxPleasure)
        {
            WinGame();
        }
    }

    private void LosePleasure()
    {
        pleasure -= basePleasureLoseAmount;
        if (pleasure < 0)
            pleasure = 0;
    }

    private void LoseGame()
    {
        gameLosePanel.SetActive(true);
    }

    public void WinGame()
    {
        isGameOver = true;
        gameWonPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AnimateHeart()
    {
         if (heartAnimCurrCooldown <= 0)
        {
            heartAnimCurrCooldown = heartAnimCooldown;
            heartPumpAnimator.SetTrigger("Pump");
        }
    }
}
