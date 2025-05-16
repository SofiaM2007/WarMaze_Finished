using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_enemy : MonoBehaviour
{
    private gameController g;

    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip enemieDied;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))  // Проверка на столкновение с пулей
        {
            Destroy(collision.gameObject); // Удаляем пулю
            Destroy(gameObject);  // Удаляем текущий объект (врага)
            
            g = FindObjectOfType<gameController>();
            g.num_of_enemies_killed++;
            // звук смерти врага
            audioSourceForSoundEffects.clip = enemieDied;
            audioSourceForSoundEffects.Play();
        }
    }
}
