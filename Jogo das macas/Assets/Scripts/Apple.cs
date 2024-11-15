using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IMovable
{
    [SerializeField] private int score = 10;    // Valor configurável através do Inspector
    [SerializeField] private float speed = 5f;  // Tornando a velocidade configurável no editor

    private Rigidbody2D rigidbody2D;

    // Propriedade somente leitura para a pontuação
    public int Score { get => score; }

    private void Start()
    {
        // Asegura que o Rigidbody2D esteja presente
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no objeto Apple!");
        }
    }

    private void Update()
    {
        // Aplica a velocidade no eixo Y para mover a maçã para baixo
        rigidbody2D.velocity = Vector2.up * -speed;

        // Verifica se o objeto está fora dos limites da tela
        if (transform.position.y < -GameManager.instance.ScreenBounds.y)
        {
            Destroy(gameObject);  // Destrui a maçã se sair da tela
        }
    }

    // Método que implementa a interface IMovable para mover o objeto em uma direção específica
    public void Move(Vector2 direction)
    {
        rigidbody2D.velocity = direction * speed;
    }

    // Método que implementa a interface IMovable para ajustar a velocidade do objeto
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}


