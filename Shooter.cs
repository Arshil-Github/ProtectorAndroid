using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shooter
{
    public Transform FirePoint;
    public GameObject pfbullet;

    public float bulletForce = 20f;
    public int damage = 2;
}
