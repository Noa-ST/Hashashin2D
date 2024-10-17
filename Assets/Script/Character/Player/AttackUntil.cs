using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUntil : MonoBehaviour
{
    PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float attackDamage = _playerController.PlayerStats.damageAttackUntil;
        _playerController.PerformAttack(collision, attackDamage);
    }
}
