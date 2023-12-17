using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChasing : MonoBehaviour
{
    private EnemySystem enemy;

    private void Awake() 
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemySystem>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<PlayerMovement>())
        {
            enemy.setchasing();
        }    
    }
}
