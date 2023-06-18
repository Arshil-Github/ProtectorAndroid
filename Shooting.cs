using Bolt;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject pfbullet;

    public float bulletForce = 20f;
    public int shootType = 0;
    public bool Enraged = false;
    public float timeInBtw = 0.1f;
    public int damage = 2;
    public int timeBetweenHoldShoot = 5;

    private int Private_timeBetweenHoldShoot;

    public string bulletType = "normal";

    public GameObject[] postprocessing;

    public AudioSource sfx_shoot;
    public Joystick attackJoystick;
    private void Start()
    {
        Private_timeBetweenHoldShoot = timeBetweenHoldShoot;
    }
    void Update()
    {
        #region Shooting
        if (!Enraged && Private_timeBetweenHoldShoot == 0)
        {
            if(attackJoystick.Horizontal != 0 || attackJoystick.Vertical != 0)
            {
                Private_timeBetweenHoldShoot = timeBetweenHoldShoot;
                switch (shootType)
                {
                    case 0:
                        Shoot();
                        Debug.Log("Shoot");
                        sfx_shoot.Play();
                        break;
                    case 1:
                        Shoot1();
                        sfx_shoot.Play();
                        break;
                    case 2:
                        Shoot2();
                        sfx_shoot.Play();
                        break;
                }
            }
        }

        if(Private_timeBetweenHoldShoot != 0)
        {
            Private_timeBetweenHoldShoot -= 1;
        }
        #endregion

        #region Enraging
        if (Enraged)
        {
            StartCoroutine(enrage());
            postprocessing[0].SetActive(false);
            postprocessing[1].SetActive(true);
        }
        else
        {
            postprocessing[1].SetActive(false);
            postprocessing[0].SetActive(true);
        }
        #endregion

    }
    void Shoot()
    {
        GameObject bullet = Instantiate(pfbullet, FirePoint.position, FirePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);

        CheckForBullet(bullet);
    }
    public void Shoot1()
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(pfbullet, FirePoint.position, FirePoint.rotation);
            CheckForBullet(bullet);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            switch(i)
            {
                case 0:
                    rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
                    break;
                case 1:
                    rb.AddForce(-FirePoint.up * bulletForce, ForceMode2D.Impulse);
                    break;
                case 2:
                    rb.rotation += 90;
                    rb.AddForce(FirePoint.right * bulletForce, ForceMode2D.Impulse);
                    break;
                case 3:
                    rb.rotation += 90;
                    rb.AddForce(-FirePoint.right * bulletForce, ForceMode2D.Impulse);
                    break;
            }
        }
    }
    public void Shoot2()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(pfbullet, FirePoint.position, FirePoint.rotation);
            CheckForBullet(bullet);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            switch (i)
            {
                case 0:
                    rb.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);
                    break;
                case 1:
                    rb.AddForce(-FirePoint.up * bulletForce, ForceMode2D.Impulse);
                    break;
                case 2:
                    rb.rotation += 90;
                    rb.AddForce(FirePoint.right * bulletForce, ForceMode2D.Impulse);
                    break;
                case 3:
                    rb.rotation += 90;
                    rb.AddForce(-FirePoint.right * bulletForce, ForceMode2D.Impulse);
                    break;
                case 4:
                    rb.rotation += 135;
                    rb.AddForce((FirePoint.right + FirePoint.up) * bulletForce, ForceMode2D.Impulse);
                    break;
                case 5:
                    rb.rotation += 45;
                    rb.AddForce((FirePoint.right - FirePoint.up) * bulletForce, ForceMode2D.Impulse);
                    break;
                case 6:
                    rb.rotation -= 135;
                    rb.AddForce((-FirePoint.right + FirePoint.up) * bulletForce, ForceMode2D.Impulse);
                    break;
                case 7:
                    rb.rotation -= 45;
                    rb.AddForce((-FirePoint.right - FirePoint.up) * bulletForce, ForceMode2D.Impulse);
                    break;
            }
        }
    }

    IEnumerator enrage()
    {
        yield return new WaitForSeconds(timeInBtw);
        Shoot2();
        StopAllCoroutines();
    }
    void CheckForBullet(GameObject bullet)
    {
        if (bullet.GetComponent<Bullet>() != null && bulletType == "normal")
        {
            bullet.GetComponent<Bullet>().damage = damage;
        }
        else if (bullet.GetComponent<Bullet_UI>() != null && bulletType == "ui")
        {
            bullet.GetComponent<Bullet_UI>().damage = damage;
        }
        else if (bullet.GetComponent<Bullet_Rico>() != null && bulletType == "rico")
        {
            bullet.GetComponent<Bullet_Rico>().damage = damage;
        }
    }
}
