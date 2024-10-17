using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{
    [Header("Common")]
    public ActionStats statData;
    private bool _isDead; 
    private float _curHp;
    protected Rigidbody2D _rb; 
    protected Animator _amin;

    [Header("Event:")]
    public UnityEvent OnInit; 
    public UnityEvent OnTakeDamage; 
    public UnityEvent OnDead;

    public bool IsDead { get => _isDead; set => _isDead = value; }

    public float CurHp
    {
        get => _curHp;
        set => _curHp = value;
    }

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _amin = GetComponentInChildren<Animator>(); 
    }

    protected virtual void Start()
    {
        Init(); 
        OnInit?.Invoke(); 
    }

    public virtual void Init()
    {
    }

    public virtual void Takedamage(float damage)
    {
        if (damage < 0 ) return; 

        _curHp -= damage;

        CharacterEvents.characterDamaged?.Invoke(gameObject, damage);
        if (_curHp <= 0) 
        {
            _isDead = true;
            _rb.velocity = Vector3.zero;
            OnDead?.Invoke();
            Destroy(gameObject, 0.5f);
        }
        OnTakeDamage?.Invoke();
    }
}
