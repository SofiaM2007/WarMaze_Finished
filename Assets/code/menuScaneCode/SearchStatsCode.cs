using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;



public class SearchStatsCode : MonoBehaviour
{
    private AccountManager AccountManager;
    public TMP_InputField inputField;

    void Start() {
        AccountManager = FindObjectOfType<AccountManager>();
    }
    Func<User, int> variable = null;
    
    public void Sort(int Option) {
        int length = 0;
        List<User> allUsers = new List<User>();
        foreach (string line in File.ReadLines("playerdata.json")) {
            User user = JsonUtility.FromJson<User>(line);
            allUsers.Add(user);
            length++;
        }

        switch (Option)
        {
            case 1:
                sortByName(allUsers);
                break; 
            case 2:
                variable = u => u.money;
                sortBubble(allUsers);
                break;
            case 3:
                variable = u => u.killedEnemiesScore;
                sortBubble(allUsers);
                break;
            case 4:
                variable = u => u.eatedFoodScore;
                sortBubble(allUsers);
                break;
            default:
                Debug.LogWarning("Unknown ID");
                return;
        }

        AccountManager.output(allUsers);
    }
    private void sortBubble(List<User> allUsers) {
        for (int i = 0; i < allUsers.Count - 1; i++)
            for (int j = 0; j < allUsers.Count - i - 1; j++) {
                if (variable(allUsers[j]) < variable(allUsers[j + 1])) {
                    var temp = allUsers[j];
                    allUsers[j] = allUsers[j + 1];
                    allUsers[j + 1] = temp;
                }
            }
    }
    private void sortByName(List<User> allUsers) {
        for (int i = 0; i < allUsers.Count; i++)
            for (int j = 0; j < allUsers.Count - i - 1; j++) {//перебір слів
                bool swapped = false;
                for (int k = 0; k < allUsers[j].username.Length && k < allUsers[j+1].username.Length; k++) { //перебір букв в слові
                    
                    if (Char.ToLower(allUsers[j].username[k]) > Char.ToLower(allUsers[j+1].username[k])) {
                        User temp = allUsers[j];
                        allUsers[j] = allUsers[j + 1];
                        allUsers[j + 1] = temp;
                        swapped = true;
                        break;
                    } else if (Char.ToLower(allUsers[j].username[k]) < Char.ToLower(allUsers[j+1].username[k])) {
                        swapped = true;
                        break;
                    }
                }
                // Якщо всі символи однакові, але один коротший — міняємо
                if (!swapped && allUsers[j].username.Length > allUsers[j + 1].username.Length) {
                    User temp = allUsers[j];
                    allUsers[j] = allUsers[j + 1];
                    allUsers[j + 1] = temp;
                }
            }
    }

    public Image buttons;
    bool toggle = false;
    public void sortButton() {
        if (toggle) {
        buttons.gameObject.SetActive(false);
        } else {
        buttons.gameObject.SetActive(true);
        }
        toggle = !toggle;
    }

    public void Search() {
        string input = inputField.text;
        if (input.Length == 0) {
            AccountManager.Load();
            Debug.Log("input is null");
            return;
        }
        
        List<User> tempArr = new List<User>();
        string strColor1 = "<color=#00FF00>", strColor2 = "</color>";
        foreach (string line in File.ReadLines("playerdata.json")) {//перебирає користувачів
            User user = JsonUtility.FromJson<User>(line);
            bool flag = false;

            for (int i = 0; i < user.username.Length; i++) {//перебирає букви в іменах

                if (user.username[i] == input[0]) {
                    for (int j = 0; j < input.Length && i + j < user.username.Length; j++) {
                        if (user.username[i + j] != input[j]) {
                            break;
                        } else if (j == input.Length - 1) {
                            //перед тим як виводити користувача додамо до ім'я форматування
                            Debug.Log($"input :{input}");

                            string newStr = "";
                            for (int k = 0; k < i; k++)
                                newStr += user.username[k];

                            newStr += strColor1 + input + strColor2;
                            
                            for (int k = i + j +1; k < user.username.Length; k++)
                                newStr += user.username[k];
                            //Debug.Log($"str: {newStr}");

                            //user.username = newStr; не можна змінювати прочитану в файлі інформацію коли файл відкрит для читання
                            User newUser = new User
                            {
                                username = newStr,
                                killedEnemiesScore = user.killedEnemiesScore,
                                eatedFoodScore = user.eatedFoodScore,
                                money = user.money
                            };
                            tempArr.Add(newUser);
                            flag = true;
                        }
                    }
                    if (flag)
                        break;//щоб не виводило одного користувача двічі
                }
            }
        }
        AccountManager.output(tempArr);
        //Debug.Log($"Test input: {input}");
    }
}