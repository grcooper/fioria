using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {
    public int dmg = 5;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            other.gameObject.SendMessage("TakeDamage", dmg);
        }
    }
}
