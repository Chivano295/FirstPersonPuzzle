using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityUtils;

[CreateAssetMenu(fileName = "Set_setting", menuName = "Custom/Setting Preview")]
public class SettingPreviewPaneText : ScriptableObject
{
    public Sprite PreviewImg;
    public string Description;
    public bool EnableButton;
    [DrawIf(nameof(EnableButton), true, ComparisonType.Equals, DisablingType.ReadOnly)]
    public string ButtonText;
    [DrawIf(nameof(EnableButton), true, ComparisonType.Equals, DisablingType.ReadOnly)]
    public UnityEvent ButtonEvent;
}
