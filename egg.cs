using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class egg : MonoBehaviour
{
    public float timer = 5;
    float a;
    public int addControl = 5;
    public GameObject pf_enemy;
    public healthbar bar;

    public int Min_coinsRewarded;
    public int Max_coinsRewarded;
    public GameObject coinPopup;


    public AudioSource explode;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy());
        bar.SetMaxHealth(timer);
        a = timer;
        explode.Play();
    }

    IEnumerator spawnEnemy()
    {
        StartCoroutine(displaytimer());
        yield return new WaitForSeconds(timer);

        Instantiate(pf_enemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().control += addControl;

            int c = Random.Range(Min_coinsRewarded, Max_coinsRewarded);

            collision.GetComponent<PlayerMovement>().PlayCoinAudio();

            GameObject a = Instantiate(coinPopup, transform.position, Quaternion.identity);
            a.GetComponent<TextMeshPro>().text = c.ToString();

            GameObject.Find("Manager").GetComponent<stopwatch>().coins += c;
            PlayerPrefs.SetInt("Coins", GameObject.Find("Manager").GetComponent<stopwatch>().coins);
            Destroy(gameObject);
        }
    }
    IEnumerator displaytimer()
    {
        if(timer != 0)
        {
            yield return new WaitForSeconds(1);
            a -= 1;
            bar.SetHealth(a);
            StartCoroutine(displaytimer());
        }
    }
}
