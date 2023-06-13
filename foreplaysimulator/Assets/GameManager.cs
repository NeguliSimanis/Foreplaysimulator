using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject characterSelectMenu;
    public GameObject partnerSelectMenu;
    public GameObject gameScene;
    public bool isGameStarted = false;

    public Player player;

    public GameObject tongue;

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
    }

    private void Update()
    {
        if (!isGameStarted)
            return;

        if (Input.GetKey(KeyCode.A))
        {
            player.OpenMouth();
            Debug.Log("OPEN");
        }
        else if(player.isMouthOpen)
        {
            player.OpenMouth(false);
            Debug.Log("lose");
        }
    }
}
