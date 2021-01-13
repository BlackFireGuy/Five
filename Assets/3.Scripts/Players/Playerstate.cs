using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstate : MonoBehaviour
{

    public static Playerstate instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(this);
        }
    }

    [Header("PlayerState")]
    public string playerName, description;

    public int playerLevel, maxLevel;

    public int currentExp;

    public int[] nextlevelExp;//

    public float currentHp, currentMp, maxHp, maxMp;
    public float attack, defense;

    private void Start()
    {
        nextlevelExp = new int[maxLevel+1];
        nextlevelExp[1] = 1000;
        for (int i = 2; i < maxLevel; i++)
        {
            nextlevelExp[i] = Mathf.RoundToInt(nextlevelExp[i - 1] * 1.1f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddExp(600);
        }
    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        if (playerLevel < maxLevel&&currentExp >= nextlevelExp[playerLevel])
        {
            levelUp();
        }
        if(playerLevel >= maxLevel)
        {
            currentExp = 0;
        }
    }

    private void levelUp()
    {
        currentExp -= nextlevelExp[playerLevel];
        playerLevel++;
        maxHp  =Mathf.RoundToInt(200 + maxHp * 1.2f);
        currentHp = maxHp;
        maxMp += 20;
        currentMp = maxMp;
        attack = Mathf.CeilToInt(attack * 1.1f);//取上线最小整数7.8取8
        defense = Mathf.CeilToInt(defense * 1.05f);
    }
}
