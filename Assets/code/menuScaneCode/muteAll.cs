using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class muteAll : MonoBehaviour
{
    [SerializeField] public Toggle musikToggle;
    [SerializeField] private Slider musikSlider;
    [SerializeField] private Slider musikeffectsSlider;

    private MusikSettingsInMeue MusikSettingsInMeue;

    void Start()
    {
       // OnToggleChanged;
        musikToggle.onValueChanged.AddListener(OnToggleChanged);

        MusikSettingsInMeue=FindObjectOfType<MusikSettingsInMeue>();
    }

    public void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("Галочка включена");
           // musikSlider.value=0;
           //  musikeffectsSlider.value=0;
             MusikSettingsInMeue.OnMusikSliderChanged(0.0f);
            musikSlider.interactable = false;
            musikeffectsSlider.interactable = false;

        }
        else
        {
            
            Debug.Log("Галочка выключена");
            musikSlider.interactable = true;
            musikeffectsSlider.interactable = true;
            MusikSettingsInMeue.OnMusikSliderChanged((float)musikSlider.value);
        }
    }
}
