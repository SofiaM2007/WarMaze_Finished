using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MusikSettingsInMeue : MonoBehaviour
{
    [SerializeField] private Slider musikSlider;
    [SerializeField] public AudioSource audioSourceForMusik; 

    void Start()
    {
        
        //StartCoroutine(WaitASecond());


        musikSlider.onValueChanged.AddListener(OnMusikSliderChanged);
        OnMusikSliderChanged(musikSlider.value); 
    }

   /* IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(0.01f);
        save=FindObjectOfType<save>();
        save.Instance.setAll();
        Debug.Log("setAll wurde aufgeruft");
    }*/

    public void OnMusikSliderChanged(float value)
    {
        audioSourceForMusik.volume=(float)(value/100f);
    }

}
