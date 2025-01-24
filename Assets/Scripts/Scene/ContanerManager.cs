using UnityEngine;

public class ContanerManager : MonoBehaviour
{
    // �����, ������� ����� ����������, ����� ��������� ���� �������� ��������
    public void DisableAllChildren()
    {
        // ���������� ���� �������� �������� ��������
        foreach (Transform child in transform)
        {
            // ������ �������� ������ ����������
            child.gameObject.SetActive(false);
        }
    }

    // �� ������ ������� ��� �������, ��������, � ������ Start, ����� ����� ������� �������� ������� ����������� ��� ������
    void Start()
    {
        DisableAllChildren();
    }
}
