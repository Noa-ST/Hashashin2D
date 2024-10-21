using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_player == null) return;  
            _player.CurHp += _bonus;  
            _player.CurHp = Mathf.Clamp(_player.CurHp, 0, _player.PlayerStats.hp);
            GUIManager.Ins.UpdateHpInfo(_player.CurHp, _player.PlayerStats.hp);
            Destroy(gameObject);
        }
    }
}

