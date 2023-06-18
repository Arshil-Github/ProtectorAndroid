using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCombine : MonoBehaviour
{
    public GameObject combinedEnemy;
    public GameObject spawnEffect;

    bool Spawned = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<RedCombine>() != null && Spawned == false)
        {
            Spawned = true;
            collision.gameObject.GetComponent<RedCombine>().Spawned = true;

            Destroy(gameObject);
            Destroy(collision.gameObject);

            Instantiate(combinedEnemy, transform.position, Quaternion.identity);
        }
    }
}
