using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitFromPlay : MonoBehaviour
{
     public void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
