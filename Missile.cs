using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject blastEffect;

    Rigidbody2D rb;
    Transform player;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        //MOve Towards Player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        ChangeRotation();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        //Check for WHat collided
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().takeDamage(damage);
        }else if (collision.gameObject.CompareTag("Green"))
        {
            collision.gameObject.GetComponent<gbMovement>().takeDamage(damage);
        }else if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<spawner>() == null)
        {
            collision.gameObject.GetComponent<enemy>().TakeDamage(damage);
        }

        if(collision.gameObject.GetComponent<spawner>() == null)
        {
            //Explode
            Destroy(gameObject, 0.05f);
        }
    }
    void ChangeRotation()
    {
        Vector3 diff = player.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
