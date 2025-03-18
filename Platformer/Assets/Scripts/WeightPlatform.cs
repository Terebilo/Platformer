using UnityEngine;

public class WeightPlatform : MonoBehaviour
{
    public float fallSpeed = 2f; // Скорость опускания платформы
    public float returnSpeed = 1f; // Скорость возвращения платформы
    public float heavyMassThreshold = 2f; // Масса, при которой платформа начинает опускаться

    private Rigidbody2D platformRigidbody;
    private bool isFalling = false;
    private Vector3 initialPosition; // Начальная позиция платформы
    private bool isReturning = false; // Флаг для возвращения платформы

    void Start()
    {
        // Получаем компонент Rigidbody2D платформы
        platformRigidbody = GetComponent<Rigidbody2D>();
        // Сохраняем начальную позицию платформы
        initialPosition = transform.position;
    }

    void Update()
    {
        // Если платформа должна опускаться, двигаем её вниз
        if (isFalling)
        {
            platformRigidbody.velocity = new Vector2(0, -fallSpeed);
        }
        // Если платформа должна возвращаться, двигаем её вверх
        else if (isReturning)
        {
            // Плавно перемещаем платформу к начальной позиции
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);

            // Если платформа вернулась в начальную позицию, останавливаем возвращение
            if (transform.position == initialPosition)
            {
                isReturning = false;
            }
        }
        // Если платформа не должна двигаться, останавливаем её
        else
        {
            platformRigidbody.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, что на платформу встал объект с Rigidbody2D
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Если масса объекта больше порога, платформа начинает опускаться
            if (otherRigidbody.mass >= heavyMassThreshold)
            {
                isFalling = true;
                isReturning = false; // Отключаем возвращение, если платформа снова опускается
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Проверяем, что объект с Rigidbody2D покинул платформу
        Rigidbody2D otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
            // Если масса объекта больше порога, платформа начинает возвращаться
            if (otherRigidbody.mass >= heavyMassThreshold)
            {
                isFalling = false;
                isReturning = true; // Включаем возвращение
            }
        }
    }
}