using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrigger : MonoBehaviour
{
    public int Damage = 1;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(Damage);
        }
    }
}
