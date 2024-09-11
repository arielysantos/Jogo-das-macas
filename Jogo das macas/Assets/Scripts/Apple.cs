using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Apple : MonoBehaviourPun
{
    const int speed = 5;
    [SerializeField] int score;
    Rigidbody2D rigidbody2D;

    public int Score { get => score; }

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidbody2D.velocity = Vector2.up * -speed;

        if(transform.position.y < -GameManager.instance.ScreenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    void Destroy()
    {
        Destroy(gameObject);
    }
}
