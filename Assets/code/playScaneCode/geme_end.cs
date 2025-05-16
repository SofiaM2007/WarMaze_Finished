using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class geme_end : MonoBehaviour
{
    
    private gameController gameController;
    private output output;
    private addStatsToUsersAcc addStatsToUsersAcc;


    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip youDied;

    void Start(){
        gameController = FindObjectOfType<gameController>();
        output = FindObjectOfType<output>();
        addStatsToUsersAcc = FindObjectOfType<addStatsToUsersAcc>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )  // Проверка на столкновение с червяком
        {
            Debug.Log("игра закончена!!!!");
            audioSourceForSoundEffects.clip = youDied;
            audioSourceForSoundEffects.Play();

            Camera.main.transform.position = new Vector3(0, -200, Camera.main.transform.position.z);
            Time.timeScale = 0f;
            output.writeLoseResult();

            addStatsToUsersAcc.addStats();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("body")){
            Debug.Log("1игра закончена!!!!");
            audioSourceForSoundEffects.clip = youDied;
            audioSourceForSoundEffects.Play();

            Camera.main.transform.position = new Vector3(0, -200, Camera.main.transform.position.z);
            Time.timeScale = 0f;
            output.writeLoseResult();
        }
    }

}
