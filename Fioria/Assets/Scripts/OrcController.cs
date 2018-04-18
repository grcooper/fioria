using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcController : EnemyController
{
    public int moveCD = 3;
    float currentMoveTimer;
    int moveState = 0;

    // Use this for initialization
    public override void Start () {
        maxHealth = 16;
        base.Start();
        currentMoveTimer = moveCD;
	}
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
		if(currentHealth <= 0) {
            Destroy(gameObject);
        }

        Move();
    }

    public override void Move() {
        Vector2 movement_vector = new Vector2();
        switch (moveState) {
            case 0:
                movement_vector = new Vector2(0, 1);
                break;
            case 1:
                movement_vector = new Vector2(1, 0);
                break;
            case 2:
                movement_vector = new Vector2(0, -1);
                break;
            case 3:
                movement_vector = new Vector2(-1, 0);
                break;
        }
        currentMoveTimer -= Time.deltaTime;
        if (currentMoveTimer <= 0) {
            currentMoveTimer = moveCD;
            anim.SetInteger("state", 2);
            moveState = (moveState + 1) % 4;
        }
        else {
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
            anim.SetInteger("state", 1);
            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);
        }
    }
}
