using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class gbMovement : MonoBehaviour
{
    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;

    public int health = 20;
    public PlayableDirector loseCinemachine;

    public healthbar hb;
    Rigidbody2D rb;

    public string MovementType;

    [Header("PowerUps")]
    public bool shield = false;
    public float shieldTime;
    public GameObject Shield;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hb.SetMaxHealth(health);
    }
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
        if (health <= 0)
        {
            Destroyed();
            Debug.Log("Played");
        }
    }
    public void takeDamage(int damage)
    {
        if(!shield)
        {
            health -= damage;
            hb.SetHealth(health);
        }
    }
    public void Destroyed()
    {
        loseCinemachine.Play();
    }


    #region Powerups
    public void Enable_PowerUp_Shield()
    {
        shield = true;
        Shield.SetActive(true);
        StartCoroutine(Disable_PowerUp_Shield());
    }
    IEnumerator Disable_PowerUp_Shield()
    {
        yield return new WaitForSeconds(shieldTime);
        shield = false;
        Shield.SetActive(false);
        StopAllCoroutines();
    }
    #endregion
}
