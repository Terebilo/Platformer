using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject player1; // Первый персонаж
    public GameObject player2; // Второй персонаж
    public CameraFollow cameraFollow; // Ссылка на скрипт CameraFollow

    private bool isPlayer1Active = true; // Флаг для отслеживания активного персонажа

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Проверяем нажатие клавиши Q
        {
            SwitchPlayer(); // Переключаем персонажей
        }
    }

    void SwitchPlayer()
    {
        if (isPlayer1Active)
        {
            // Переключаемся на второго персонажа
            player2.transform.position = player1.transform.position;
            player1.SetActive(false);
            player2.SetActive(true);
            cameraFollow.SwitchTarget(player2.transform); // Переключаем цель камеры
        }
        else
        {
            // Переключаемся на первого персонажа
            player1.transform.position = player2.transform.position;
            player2.SetActive(false);
            player1.SetActive(true);
            cameraFollow.SwitchTarget(player1.transform); // Переключаем цель камеры
        }

        isPlayer1Active = !isPlayer1Active; // Меняем флаг
    }
}