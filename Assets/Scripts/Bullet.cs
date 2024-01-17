using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    public int bulletDamage=25;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Alien"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);

            Destroy(gameObject);
        }
    }
}
