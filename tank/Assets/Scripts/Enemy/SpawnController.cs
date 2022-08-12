using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject enemy;
    public int xPos;
    public int zPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        if (enemy != null)
        {
            while (enemyCount < 9)
            {
                xPos = Random.Range(-110, -12);
                zPos = Random.Range(-10, -150);
                //Enemy initialization
                //Quaternion.identity is This quaternion corresponds to "no rotation" - the object is perfectly aligned with the world or parent axes.
                Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
                //is a method of Unity that delays 5 Coroutine with an interval in seconds
                yield return new WaitForSeconds(5f);
                enemyCount += 1;
            }
        }else
        {
            DontDestroyOnLoad(enemy);
        }
           
    }
}
