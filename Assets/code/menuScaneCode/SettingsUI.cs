using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public Slider Slider;
    public TextMeshProUGUI Text;

    public string textForExplaine;

    void Start()
    {
        Slider.onValueChanged.AddListener(OnEnemySliderChanged);
        OnEnemySliderChanged(Slider.value); // початкове значення
    } 

    void OnEnemySliderChanged(float value)
    {
        int count = Mathf.RoundToInt(value);
        //enemyText.text = $"Enemies: {count}";
        Text.text=textForExplaine+count.ToString();
    }
}
