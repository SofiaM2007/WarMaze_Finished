using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    // в лабиринтах: 1-стена, 0,2-пустота, 2-места где не будут генерироваться враги но фрукты будут
    public int[,] maze = // массив лабирнта сейчас (его берут другие классы)
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 2, 2, 2, 2, 0, 0, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };
    public int[,] maze_start1 = // массивы всех лабиринтов
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 2, 2, 2, 2, 0, 0, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };

    public int[,] maze_start2 = 
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 2, 2, 2, 1, 0, 1, 0, 0, 0, 0, 1 },
        { 1, 2, 2, 1, 1, 0, 1, 0, 0, 1, 0, 1 },
        { 1, 2, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1 },
        { 1, 2, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1 },
        { 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };

    public int[,] maze_start3 = 
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 2, 2, 2, 1, 0, 0, 1, 0, 1, 0, 1 },
        { 1, 2, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };

    public int[,] maze_center1 =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 1 },
        { 1, 1, 1, 1, 1, 0, 1, 0, 1, 2, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };
    public int[,] maze_center2 = 
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 1, 2, 2, 1, 1 },
        { 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
        { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };
    public int[,] maze_center3 = 
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 2, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 2, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
    };
    public int[,] maze_end1 =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 1, 0, 2, 2, 2, 1 },
        { 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
        { 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    };
    public int[,] maze_end3 = 
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 2, 1 },
        { 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 2, 1 },
        { 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 2, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1 },
        { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    };
    public int[,] maze_end2 = 
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1 },
        { 1, 0, 0, 1, 1, 0, 0, 0, 2, 2, 2, 1 },
        { 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1 },
        { 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    };

    public float player_speed; //скорость игрока
    public float enemy_speed; //скорость врага
    public int num_of_enemy; // количество врагов для генерации
    public int num_of_food; //количество еды для генерации
    public int num_of_money; //количество денег для генерации

    public int player_x, player_y;// координаты игрока в массиве лабиринта
                                  //public int enemy_x, enemy_y;// координаты врага в массиве лабиринта

    public int maze_h, maze_b; // высота и длина лабиринта (массива лабиритна)

    public List<GameObject> mazeArray = new List<GameObject>(); // список куда будут генерироваться при начале игры все лабиринты
    public List<GameObject> mazeArrayForObjektGeneration = new List<GameObject>(); // список куда будут генерироваться при начале игры все копии лабиринтов
    public int level_now = 0; // текущий уровень лабиринта
    public int my_max_level = 0; // максимальный уровень лабиринта на котором был игрок
    public int levels; // количество уровней лабиринта (взяь с настроек)
    public int num_of_enemies_killed=0; // количество убитых врагов
    public int plus_Moeny=0; // количество взятых монеток за игру

    public int num_of_food_eaten=0; // количество съеденной еды
    public int num_of_bullets_left=0; // количество оставшихся патронов(количество сегментов червяка)



    public Vector2 localSize; // для определения позиции врага в массиве лабиринта(не трогать эту часть)
    public GameObject mazeObject, playerObject, enemyObject, switchMazeTriggerObject, foodObject, winGameObject;
    Image image;
    private settingsToPlay settingsToPlay;
    private generate_enemy generate_enemy;
    private generate_food generate_food;

    void Start()
    {
        settingsToPlay=FindObjectOfType<settingsToPlay>();
        generate_enemy=FindObjectOfType<generate_enemy>();
        generate_food=FindObjectOfType<generate_food>();

        //настройки игры
        player_speed = 25.0f; // for settings
        enemy_speed = 2.5f;//було 2,5
        plus_Moeny=0;
        /*num_of_enemy = 1; //default 4
        num_of_food = 5; //default 5*/
        num_of_money = 3; //default 2
            num_of_enemy=settingsToPlay.Instance.enemies;
            num_of_food=num_of_enemy+2;
            levels = settingsToPlay.Instance.levels;

        num_of_enemies_killed=0; // for output
        num_of_food_eaten=0;
        num_of_bullets_left=0;


        maze_h = maze.GetLength(0);
        maze_b = maze.GetLength(1);

        mazeObject = GameObject.Find("maze_Start1");
         

        playerObject = GameObject.FindWithTag("Player");
        enemyObject = GameObject.FindWithTag("enemy");
        switchMazeTriggerObject = GameObject.FindWithTag("trigger");
        foodObject=GameObject.Find("food");
        winGameObject=GameObject.Find("trigger_complite_game");

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector2 worldSize = sr.bounds.size;
        localSize = new Vector2(worldSize.x / transform.lossyScale.x, worldSize.y / transform.lossyScale.y);

        //добавление всех лабиринтов в список (когда дадут все лабитринты будет рандмная генерация)
        //levels = 3;
        addRandomMazeToArray(levels);

        arrangeLabirints(); //расставляет все лабиринты в правильном порядке и добавляет триегр выхода
        SwitchMaze(level_now); //переключаетмассив лабиринта лабиринт на первый
        onStart(); //выключает все лабиринты и включает первый

    }

    void addRandomMazeToArray(int n){
        System.Random rnd = new System.Random();
        int randomNum = rnd.Next(1, 4);
        switch(randomNum){
            case 1:
                mazeArray.Add(GameObject.Find("maze_Start1"));
                break;
            case 2:
                mazeArray.Add(GameObject.Find("maze_Start2"));
                break;
            default:
                mazeArray.Add(GameObject.Find("maze_Start3"));
                break;
        } 
        CopyMaze(mazeArray[mazeArray.Count - 1]);

        for(int i = 0; i < n-2; i++){
             randomNum = rnd.Next(1, 4);
            switch(randomNum){
                case 1:
                    mazeArray.Add(GameObject.Find("maze_Center1"));
                    break;
                case 2:
                    mazeArray.Add(GameObject.Find("maze_Center2"));
                    break;
                default:
                    mazeArray.Add(GameObject.Find("maze_Center3"));
                    break;
            }
            CopyMaze(mazeArray[mazeArray.Count - 1]);
        }


         randomNum = rnd.Next(1, 4);
        switch(randomNum){
            case 1:
                mazeArray.Add(GameObject.Find("maze_End1"));
                break;
            case 2:
                mazeArray.Add(GameObject.Find("maze_End2"));
                break;
            default:
                mazeArray.Add(GameObject.Find("maze_End3"));
                break;
        }
        CopyMaze(mazeArray[mazeArray.Count - 1]);

    }

    void CopyMaze(GameObject copy){
        GameObject spawnedMaze = Instantiate(copy);
        mazeArrayForObjektGeneration.Add(spawnedMaze);
    }

    void Update() // распознания координат игрока в массиве
    {
        
        if (mazeArrayForObjektGeneration[level_now] != null && playerObject != null)
        {

            Renderer mazeRenderer = mazeArrayForObjektGeneration[level_now].GetComponent<Renderer>();
            if (mazeRenderer != null)
            {
                Vector3 worldSize = mazeRenderer.bounds.size;
                Vector3 worldMin = mazeRenderer.bounds.min;

                Vector3 playerWorldPos = playerObject.transform.position;

                float normalizedX = (playerWorldPos.x - worldMin.x) / worldSize.x;
                float normalizedY = (playerWorldPos.y - worldMin.y) / worldSize.y;

                player_x = Mathf.Clamp(Mathf.FloorToInt(normalizedX * 12), 0, 11);
                player_y = Mathf.Clamp(Mathf.FloorToInt(normalizedY * 12), 0, 11);

            }
            else
            {
                Debug.LogError("Ошибка: у mazeObject нет Renderer!");
            }

        }

        else
        {
            Debug.LogWarning("мэйз или плэер не найдены!");
        }

    }

    void arrangeLabirints()
    {
        float x_pos = 0;
        float dist=27.7f;
        for (int i = 0; i < levels; i++)
        {
            Transform mazeTransform = mazeArray[i].transform;

            Vector2 copyMazePosition = new Vector3(x_pos, 0, mazeTransform.position.z);
            
            mazeArrayForObjektGeneration[i].transform.position=copyMazePosition;

            x_pos += dist;
        }
        winGameObject.transform.position = new Vector2(x_pos-27.7f+5.7f, 10.5f);
        winGameObject.GetComponent<SpriteRenderer>().enabled = false;


    }


    public void SwitchMaze(int i)
    {
        if(i<levels){
            if (mazeArray[i] == GameObject.Find("maze_Start1")){
                maze = maze_start1;
            }
            else if (mazeArray[i] == GameObject.Find("maze_Center1")){
                maze = maze_center1;
            }
            else if (mazeArray[i] == GameObject.Find("maze_End1")){
                maze = maze_end1;
            }
            else if (mazeArray[i] == GameObject.Find("maze_Start2")){
                maze = maze_start2;
            }
            else if (mazeArray[i] == GameObject.Find("maze_Center2")){
                maze = maze_center2;
            }
            else if (mazeArray[i] == GameObject.Find("maze_End2")){
                maze = maze_end2;
            }
            else if (mazeArray[i] == GameObject.Find("maze_Start3")){
                maze = maze_start3;
            }
            else if (mazeArray[i] == GameObject.Find("maze_Center3")){
                maze = maze_center3;
            }
            else if (mazeArray[i] == GameObject.Find("maze_End3")){
                maze = maze_end3;
            }
        }
        else{
            Debug.LogError("Такого лабиринта нет (см. SwitchMaze в gameController)");
        }
    }

    void onStart(){
        for(int i = 1; i < levels; i++)
        {
            SetMazeVisible(false, i);
        }
    }
    public void SetMazeVisible(bool visible, int i)
    {
        mazeArrayForObjektGeneration[i].GetComponent<SpriteRenderer>().enabled = visible;
    }

    // Метод для копирования компонентов и свойств
    public void CopyObjectProperties(GameObject original, GameObject clone)
    {
        foreach (var component in original.GetComponents<Component>())
        {
            if (component is Transform) continue;
            System.Type type = component.GetType();
            Component newComponent = clone.GetComponent(type);

            if (newComponent == null)
            {
                newComponent = clone.AddComponent(type);
            }

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                field.SetValue(newComponent, field.GetValue(component));
            }
        }

        Renderer originalRenderer = original.GetComponent<Renderer>();
        if (originalRenderer != null)
        {
            Renderer cloneRenderer = clone.GetComponent<Renderer>();
            if (cloneRenderer != null)
            {
                cloneRenderer.material = new Material(originalRenderer.material);
            }
        }

        Collider originalCollider = original.GetComponent<Collider>();
        if (originalCollider != null)
        {
            Collider cloneCollider = clone.GetComponent<Collider>();
            if (cloneCollider != null)
            {
                cloneCollider.isTrigger = originalCollider.isTrigger;
            }
        }
    }

    public GameObject getmazeArrayForObjektGeneration(int i){
        return mazeArrayForObjektGeneration[i];
    }
}
