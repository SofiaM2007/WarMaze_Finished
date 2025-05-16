using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



[System.Serializable]
public class User
{
    public string username;
    public string password;
    public int killedEnemiesScore;
    public int eatedFoodScore;
    public int money;
    public int purchasedItems;
    public bool selected;
}



public class AccountManager : MonoBehaviour
{
    public TMP_InputField NewPlayerName;
    public TMP_InputField NewPassword;
    public TMP_InputField PlayerNameForLogging;
    public TMP_InputField PasswordForLogging;
    public TMP_Text nameText;
    public TMP_Text moneyScoreText;
    public TMP_Text enemiesScoreText;
    public TMP_Text foodScoreText;
    public TMP_Text myStatsOutputText;

    private string filePath = "playerdata.json";
    private fromDataBase fromDataBase;
    private choosFilter choosFilter;
    private CameraMoveCode cammove;
    private clearTextViews clearTextViews;

    private string SavePath => Path.Combine(Application.persistentDataPath, "saveData.json");
    public TMP_Text popuptext1;
    public TMP_Text popuptext2;

    //додає одного користувача в файл
    public void CreateNewPlayer()
    {
        /*
         if (NewPlayerName.text.Length > 16) {
             popuptext1.gameObject.SetActive(true);
             popuptext1.text = "Username must be no longer than 16 characters";
             return;
         }*/
        if (NewPlayerName.text.Length > 10 || NewPassword.text.Length > 10)
        {
            return;
        }
        if (NewPlayerName.text.Length == 0)
        {
            //popuptext1.gameObject.SetActive(true);
            popuptext1.text = "Enter your login";
            return;
        }
        else if (NewPassword.text.Length == 0)
        {
            // popuptext1.gameObject.SetActive(true);
            popuptext1.text = "Enter your password";
            return;
        }

        foreach (string line in File.ReadLines(filePath))
        {
            User user = JsonUtility.FromJson<User>(line);
            if (user.username == NewPlayerName.text)
            {
                //popuptext1.gameObject.SetActive(true);
                popuptext1.text = "User with this name already exist";
                return;
            }
        }

        User newUser = new User
        {
            username = NewPlayerName.text,
            password = NewPassword.text,
            killedEnemiesScore = 0,
            eatedFoodScore = 0,
            money = 0,
            purchasedItems = 64,
            selected = true
        };

        //Debug.Log($"Username: {newUser.username}, Password: {newUser.password}, Score: {newUser.money}");

        string json = JsonUtility.ToJson(newUser);

        if (File.Exists(filePath))
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(json);
            }
        }
        else
        {
            File.WriteAllText(filePath, json);
        }

        //popuptext1.gameObject.SetActive(false);
        popuptext1.text = "";
        //Debug.Log("Користувача додано!");
        Load();
        cammove.MoveCamToMenu();
    }

    //читає файл та виводить інфу
    public void Load()
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
            nameText.text = "There is no saved users\n";
            Vector2 pos = rt.anchoredPosition;
            pos.y = 1.9f;
            rt.anchoredPosition = pos;
            return;
        }
        List<User> allUsers = new List<User>();

        foreach (string line in File.ReadLines(filePath))
        {
            User user = JsonUtility.FromJson<User>(line);
            allUsers.Add(user);
        }

        output(allUsers);
    }
    public void output(List<User> allUsers)
    {
        nameText.text = "name:\n";
        moneyScoreText.text = "money:\n";
        foodScoreText.text = "food eaten:\n";
        enemiesScoreText.text = "killed enemies:\n";
        string colorTag1 = "<color=#b9b6ff>", colorTag2 = "</color>";

        // вивід в спільну статистику
        int counter = 0;
        foreach (var user in allUsers)
        {
            counter++;
            //Debug.Log($"Output is called, Username: {user.username}, Password: {user.password}, money: {user.money}");

            //вивід статистики вибраного користувача
            if (user.selected)
            {
                myStatsOutputText.text = user.username + '\n' + user.password + '\n' + user.killedEnemiesScore + '\n' + user.eatedFoodScore + '\n' + user.money + "\n\n" + outputBoughtItems(user);
                nameText.text += colorTag1 + user.username.ToString() + " (you)" + colorTag2 + "\n";
                moneyScoreText.text += colorTag1 + user.money.ToString() + colorTag2 + '\n';
                foodScoreText.text += colorTag1 + user.eatedFoodScore.ToString() + colorTag2 + '\n';
                enemiesScoreText.text += colorTag1 + user.killedEnemiesScore.ToString() + colorTag2 + '\n';

            }
            else
            {
                nameText.text += user.username.ToString() + '\n';
                moneyScoreText.text += user.money.ToString() + '\n';
                foodScoreText.text += user.eatedFoodScore.ToString() + '\n';
                enemiesScoreText.text += user.killedEnemiesScore.ToString() + '\n';
            }
        }

        Vector2 pos = rt.anchoredPosition;
        //підганяємо розмір листаємого списку під кількість виведенних користувачів
        if (counter < 7)
        {
            size.y = 36 - counter * 6f;
            pos.y = 10f;
        }
        else
        {
            size.y = 11 + (counter - 7) * 6.75f;
            rt.sizeDelta = size;
            pos.y = -50f;//це ми змінюємо розположення щоб листаємий список сам "пролистався" догори
        }
        rt.sizeDelta = size;
        rt.anchoredPosition = pos;

        choosFilter.Start();
        fromDataBase.updateTextView();
    }
    //private GameObject obj;
    //Vector2 size = rt.sizeDelta;
    private RectTransform rt;
    private Vector2 size;

    void Start()
    {
        fromDataBase = FindObjectOfType<fromDataBase>();
        choosFilter = FindObjectOfType<choosFilter>();
        cammove = FindObjectOfType<CameraMoveCode>();
        clearTextViews = FindObjectOfType<clearTextViews>();

        GameObject obj = GameObject.FindWithTag("scrollList");
        rt = obj.GetComponent<RectTransform>();
        size = rt.sizeDelta;

        Load();
        fromDataBase.loadMyBalance();
        fromDataBase.updateTextView();
    }

    private string outputBoughtItems(User user)
    {
        string output = "";
        for (int i = 12; i >= 0; i--)
        {
            if ((1 & (user.purchasedItems >> i)) != 0)//в c# умова має обов'язково бути типом bool
                output += (i + 1).ToString() + ", ";
        }
        if (output.Length > 2)
            return output.Remove(output.Length - 2, 2);
        else
            return "something wrong";
    }

    //log in тут значить те саме що й sign in
    public void LogIn()
    {
        string tempJsonString = "";
        bool doesNameExist = false;
        foreach (string line in File.ReadLines(filePath))
        {
            User user = JsonUtility.FromJson<User>(line);
            if (user.username == PlayerNameForLogging.text)
            {
                doesNameExist = true;
                if (user.password == PasswordForLogging.text)
                {
                    user.selected = true;
                    // popuptext2.gameObject.SetActive(false);
                    popuptext2.text = "";
                    //Debug.Log("Ви увійшли в аккаунт");
                    tempJsonString += JsonUtility.ToJson(user) + "\n";
                    cammove.MoveCamToMenu();
                }
                else
                {
                    // popuptext2.gameObject.SetActive(true);
                    popuptext2.text = "Incorrect password";
                    tempJsonString += line + "\n";
                }
            }
            else
            {
                tempJsonString += line + "\n";
            }
        }
        if (!doesNameExist)
        {
            // popuptext2.gameObject.SetActive(true);
            popuptext2.text = "There is no user with such name!";
            //Debug.Log("Користувача с таким ім'ям не існує!");
        }
        File.WriteAllText(filePath, tempJsonString);
        Load();
        fromDataBase.loadMyBalance();
        choosFilter.Start();
        fromDataBase.updateTextView();
    }

    public void LogOut()
    {
        string tempJsonString = "";

        foreach (string line in File.ReadLines(filePath))
        {
            User user = JsonUtility.FromJson<User>(line);

            if (user.selected)
                user.selected = false;

            tempJsonString += JsonUtility.ToJson(user) + "\n";
        }
        File.WriteAllText(filePath, tempJsonString);
        //трохи інший спосіб перезаписати файл

        fromDataBase.loadMyBalance();
        choosFilter.Start();
        fromDataBase.updateTextView();
        Load();
        clearPrivateFilewithSettings();
        clearTextViews.ClearField();
    }

    public void Delete()
    {
        string tempJsonString = "";
        foreach (string line in File.ReadLines(filePath))
        {
            User user = JsonUtility.FromJson<User>(line);
            if (!user.selected)
                tempJsonString += JsonUtility.ToJson(user) + "\n";
        }
        File.WriteAllText(filePath, tempJsonString);

        fromDataBase.loadMyBalance();
        choosFilter.Start();
        fromDataBase.updateTextView();
        Load();
        clearPrivateFilewithSettings();
        clearTextViews.ClearField();
    }

    private void clearPrivateFilewithSettings()
    {
        string json = File.ReadAllText(SavePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        data.myMoney = 0;
        data.myCostumes = 64;

        string newJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, newJson);
    }
}