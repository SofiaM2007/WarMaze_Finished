using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class addStatsToUsersAcc : MonoBehaviour
{
    private string filePath = "playerdata.json";
    User selectedUser;
    private gameController gameController;


    public void addStats() {
        bool isLogged = false;
        gameController = FindObjectOfType<gameController>();
        List<User> tempArr = new List<User>();
        foreach (string line in File.ReadLines(filePath))
        {
            User user = JsonUtility.FromJson<User>(line);
            isLogged = isLogged || user.selected;
            
            if (user.selected) {
                user.killedEnemiesScore += gameController.num_of_enemies_killed;
                user.eatedFoodScore += gameController.num_of_food_eaten;
                user.money += gameController.plus_Moeny;
                selectedUser = user;
            } else {
                tempArr.Add(user);
            }
        }
    
    //Перезаписуємо файл повністю
    if (isLogged) {
        File.WriteAllText(filePath, JsonUtility.ToJson(selectedUser) + "\n");
        foreach (User user in tempArr) {
            File.AppendAllText(filePath, JsonUtility.ToJson(user) + "\n");
        }
            Debug.Log("Info added");
        } else {
        Debug.Log("Ви не увійшли в аккаунт тому ваша статистика нікуди не збереглась!"); }
}
}