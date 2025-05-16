using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emenyFire : MonoBehaviour
{
    private GameObject objectEvilBullet;
    private float speed = 10f;  // Скорость полета дубликата

    private GameObject duplicatedObject;

    void Start()
    {
        objectEvilBullet=GameObject.Find("evilBullet");

        StartCoroutine(CallEveryFiveSeconds());
    }

    IEnumerator CallEveryFiveSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if(GetComponent<SpriteRenderer>().enabled){
                DuplicateAndLaunch();
            }
        }
    }

    void Update(){
        if(duplicatedObject!=null){
            if(GetComponent<SpriteRenderer>().enabled){
                duplicatedObject.GetComponent<SpriteRenderer>().enabled=true;
            }
            else{
                duplicatedObject.GetComponent<SpriteRenderer>().enabled=false;
            }
        }
    }

     private void DuplicateAndLaunch()
    {
        
        duplicatedObject = Instantiate(objectEvilBullet, transform.position, Quaternion.identity);

        Rigidbody2D rb = duplicatedObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = duplicatedObject.AddComponent<Rigidbody2D>();
        }

        duplicatedObject.tag = "evilBullet"; 

        rb.gravityScale = 0;
        rb.velocity = transform.up * speed;

        if (transform.up != Vector3.zero)
        {
            float angle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg-90.0f;
            rb.rotation = angle;
        }

        Collider2D enemyCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = duplicatedObject.GetComponent<Collider2D>();

        if (enemyCollider != null && bulletCollider != null)
        {
            bulletCollider.enabled = false;

            Physics2D.IgnoreCollision(enemyCollider, bulletCollider);

            StartCoroutine(EnableColliderAfterDelay(bulletCollider));
        }

    }

     private IEnumerator EnableColliderAfterDelay(Collider2D collider) // ВЕРОЯТНЫЙ ИСТОЧНИК ОШИБОК
    {
        yield return new WaitForSeconds(0.1f);

        if (collider != null)
        {
            collider.enabled = true;
        }
    }
}
