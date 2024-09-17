using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    const int speed = 10;

    int direction;
    Rigidbody2D rigidbody2D;

    bool playerLocal;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        playerLocal = photonView.IsMine;
        if (!playerLocal)
        {
            Color color = Color.white;
            color.a = 0.2f;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void Update()
    {
        if (playerLocal)
        {
            direction = (int)Input.GetAxis("Horizontal");

            Move();
        }
    }

    void Move()
    {
        rigidbody2D.velocity = new Vector2(direction * speed, 0);

        Vector2 currentPosition =  transform.position;

    }
}