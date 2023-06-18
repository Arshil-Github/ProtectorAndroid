using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainMenuManager : MonoBehaviour
{
    public GameObject LeavePanel;
    public TextMeshPro coinText;
    public int coin;

    public int cost_Shield;
    public int cost_Destroy;

    public TextMeshPro ShieldNumber;
    public TextMeshPro DestroyNumber;

    
    private void Start()
    {
        if(PlayerPrefs.HasKey("Coins"))
        {
            coin = PlayerPrefs.GetInt("Coins");
        }
        coinText.text = coin.ToString();
        ShieldNumber.text = PlayerPrefs.GetInt("ShieldCount").ToString();
    }

    #region Shop
    
    public void Shop_Shield()
    {
        if(coin > cost_Shield)
        {
            coin -= cost_Shield;
            coinText.text = coin.ToString();
            PlayerPrefs.SetInt("Coins", coin);
            PlayerPrefs.SetInt("ShieldCount", PlayerPrefs.GetInt("ShieldCount") + 1);

            ShieldNumber.text = PlayerPrefs.GetInt("ShieldCount").ToString();
        }
        
    }
    public void Shop_Destroy()
    {
        if (coin > cost_Destroy)
        {
            coin -= cost_Destroy;
            coinText.text = coin.ToString();
            PlayerPrefs.SetInt("Coins", coin);
            PlayerPrefs.SetInt("DestroyCount", PlayerPrefs.GetInt("DestroyCount") + 1);

            DestroyNumber.text = PlayerPrefs.GetInt("DestroyCount").ToString();
        }

    }
    public void ChangeSkin(Skin s)
    {
        bool isPurchased = false;
        //Check whether skin array contains the givin skin name
        for(int i = 0; i < GlobalObject.Instance.skins.Count; i++)
        {
            if(GlobalObject.Instance.skins[i] == s.name)
            {
                isPurchased = true;
                break;
            }    
        }

        
        if (!isPurchased)
        {
            if (coin > s.cost)
            {
                coin -= s.cost;
                coinText.text = coin.ToString();
                PlayerPrefs.SetInt("Coins", coin);

                GlobalObject.Instance.skins.Add(s.name);
                GlobalObject.Instance.playerColor = s.color;
                GlobalObject.Instance.playerSprite = s.sprite;
                s.Purchased.SetActive(true);
            }
        }else if(isPurchased)
        {
            GlobalObject.Instance.playerColor = s.color;
            GlobalObject.Instance.playerSprite = s.sprite;
            GlobalObject.Instance.skins.Add(s.name);
        }
    }
    #endregion

    #region Exit
    public void Exit()
    {
        LeavePanel.SetActive(true);
    }
    public void Yes_ExitPanel()
    {
        Application.Quit();
    }
    public void No_ExitPanel()
    {
        LeavePanel.SetActive(false);
    }
    #endregion

    #region Setting
    
    public void Volume_Up()
    {

    }
    public void Volume_Down()
    {
    }

    #endregion

    #region Play
    public void Start_Survival()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(1);
    }
    #endregion

    private void Update()
    {
    }
}
