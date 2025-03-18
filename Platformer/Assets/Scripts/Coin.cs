using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject); // Уничтожаем монету
            GameManager.Instance.AddScore(1); // Увеличиваем счет
        }
    }
}