using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStats : Stats
{
    [Header("Base Stats:")]
    public float hp;


    public override bool IsMaxLevel()
    {
        return false;
    }

    public override void Load()
    {
    }

    public override void Save()
    {
    }

    public override void Upgrade(Action OnSuccess = null, Action OnFalled = null)
    {
    }
}
