using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxGenerator : MonoBehaviour
{

    public GameObject[] spawnableBlocks;
    public int TimeBtwSpawns;

    public int minBlockSpawn;
    public int maxBlockSpawn;

    int spawns = 0;
    bool NoSpawn = false;

    private void Update()
    {
        if(!NoSpawn)
        {
            NoSpawn = true;

            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Breakable"))
            {
                g.GetComponent<Breakable>().DelayedDeath() ;
            }

            int i = Random.Range(minBlockSpawn, maxBlockSpawn);
            while(spawns < i)
            {
                StartCoroutine(SpawnObstacles());
                i = i - 1;
            }
        }
    }
    IEnumerator SpawnObstacles()
    {
        #region RandomisePosition
        float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        #endregion

        #region RandomiseSpawn

        int i = Random.Range(0, spawnableBlocks.Length);
        float Rotation = Random.Range(0, 360);
        #endregion

        GameObject Block = Instantiate(spawnableBlocks[i], spawnPosition, Quaternion.Euler(0f, 0f, Rotation));

        yield return new WaitForSeconds(TimeBtwSpawns);
        
        NoSpawn = false;
    }
}
