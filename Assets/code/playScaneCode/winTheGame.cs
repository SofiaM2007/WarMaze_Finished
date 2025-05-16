using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winTheGame : MonoBehaviour
{
    private gameController gameController;
    private output output;
    private addStatsToUsersAcc addStatsToUsersAcc;

    void Start(){
        gameController = FindObjectOfType<gameController>();
        output = FindObjectOfType<output>();
        addStatsToUsersAcc = FindObjectOfType<addStatsToUsersAcc>();
    }

    void Update(){
        if(gameController.num_of_enemies_killed==(gameController.levels-1)*gameController.num_of_enemy+1){
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else{
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_collider") && gameController.num_of_enemies_killed==(gameController.levels-1)*gameController.num_of_enemy+1)  // Проверка на столкновение с червяком и что он убил всех врагов
        {
            Debug.Log("победа !!!!");
            Camera.main.transform.position = new Vector3(0, -200, Camera.main.transform.position.z);
            Time.timeScale = 0f;
            output.writeWinResult();

            addStatsToUsersAcc.addStats();
        }
    }
}