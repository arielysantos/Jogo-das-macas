using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameManager : MonoBehaviourPun
{
    Vector2 screenBounds;
    int score, playerInGame;
    Text scoreText;

    public static GameManager instance;

    public Vector2 ScreenBounds { get => screenBounds; }

    private void Awake()
    {
        instance = this;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenBounds += new Vector2(-1, 1);

        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
}
