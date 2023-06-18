using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetComponent<PlayerMovement>() != null)
        {
            collision.GetComponent<PlayerMovement>().takeDamage(1);
        }
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            GameObject hitpf = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitpf, .5f);
            Destroy(gameObject);
        }
    }
}
