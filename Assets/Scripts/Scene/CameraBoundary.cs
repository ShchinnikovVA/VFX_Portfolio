using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public float warningDistance = 5f; // Расстояние, при котором система активируется
    public GameObject energyFieldPrefab; // Префаб энергетического поля
    private GameObject energyFieldInstance; // Экземпляр энергетического поля

    private void Update()
    {
        // Проверяем расстояние до ближайшего коллайдера
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, warningDistance);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Boundary"))
            { // Убедитесь, что у ваших границ есть тег
                ShowEnergyField();
                return; // Выходим из метода, если камера слишком близко
            }
        }

        HideEnergyField(); // Скрыть эффект, если камера далеко
    }

    private void ShowEnergyField()
    {
        if (energyFieldInstance == null)
        {
            // Создаем и позиционируем энергетическое поле
            energyFieldInstance = Instantiate(energyFieldPrefab, transform.position, Quaternion.identity);
            energyFieldInstance.transform.SetParent(transform); // Привязываем к камере, чтобы следить за ее движением
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