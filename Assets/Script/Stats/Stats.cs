using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : ScriptableObject
{
    public abstract void Save();
    public abstract void Load();
    public abstract void Upgrade(Action OnSuccess = null, Action OnFalled = null);
    public abstract bool IsMaxLevel();
}
