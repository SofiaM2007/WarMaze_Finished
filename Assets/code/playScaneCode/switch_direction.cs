using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_direction : MonoBehaviour
{

    private eat_grow eatGrow;
    void Start()
    {
        eatGrow = FindObjectOfType<eat_grow>();
       //eatGrow.warmSegments
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        int count = eatGrow.warmSegments.Count;
    
        for (int i = 0; i < count / 2; i++)
        {
            int oppositeIndex = count - 1 - i;

            Vector3 tempPosition = eatGrow.warmSegments[i].position;
            eatGrow.warmSegments[i].position = eatGrow.warmSegments[oppositeIndex].position;
            eatGrow.warmSegments[oppositeIndex].position = tempPosition;
        }
    }
}
