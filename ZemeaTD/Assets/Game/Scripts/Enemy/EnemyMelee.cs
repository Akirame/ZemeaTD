﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public float timeToAttack;
    private float timer;
    private bool isAttacking;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        timer = 0;
    }
    private void Update()
    {
        if (rampart)
            if (rampart.IsAlive())
            {
                timer += Time.deltaTime;
                if (timer >= timeToAttack)
                {
                    rampart.Attacked(damage);
                    CameraShake.GetInstance().Shake(0.02f, 0.01f);
                    timer = 0;
                }
            }
            else
            {
                rampart = null;
                movementBehaviour.SetCanMove(true);
            }
    }

    public void EnemyBossShake()
    {
        CameraShake.GetInstance().Shake(0.02f, 0.05f);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = collision.GetComponent<Rampart>();
            movementBehaviour.SetCanMove(false);
            anim.enabled = false;
        }
    }
}
