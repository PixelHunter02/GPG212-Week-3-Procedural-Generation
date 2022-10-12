using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemiesAtSpawn : MonoBehaviour
{
    private float timer = 0.5f;
    private void Update() {
        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
