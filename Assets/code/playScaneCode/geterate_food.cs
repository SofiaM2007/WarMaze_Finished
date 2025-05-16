using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_food : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private GameObject mazeObject;
    [SerializeField] private int spawnCount; // Количество объектов для генерации
    private gameController g;

    public void Start()
    {
        StartCoroutine(WaitASecond());

        g = FindObjectOfType<gameController>();
        spawnCount = g.num_of_food;

        objectToSpawn=GameObject.Find("food");
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(0.01f);

        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("Object to spawn is null!");
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
                ky=rnd.Next(1, 11);
            } while (g.maze[kx, ky] == 1);


            float cellSizeX = worldSize.x / 12f;
            float cellSizeY = worldSize.y / 12f;

            float worldPosX = worldMin.x + (kx + 0.5f) * cellSizeX;
            float worldPosY = worldMin.y + (ky + 0.5f) * cellSizeY;



            Vector2 randomPosition = new Vector2(worldPosX, worldPosY);

            GameObject spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);

            CopyObjectProperties(objectToSpawn, spawnedObject);
        }
    }

    // Метод для получения границ объекта
    private Bounds GetObjectBounds(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds;
        }

        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds;
        }

        Debug.LogError("Object does not have a Renderer or Collider to calculate bounds!");
        return new Bounds(obj.transform.position, Vector3.zero);
    }

    // Метод для копирования компонентов и свойств
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
}
