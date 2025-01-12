using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float mainSpeed = 100.0f; // regular speed
    public float shiftAdd = 250.0f; // multiplied by how long shift is held. Basically running
    public float maxShift = 1000.0f; // Maximum speed when holding shift
    public float camSens = 0.25f; // How sensitive it is with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); // position of the last mouse
    private float totalRun = 1.0f;
    private float verticalAngle = 0.0f; // Угол поворота относительно оси Y

    void Update()
    {
        // Сохраняем текущее положение мыши
        Vector3 mousePosition = Input.mousePosition;

        // Проверка нажатия правой кнопки мыши
        if (Input.GetMouseButton(1))
        {
            // Вычисляем смещение мыши с прошлого кадра 
            Vector3 mouseDelta = mousePosition - lastMouse;

            // Поворачиваем камеру по горизонтали
            transform.Rotate(0, mouseDelta.x * camSens, 0, Space.World);

            // Добавляем смещение по вертикали
            verticalAngle -= mouseDelta.y * camSens;

            // Ограничиваем угол по вертикали
            verticalAngle = Mathf.Clamp(verticalAngle, -90f, 90f); // Ограничиваем угол от -90 до 90 градусов

            // Применяем новый угол к камере
            transform.localEulerAngles = new Vector3(verticalAngle, transform.localEulerAngles.y, 0);
        }

        // Обратная связь для последнего положения мыши
        lastMouse = mousePosition;

        // Перемещение камеры
        float speed = mainSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) speed += shiftAdd;
        speed = Mathf.Clamp(speed, 0.0f, maxShift);

        float translation = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) transform.Translate(0, 0, translation);
        if (Input.GetKey(KeyCode.S)) transform.Translate(0, 0, -translation);
        if (Input.GetKey(KeyCode.A)) transform.Translate(-translation, 0, 0);
        if (Input.GetKey(KeyCode.D)) transform.Translate(translation, 0, 0);

    }

}
