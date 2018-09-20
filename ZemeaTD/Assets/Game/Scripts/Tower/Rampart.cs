﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampart : MonoBehaviour
{
    private int health;
    private SpriteRenderer rend;
    private BoxCollider2D coll;

	void Start ()
    {
        health = 100;
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
	}
    private void Update()
    {
        if (IsAlive())
        {            
        }
        else
        {
            coll.enabled = false;
            Color c = new Vector4(0, 0, 0, 0);
            rend.color = c;
        }
    }
    public void Attacked(int damage)
    {
        health -= damage;        
        rend.color = Color.red;        
    }
    private bool IsAlive()
    {
        if (health > 0)
            return true;
        else
            return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!collision.GetComponent<EnemyRanged>())
            {
                Attacked(collision.GetComponent<Enemy>().damage);                
            }
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Attacked(collision.GetComponent<Bullet>().GetDamage());            
        }
    }
}