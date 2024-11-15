using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Interface para o controle do jogador
public interface IPlayerController
{
    void Move(float input); // Método para movimentação
    void SetSpeed(float newSpeed); // Define a velocidade do jogador
    void HandleCollision(GameObject collidedObject); // Lida com colisões
}
public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float speed = 10f; // Velocidade do jogador ajustável via Inspector
    private float direction;
    private Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        // Obtém a entrada do jogador para o movimento horizontal
        direction = Input.GetAxis("Horizontal");
        // Realiza o movimento com base na direção capturada
        Move(direction);
    }
    // Implementação do método de movimento
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
        // Lida com a colisão de forma genérica
        HandleCollision(collision.gameObject);
    }
    // Implementação do método de tratamento de colisões
    public void HandleCollision(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Apple"))
        {
            Apple apple = collidedObject.GetComponent<Apple>();
            if (apple != null)
            {
                // Adiciona a pontuação ao GameManager
                GameManager.instance.AddScore(apple.Score);
                // Destroi o objeto que colidiu
                Destroy(collidedObject);
            }
        }
    }
}
