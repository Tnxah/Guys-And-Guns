using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;

    public string owner = "Test";

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        Destroy(gameObject);     
       // Destroy(collision.gameObject);       
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
