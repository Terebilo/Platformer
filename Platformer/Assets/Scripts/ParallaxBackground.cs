 using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform[] layers; // Массив слоёв фона (каждый слой содержит два спрайта)
    public float[] parallaxSpeeds; // Скорость параллакса для каждого слоя
    public float smoothing = 1f; // Плавность перемещения

    private Transform cam; // Ссылка на камеру
    private Vector3 previousCamPos; // Предыдущая позиция камеры
    private float[] spriteWidths; // Ширина спрайтов для каждого слоя
    private float cameraHorizontalExtent; // Половина ширины камеры

    void Start()
    {
        // Получаем ссылку на камеру
        cam = Camera.main.transform;
        // Сохраняем начальную позицию камеры
        previousCamPos = cam.position;

        // Вычисляем половину ширины камеры
        float cameraHeight = 2f * cam.GetComponent<Camera>().orthographicSize;
        cameraHorizontalExtent = cameraHeight * cam.GetComponent<Camera>().aspect;

        // Инициализируем ширину спрайтов для каждого слоя
        spriteWidths = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            spriteWidths[i] = layers[i].GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            // Вычисляем смещение фона (в противоположную сторону)
            float parallax = (previousCamPos.x - cam.position.x) * parallaxSpeeds[i];

            // Перемещаем оба спрайта в слое
            for (int j = 0; j < 2; j++)
            {
                // Вычисляем новую позицию спрайта
                float backgroundTargetPosX = layers[i].GetChild(j).position.x + parallax;
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, layers[i].GetChild(j).position.y, layers[i].GetChild(j).position.z);

                // Плавно перемещаем спрайт к новой позиции
                layers[i].GetChild(j).position = Vector3.Lerp(layers[i].GetChild(j).position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

            // Проверяем, вышел ли правый край камеры за правый край второго спрайта
            if (cam.position.x + cameraHorizontalExtent > layers[i].GetChild(1).position.x + spriteWidths[i]*0.75)
            {
                // Перемещаем первый спрайт вправо на две ширины
                Vector3 newPos = layers[i].GetChild(0).position;
                newPos.x += spriteWidths[i] * 2;
                layers[i].GetChild(0).position = newPos;

                // Меняем порядок спрайтов в слое
                Transform temp = layers[i].GetChild(0);
                layers[i].GetChild(0).SetSiblingIndex(1);
            }

            // Проверяем, вышел ли левый край камеры за левый край первого спрайта
            if (cam.position.x - cameraHorizontalExtent < layers[i].GetChild(0).position.x - spriteWidths[i]*0.75)
            {
                // Перемещаем второй спрайт влево на две ширины
                Vector3 newPos = layers[i].GetChild(1).position;
                newPos.x -= spriteWidths[i] * 2;
                layers[i].GetChild(1).position = newPos;

                // Меняем порядок спрайтов в слое
                Transform temp = layers[i].GetChild(1);
                layers[i].GetChild(1).SetSiblingIndex(0);
            }
        }

        // Обновляем предыдущую позицию камеры
        previousCamPos = cam.position;
    }
}