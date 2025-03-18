using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeEffect : MonoBehaviour
{
    public float fadeDuration = 1f; // Длительность затемнения
    public float restartDelay = 1f; // Задержка перед перезапуском

    private Image fadeImage; // Компонент Image для затемнения
    private bool isFadingIn = false; // Флаг для затемнения экрана
    private bool isFadingOut = false; // Флаг для загорания экрана

    void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 1); // Начинаем с полностью затемнённого экрана
        StartFadeOut(); // Запускаем загорание экрана
    }

    void Update()
    {
        if (isFadingIn)
        {
            // Плавно увеличиваем прозрачность (затемнение)
            float alpha = Mathf.Clamp01(fadeImage.color.a + Time.deltaTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);

            // Если экран полностью затемнён, перезагружаем уровень
            if (alpha >= 1f)
            {
                isFadingIn = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (isFadingOut)
        {
            // Плавно уменьшаем прозрачность (загорание)
            float alpha = Mathf.Clamp01(fadeImage.color.a - Time.deltaTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);

            // Если экран полностью загорелся, завершаем процесс
            if (alpha <= 0f)
            {
                isFadingOut = false;
            }
        }
    }

    // Метод для запуска затемнения
    public void StartFadeIn()
    {
        isFadingIn = true;
    }

    // Метод для запуска загорания
    public void StartFadeOut()
    {
        isFadingOut = true;
    }
}