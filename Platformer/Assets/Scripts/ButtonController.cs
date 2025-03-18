using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public DoorController door; // Ссылка на объект двери
    public int coinsRequired = 5; // Количество монет, необходимых для открытия
    public Text hintText; // Ссылка на UI Text для подсказки
    public GameObject hiddenObject; // Ссылка на скрытый объект

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hintText.gameObject.SetActive(true); // Показываем подсказку
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E)) // Проверяем нажатие клавиши E
            {
                 if (!door.isOpen) // Проверяем, что дверь ещё не открыта
                {
                    if (GameManager.Instance.GetScore() >= coinsRequired)
                    {
                        GameManager.Instance.SubtractScore(coinsRequired); // Уменьшаем счёт
                        door.OpenDoor(); // Открываем дверь
                        hiddenObject.SetActive(true); // Активируем скрытый объект
                    }
                    else
                    {
                        hintText.text = "Нужно монет: 5";
                        hintText.gameObject.SetActive(true); // Показываем подсказку
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hintText.gameObject.SetActive(false); // Скрываем подсказку
        }
    }
}