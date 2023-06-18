using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float Health;
    public float bulletDamage;
    
    float DecreaseAlphaBy;
    SpriteRenderer sp;

    private void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        if(Health <= 0)
        {
            DelayedDeath();
        }
    }
    public void DelayedDeath()
    {
        StartCoroutine(Die());
    }
    public IEnumerator Die()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
