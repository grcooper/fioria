using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public int MaxHealth = 10;
    int curHealth = 10;

    Rigidbody2D rbody;
    Animator anim;

    public static bool isTimeStopped;

    public bool isUsingArrow = true;

    List<Combo.Key> curCombo = new List<Combo.Key>();
    List<Combo> comboInventory = new List<Combo>();

    public Object projectile;
    public float projectileDelay = 0.3f;
    float projectileTimer;
    bool launchedProjectile = false;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHealth = MaxHealth;
        isTimeStopped = false;
        projectileTimer = projectileDelay;


        // Defaulting a list of combos, will have to make a combo class after a while
        List<Combo.Key> combo1 = new List<Combo.Key>();
        combo1.Add(Combo.Key.UP);
        combo1.Add(Combo.Key.DOWN);
        List<Combo.Key> combo2 = new List<Combo.Key>();
        combo2.Add(Combo.Key.UP);
        combo2.Add(Combo.Key.RIGHT);
        combo2.Add(Combo.Key.DOWN);
        combo2.Add(Combo.Key.LEFT);

        comboInventory.Add(new Combo("updown", combo1));
        comboInventory.Add(new Combo("spin", combo2));
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        bool isAttacking = false;

        if (Input.GetKeyDown("space")) {
            if (isTimeStopped) {
                isTimeStopped = false;
                Time.timeScale = 1;
                ComboMatches();
                curCombo = new List<Combo.Key>();
            }
            else {
                Time.timeScale = 0;
                isTimeStopped = true;
            }
        }

        if (!isTimeStopped) {
            if(launchedProjectile) {
                projectileTimer -= Time.deltaTime;
            }
            if(projectileTimer <= 0f) {
                launchedProjectile = false;
                Instantiate(projectile, rbody.transform.position, Quaternion.identity);
                projectileTimer = projectileDelay;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                anim.SetInteger("state", 2);
                anim.SetFloat("attack_x", 0);
                anim.SetFloat("attack_y", 1);
                isAttacking = true;
                if (isUsingArrow) {
                    launchedProjectile = true;
                    projectileTimer = projectileDelay;
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                anim.SetInteger("state", 2);
                anim.SetFloat("attack_x", 0);
                anim.SetFloat("attack_y", -1);
                isAttacking = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                anim.SetInteger("state", 2);
                anim.SetFloat("attack_x", -1);
                anim.SetFloat("attack_y", 0);
                isAttacking = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                anim.SetInteger("state", 2);
                anim.SetFloat("attack_x", 1);
                anim.SetFloat("attack_y", 0);
                isAttacking = true;
            }


            // Currently, can only attack if you are not moving, makes sense?
            if (movementVector != Vector2.zero) {
                if (!isAttacking) anim.SetInteger("state", 1);
                anim.SetFloat("input_x", movementVector.x);
                anim.SetFloat("input_y", movementVector.y);
            }
            else if (!isAttacking) {
                anim.SetInteger("state", 0);
            }
            rbody.MovePosition(rbody.position + movementVector * Time.deltaTime);
        }
        else {
            // Combo's Go in here
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                curCombo.Add(Combo.Key.UP);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                curCombo.Add(Combo.Key.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                curCombo.Add(Combo.Key.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                curCombo.Add(Combo.Key.RIGHT);
            }
        }
	}

    bool ComboMatches()
    {
        foreach(Combo combo in comboInventory) {
            if (combo.Equal(curCombo)) {
                Debug.Log("Found matching combo");
                return true;
            }
        }
        return false;
    }
}
