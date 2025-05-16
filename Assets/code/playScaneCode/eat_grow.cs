using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat_grow : MonoBehaviour
{
    [SerializeField] private GameObject objectToDuplicate; //  сегмент тела
    [SerializeField] private float segmentSpacing = 0.5f; // Фиксированное расстояние между сегментами

    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip eatFood;

    public List<Transform> warmSegments = new List<Transform>(); // Сегменты тела

    private gameController g;

    private void Start()
    {
        g = FindObjectOfType<gameController>();
    }

    private void Awake()
    {
        warmSegments.Add(transform); // Голова — первый сегмент

        if (objectToDuplicate == null)
        {
            objectToDuplicate = GameObject.Find("warm_bady");
            if (objectToDuplicate == null)
            {
                Debug.LogError("ObjectToDuplicate не найден в сцене!");
            }
        }
    }

    private void Update()
    {

        /*for (int i = 1; i < warmSegments.Count; i++) //нормальный но как палка
        {
            Vector3 direction = warmSegments[i - 1].position - warmSegments[i].position;
            if (direction != Vector3.zero)
            {
                warmSegments[i].rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }
            //warmSegments[i].position = positions[Mathf.Min(i, positions.Count - 1)];
            warmSegments[i].position=warmSegments[i - 1].position - warmSegments[i - 1].up * segmentSpacing;
            //warmSegments[i].position = positions[Mathf.Min(i-1, positions.Count - 1)]-warmSegments[Mathf.Min(i-1, positions.Count - 1)].up * segmentSpacing;
            //warmSegments[i].position = positions[i- 1]- warmSegments[i - 1].up * segmentSpacing;
        }*/
        /*for (int i = 1; i < warmSegments.Count; i++) //прикольный
        {
            Vector3 direction = (warmSegments[i - 1].position - warmSegments[i].position).normalized;

            if (direction != Vector3.zero)
            {
                // Плавно изменяем угол поворота сегмента
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                warmSegments[i].rotation = Quaternion.Slerp(warmSegments[i].rotation, targetRotation, Time.deltaTime * 10f);
            }

            Vector3 targetPosition = warmSegments[i - 1].position - warmSegments[i - 1].up * segmentSpacing;
            warmSegments[i].position = Vector3.Lerp(warmSegments[i].position, targetPosition, Time.deltaTime * 10f);
        }*/

        /*for (int i = 1; i < warmSegments.Count; i++) //работающший но не очень
        {
            Transform previousSegment = warmSegments[i - 1];
            Transform nowSegment = warmSegments[i];

            Vector3 direction = (previousSegment.position - nowSegment.position).normalized;

            //nowSegment.position = previousSegment.position - direction * segmentSpacing;
            nowSegment.position = previousSegment.position - direction * segmentSpacing;
            if (direction != Vector3.zero)
            {
                nowSegment.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }
            //nowSegment.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }*/
        /*for (int i = 1; i < warmSegments.Count; i++) //работающший +-
        {
            Transform previousSegment = warmSegments[i - 1];
            Transform nowSegment = warmSegments[i];

            float distance = Vector3.Distance(nowSegment.position, previousSegment.position);
            

            if (distance > segmentSpacing)
            {
                Vector3 direction = (previousSegment.position - nowSegment.position).normalized;
                nowSegment.position += direction * (distance - segmentSpacing);
            }
            Vector3 direction1 = (previousSegment.position - nowSegment.position).normalized;
            nowSegment.rotation = Quaternion.LookRotation(Vector3.forward, direction1);
            //nowSegment.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }*/
        //работающший но всё ещё +-
        for (int i = 1; i < warmSegments.Count; i++) 
        {
            Transform previousSegment = warmSegments[i - 1];
            Transform nowSegment = warmSegments[i];

            float distance = Vector3.Distance(nowSegment.position, previousSegment.position);
            
            Vector3 direction = (previousSegment.position - nowSegment.position).normalized;
            nowSegment.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            if (distance > segmentSpacing)
            {
                nowSegment.position = previousSegment.position - direction * segmentSpacing;
            }
        }
        g.num_of_bullets_left = warmSegments.Count;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("food"))
        {
            Debug.Log("Съели еду!");
            audioSourceForSoundEffects.clip = eatFood;
            audioSourceForSoundEffects.Play();

            Vector3 newSegmentPosition = warmSegments[warmSegments.Count - 1].position - warmSegments[warmSegments.Count - 1].up * segmentSpacing;

            // Создаём новый сегмент
            GameObject newSegment = Instantiate(objectToDuplicate, newSegmentPosition, Quaternion.identity);
            SpriteRenderer sr = newSegment.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = -warmSegments.Count; // Каждый новый сегмент ниже предыдущего
            }

            newSegment.tag = "body"; 

            warmSegments.Add(newSegment.transform);

            // Удаляем еду
            Destroy(other.gameObject);
            g.num_of_food_eaten++;

        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat_grow : MonoBehaviour
{
    [SerializeField] private GameObject objectToDuplicate;
    [SerializeField] private float segmentSpacing = 0.5f;

    public List<Transform> warmSegments = new List<Transform>();

    private void Awake()
    {
        warmSegments.Add(this.transform);

        if (objectToDuplicate == null)
        {
            objectToDuplicate = GameObject.Find("warm_bady");
            if (objectToDuplicate == null)
            {
                Debug.LogError("ObjectToDuplicate не установлен и не найден в сцене!");
            }
        }
    }

    private void Update()
    {
        for (int i = 1; i < warmSegments.Count; i++)
        {
            Transform previousSegment = warmSegments[i - 1];
            Transform currentSegment = warmSegments[i];

            Vector3 direction = (previousSegment.position - currentSegment.position).normalized;

            currentSegment.position = previousSegment.position - direction * segmentSpacing;

            currentSegment.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("food"))
        {
            Debug.Log("Еда съедена!");

            Transform lastSegment = warmSegments[warmSegments.Count - 1];

            // Вычисляем позицию нового сегмента на фиксированном расстоянии от последнего сегмента
            Vector3 newPosition = lastSegment.position - lastSegment.up * segmentSpacing;
            Quaternion newRotation = lastSegment.rotation;

            GameObject newSegment = Instantiate(objectToDuplicate, newPosition, newRotation);

            SpriteRenderer sr = newSegment.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = -warmSegments.Count;
            }

            warmSegments.Add(newSegment.transform);

            Destroy(other.gameObject);
            Debug.Log("Сегмент добавлен!");
        }
    }
}*/





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eat_grow : MonoBehaviour
{
    [SerializeField] private GameObject objectToDuplicate; // Обьект для еды дублирования
    [SerializeField] private float segmentSpacing;  // Расстояние между сегментами тела

    public List<Transform> warmSegments = new List<Transform>(); // Список сегментов тела


    private void Awake()
    {
        warmSegments.Add(this.transform);
        segmentSpacing=0.5f;

        if (objectToDuplicate == null)
        {
            objectToDuplicate = GameObject.Find("warm_bady");
            if (objectToDuplicate == null)
            {
                Debug.LogError("ObjectToDuplicate не установлен и не найден в сцене (eat_grow Awake)!");
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("food")) 
        {
            //Debug.Log("Еду съели");

           Transform lastSegment = warmSegments[warmSegments.Count - 1];

            Vector3 newPosition;

            if (warmSegments.Count == 1)
            {
                Vector3 direction = -lastSegment.transform.up;

                 newPosition = lastSegment.position + direction * segmentSpacing;
            }
            else
            {
                Transform previousSegment = warmSegments[warmSegments.Count - 2];
                Vector3 direction = (lastSegment.position - previousSegment.position).normalized;

                newPosition = lastSegment.position + direction * segmentSpacing;
            }

            GameObject newSegment = Instantiate(objectToDuplicate, newPosition, lastSegment.rotation);

            // новый сегмент ниже остальных
            SpriteRenderer sr = newSegment.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = -warmSegments.Count; // Каждый новый сегмент ниже предыдущего
            }

            // Временное отключение коллайдеров для избежания отталкивания голов от туловища, то есть выкидывания вверх(ВЕРОЯТНО ОЧЕНЬ ПРОБЛЕМНОЕ МЕСТО)
            Collider2D newSegmentCollider = newSegment.GetComponent<Collider2D>();
            if (newSegmentCollider != null)
            {
                newSegmentCollider.enabled = false;
            }
            warmSegments.Add(newSegment.transform);

            // коллайдер с небольшой задержкой
            StartCoroutine(EnableColliderAfterDelay(newSegmentCollider));

            Destroy(other.gameObject);

            // Сюда можно добавить звук поедания

        }
    }

    private IEnumerator EnableColliderAfterDelay(Collider2D collider)
    {
        yield return new WaitForSeconds(0.1f);

        if (collider != null)
        {
            collider.enabled = true;
        }
    }


    private void Update()
    {
        for (int i = 1; i < warmSegments.Count; i++)
        {
            Transform currentSegment = warmSegments[i];
            Transform previousSegment = warmSegments[i - 1];

            Vector3 targetPosition = previousSegment.position;
            float distance = Vector3.Distance(currentSegment.position, targetPosition);

            if (distance > segmentSpacing)
            {
                Vector3 direction = (targetPosition - currentSegment.position).normalized;
                currentSegment.position += direction * (distance - segmentSpacing);
            }

            currentSegment.rotation = Quaternion.Lerp(currentSegment.rotation, previousSegment.rotation, Time.deltaTime * 10f);
        }
    }
  
}*/