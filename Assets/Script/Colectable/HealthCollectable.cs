using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là người chơi
        if (collision.CompareTag("Player"))
        {
            if (_player == null) return;  // Kiểm tra nếu _player không null
            _player.CurHp += _bonus;  // Thêm máu cho người chơi
            _player.CurHp = Mathf.Clamp(_player.CurHp, 0, _player.PlayerStats.hp);  // Giới hạn HP trong khoảng 0 - max HP

            // GUIManager.Ins.UpdateHpInfo(_player.CurHp, _player.PlayerStats.hp);  // Cập nhật GUI, nếu cần

            // Hủy vật phẩm sau khi đã được thu thập
            Destroy(gameObject);
        }
    }
}

