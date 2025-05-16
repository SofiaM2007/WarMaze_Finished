using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_maney : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn, liteToSpawn; // Префаб объекта, который нужно создавать
    [SerializeField] private int spawnCount; // Количество денег для генерации
    private Renderer mazeRenderThis;
    private gameController g;

    public void Start()
    {
        g = FindObjectOfType<gameController>();
        spawnCount = 2;
        objectToSpawn = GameObject.Find("manetka");
        liteToSpawn = GameObject.Find("DirectionalLight");

        StartCoroutine(MyCoroutine(1f, g.num_of_money));
    }


    public IEnumerator MyCoroutine(float time, int n)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < n; i++){
            yield return new WaitForSeconds(time);
            SpawnObject();
        }
    }


    private void SpawnObject()
    {
        if (g.level_now < 0 || g.level_now >= g.mazeArray.Count || g.mazeArray[g.level_now] == null)
        {
            Debug.LogError("Некорректный индекс или пустой уровень!");
            return;
        }

        if (objectToSpawn == null || liteToSpawn == null)
        {
            Debug.LogError("Объект для спавна не найден!");
            return;
        }

        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is not assigned!");
            return;
        }

        if (g.mazeArrayForObjektGeneration[g.level_now] == null)
        {
            Debug.LogError("Maze object is not assigned!");
            return;
        }

        mazeRenderThis=g.mazeArrayForObjektGeneration[g.my_max_level].GetComponent<Renderer>();

        System.Random rnd = new System.Random();
        Renderer mazeRenderer = mazeRenderThis;
        Vector3 worldSize = mazeRenderer.bounds.size;
        Vector3 worldMin = mazeRenderer.bounds.min;

        int kx, ky;
        do
        {
            kx = rnd.Next(1, 11);
            ky = rnd.Next(1, 11);
        } while (g.maze[kx, ky] == 1);

        float cellSizeX = worldSize.x / 12f;
        float cellSizeY = worldSize.y / 12f;

        float worldPosX = worldMin.x + (kx + 0.5f) * cellSizeX;
        float worldPosY = worldMin.y + (ky + 0.5f) * cellSizeY;

        Vector3 randomPosition = new Vector3(worldPosX, worldPosY, 0f);
        Vector3 lightPosition = new Vector3(worldPosX, worldPosY + 5f, -1f);

        GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        GameObject spawnedLight = Instantiate(liteToSpawn, lightPosition, Quaternion.identity);

        spawnedObject.tag = "manetka(Clone)"; 
        spawnedLight.tag="DirectionalLight(Clone)";

        if(g.my_max_level!=g.level_now){Destroy(spawnedObject); Destroy(spawnedLight);}

        StartCoroutine(waitAndRelokate(rnd.Next(10, 15), spawnedObject, spawnedLight));
    }
    
    IEnumerator waitAndRelokate(float time1, GameObject spawnedObject, GameObject spawnedLight)
    {
        
        yield return new WaitForSeconds(time1); // Ожидание time секунд

        if(g.my_max_level!=g.level_now){Destroy(spawnedObject); Destroy(spawnedLight);}

        System.Random rnd = new System.Random();
        Renderer mazeRenderer = mazeRenderThis;
        Vector3 worldSize = mazeRenderer.bounds.size;
        Vector3 worldMin = mazeRenderer.bounds.min;


        int kx, ky;
        do
        {
            kx = rnd.Next(1, 11);
            ky = rnd.Next(1, 11);
        } while (g.maze[kx, ky] == 1);

        float cellSizeX = worldSize.x / 12f;
        float cellSizeY = worldSize.y / 12f;

        float worldPosX = worldMin.x + (kx + 0.5f) * cellSizeX;
        float worldPosY = worldMin.y + (ky + 0.5f) * cellSizeY;

        Vector3 randomPosition = new Vector3(worldPosX, worldPosY, 0f);
        Vector3 lightPosition = new Vector3(worldPosX, worldPosY + 5f, -1f);

        if (spawnedObject != null)
        {
            spawnedObject.transform.position = randomPosition;
            spawnedLight.transform.position = lightPosition;

            StartCoroutine(waitAndRelokate(rnd.Next(10, 15), spawnedObject, spawnedLight));
        }
    }


}
