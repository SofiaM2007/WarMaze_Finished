using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outSettings : MonoBehaviour
{
    public void MoveCamera()
    {
        //Debug.Log("I want to move camera");
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y-1000, Camera.main.transform.position.z);
    } 
}