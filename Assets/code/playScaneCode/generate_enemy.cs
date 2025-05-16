using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class generate_enemy : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // Префаб объекта, который нужно создавать
    private GameObject objectOfBossEnemy;
    [SerializeField] private int spawnCount; // Количество объектов для генерации

    private gameController g;

    public void Start()
    {

        StartCoroutine(WaitASecond());

        g = FindObjectOfType<gameController>();
        spawnCount = g.num_of_enemy;
        objectToSpawn=GameObject.FindWithTag("enemy");
        objectOfBossEnemy=GameObject.Find("enemy_bos");
        
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(0.01f);
        SpawnObjects();
    }

    void Update()
    {
        
    }

    private void SpawnObjects()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn не обьявлён!");
        }

        if (g.mazeArrayForObjektGeneration[g.level_now] == null)
        {
            Debug.LogError("Maze object не обьявлён!");
        }

        
        System.Random rnd = new System.Random();
        Renderer mazeRenderer = g.mazeArrayForObjektGeneration[g.level_now].GetComponent<Renderer>();
        Vector3 worldSize = mazeRenderer.bounds.size;
        Vector3 worldMin = mazeRenderer.bounds.min;

        for (int i = 0; i < spawnCount; i++)
        {
            int kx, ky;
            do
            {
                kx = rnd.Next(1, 11);
                ky = rnd.Next(1, 11);
            } while (g.maze[kx, ky] != 0);

            float cellSizeX = worldSize.x / 12f;
            float cellSizeY = worldSize.y / 12f;

            float worldPosX = worldMin.x + (kx + 0.5f) * cellSizeX;
            float worldPosY = worldMin.y + (ky + 0.5f) * cellSizeY;

            Vector2 randomPosition = new Vector2(worldPosX, worldPosY);

            GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            CopyObjectProperties(objectToSpawn, spawnedObject);
        }
    }


    // для копирования компонентов и свойств
    private void CopyObjectProperties(GameObject original, GameObject clone)
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

    public void generateBoss(){
        System.Random rnd = new System.Random();
        Renderer mazeRenderer = g.mazeArrayForObjektGeneration[g.level_now].GetComponent<Renderer>();
        Vector3 worldSize = mazeRenderer.bounds.size;
        Vector3 worldMin = mazeRenderer.bounds.min;

        int kx, ky;
        do
        {
            kx = rnd.Next(1, 11);
            ky = rnd.Next(1, 11);
        } while (g.maze[kx, ky] != 0);

        float cellSizeX = worldSize.x / 12f;
        float cellSizeY = worldSize.y / 12f;

        float worldPosX = worldMin.x + (kx + 0.5f) * cellSizeX;
        float worldPosY = worldMin.y + (ky + 0.5f) * cellSizeY;

        Vector2 randomPosition = new Vector2(worldPosX, worldPosY);
        GameObject spawnedObject = Instantiate(objectOfBossEnemy, randomPosition, Quaternion.identity);
        CopyObjectProperties(objectOfBossEnemy, spawnedObject);
        
    }
}
