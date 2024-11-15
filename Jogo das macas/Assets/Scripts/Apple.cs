using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IMovable
{
    [SerializeField] private int score = 10;    // Valor configur�vel atrav�s do Inspector
    [SerializeField] private float speed = 5f;  // Tornando a velocidade configur�vel no editor

    private Rigidbody2D rigidbody2D;

    // Propriedade somente leitura para a pontua��o
    public int Score { get => score; }

    private void Start()
    {
        // Asegura que o Rigidbody2D esteja presente
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (rigidbody2D == null)
        {
            Debug.LogError("Rigidbody2D n�o encontrado no objeto Apple!");
        }
    }

    private void Update()
    {
        // Aplica a velocidade no eixo Y para mover a ma�� para baixo
        rigidbody2D.velocity = Vector2.up * -speed;

        // Verifica se o objeto est� fora dos limites da tela
        if (transform.position.y < -GameManager.instance.ScreenBounds.y)
        {
            Destroy(gameObject);  // Destrui a ma�� se sair da tela
        }
    }

    // M�todo que implementa a interface IMovable para mover o objeto em uma dire��o espec�fica
    public void Move(Vector2 direction)
    {
        rigidbody2D.velocity = direction * speed;
    }

    // M�todo que implementa a interface IMovable para ajustar a velocidade do objeto
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}


