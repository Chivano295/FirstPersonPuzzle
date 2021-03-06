using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

using UnityUtils;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

using Text = TMPro.TextMeshProUGUI;
using System.IO;

/// <summary>
/// Binds the settings and UI together
/// </summary>
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

    #endregion
    #region AudioVariables
    public AudioMixer LinkedMixer;

    public Slider MasterVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;

    #endregion
    #region SaveVariables
    public SaveGameManagment Sgm = new SaveGameManagment();
    public Text DateText;
    #endregion
    #region PreviewPane
    public Image ImagePane;
    public Text DescriptionPane;
    public GameObject ButtonSelf;
    public Text ButtonTxt;
    #endregion

    private bool loadedQuality = false;

    private void Awake()
    {
        #region SetupGraphicsUI
        //Get qualities RUNTIME
        List<TMP_Dropdown.OptionData> _qualities = new List<TMP_Dropdown.OptionData>();
        //Get the quality levels
        foreach (var qt in QualitySettings.names)
        {
            _qualities.Add(new TMP_Dropdown.OptionData(qt));
        }

        
        QualityDropdown.options = _qualities;
        //refresh the quality dropdown
        QualityDropdown.RefreshShownValue();

        //Get the current quality settings
        QualityLevel = QualitySettings.GetQualityLevel();
        AALevel = QualitySettings.antiAliasing;
        TexLevel = QualitySettings.masterTextureLimit;
        ShadowLevel = (int)QualitySettings.shadowResolution;

        //Set the dropdowns  to the current quality settings
        QualityDropdown.value = QualityLevel;
        AADropdown.value = AALevel;
        TexDropdown.value = TexLevel;
        ShadowsDropdown.value = ShadowLevel;

        //Refresh the other dropdowns
        AADropdown.RefreshShownValue();
        TexDropdown.RefreshShownValue();
        ShadowsDropdown.RefreshShownValue();
        loadedQuality = true;
        #endregion

        #region GetCurrentAudioVolumes
        LinkedMixer.GetFloat("Master", out float mst);
        LinkedMixer.GetFloat("Music", out float mus);
        LinkedMixer.GetFloat("SFX", out float sfx);

        MasterVolumeSlider.value = mst;
        MusicVolumeSlider.value = mus;
        SfxVolumeSlider.value = sfx;
        #endregion

        DateText.text = Sgm.GetLastSaveDate().ToString();

        AudioManager.Instance.FadeOut("title_bgm");
        AudioManager.Instance.Play("settings_bgm");
    }

    #region VideoFunc
    /// <summary>
    /// Sets quality level, updating the UI
    /// </summary>
    /// <param name="_level"></param>
    public void SetQuality(int _level)
    {
        if (!loadedQuality) return;
        QualityLevel = _level;
        QualitySettings.SetQualityLevel(QualityLevel, true);
        AALevel = QualitySettings.antiAliasing;
        TexLevel = QualitySettings.masterTextureLimit;
        ShadowLevel = (int)QualitySettings.shadowResolution;

        QualitySettings.renderPipeline = QualityPresetAssets[_level];

        AADropdown.RefreshShownValue();
        TexDropdown.RefreshShownValue();
        ShadowsDropdown.RefreshShownValue();
        Debug.Log("Updated Quality field");
    }

    /// <summary>
    /// Called when changing quality preset
    /// </summary>
    /// <param name="level">Preset to change quality to</param>
    /// <param name="updateFields">Internal use: show preset as "Custom" and NOT update other graaphics settings dropdown values</param>
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
    /// <summary>
    /// Set the multi-sampling anti-aliasing level
    /// </summary>
    /// <param name="level"></param>
    public void SetMSAA(int level)
    { 
        if (!loadedQuality) return;
        if (QualitySettings.GetQualityLevel() != 5) SetQuality(5, false);
        AALevel = level;
        if (!UseUrp)
            QualitySettings.antiAliasing = AALevel;
        else
        {
            //TODO: Make patch for URP
            var pipe = QualitySettings.renderPipeline as UniversalRenderPipelineAsset;
            pipe.msaaSampleCount = level;
        }
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
        QualitySettings.shadowResolution = (UnityEngine.ShadowResolution)ShadowLevel;
        Debug.Log("Updated Shadow field");
    }
    #endregion
    #region AudioFunc
    public void SetMasterVolume(float level)
    {
        LinkedMixer.SetFloat("Music", level);
    }

    public void SetMusicVolume(float level)
    {
        LinkedMixer.SetFloat("Music", level);
    }
    public void SetSfxVolume(float level)
    {
        LinkedMixer.SetFloat("SFX", level);
    }
    #endregion
    #region PreviewPaneFunc

    public void SetPreviewPane(SettingPreviewPaneText sppt)
    {
        DescriptionPane.text = sppt.Description;
        DescriptionPane.gameObject.SetActive(true);
        if (sppt.PreviewImg != null)
        {
            ImagePane.sprite = sppt.PreviewImg;
            ImagePane.gameObject.SetActive(true);
        }
        else
        {
            ImagePane.gameObject.SetActive(false);
        }
        if (sppt.EnableButton)
        {
            ButtonSelf.SetActive(true);
            ButtonTxt.text = sppt.ButtonText;
        }
    }

    public void ResetPreviewPane()
    {
        DescriptionPane.gameObject.SetActive(false);
        ImagePane.gameObject.SetActive(false);
        ButtonSelf.SetActive(false);
    }

    #endregion

    public void DeleteSave()
    {
        Sgm.Delete();
        ButtonTxt.text = "Deleted!";
    }

    //Yed no
    //Map UI to settings
    private int UIToTex(int put)
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
