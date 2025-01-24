using UnityEngine;

public class ContanerManager : MonoBehaviour
{
    // Метод, который будет вызываться, чтобы отключить всех дочерних объектов
    public void DisableAllChildren()
    {
        // Перебираем всех дочерних объектов родителя
        foreach (Transform child in transform)
        {
            // Делаем дочерний объект неактивным
            child.gameObject.SetActive(false);
        }
    }

    // Вы можете вызвать эту функцию, например, в методе Start, чтобы сразу сделать дочерние объекты неактивными при старте
    void Start()
    {
        DisableAllChildren();
    }
}
