using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public float warningDistance = 5f; // ����������, ��� ������� ������� ������������
    public GameObject energyFieldPrefab; // ������ ��������������� ����
    private GameObject energyFieldInstance; // ��������� ��������������� ����

    private void Update()
    {
        // ��������� ���������� �� ���������� ����������
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, warningDistance);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Boundary"))
            { // ���������, ��� � ����� ������ ���� ���
                ShowEnergyField();
                return; // ������� �� ������, ���� ������ ������� ������
            }
        }

        HideEnergyField(); // ������ ������, ���� ������ ������
    }

    private void ShowEnergyField()
    {
        if (energyFieldInstance == null)
        {
            // ������� � ������������� �������������� ����
            energyFieldInstance = Instantiate(energyFieldPrefab, transform.position, Quaternion.identity);
            energyFieldInstance.transform.SetParent(transform); // ����������� � ������, ����� ������� �� �� ���������
        }
    }

    private void HideEnergyField()
    {
        if (energyFieldInstance != null)
        {
            Destroy(energyFieldInstance);
        }
    }
}