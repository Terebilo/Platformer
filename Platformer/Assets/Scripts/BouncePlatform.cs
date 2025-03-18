using UnityEngine;
using System.Collections;

public class BouncePlatform : MonoBehaviour
{
    public float bounceForce = 10f; // Сила подкидывания
    public float heavyMassThreshold = 2f; // Масса, при которой платформа подкидывает персонажа
    public float bounceDelay = 0.5f; // Задержка перед подкидыванием

    private Animator platformAnimator; // Ссылка на компонент Animator
    private Rigidbody2D currentHeavyRigidbody; // Ссылка на Rigidbody2D тяжёлого персонажа

    void Start()
    {
        // Получаем компонент Animator
        platformAnimator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что на платформу встал объект с Rigidbody2D
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Если масса объекта больше порога, начинаем процесс подкидывания
            if (otherRigidbody.mass >= heavyMassThreshold)
            {
                // Сохраняем ссылку на Rigidbody2D персонажа
                currentHeavyRigidbody = otherRigidbody;

                // Воспроизводим анимацию
                if (platformAnimator != null)
                {
                    platformAnimator.SetTrigger("Bounce");
                }

                // Запускаем корутину с задержкой
                StartCoroutine(BounceWithDelay());
            }
        }
    }

    IEnumerator BounceWithDelay()
    {
        // Ждём указанное время
        yield return new WaitForSeconds(bounceDelay);

        // Прикладываем силу вверх
        if (currentHeavyRigidbody != null)
        {
            currentHeavyRigidbody.velocity = new Vector2(currentHeavyRigidbody.velocity.x, bounceForce);
        }
    }
}