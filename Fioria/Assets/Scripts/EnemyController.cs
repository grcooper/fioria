using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int maxHealth;
    public int speed;
    public int attackSpeed;

    public Animator anim;
    public Rigidbody2D rbody;

    public int currentHealth;

	// Use this for initialization
	public virtual void Start () {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public virtual void Move(){

    }

    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;
    }
}

