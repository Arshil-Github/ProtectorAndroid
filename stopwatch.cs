using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stopwatch : MonoBehaviour
{
    public Text Scoretext;
    public int score;
    public int highscore;
    public GameObject Popup;

    public int coins;

    public Text lostpanel_score;
    public Text lostpanel_highscore;
    public Text coinText;
    private void Start()
    {
        score = 0;
        Scoretext.text = score.ToString();

        if(PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        coinText.text = coins.ToString();

        if(PlayerPrefs.HasKey("HighScore"))
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }
    }
    private void Update()
    {
        lostpanel_highscore.text = highscore.ToString();
        lostpanel_score.text = score.ToString();
        coinText.text = coins.ToString();
    }
    public void AddScore(int i, Vector3 position)
    {
        GameObject a = Instantiate(Popup, position, Quaternion.identity);
        a.GetComponent<TextMeshPro>().text = i.ToString();

        score += i;
        Scoretext.GetComponent<Animator>().SetTrigger("ScaleUp");
        Scoretext.text = score.ToString();

        if(score > highscore)
        {
            Debug.Log("Yeah");
            highscore = score;
            PlayerPrefs.SetInt("HighScore", highscore);
        }
    }
}
