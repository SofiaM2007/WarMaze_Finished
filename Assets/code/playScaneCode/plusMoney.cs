using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusMoney : MonoBehaviour
{
    private gameController g;
    private MazeVisibility MazeVisibility;
    private output output;
    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip pikManey;
    void Start()
    {
        g = FindObjectOfType<gameController>();
        MazeVisibility=FindObjectOfType<MazeVisibility>();
        output=FindObjectOfType<output>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("manetka(Clone)"))  // Проверка на столкновение с монетнокй
        {
            Destroy(other.gameObject); // Удаляем монетку
            g.plus_Moeny++;
            output.setMoneyText(g.plus_Moeny);

            audioSourceForSoundEffects.clip = pikManey;
            audioSourceForSoundEffects.Play();
            // сюда звук монетки
        }

    }
}
