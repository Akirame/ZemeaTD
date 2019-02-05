﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rampart : MonoBehaviour
{
    public float shield = 200;
    public int maxShield = 200;
    public float healthPerSecond;
    public Image shieldBar;
    public ParticleSystem[] shieldParticles;
    private CapsuleCollider2D coll;
    private Animator anim;
    private bool activateCollision = false;
    private bool canBeHurt = true;

	void Start ()
    {        
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        Item.InvulnerableConsume += ShieldInvulnerable;
	}

    private void OnDestroy()
    {
        Item.InvulnerableConsume -= ShieldInvulnerable;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {            
            Attacked(50);
        }
        shieldBar.fillAmount = (float)shield / maxShield;
    }
    public void Attacked(int damage)
    {
        if(canBeHurt)
        {
            shield -= damage;
            CheckAlive();
        }
    }

    public bool IsAlive()
    {
        return (shield > 0);
    }

    IEnumerator CanBeHurtOff()
    {
        canBeHurt = !canBeHurt;
        anim.SetBool("invulnerable", !canBeHurt);
        yield return new WaitForSeconds(10);
        canBeHurt = !canBeHurt;
        anim.SetBool("invulnerable", !canBeHurt);
    }

    public void ShieldInvulnerable(Item i)
    {
        StartCoroutine(CanBeHurtOff());
    }

    private void CheckAlive() {
        if(IsAlive()) {
            if (activateCollision)
            {
                coll.enabled = true;
                activateCollision = !activateCollision;
                foreach (ParticleSystem p in shieldParticles)
                {
                    p.Play(true);
                }
            }
        }
        else {
            if (!activateCollision)
            {
                coll.enabled = false;
                activateCollision = !activateCollision;
                foreach(ParticleSystem p in shieldParticles) {
                    p.Stop(true);
                }
            }
        }
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
            Attacked(collision.GetComponent<EnemyBullet>().GetDamage());
        }
    }

    public void RepairAll()
    {
        shield = maxShield;
        CheckAlive();
    }

    public void RepairRampart(int multiplier) {
        if(shield < maxShield) {
            shield += healthPerSecond * Time.deltaTime * multiplier;
            CheckAlive();
        }
    }
}
