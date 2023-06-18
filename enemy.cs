using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class enemy : MonoBehaviour
{
    public float speed = 10f;
    public float stopAt = 5f;
    public float health = 10;
    public Text healthText;
    public int reduceControl = 5;
    public int timeInBtw = 1;

    public bool towardPlayer;

    public GameObject hitEffect;
    public healthbar hb;

    public GameObject egg;

    private Transform target;

    public string Enemytype;
    public int scoreRewarded;

    public Shooter forShooters;
    bool Attacking = false;
    float timeLeft;
    private Vector2 movement;
    public float accelerationTime = 2f;
    public Rigidbody2D rb;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthText.text = health.ToString();

        hb.SetMaxHealth(health);
    }
    private void Update()
    {
        #region Movement

        if(towardPlayer)
        {
            #region TowardPlayerMovement
            if (Vector2.Distance(transform.position, target.position) > stopAt && Attacking == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else
            {
                switch (Enemytype)
                {
                    case "normal":
                        StartCoroutine(damagePlayer());
                        break;
                    case "shooter":
                        StartCoroutine(Shoot());
                        break;
                }
                Attacking = false;
            }
            #endregion
        }
        else
        {
            #region RandomMovement

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                timeLeft += accelerationTime;
            }

            if (Vector2.Distance(transform.position, target.position) > stopAt && Attacking == false)
            {
                rb.AddForce(movement * speed);
            }
            else
            {
                switch (Enemytype)
                {
                    case "normal":
                        StartCoroutine(damagePlayer());
                        break;
                    case "shooter":
                        StartCoroutine(Shoot());
                        break;
                }
                Attacking = false;
            }

            #endregion
        }

        #endregion

    }
    public void TakeDamage(int damage)
    {
        if(health > 1)
        {
            health -= damage;
            GameObject.Find("Main Camera").GetComponent<CameraShake>().shakeDuration = 0.5f;
            GameObject.Find("Main Camera").GetComponent<CameraShake>().shakeAmount = 0.1f;
            healthText.text = health.ToString();
            hb.SetHealth(health);
        }
        else if(health <= 0)
        {
            die();
        }
    }
    IEnumerator damagePlayer()
    {
        yield return new WaitForSeconds(timeInBtw);
        target.GetComponent<PlayerMovement>().takeDamage(reduceControl);
        StopAllCoroutines();
    }
    void die()
    {
        Destroy(gameObject);

        GameObject.Find("Manager").GetComponent<stopwatch>().AddScore(scoreRewarded, transform.position);

        Instantiate(egg, transform.position, Quaternion.identity);
        GameObject hitpf = Instantiate(hitEffect, transform.position, Quaternion.identity);

        Destroy(hitpf, .5f);
        GameObject.Find("Main Camera").GetComponent<CameraShake>().shakeDuration = 0.6f;
        GameObject.Find("Main Camera").GetComponent<CameraShake>().shakeAmount = 0.5f;
    }
    #region Enemy_types
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(timeInBtw);
        GameObject bullet = Instantiate(forShooters.pfbullet, forShooters.FirePoint.position, forShooters.FirePoint.rotation);

        Vector2 shootTowards = GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position;

        bullet.GetComponent<EnemyBullet>().damage = forShooters.damage;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.AddForce(shootTowards * forShooters.bulletForce, ForceMode2D.Impulse);
        Attacking = true;
        StopAllCoroutines();
    }
    #endregion
}
