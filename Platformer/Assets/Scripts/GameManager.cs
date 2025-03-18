using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Синглтон для доступа к GameManager из других скриптов
    public Text scoreText; // Ссылка на UI Text
    private int score = 0; // Переменная для хранения счета

    private void Awake()
    {
        // Реализация синглтона
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дубликат
        }
    }

    // Метод для увеличения счета
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score; // Обновляем текст
    }

    public void SubtractScore(int points)
    {
        score -= points;
        if (score < 0) score = 0; // Не позволяем счёту уйти в минус
        scoreText.text = "Score: " + score; // Обновляем текст
    }
    
    public int GetScore()
    {
        return score;
    }
}
