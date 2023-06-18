using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_UI : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag(gameObject.tag))
        {
            GameObject hitpf = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitpf, .5f);
            Destroy(gameObject);
        }
    }
}
