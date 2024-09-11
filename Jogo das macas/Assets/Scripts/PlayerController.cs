using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    const float speed = 10;
    float direction;
    Rigidbody2D rigidbody2D;

    bool playerLocal;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        playerLocal = photonView.IsMine;

        if(!playerLocal)
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
            direction = Input.GetAxis("Horizontal");

            Move();
        }
    }

    private void Move()
    {
        rigidbody2D.velocity = direction * speed * Vector3.right;

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(transform.position.x, -GameManager.instance.ScreenBounds.x, GameManager.instance.ScreenBounds.x);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple") && playerLocal)
        {
            int value = collision.GetComponent<Apple>().Score;
            GameManager.instance.photonView.RPC("AddScore", RpcTarget.All, value);
            //GameManager.instance.AddScore(value);
            collision.GetComponent<Apple>().photonView.RPC("Destroy", RpcTarget.All);
        }
    }
}
