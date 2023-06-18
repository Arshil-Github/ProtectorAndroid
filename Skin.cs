using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skin : MonoBehaviour
{
    public string name;
    public Color color;

    public int cost;
    public GameObject Purchased;

    public Sprite sprite;

    private void Start()
    {
        bool isPurchased = false;
        //Check whether skin array contains the givin skin name
        for (int i = 0; i < GlobalObject.Instance.skins.Count; i++)
        {
            if (GlobalObject.Instance.skins[i] == name)
            {
                isPurchased = true;
                break;
            }
        }
        if (isPurchased)
        {
            Purchased.SetActive(true);
        }
    }
}
