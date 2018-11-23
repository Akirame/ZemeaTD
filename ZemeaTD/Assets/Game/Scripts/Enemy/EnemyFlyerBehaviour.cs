﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerBehaviour : MonoBehaviour
{        
    public float maxSpeedUpDown = 4f;
    public float minSpeedUpDown = 2f;
    public float maxDistanceUpDown = 10f;
    public float minDistanceUpDown = 2f;
    private float distanceUpDown;
    private float speedUpDown;
    private float centerPos;
    public Balloon[] balloonsGroup;

    private void Start()
    {       
        centerPos = Random.Range(15f, 50f);
        distanceUpDown = Random.Range(minDistanceUpDown, maxDistanceUpDown);
        speedUpDown = Random.Range(minSpeedUpDown, maxSpeedUpDown);
        for(int i = 0; i < balloonsGroup.Length; i++)
            balloonsGroup[i].onDeath += BalloonDestroyed;
    }
    private void Update()
    {
        if(balloonsGroup.Length == 0)
            gameObject.GetComponent<Enemy>().Kill();
    }
    private void LateUpdate()
    {        
        Vector3 mov = new Vector3(transform.position.x,  centerPos + Mathf.Sin(speedUpDown * Time.time) * distanceUpDown, transform.position.z);
        transform.position = mov;
    }
    private void BalloonDestroyed(Balloon b)
    {
        for(int i = 0; i < balloonsGroup.Length; i++)
            if(balloonsGroup[i] == b)
            {
                Destroy(balloonsGroup[i].gameObject);
                break;
            }
        Debug.Log("holi");
    }
}
