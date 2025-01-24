using UnityEngine;
using UnityEngine.UI;

public class DynamicGridSize : MonoBehaviour
{
    public int columns = 8; // ������������� ���������� ��������
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
        // �������� ���������� �������� ��������
        int childCount = transform.childCount;

        if (childCount == 0)
        {
            // ���� ��� �������� ��������, ������������� ������ � ����
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);
            return;
        }

        // ��������� ���������� ����� �� ������ ���������� �������� �������� � �������������� ���������� ��������
        int rows = Mathf.CeilToInt(childCount / (float)columns);

        // ��������� ����� ������ ����������
        float cellHeight = gridLayoutGroup.cellSize.y;
        float spacing = gridLayoutGroup.spacing.y;
        float totalHeight = (cellHeight + spacing) * rows - spacing; // ������� ��������� ������

        // ������������� ����� ������ RectTransform
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, totalHeight);
    }

    // �������� ���������� ������� ��� ���������� ��� �������� ��������
    void Update()
    {
        UpdateGridSize();
    }
}