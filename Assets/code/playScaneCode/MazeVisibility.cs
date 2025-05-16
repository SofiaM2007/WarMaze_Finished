using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeVisibility : MonoBehaviour
{
    public GameObject maze; // Лабиринт
    public gameController g;
    public int numberOfIntersections = 0;
    private generate_enemy generate_enemy;
    private generate_food generate_food;
    private generate_maney generate_maney;
    private output output;

    public int numOfManeyForLastMaze = 0;

    [SerializeField] private TextMeshProUGUI TextForTime;
    [SerializeField] private TextMeshProUGUI TextForMoney;
    [SerializeField] private Button stopButton;
    [SerializeField] private TextMeshProUGUI TextForLevel;

    void Start()
    {
        g = FindObjectOfType<gameController>();
        generate_enemy = FindObjectOfType<generate_enemy>();
        generate_food = FindObjectOfType<generate_food>();
        generate_maney = FindObjectOfType<generate_maney>();
        output = FindObjectOfType<output>();

        GetComponent<SpriteRenderer>().enabled = true;

    }

    void Update()
    {
        if (gameObject.name == "trigger_out_of_maze(Clone)")
        {
            if (g.num_of_enemies_killed >= (g.my_max_level + 1) * g.num_of_enemy)
            { // Если убиты все враги на уровне разрешаеться проход дальше (спомощью включения тригера)
                Debug.Log("ffffff" + g.num_of_enemies_killed);
                Collider2D collider = GetComponent<BoxCollider2D>();
                collider.isTrigger = true;

                // GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = null;

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_collider") && numberOfIntersections % 2 == 0) // Если игрок вошёл на тригер
        {

            g.SetMazeVisible(false, g.level_now); // Выключаем видимость нынешнего лаиюирнта(с которого ушли)
            numberOfIntersections++;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 27.7f, Camera.main.transform.position.y, Camera.main.transform.position.z); //передвигаем камеру
            moveOutput(1);
            g.playerObject.transform.position = new Vector3(g.playerObject.transform.position.x + 3f, g.playerObject.transform.position.y, g.playerObject.transform.position.z); //передвигаем немного ирока

            g.level_now++;
            g.SetMazeVisible(true, g.level_now); // Включаем видимость нового лабиринта (в который перешли)
            g.SwitchMaze(g.level_now); //еняем массив лабиринта на тот в которм сейчас(для ориентации врагов)
            turnOnOff_ObjectsOnPlatform(g.mazeArrayForObjektGeneration[g.level_now - 1], false); //выключаем всё что было на предыдущем лабиринте
            turnOnOff_ObjectsOnPlatform(g.mazeArrayForObjektGeneration[g.level_now], true); //включаем всё что есть на нынешнем лабиринте

            //новые настройки для нового уровня
            if (g.level_now > g.my_max_level)
            { //проверяем надо ли переключать новый уровень и генрерировать врагов и еду для него
                delete_mony();
                g.my_max_level = g.level_now;
                generate_food.Start();
                generate_maney.Start();

                if (g.level_now + 1 == g.levels)
                {
                    generate_enemy.generateBoss();
                }
                else
                {
                    generate_enemy.Start();

                }

                numOfManeyForLastMaze = g.num_of_money;

                generateTriggerForNextMaze();
            }
        }
        else if (other.CompareTag("Player_collider") && numberOfIntersections % 2 == 1)
        { //всё то же самлое что и для этого только если назад идйт игрок
            g.SetMazeVisible(false, g.level_now);
            g.level_now--;

            g.SetMazeVisible(true, g.level_now);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - 27.7f, Camera.main.transform.position.y, Camera.main.transform.position.z);
            moveOutput(-1);
            g.playerObject.transform.position = new Vector3(g.playerObject.transform.position.x - 3f, g.playerObject.transform.position.y, g.playerObject.transform.position.z);
            numberOfIntersections--;
            turnOnOff_ObjectsOnPlatform(g.mazeArrayForObjektGeneration[g.level_now + 1], false);
            delete_mony();
            turnOnOff_ObjectsOnPlatform(g.mazeArrayForObjektGeneration[g.level_now], true);

        }

        if (other.CompareTag("Player_collider"))
        {
            TextForLevel.text = (g.level_now + 1).ToString() + " level";
        }
    }
    void generateTriggerForNextMaze()
    {
        if (g.level_now + 1 != g.levels)
        {
            Vector2 newTriggerPosition = new Vector2(transform.position.x + 27.7f, transform.position.y);
            GameObject spawnedTrigger = Instantiate(g.switchMazeTriggerObject);
            spawnedTrigger.transform.position = newTriggerPosition;
        }
    }

    void moveOutput(int direktion)
    {
        TextForTime.transform.position = new Vector3(TextForTime.transform.position.x + direktion * 27.7f, TextForTime.transform.position.y, TextForTime.transform.position.z);
        TextForLevel.transform.position = new Vector3(TextForLevel.transform.position.x + direktion * 27.7f, TextForLevel.transform.position.y, TextForLevel.transform.position.z);
        TextForMoney.transform.position = new Vector3(TextForMoney.transform.position.x + direktion * 27.7f, TextForMoney.transform.position.y, TextForMoney.transform.position.z);
        stopButton.transform.position = new Vector3(stopButton.transform.position.x + direktion * 27.7f, stopButton.transform.position.y, stopButton.transform.position.z);
    }

    void turnOnOff_ObjectsOnPlatform(GameObject platform, bool turnOn) //функция для выключения или включения всех обьектов находящихся на platform
    {
        //выключаем 2д
        Collider2D[] objectsOnPlatform = Physics2D.OverlapBoxAll(platform.transform.position, platform.GetComponent<Collider2D>().bounds.size, 0);

        foreach (Collider2D obj in objectsOnPlatform)
        {
            if (obj.gameObject != g.playerObject && obj.gameObject != GameObject.FindWithTag("Player_collider")) // Чтобы не отключить саму платформу, 
            {
                obj.GetComponent<SpriteRenderer>().enabled = turnOn; // Отключаем объект
                MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
                foreach (var script in scripts)
                    script.enabled = turnOn;
            }

            if ((obj.gameObject == GameObject.Find("trigger_out_of_maze") || obj.gameObject == GameObject.Find("trigger_out_of_maze(Clone)")) && turnOn)
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }


    }

    void turnOnText(bool turnOn)
    {
        GameObject textObject = GameObject.Find("bossHP");

        if (textObject != null)
        {
            textObject.SetActive(turnOn);
        }
    }

    void delete_mony()
    {
        GameObject[] money_objects = GameObject.FindGameObjectsWithTag("manetka(Clone)");
        GameObject[] lite_objects = GameObject.FindGameObjectsWithTag("DirectionalLight(Clone)");

        foreach (GameObject obj in money_objects)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in lite_objects)
        {
            Destroy(obj);
        }
    }
}
