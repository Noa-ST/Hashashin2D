using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pref
{
    public static int coins
    {
        set => PlayerPrefs.SetInt(PrefConst.COIN_KEY, value);
        get => PlayerPrefs.GetInt(PrefConst.COIN_KEY, 0);
    }

    public static string playerData
    {
        set => PlayerPrefs.SetString(PrefConst.PLAYER_DATA_KEY, value);
        get => PlayerPrefs.GetString(PrefConst.PLAYER_DATA_KEY);
    }

    public static string enemyData
    {
        set => PlayerPrefs.SetString(PrefConst.ENEMY_DATA_KEY, value);
        get => PlayerPrefs.GetString(PrefConst.ENEMY_DATA_KEY);
    }

    public static bool IsEnoughCoins(int coinToCheck)
    {
        return coins >= coinToCheck;
    }
}
