using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTracker : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject closestEnemy;
    Color closetColor = Color.blue;
    Renderer rend;

    void Update()
    {
        CheckDistanceToEnemies(enemyList);      
    }

    public GameObject CheckDistanceToEnemies(List<GameObject> objectsToCheck)
    {
        float minDistance = float.MaxValue;

        foreach (GameObject enemy in objectsToCheck)
        {
            
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < minDistance)
            {
                closestEnemy = enemy;
                //rend = closestEnemy.GetComponent<Renderer>();
                //rend.material.color = closetColor;
                minDistance = distance;
            }
        }

        return closestEnemy;
    }
}
