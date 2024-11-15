using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Interface para o controle do jogador
public interface IPlayerController
{
    void Move(float input); // M�todo para movimenta��o
    void SetSpeed(float newSpeed); // Define a velocidade do jogador
    void HandleCollision(GameObject collidedObject); // Lida com colis�es
}
public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float speed = 10f; // Velocidade do jogador ajust�vel via Inspector
    private float direction;
    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // Obt�m a entrada do jogador para o movimento horizontal
        direction = Input.GetAxis("Horizontal");
        // Realiza o movimento com base na dire��o capturada
        Move(direction);
    }
    // Implementa��o do m�todo de movimento
    public void Move(float input)
    {
        // Define a velocidade do jogador
        rigidbody2D.velocity = input * speed * Vector3.right;
        // Restringe o movimento dentro dos limites da tela
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -GameManager.instance.ScreenBounds.x, GameManager.instance.ScreenBounds.x);
        transform.position = position;
    }
    // Permite alterar a velocidade do jogador dinamicamente
    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Max(0, newSpeed); // Garante que a velocidade seja sempre positiva ou zero
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Lida com a colis�o de forma gen�rica
        HandleCollision(collision.gameObject);
    }
    // Implementa��o do m�todo de tratamento de colis�es
    public void HandleCollision(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Apple"))
        {
            Apple apple = collidedObject.GetComponent<Apple>();
            if (apple != null)
            {
                // Adiciona a pontua��o ao GameManager
                GameManager.instance.AddScore(apple.Score);
                // Destroi o objeto que colidiu
                Destroy(collidedObject);
            }
        }
    }
}
