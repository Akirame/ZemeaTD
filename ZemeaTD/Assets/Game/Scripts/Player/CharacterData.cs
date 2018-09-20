﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Data", menuName = "CharacterData/DataTest", order = 1)]
public class CharacterData : ScriptableObject
{
    public float floorSpeed;
    public float airSpeed;
    public float attackSpeed;
    public float jumpForce;
    public int minDamage;
    public int maxDamage;
}