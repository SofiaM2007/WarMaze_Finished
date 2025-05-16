using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    private Transform targetTransform; // Голова
    private float offsetDistance;      // Расстояние до головы

    public void Initialize(Transform target, float offset)
    {
        targetTransform = target; // Устанавливаем голову
        offsetDistance = offset;  // расстояние до неё
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            //позиция за головой с учетом дистанции
            Vector3 targetPosition = targetTransform.position - targetTransform.forward * offsetDistance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, Time.deltaTime * 10f);
        }
    }
}
