using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [Header("Кнопки")]
    public GameObject leftUpperGroup;
    public GameObject rightUpperGroup;
    [Header("Окна закрытия")]
    public GameObject[] _windows;

    private void Start()
    {
        CloseAllWindows(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllWindows(true);
        }
    }

    public void CloseAllWindows(bool _isClose)
    {
        foreach (GameObject _win in _windows)
        {
            if (_isClose)
            {
                _win.SetActive(false);
                leftUpperGroup.SetActive(true);
                rightUpperGroup.SetActive(true);
            }
            else
            {
                leftUpperGroup.SetActive(false);
                rightUpperGroup.SetActive(false);
            }
        }
        
    }
}
