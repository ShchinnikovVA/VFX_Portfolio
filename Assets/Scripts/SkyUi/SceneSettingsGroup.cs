using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettingsGroup : MonoBehaviour
{
    #region Properties
    //[Header("ѕол")]
    //public GameObject floor;
    //public Material floorMaterial;
    //public Material gridMaterial;
    //public Texture grid;
    [Header("—тартовые значени€")]
    [SerializeField]
    private Material mainSky;
    //[SerializeField]
    //private Color startFloorColor = new Color(63, 63, 63);
    [SerializeField]
    private float switchTime = 0.01f;
    #region Privates
    private string topName = "_TopColor";
    private string midName = "_MiddleColor";
    private string botName = "_BottomColor";
    private int step = 50;
    private int _step;
    private float[] _floorColors = {0, 0, 0};
    private bool isSwithedColor;
    #endregion
    #endregion

    #region Default Functionality
    private void Start() // стартовые настройки сцены
    {   
        _step = step;
        //floorMaterial.color = startFloorColor;
        //floor.SetActive(true);
        RenderSettings.skybox = mainSky;
        mainSky.SetColor(topName, Color.black);
        mainSky.SetColor(midName, Color.black);
        mainSky.SetColor(botName, Color.black);
        //gridMaterial.color = Color.white;
    }
    //public void SetGrid(Toggle toggle) // установка сетки на пол
    //{
    //    if (!toggle.isOn) gridMaterial.color = new Color(255, 255, 255, 0);
    //    else gridMaterial.color = Color.white;
    //}
    //public void ShowFloor(Toggle toggle) => floor.SetActive(toggle.isOn);
    #endregion

    #region Sky Floor Color Switcher
    //public void SetSceneColor(Image colorDonor)
    //{
    //    if (!isSwithedColor) _floorColors = ColorComparison(colorDonor.color, floorMaterial.color); // сохранение нового цвета пола
    //}
    public void WaveSkyColor(SkySetterButton buttonColors) // плавна€ смена неба
    {
        if (!isSwithedColor)
        {
            var top = buttonColors.top; // получение значений цветов старого и нового небес
            var mid = buttonColors.mid;
            var bot = buttonColors.bot;
            var _top = mainSky.GetColor(topName);
            var _mid = mainSky.GetColor(midName);
            var _bot = mainSky.GetColor(botName);
            float[] countTop = ColorComparison(top, _top); // получение разницы между цветами
            float[] countMid = ColorComparison(mid, _mid);
            float[] countBot = ColorComparison(bot, _bot);

            if (top.r < _top.r) StartCoroutine(SwitchColor(countTop, countMid, countBot, false)); // начало смены цветов
            else if (top.r > _top.r) StartCoroutine(SwitchColor(countTop, countMid, countBot, true)); // в зависимости от цвета небо либо темнеет, либо светлеет
        }
    }
    private float[] ColorComparison(Color newColor, Color oldColor) // запись цветов небес
    {
        float[] count = {0, 0, 0};
        count[0] = (newColor.r - oldColor.r) / step;
        count[1] = (newColor.g - oldColor.g) / step;
        count[2] = (newColor.b - oldColor.b) / step;
        return count;
    }
    IEnumerator SwitchColor(float[] countTop, float[] countMid, float[] countBot, bool isUpColor)
    {
        isSwithedColor = true;
        var _top = mainSky.GetColor(topName); // локальна€ запись значений цветов текущего неба
        var _mid = mainSky.GetColor(midName);
        var _bot = mainSky.GetColor(botName);
        // изменение неба и пола на полученное значение
        mainSky.SetColor(topName, new Color(_top.r + countTop[0], _top.g + countTop[1], _top.b + countTop[2]));
        mainSky.SetColor(midName, new Color(_mid.r + countMid[0], _mid.g + countMid[1], _mid.b + countMid[2]));
        mainSky.SetColor(botName, new Color(_bot.r + countBot[0], _bot.g + countBot[1], _bot.b + countBot[2]));
        //floorMaterial.color = new Color(floorMaterial.color.r + _floorColors[0], floorMaterial.color.g + _floorColors[1], floorMaterial.color.b + _floorColors[2]);
        
        yield return new WaitForSeconds(switchTime);
        if (step > 0) // делаем это определЄнное количество раз, чтобы переход был более плавный
        {
            step--;
            StartCoroutine(SwitchColor(countTop, countMid, countBot, isUpColor));
        }
        else if (step == 0)
        {
            step = _step;
            isSwithedColor = false;
            StopCoroutine("SwitchColor");
        }
    }
    #endregion
}
