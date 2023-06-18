using System.Collections;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public float minTime;
    public float maxTime;

    public GameObject pf_enemy;

    private void Start()
    {
        StartCoroutine(spawnEnemy());
    }
    IEnumerator spawnEnemy()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        Instantiate(pf_enemy, transform.position, Quaternion.identity);

        StartCoroutine(spawnEnemy());
    }
}
