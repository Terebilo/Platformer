using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    public bool isOpen { get; private set; } // Свойство для проверки состояния двери

    private void Start()
    {
        animator = GetComponent<Animator>(); // Получаем компонент Animator
        isOpen = false; // Дверь изначально закрыта
    }

    public void OpenDoor()
    {
        if (!isOpen) // Проверяем, что дверь ещё не открыта
        {
            animator.SetTrigger("Open"); // Запускаем анимацию открытия
            isOpen = true; // Устанавливаем состояние двери в "открыта"
        }
    }
}