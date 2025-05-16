using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CameraMoveCode : MonoBehaviour
{
    public void myAcc() {
        bool isLogged = false;
        foreach (string line in File.ReadLines("playerdata.json")) {
            User user = JsonUtility.FromJson<User>(line);
            isLogged = isLogged || user.selected;
        }
        Debug.Log($"User is logged: {isLogged}");
        
        if (isLogged) {
            Camera.main.transform.position = new Vector3(2777, 256, -10);
        } else {
            Camera.main.transform.position = new Vector3(2177, -524, -10);
        }
    }
    public void MoveCamToStatistics()
    {
        Debug.Log("Cam moved to stats");
        Camera.main.transform.position = new Vector3(-525, Camera.main.transform.position.y-816, Camera.main.transform.position.z);
    }
    public void MoveCamToMenu()
    {
        Debug.Log("Cam moved to menu");
        Camera.main.transform.position = new Vector3(777, 276, -10);
    }
    public void MoveCamToRegistration()
    {
        Debug.Log("Cam moved to Registration");
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y-816, Camera.main.transform.position.z);
    }
}