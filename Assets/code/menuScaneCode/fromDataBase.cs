using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class fromDataBase : MonoBehaviour
{

    public TextMeshProUGUI TextMyMoney;
    public float bestTime;
    public int myMoney;
    public int boughtCostume;


     void Start()
    {
        //ці функції перенесені в класс AccountManager
        //беремо файла
        //loadMyBalance();
        //boughtCostume=64;// куплен только 7 костюм(дефолтный)
        //updateTextView();
    }
    public void loadMyBalance() {
        bool isLogged = false;
        //читаємо файл
        foreach (string line in File.ReadLines("playerdata.json")) {
            User user = JsonUtility.FromJson<User>(line);
            isLogged = isLogged || user.selected;
            if (user.selected) {
                myMoney = user.money;
                boughtCostume = user.purchasedItems;
            }
        }
        if (!isLogged) {
           myMoney = 0;
           boughtCostume = 64;
        }
    }

    User selectedUser;
    public void saveMyBallance() {//тут теж треба зробити перевірку на наявність файлу
        List<User> tempArr = new List<User>();
        foreach (string line in File.ReadLines("playerdata.json"))
        {
            User user = JsonUtility.FromJson<User>(line);
            
            if (user.selected) {
                user.money = myMoney;
                user.purchasedItems = boughtCostume;
                selectedUser = user;
            } else {
                tempArr.Add(user);
            }
        }
    
    //Перезаписуємо файл повністю 
        File.WriteAllText("playerdata.json", JsonUtility.ToJson(selectedUser) + "\n");
        foreach (User user in tempArr) {
            File.AppendAllText("playerdata.json", JsonUtility.ToJson(user) + "\n");
        }
            Debug.Log("Info added");
    //Debug.Log("Ви не увійшли в аккаунт тому ваша статистика нікуди не збереглась!"); }
    }

    public void updateTextView(){
        TextMyMoney.text = myMoney.ToString() + "WM";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}