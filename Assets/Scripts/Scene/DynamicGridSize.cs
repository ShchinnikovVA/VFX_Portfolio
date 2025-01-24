using UnityEngine;
using UnityEngine.UI;

public class DynamicGridSize : MonoBehaviour
{
    public int columns = 8; // Фиксированное количество столбцов
    private GridLayoutGroup gridLayoutGroup;
    private RectTransform rectTransform;

    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        UpdateGridSize();
    }

    void UpdateGridSize()
    {
        // Получаем количество дочерних объектов
        int childCount = transform.childCount;

        if (childCount == 0)
        {
            // Если нет дочерних объектов, устанавливаем высоту в ноль
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
            return;
        }

        // Вычисляем количество рядов на основе количества дочерних объектов и фиксированного количества столбцов
        int rows = Mathf.CeilToInt(childCount / (float)columns);

        // Вычисляем общую высоту контейнера
        float cellHeight = gridLayoutGroup.cellSize.y;
        float spacing = gridLayoutGroup.spacing.y;
        float totalHeight = (cellHeight + spacing) * rows - spacing; // Убираем последний пробел

        // Устанавливаем новый размер RectTransform
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalHeight);
    }

    // Вызываем обновление размера при добавлении или удалении объектов
    void Update()
    {
        UpdateGridSize();
    }
}