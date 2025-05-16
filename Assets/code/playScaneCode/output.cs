using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class output : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextForTime;
    [SerializeField] private TextMeshProUGUI TextForMoney;
    [SerializeField] private TextMeshProUGUI TextForLevel;

    //for game end
    [SerializeField] private TextMeshProUGUI TextForGameEnd;
    [SerializeField] private TextMeshProUGUI TextForEndTime;
    [SerializeField] private TextMeshProUGUI TextForEndMoney;
    [SerializeField] private TextMeshProUGUI TextForEndFood;
    [SerializeField] private TextMeshProUGUI TextForEndEnemy;

    [SerializeField] public AudioSource audioSource; 
    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip clipForLose;
    [SerializeField] public AudioClip clipForPlay;
    [SerializeField] public AudioClip clipButtonClicked;
    [SerializeField] public Image imageForEndBackground;
    [SerializeField] Sprite spriteOnWin;


    public float timer = 0f;
    private bool isRunning = true; // if pause -> fasle

    private gameController gameController;
    private settingsToPlay settingsToPlay;

    float cameraX, cameraY;

    private string SavePath => Path.Combine(Application.persistentDataPath, "saveData.json");


    void Start()
    {       

        gameController=FindObjectOfType<gameController>();
       // Debug.Log("@@@@@@@"+gameController.mazeArrayForObjektGeneration[gameController.level_now]);
        settingsToPlay=FindObjectOfType<settingsToPlay>();
        //Debug.Log(settingsToPlay.musik+ " -- "+ settingsToPlay.soundEffekts);
        TextForMoney.text="0WM";

        audioSource.volume=(float)settingsToPlay.Instance.musik;
        audioSourceForSoundEffects.volume=(float)settingsToPlay.Instance.soundEffekts;

    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            TextForTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

    }
    public void setMoneyText(int n){
        TextForMoney.text=n.ToString()+"WM";
    }

    public void onStop(){
        //Debug.Log("Pause");
        audioSource.Pause(); 

        cameraX=Camera.main.transform.position.x;
        cameraY=Camera.main.transform.position.y;
        Camera.main.transform.position = new Vector3(0, -100, Camera.main.transform.position.z);

        Time.timeScale = 0f;
    }

    public void fromStop(){
        //Debug.Log("Pause");
        audioSource.UnPause();

        Time.timeScale = 1f;

        Camera.main.transform.position = new Vector3(cameraX, cameraY, Camera.main.transform.position.z);
    }
    public void giveUp(){
        Camera.main.transform.position = new Vector3(0, -200, Camera.main.transform.position.z);
        writeLoseResult();
    }

    public void writeLoseResult(){
        audioSource.Stop();

        TextForEndMoney.text=gameController.plus_Moeny.ToString()+"WM";
        TextForEndEnemy.text=gameController.num_of_enemies_killed.ToString();
        TextForEndFood.text=gameController.num_of_food_eaten.ToString();

        /*TextForGameEnd.text="Game End \n You lost";
        TextForEndTime.text="your time: - ";//+string.Format("{0:00}:{1:00}", minutes, seconds);
        TextForEndMoney.text="money earned: "+ gameController.plus_Moeny.ToString()+"WM";
        TextForEndEnemy.text="enemies killed: "+ gameController.num_of_enemies_killed.ToString();
        TextForEndFood.text="food eaten: "+ gameController.num_of_food_eaten.ToString();*/
        rewriteJson();
        
    }

    public void writeWinResult(){
        audioSource.Stop();
        audioSource.clip = clipForLose;
        audioSource.Play();

        imageForEndBackground.sprite = spriteOnWin;

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        TextForEndTime.text=string.Format("{0:00}:{1:00}", minutes, seconds);
        TextForEndMoney.text=gameController.plus_Moeny.ToString()+"WM";
        TextForEndEnemy.text=gameController.num_of_enemies_killed.ToString();
        TextForEndFood.text=gameController.num_of_food_eaten.ToString();

        rewriteJson();
        /*
        TextForGameEnd.text="Game End \n You wone";
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        TextForEndTime.text="your time: "+string.Format("{0:00}:{1:00}", minutes, seconds);
        TextForEndMoney.text="money earned: "+ gameController.plus_Moeny.ToString()+"WM";
        TextForEndEnemy.text="enemies killed: "+ gameController.num_of_enemies_killed.ToString();
        TextForEndFood.text="food eaten: "+ gameController.num_of_food_eaten.ToString();
        */

    }

    void rewriteJson(){
        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        data.myMoney += gameController.plus_Moeny;

       // Debug.Log("pppp"+data.myMoney);

        string newJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, newJson);
    }


    public void toMenue(){
        if(settingsToPlay.Instance != null){
            Destroy(settingsToPlay.Instance.gameObject);
            settingsToPlay.Instance = null;
        }

     //   SceneManager.SetActiveScene(SceneManager.GetSceneByName("menueScene"));

        SceneManager.LoadScene("menueScene");
    }

    public void playOneMoreTime(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("playScane");
    }

    public void MusikButtonclick(){
        audioSourceForSoundEffects.clip = clipButtonClicked;
        audioSourceForSoundEffects.Play();
    }
}
