using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkySetterButton : MonoBehaviour
{
    #region Properties
    [Header("÷‚ÂÚ ÌÂ·‡")]
    public Color top;
    public Color mid;
    public Color bot;
    [Header("√Î‡‚Ì˚È ÒÍËÔÚ")]
    public GameObject myManager;
    private SceneSettingsGroup _sceneSettingsGroup;
    #endregion

    #region Default
    private void Start()
    {
        SetMyManager(GameObject.Find("Scene Settings")); // œ≈–≈ƒ≈À¿“‹, ›“Œ  Œ—“€À‹ !!!!!
        gameObject.GetComponent<Image>().color = mid;
    }

    public void SetMyManager(GameObject _manager)
    {
        myManager = _manager;
        _sceneSettingsGroup = myManager.GetComponent<SceneSettingsGroup>();
        SetListener();
    }
    public void SetListener()
    {
        //gameObject.GetComponent<Button>().onClick.AddListener(() => _sceneSettingsGroup.SetSceneColor(gameObject.GetComponent<Image>()));
        gameObject.GetComponent<Button>().onClick.AddListener(() => _sceneSettingsGroup.WaveSkyColor(gameObject.GetComponent<SkySetterButton>()));
    }
    #endregion

    #region Colors
    public void TopColor(Color color) => top = color;
    public void MidColor(Color color)
    {
        mid = color;
        gameObject.GetComponent<Image>().color = color;
    }
    public void BotColor(Color color) => bot = color;
    #endregion
}
