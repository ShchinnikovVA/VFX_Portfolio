using UnityEngine;

public class IntersectionManager : MonoBehaviour
{
    public GameObject objectToCut;
    public GameObject cuttingObject;
    private Renderer objectToCutRenderer;

    void Start()
    {
        objectToCutRenderer = objectToCut.GetComponent<Renderer>();
    }

    void Update()
    {
        if (IsIntersecting(objectToCut, cuttingObject))
        {
            SetCutShader(true); // Показываем дырку
        }
        else
        {
            SetCutShader(false); // Показываем текстуру
        }
    }

    private bool IsIntersecting(GameObject objA, GameObject objB)
    {
        Collider colliderA = objA.GetComponent<Collider>();
        Collider colliderB = objB.GetComponent<Collider>();
        return colliderA.bounds.Intersects(colliderB.bounds);
    }

    private void SetCutShader(bool showHole)
    {
        objectToCutRenderer.material.SetFloat("_ShowHole", showHole ? 0 : 1);
    }
}