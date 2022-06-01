using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

using UnityUtils;

public class SettingsBinder : MonoBehaviour
{
    //URP SPECIFIC
    public bool UseUrp = false;
    [DrawIf(nameof(UseUrp), true, ComparisonType.Equals, DisablingType.DontDraw)]
    public RenderPipelineAsset[] QualityPresetAssets;
    //END

    #region VideoVariables
    public TMP_Dropdown QualityDropdown;
    public TMP_Dropdown AADropdown;
    public TMP_Dropdown TexDropdown;
    public TMP_Dropdown ShadowsDropdown;
    public TextMeshProUGUI KeyText;

    public int QualityLevel = 0;
    public int AALevel = 0;
    public int TexLevel = 0;
    public int ShadowLevel = 0;

    private bool loadedQuality = false;
#endregion

    private void Awake()
    {
        if (KeyText != null) KeyText.text = string.Format(KeyText.text,PlayerPrefsExt.GetLong("game.scores.HighScore", 04324).ToString());
        //Set qualities RUNTIME
        List<TMP_Dropdown.OptionData> _qualities = new List<TMP_Dropdown.OptionData>();
        //qualityDropdown.options = null;
        foreach (var qt in QualitySettings.names)
        {
            Debug.Log(qt);
            _qualities.Add(new TMP_Dropdown.OptionData(qt));
        }
        
        QualityDropdown.options = _qualities;
        QualityDropdown.RefreshShownValue();
        QualityLevel = QualitySettings.GetQualityLevel();
        AALevel = QualitySettings.antiAliasing;
        TexLevel = QualitySettings.masterTextureLimit;
        ShadowLevel = (int)QualitySettings.shadowResolution;
        QualityDropdown.value = QualityLevel;
        AADropdown.value = AALevel;
        TexDropdown.value = TexLevel;
        ShadowsDropdown.value = ShadowLevel;
        AADropdown.RefreshShownValue();
        TexDropdown.RefreshShownValue();
        ShadowsDropdown.RefreshShownValue();
        loadedQuality = true;
    }
    public void SetQuality(int _level)
    {
        if (!loadedQuality) return;
        QualityLevel = _level;
        QualitySettings.SetQualityLevel(QualityLevel, true);
        AALevel = QualitySettings.antiAliasing;
        TexLevel = QualitySettings.masterTextureLimit;
        ShadowLevel = (int)QualitySettings.shadowResolution;
        //qualityDropdown.value = qualityLevel;
        //AADropdown.value = AALevel;
        //TexDropdown.value = TexLevel;
        //ShadowsDropdown.value = ShadowLevel;

        QualitySettings.renderPipeline = QualityPresetAssets[_level];

        AADropdown.RefreshShownValue();
        TexDropdown.RefreshShownValue();
        ShadowsDropdown.RefreshShownValue();
        Debug.Log("Updated Quality field");
    }
    public void SetQuality(int level, bool updateFields)
    {
        QualityLevel = level;
        QualityDropdown.value = QualityLevel;
        if (updateFields)
        {
            AALevel = QualitySettings.antiAliasing;
            TexLevel = QualitySettings.masterTextureLimit;
            ShadowLevel = (int)QualitySettings.shadowResolution;
            AADropdown.value = AALevel;
            TexDropdown.value = TexLevel;
            ShadowsDropdown.value = ShadowLevel;
            Debug.Log("Updated Quality field");
        }
        QualitySettings.SetQualityLevel(QualityLevel, true);
    }
    public void SetMSAA(int level)
    { 
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        AALevel = level;
        QualitySettings.antiAliasing = AALevel;
        Debug.Log("Updated AA field");
    }
    public void SetTex(int level)
    {  
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        TexLevel = level;
        QualitySettings.masterTextureLimit = UIToTex(TexLevel + 1);
        Debug.Log("Updated Tex field");
    }
    public void SetShadows(int level)
    { 
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        ShadowLevel = level;
        QualitySettings.shadowResolution = (ShadowResolution)ShadowLevel;
        Debug.Log("Updated Shadow field");
    }

    public void RemoveData()
    {
        PlayerPrefs.DeleteAll();
    }

    //Yed no
    int UIToTex(int put)
    {
        int res = 0;
        switch (put)
        { 
            case 0:
                res = 4;
                break;
            case 1:
                res = 3;
                break;
            case 2:
                res = 2;
                break;
            case 3:
                res = 1;
                break;
            case 4:
                res = 0;
                break;
            default:
                break;
        }
        return res;
    }

}
