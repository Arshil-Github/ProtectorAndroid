using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerups : MonoBehaviour
{
    GameObject GreenBall;
    [Header("Shield")]
    public int shieldPowerUps;
    public Text shieldCount;

    [Header("DestroyEnemies")]
    public int destoyPowerUps;
    public Text DestroyCount;
    private void Start()
    {
        GreenBall = GameObject.FindGameObjectWithTag("Green");
        shieldCount.text = shieldPowerUps.ToString();
        DestroyCount.text = destoyPowerUps.ToString();

        if(PlayerPrefs.HasKey("ShieldCount"))
        {
            shieldPowerUps = PlayerPrefs.GetInt("ShieldCount");
        }
        shieldCount.text = shieldPowerUps.ToString();

    }
    public void CheckforShieldPowerUp()
    {
        if(shieldPowerUps != 0)
        {
            shieldPowerUps -= 1;
            shieldCount.text = shieldPowerUps.ToString();
            PlayerPrefs.SetInt("ShieldCount", shieldPowerUps);
            GreenBall.GetComponent<gbMovement>().Enable_PowerUp_Shield();
        }
    }
    public void CheckforDestroyPowerUp()
    {
        if (destoyPowerUps != 0)
        {
            destoyPowerUps -= 1;
            DestroyCount.text = destoyPowerUps.ToString();

            GameObject[] e = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject g in e)
            {
                Destroy(g);
            }

        }
    }
}
