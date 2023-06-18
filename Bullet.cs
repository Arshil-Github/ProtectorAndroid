using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Green"))
        {
            collision.GetComponent<gbMovement>().takeDamage(1);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<enemy>().TakeDamage(damage);
        }

        if(collision.gameObject.CompareTag("Breakable"))
        {
            collision.GetComponent<Breakable>().Health -= damage;
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
            GameObject hitpf = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitpf, .5f);
            Destroy(gameObject);
        }
    }
}
