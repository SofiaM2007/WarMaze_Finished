using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class killBoss : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textForBossHP;
    [SerializeField] public AudioSource audioSourceForSoundEffects; 
    [SerializeField] public AudioClip enemieDied;
    private gameController gameController;

    public Vector3 offset = new Vector3(0, 1.5f, 0);

    private int hp=0, max_hp=0;
    void Start()
    {
        gameController=FindObjectOfType<gameController>();
        

        hp=gameController.levels;
        max_hp=hp;

        textForBossHP.text = hp.ToString()+"/"+max_hp.ToString()+" hp";

    }

    

    void LateUpdate(){
        textForBossHP.transform.position = transform.position + offset;
        textForBossHP.transform.rotation = Quaternion.identity;

        if(!GetComponent<SpriteRenderer>().enabled) textForBossHP.enabled=false;
        else textForBossHP.enabled=true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))  // Проверка на столкновение с пулей
        {
            Destroy(collision.gameObject); // Удаляем пулю
            hp--;
            textForBossHP.text = hp.ToString()+"/"+max_hp.ToString()+" hp";
            if(hp==0){
                Destroy(gameObject); 
                gameController.num_of_enemies_killed++;

                // звук смерти врага
                audioSourceForSoundEffects.clip = enemieDied;
                audioSourceForSoundEffects.Play();
            }
        }
    }

   

    
}
