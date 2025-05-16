using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class toPlayScene : MonoBehaviour
{
    private choosFilter choosFilter;
    private settingsToPlay settingsToPlay;
    //private save save;
    
    void Start(){
        Debug.Log("toPlayScene Start()");
        choosFilter=FindObjectOfType<choosFilter>();
        settingsToPlay=FindObjectOfType<settingsToPlay>();
        //save=FindObjectOfType<save>();

         //StartCoroutine(WaitASecond());
    }

    /*void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Сцена загружена: " + scene.name);
        StartCoroutine(WaitASecond());
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(0.1f);
        //save=FindObjectOfType<save>();
        save.Instance.setAll();
        Debug.Log("setAll wurde aufgeruft");
    }*/
   public void LoadScene(string sceneName)
    {
        Debug.Log("!!!!!!!!");
        choosFilter.chooseFilter();
        //save.saveAll();
        settingsToPlay.endUpdateBeforePlayBeggin();
        Time.timeScale = 1f;

      //  Destroy(this.gameObject);
        SceneManager.LoadScene(sceneName);
       // SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
