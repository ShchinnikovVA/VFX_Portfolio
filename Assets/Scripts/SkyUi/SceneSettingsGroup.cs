using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSettingsGroup : MonoBehaviour
{
    #region Properties
    [Header("��������� ��������")]
    [SerializeField]
    private Material mainSky;
    [SerializeField]
    private float switchTime = 0.01f;
    #region Privates
    private string topName = "_TopColor";
    private string midName = "_MidColor";
    private string botName = "_BotColor";
    private int step = 50;
    private int _step;
    private bool isSwithedColor;
    #endregion
    #endregion

    #region Default Functionality
    private void Start() // ��������� ��������� �����
    {   
        _step = step;
        RenderSettings.skybox = mainSky;
        mainSky.SetColor(topName, Color.black);
        mainSky.SetColor(midName, Color.black);
        mainSky.SetColor(botName, Color.black);
    }
    #endregion

    #region Sky Color Switcher
    public void WaveSkyColor(SkySetterButton buttonColors) // ������� ����� ����
    {
        if (!isSwithedColor)
        {
            var top = buttonColors.top; // ��������� �������� ������ ������� � ������ �����
            var mid = buttonColors.mid;
            var bot = buttonColors.bot;
            var _top = mainSky.GetColor(topName);
            var _mid = mainSky.GetColor(midName);
            var _bot = mainSky.GetColor(botName);
            float[] countTop = ColorComparison(top, _top); // ��������� ������� ����� �������
            float[] countMid = ColorComparison(mid, _mid);
            float[] countBot = ColorComparison(bot, _bot);

            if (top.r < _top.r) StartCoroutine(SwitchColor(countTop, countMid, countBot, false)); // ������ ����� ������
            else if (top.r > _top.r) StartCoroutine(SwitchColor(countTop, countMid, countBot, true)); // � ����������� �� ����� ���� ���� �������, ���� ��������
        }
    }
    private float[] ColorComparison(Color newColor, Color oldColor) // ������ ������ �����
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
        var _top = mainSky.GetColor(topName); // ��������� ������ �������� ������ �������� ����
        var _mid = mainSky.GetColor(midName);
        var _bot = mainSky.GetColor(botName);
        // ��������� ���� �� ���������� ��������
        mainSky.SetColor(topName, new Color(_top.r + countTop[0], _top.g + countTop[1], _top.b + countTop[2]));
        mainSky.SetColor(midName, new Color(_mid.r + countMid[0], _mid.g + countMid[1], _mid.b + countMid[2]));
        mainSky.SetColor(botName, new Color(_bot.r + countBot[0], _bot.g + countBot[1], _bot.b + countBot[2]));
        
        yield return new WaitForSeconds(switchTime);
        if (step > 0) // ������ ��� ����������� ���������� ���, ����� ������� ��� ����� �������
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
