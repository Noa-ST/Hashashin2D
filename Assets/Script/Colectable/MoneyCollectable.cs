using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyCollectable : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pref.coins += _bonus;
            //GUIManager.Ins.UpdateCoinsCounting(Pref.coins);
            Destroy(gameObject);
        }   
    }
}
