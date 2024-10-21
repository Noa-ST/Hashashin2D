using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : ActorVisual
{
    private PlayerController _player;
    private PlayerStats _playeStats;

    private void Start()
    {
        _player = (PlayerController)_actor;
        _playeStats = _player.PlayerStats;
    }

    public override void OnTakeDamage()
    {
        base.OnTakeDamage();
        GUIManager.Ins.UpdateHpInfo(_actor.CurHp, _actor.statData.hp);
    }

    public void OnDead()
    {
        GUIManager.Ins.ShowGameoverDialog();
    }

    public void OnAddXp()
    {
        if (_playeStats == null) return;
        GUIManager.Ins.UpdateLevelInfo(_playeStats.level, _playeStats.xp, _playeStats.levelUpXpRequired);
    }
}
