using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName = "Nora/TDS/Create Enemy Stats")]
public class EnemyStat : ActionStats
{
    [Header("Xp Bonus:")]
    public float minXpBonus;
    public float maxXpBonus;

    [Header("Attack:")]
    public int damage;


    public override void Load()
    {
        if (!string.IsNullOrEmpty(Pref.enemyData))
        {
            JsonUtility.FromJsonOverwrite(Pref.enemyData, this);
        }
    }

    public override void Save()
    {
        Pref.enemyData = JsonUtility.ToJson(this);
    }
}
