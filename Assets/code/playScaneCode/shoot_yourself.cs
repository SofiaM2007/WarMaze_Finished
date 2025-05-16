using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_yourself : MonoBehaviour
{

    private eat_grow eatGrow;

     void Start()
    {
        // Получаем ссылку на компонент eat_grow
        eatGrow = FindObjectOfType<eat_grow>();
        if (eatGrow == null)
        {
            Debug.LogError("Не найден объект с компонентом eat_grow!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet")) 
        {
            Destroy(collision.gameObject); // Удаляем пулю

            if (eatGrow != null && eatGrow.warmSegments.Count > 1)// Удаляем последний сегмент змеи
            {
                Transform lastSegment = eatGrow.warmSegments[eatGrow.warmSegments.Count - 1];
                Destroy(lastSegment.gameObject);
                eatGrow.warmSegments.RemoveAt(eatGrow.warmSegments.Count - 1);
            }

        }
    }
}
