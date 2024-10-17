using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : ActorVisual
{
    private PlayerController _player;
    private PlayerStats _playeStats;
    private Animator _amin;

    private void Start()
    {
        _player = (PlayerController)_actor;
        _playeStats = _player.PlayerStats;
        _amin = GetComponent<Animator>();
    }

    public void OnDead()
    {


        //AudioController.Ins.PlaySound(AudioController.Ins.playerDeath);
        //GUIManager.Ins.ShowGameoverDialog();
    }
}
