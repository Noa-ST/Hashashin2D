using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyWalk _enemyWalk;

    private void Awake()
    {
        _enemyWalk = GetComponentInParent<EnemyWalk>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float attackDamage = _enemyWalk.EnemyStat.damage;
        _enemyWalk.PerformAttack(collision, attackDamage);
    }
}
