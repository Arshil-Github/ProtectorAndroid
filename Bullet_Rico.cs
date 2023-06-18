using UnityEngine;

public class Bullet_Rico : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage;
    public bool hasHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (hasHit == true && collision.gameObject.CompareTag("Walls"))
        {
            GameObject hitpf = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitpf, .5f);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            hasHit = true;
        }
        if (collision.gameObject.CompareTag("Green"))
        {
            collision.gameObject.GetComponent<gbMovement>().takeDamage(1);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<enemy>().TakeDamage(damage);
        }
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Walls"))
        {
            GameObject hitpf = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitpf, .5f);
            Destroy(gameObject);
        }
    }
}
