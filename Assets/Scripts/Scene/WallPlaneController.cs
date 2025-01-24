using UnityEngine;

public class WallPlaneController : MonoBehaviour
{
    public Transform cameraTransform; // ������ �� ��������� ������
    public GameObject wallPlane; // ������ �� ��������� (�������������� ����)
    public float maxDistance = 1.0f; // ������������ ���������� ��� ��������� ���������

    private void Update()
    {
        Vector3 cameraPosition = cameraTransform.position;

        // ���������� �� ����� ����� ��������� ������
        if (Mathf.Abs(cameraPosition.x) > Mathf.Abs(cameraPosition.y) && Mathf.Abs(cameraPosition.x) > Mathf.Abs(cameraPosition.z))
        {
            // ������ ����� � ������� ����� �� X
            PositionWallPlane(new Vector3(Mathf.Sign(cameraPosition.x) * 5, 0, 0), Quaternion.Euler(0, cameraPosition.x < 0 ? 0 : 180, 0));
        }
        else if (Mathf.Abs(cameraPosition.y) > Mathf.Abs(cameraPosition.x) && Mathf.Abs(cameraPosition.y) > Mathf.Abs(cameraPosition.z))
        {
            // ������ ����� � ������� ��� ����
            PositionWallPlane(new Vector3(0, Mathf.Sign(cameraPosition.y) * 5, 0), Quaternion.Euler(cameraPosition.y < 0 ? 90 : -90, 0, 0));
        }
        else
        {
            // ������ ����� � ������ ����� ��� ������
            PositionWallPlane(new Vector3(0, 0, Mathf.Sign(cameraPosition.z) * 5), Quaternion.Euler(0, 0, 0));
        }
    }

    private void PositionWallPlane(Vector3 position, Quaternion rotation)
    {
        // ������������� ������� � ������� ���������
        wallPlane.transform.position = position;
        wallPlane.transform.rotation = rotation;

        // ���������� ���������� �� ������
        float distance = Vector3.Distance(wallPlane.transform.position, cameraTransform.position);

        // ������������� ������� � ����������� �� ����������
        float scale = Mathf.Clamp01(1 - (distance / maxDistance));
        wallPlane.transform.localScale = new Vector3(scale, scale, scale);

        // �������� ���������, ���� ��� ���������
        if (!wallPlane.activeSelf)
        {
            wallPlane.SetActive(true);
        }

        // ���� ������ ������ ��� maxDistance, �������� ���������
        if (distance > maxDistance)
        {
            wallPlane.SetActive(false);
        }
    }
}
