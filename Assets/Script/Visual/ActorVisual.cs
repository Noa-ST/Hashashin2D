using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class ActorVisual : MonoBehaviour
{
    protected Actor _actor;

    protected virtual void Awake()
    {
        _actor = GetComponent<Actor>();
    }

    public virtual void OnTakeDamage()
    {

    }
}
