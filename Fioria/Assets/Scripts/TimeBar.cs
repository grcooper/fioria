using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour {

    public float barDisplay; //current progress
    public float maxDisplay = 5f;
    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture2D emptyTex;
    public Texture2D fullTex;

    float prevTime;

    void OnGUI()
    {
        pos = Camera.main.gameObject.transform.position;
        pos.x = 0;
        
        size.x = Screen.width + 1;
        //draw the background:
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * (barDisplay / maxDisplay), size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Start() {
        prevTime = Time.realtimeSinceStartup;
        barDisplay = maxDisplay;
    }

    void Update()
    {
        //for this example, the bar display is linked to the current time,
        //however you would set this value based on your desired display
        //eg, the loading progress, the player's health, or whatever.
        float curTime = Time.realtimeSinceStartup;
        float deltaTime = curTime - prevTime;
        prevTime = curTime;

        
        if(PlayerScript.isTimeStopped) {
            barDisplay -= deltaTime;
        }
        else {
            barDisplay += deltaTime;
        }
        if(barDisplay < 0f) {
            barDisplay = 0f;
        }
        else if(barDisplay > maxDisplay) {
            barDisplay = maxDisplay;
        }
        //        barDisplay = MyControlScript.staticHealth;
    }
}
