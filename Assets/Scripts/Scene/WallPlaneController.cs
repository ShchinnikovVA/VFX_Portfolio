using UnityEngine;

public class WallPlaneController : MonoBehaviour
{
    public Transform cameraTransform; // Ссылка на трансформ камеры
    public GameObject wallPlane; // Ссылка на плоскость (энергетическое поле)
    public float maxDistance = 1.0f; // Максимальное расстояние для появления плоскости

    private void Update()
    {
        Vector3 cameraPosition = cameraTransform.position;

        // Определяем на какой стене находится камера
        if (Mathf.Abs(cameraPosition.x) > Mathf.Abs(cameraPosition.y) && Mathf.Abs(cameraPosition.x) > Mathf.Abs(cameraPosition.z))
        {
            // Камера ближе к боковой стене по X
            PositionWallPlane(new Vector3(Mathf.Sign(cameraPosition.x) * 5, 0, 0), Quaternion.Euler(0, cameraPosition.x < 0 ? 0 : 180, 0));
        }
        else if (Mathf.Abs(cameraPosition.y) > Mathf.Abs(cameraPosition.x) && Mathf.Abs(cameraPosition.y) > Mathf.Abs(cameraPosition.z))
        {
            // Камера ближе к потолку или полу
            PositionWallPlane(new Vector3(0, Mathf.Sign(cameraPosition.y) * 5, 0), Quaternion.Euler(cameraPosition.y < 0 ? 90 : -90, 0, 0));
        }
        else
        {
            // Камера ближе к задней стене или фронту
            PositionWallPlane(new Vector3(0, 0, Mathf.Sign(cameraPosition.z) * 5), Quaternion.Euler(0, 0, 0));
        }
    }

    private void PositionWallPlane(Vector3 position, Quaternion rotation)
    {
        // Устанавливаем позицию и поворот плоскости
        wallPlane.transform.position = position;
        wallPlane.transform.rotation = rotation;

        // Определяем расстояние до камеры
        float distance = Vector3.Distance(wallPlane.transform.position, cameraTransform.position);

        // Устанавливаем масштаб в зависимости от расстояния
        float scale = Mathf.Clamp01(1 - (distance / maxDistance));
        wallPlane.transform.localScale = new Vector3(scale, scale, scale);

        // Включаем плоскость, если она неактивна
        if (!wallPlane.activeSelf)
        {
            wallPlane.SetActive(true);
        }

        // Если камера дальше чем maxDistance, скрываем плоскость
        if (distance > maxDistance)
        {
            wallPlane.SetActive(false);
        }
    }
}
