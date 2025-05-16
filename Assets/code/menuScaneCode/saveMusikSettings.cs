using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Slider levelsSlider;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private Slider musikSlider;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private Toggle muteAllToggle;
    [SerializeField] private TextMeshProUGUI TextMyMoney;

    private shopScroll shopScroll;
     private fromDataBase fromDataBase;
     private choosFilter choosFilter;
    private string SavePath => Path.Combine(Application.persistentDataPath, "saveData.json");

    private void Start()
    {
        shopScroll = FindObjectOfType<shopScroll>();
        fromDataBase = FindObjectOfType<fromDataBase>();
        choosFilter = FindObjectOfType<choosFilter>();
        LoadFromFile(); // Загрузим данные при старте сцены
        ApplySettingsToUI(); // Применим к UI
    }

    private void OnDisable()
    {
        SaveToFile(); // Сохраняем при уходе со сцены (при выгрузке объекта)
    }

    public void SaveToFile()
    {
        SaveData data = new SaveData
        {
            currentIndexShop = shopScroll.currentIndex,
            musik = musikSlider.value / 100f,
            soundEffekts = effectsSlider.value / 100f,
            enemies = (int)enemySlider.value,
            levels = (int)levelsSlider.value,
            isMuteAll = !musikSlider.interactable,
            myMoney=fromDataBase.myMoney,
            myCostumes=fromDataBase.boughtCostume
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("Сохранено в файл: " + SavePath);
    }

    public void LoadFromFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("Файл не найден, загружаем значения по умолчанию.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        //Debug.Log("pppp"+data.myMoney);


        // Применим к UI
        levelsSlider.value = data.levels;
        enemySlider.value = data.enemies;
        musikSlider.value = data.musik * 100f;
        effectsSlider.value = data.soundEffekts * 100f;
        muteAllToggle.isOn = data.isMuteAll;
        fromDataBase.myMoney=data.myMoney;
        fromDataBase.boughtCostume =data.myCostumes;

        choosFilter.Start();
        fromDataBase.updateTextView();


        musikSlider.interactable = !data.isMuteAll;
        effectsSlider.interactable = !data.isMuteAll;

        if (shopScroll != null)
        {
            shopScroll.currentIndex = data.currentIndexShop;
            shopScroll.UpdateScrollPosition();
        }

        Debug.Log("Загружено из файла: " + SavePath);
    }

    private void ApplySettingsToUI() // не удалять, без него чегото не работает всё, хз чего
    {
        // Применение идёт уже в LoadFromFile
        // Этот метод можно использовать, если нужна отдельная ручная загрузка в интерфейс
    }
}
