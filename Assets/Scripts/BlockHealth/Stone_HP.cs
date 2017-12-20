﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_HP : MonoBehaviour {

    public int state;

    int timer = 50;

    public Sprite normal;
    public Sprite littledmg;
    public Sprite middmg;
    public Sprite heavydmg;
    
    private GameObject bird;

    SpriteRenderer sp;

	// Use this for initialization
	void Start () {
        bird = GameObject.FindGameObjectWithTag("Bird");
        sp = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bird" && timer == 0)
        {
            state += 2;
            int amount = 300 + (int)Mathf.Round(bird.GetComponent<Rigidbody2D>().velocity.magnitude) * 200;
            ScoreController.screenPos = gameObject.transform.position;
            ScoreController.CreateText(amount.ToString(), transform);
            UI.score += amount;
        }
    }

    // Update is called once per frame
    void Update () {

        if (timer > 0)
        {
            timer -= 1;
        }

        if (timer == 0)
        {
            if (state == 0)
            {
                //No dmg
                sp.sprite = normal;
            }
            if (state == 1)
            {
                //little dmg
                sp.sprite = littledmg;
            }
            if(state == 2)
            {
                //mid dmg
                sp.sprite = middmg;
            }
            if (state == 3)
            {
                //heavely dmgd
                sp.sprite = heavydmg;
            }
            if (state >= 4)
            {
                //broken
                UI.score += 500;
                ScoreController.screenPos = gameObject.transform.position;
                ScoreController.CreateText("500", transform);
                ObjectPoolManager.instance.SpawnPoolObject("StoneBreakParticles", transform.position);
                Destroy(gameObject);
            }
        }
    }
}
