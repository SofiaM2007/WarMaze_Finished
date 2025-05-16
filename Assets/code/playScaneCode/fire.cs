using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject objectToDuplicate;  // обьект пули для дублирования
    [SerializeField] private float speed = 10f;  // Скорость полета дубликата
    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip clipForNoBullets;
    private eat_grow eatGrow;

    void Start()
    {
        objectToDuplicate = GameObject.Find("bullet");

        eatGrow = FindObjectOfType<eat_grow>();
        if (eatGrow == null)
        {
            Debug.LogError("Не найден объект c компонентом eat_grow!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && eatGrow != null && eatGrow.warmSegments.Count > 1)  // Проверка нажатия пробела и то что хвост не коньчился ещё
        {
            DuplicateAndLaunch();
            RemoveLastWarmSegment();

            //сюда добавлять звук выстрела
        }
        else if(Input.GetMouseButtonDown(1) && eatGrow != null && eatGrow.warmSegments.Count <= 1){
            audioSourceForSoundEffects.clip = clipForNoBullets;
            audioSourceForSoundEffects.Play();
            
        }
        
    }

    private void RemoveLastWarmSegment()
    {
        Destroy(eatGrow.warmSegments[eatGrow.warmSegments.Count - 1].gameObject); 
        eatGrow.warmSegments.RemoveAt(eatGrow.warmSegments.Count - 1);
    }

    private void DuplicateAndLaunch()
    {
        
        GameObject duplicatedObject = Instantiate(objectToDuplicate, transform.position, Quaternion.identity);

        Rigidbody2D rb = duplicatedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = duplicatedObject.AddComponent<Rigidbody2D>();
        }

        rb.gravityScale = 0;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        rb.velocity = direction * speed;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90.0f;
            rb.rotation = angle;
        }

        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = duplicatedObject.GetComponent<Collider2D>();

        if (playerCollider != null && bulletCollider != null)
        {
            bulletCollider.enabled = false;

            Physics2D.IgnoreCollision(playerCollider, bulletCollider);

            StartCoroutine(EnableColliderAfterDelay(bulletCollider));
        }

    }

     private IEnumerator EnableColliderAfterDelay(Collider2D collider) // ВЕРОЯТНЫЙ ИСТОЧНИК ОШИБОК
    {
        // Ждем небольшую задержку
        yield return new WaitForSeconds(0.1f);

        // Включаем коллайдер обратно
        if (collider != null)
        {
            collider.enabled = true;
        }
    }

}
