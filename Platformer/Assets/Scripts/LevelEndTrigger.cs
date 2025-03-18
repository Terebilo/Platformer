using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndTrigger : MonoBehaviour
{
    public string nextLevelName; // Имя следующей сцены
    public FadeEffect fadeEffect; // Ссылка на скрипт FadeEffect
    public float transitionDelay = 1f; // Задержка перед переходом
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что столкнулись с игроком
        if (collision.CompareTag("Player"))
        {
            
            // Запускаем затемнение
            if (fadeEffect != null)
            {
                fadeEffect.StartFadeIn();
            }
            else
            {
                Debug.LogError("FadeEffect не назначен!");
            }

            // Запускаем переход с задержкой
            Invoke("LoadNextLevel", transitionDelay);
        }
    }

    private void LoadNextLevel()
    {
        // Загружаем следующую сцену
        SceneManager.LoadScene(nextLevelName);
    }
}