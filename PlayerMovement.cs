using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float walkSpeed;
    private float curSpeed;
    public int maxControl;
    public int control = 100;

    public float timeForReduce = 1f;
    public float speed;
    public float agility;
    public int Changeto1;
    public int Changeto2;
    public int timeEnraged;
    Rigidbody2D rb;
    Shooting shootScript;

    public healthbar controlBar;
    public AudioSource coinCollect;
    public bool MobileControl = false;

    public Joystick moveJoystick;
    public Joystick attackJoystick;
    void Start()
    {
        walkSpeed = (float)(speed + (agility / 5));
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootScript = gameObject.GetComponent<Shooting>();

        control = maxControl;

        controlBar.SetMaxHealth(maxControl);
    }

    public void PlayCoinAudio()
    {
        coinCollect.Play();
    }
    bool isEnragedNow = false;
    private void Update()
    {
        ChangeRotation();
        if(control > 0 && !isEnragedNow)
        {
            StartCoroutine(ReduceControl());
            shootScript.Enraged = false;
        }
        else if(control <= 0 && !isEnragedNow)
        {
            // control == 1 Enraged = true
            isEnragedNow = true;
            shootScript.Enraged = true;
            StartCoroutine(returntoNormal());
        }else if(control > 0 && isEnragedNow)
        {
            StartCoroutine(returntoNormal());
        }

        if (controlBar != null)
        {
            controlBar.SetHealth(control);
        }
        
    }
    void FixedUpdate()
    {
        curSpeed = walkSpeed;

        // Move senteces
        rb.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * curSpeed, 0.8f),
                                             Mathf.Lerp(0, Input.GetAxis("Vertical") * curSpeed, 0.8f));

        rb.velocity = new Vector2(Mathf.Lerp(0, moveJoystick.Horizontal * curSpeed, 0.8f),
                                             Mathf.Lerp(0, moveJoystick.Vertical * curSpeed, 0.8f));
    }


    void ChangeRotation()
    {
        if(!MobileControl)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            var x = attackJoystick.Horizontal;
            var y = attackJoystick.Vertical;
            if (x != 0.0 || y != 0.0)
            {
                var angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
            }
        }


    }
    IEnumerator ReduceControl()
    {
        yield return new WaitForSeconds(timeForReduce);
        takeDamage(1);
        StopAllCoroutines();
    }
    IEnumerator returntoNormal()
    {
        yield return new WaitForSeconds(timeEnraged);
        isEnragedNow = false;
        control = maxControl;
        shootScript.shootType = 0;
        shootScript.Enraged = false;
    }
    public void die()
    {

    }
    public void takeDamage(int damage)
    {
        control -= damage;

        if (control < Changeto1 && control > Changeto2)
        {
            shootScript.shootType = 1;
        }
        else if (control < Changeto2)
        {
            shootScript.shootType = 2;
        }
    }
}
