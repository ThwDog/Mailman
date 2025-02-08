using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StartChasing : MonoBehaviour
{
    public enum chase
    {
        start , finish
    }
    private EnemySystem enemy;
    public chase _chase; 

    private void Awake() 
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemySystem>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<PlayerMovement>() && _chase == chase.start)
        {
            enemy.setchasing();
        }    
        else if(other.GetComponent<PlayerMovement>() && _chase == chase.finish)
        {
            enemy.stopchasing();
        }
    }
}
