using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsToPlay : MonoBehaviour
{
    public static settingsToPlay Instance;

    public float filter = 0;
    public int enemies=2;
    public int levels=2;
    public float musik=20.0f;
    public float soundEffekts=50.0f;

    [SerializeField] private Slider levelsSlider;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private Slider musikSlider;
    [SerializeField] private Slider effectsSlider;

    private choosFilter f;

    void Start()
    {
        f=FindObjectOfType<choosFilter>();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Оставляем объект при смене сцен
        }
        else
        {
            Destroy(gameObject); // Удаляем дублирующийся объект при перезапуске сцены
        }
    }

    public void endUpdateBeforePlayBeggin(){
        filter=f.filter;
        levels=(int)levelsSlider.value;
        enemies=(int)enemySlider.value;

        if(musikSlider.interactable){
            musik=(float)((int)musikSlider.value)/100f;
            soundEffekts=(float)((int)effectsSlider.value)/100f;
        }
        else{
            musik=0.0f;
            soundEffekts=0.0f;
        }

        Debug.Log(musik+ " - "+ soundEffekts);

    }
}
