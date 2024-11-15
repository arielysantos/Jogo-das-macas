using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Interface para gerenciar pontuações
public interface IScoreManager
{
    void AddScore(int value);
    void ResetScore();
    float GetScore();
}
public class GameManager : MonoBehaviour, IScoreManager
{
    Vector2 screenBounds;
    float score;
    [SerializeField] Text scoreText;
    public static GameManager instance;
    public Vector2 ScreenBounds { get => screenBounds; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) + new Vector3(-1, 1);
    }
    // Implementação dos métodos da interface IScoreManager
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }
    public float GetScore()
    {
        return score;
    }
    // Método para atualizar o texto da pontuação na UI
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
    // Exemplo de método com parâmetros adicionais
    public void AddScoreWithMultiplier(int baseValue, float multiplier)
    {
        int calculatedScore = Mathf.RoundToInt(baseValue * multiplier);
        AddScore(calculatedScore);
    }
}
