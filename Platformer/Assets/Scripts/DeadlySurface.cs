using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlySurface : MonoBehaviour
{
    public float restartDelay = 1f; // Задержка перед перезапуском
    
    public FadeEffect fadeEffect; // Ссылка на скрипт FadeEffect

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что столкнулись с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            
            // Запускаем затемнение
            fadeEffect.StartFadeIn();
            // Запускаем перезапуск с задержкой
            Invoke("RestartLevel", restartDelay);
        }
    }

    private void RestartLevel()
    {
        // Перезагружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}