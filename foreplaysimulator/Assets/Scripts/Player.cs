using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    public Sprite lipsClosed;
    public Sprite lipsOpen;

    public SpriteRenderer lipRenderer;

    public bool isMouthOpen = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OpenMouth(bool open = true)
    {
        if (open)
        {
            lipRenderer.sprite = lipsOpen;
            isMouthOpen = true;
            gameManager.tongue.SetActive(true);
        }
        else
        {
            lipRenderer.sprite = lipsClosed;
            isMouthOpen = false;
            gameManager.tongue.SetActive(false);
        }
    }
}
