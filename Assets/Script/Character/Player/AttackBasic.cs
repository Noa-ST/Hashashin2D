using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBasic : MonoBehaviour
{
    PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float attackDamage = _playerController.PlayerStats.damageBasicAttack;
       _playerController.PerformAttack(collision, attackDamage);
    }
}
