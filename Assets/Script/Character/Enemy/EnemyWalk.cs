using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : Actor
{
    public float walkSpeed;
    public float walkStopRate;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    TouchingDirections _touchingDirections;
    EnemyStat _enemyStat;
    float m_xpBonus;

    public override void Init()
    {
        if (statData == null) return;
        _enemyStat = (EnemyStat)statData;
        _enemyStat.Load();
        CurHp = _enemyStat.hp;
        OnDead.AddListener(OnSpawnColectable); 
        OnDead.AddListener(OnAddXpToPlayer);
    }

    public enum WalkAbleDirection
    {
        Left, Right
    }

    WalkAbleDirection _walkDirection = WalkAbleDirection.Right;
    Vector2 walkDirectionVector;

    public WalkAbleDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                if (value == WalkAbleDirection.Right)
                {
                    gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
                    walkDirectionVector = Vector2.right; 
                }
                else if (value == WalkAbleDirection.Left)
                {
                    gameObject.transform.localScale = new Vector2(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
                    walkDirectionVector = Vector2.left; 
                }
            }

            _walkDirection = value;
        }
    }


    public bool hasTarget = false;

    public bool HasTarget
    {
        get { return hasTarget; }
        private set
        {
            hasTarget = value;
            _amin.SetBool(AminConts.ENEMY_HASTARGET, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return _amin.GetBool(AminConts.CAN_MOVE);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return _amin.GetFloat(AminConts.ATTACK_COOLDOWN);
        }
        private set
        {
            _amin.SetFloat(AminConts.ATTACK_COOLDOWN, Mathf.Max(value, 0));
        }
    }

    public EnemyStat EnemyStat { get => _enemyStat; set => _enemyStat = value; }

    protected override void Awake()
    {
        base.Awake();
        _touchingDirections = GetComponent<TouchingDirections>();
        WalkDirection = _walkDirection;
        walkDirectionVector = Vector2.right;
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_touchingDirections.IsGround && _touchingDirections.IsOnWall && !HasTarget)
        {
            FlipDirection();
        }

        if (CanMove)
            _rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(Mathf.Lerp(_rb.velocity.x, 0, walkStopRate), _rb.velocity.y);
    }

    private void FlipDirection()
    {
        WalkDirection = (WalkDirection == WalkAbleDirection.Right) ? WalkAbleDirection.Left : WalkAbleDirection.Right;
    }


    public void OnCliffDetected()
    {
        if(_touchingDirections.IsGround)
        {
            FlipDirection();
        }
    }

    public override void Takedamage(float damage)
    {
        base.Takedamage(damage);
        _amin.SetTrigger(AminConts.ENEMY_TAKE_HIT);
        if (CurHp <= 0)
        {
            _amin.SetTrigger(AminConts.DEALTH_ENEMY);
        }
    }

    public void PerformAttack(Collider2D collision, float attackDamage)
    {
        PlayerController damageAble = collision.GetComponent<PlayerController>();
        if (damageAble != null)
        {
            damageAble.Takedamage(attackDamage);
        }
    }

    private void OnAddXpToPlayer()
    {
        // Thêm XP cho người chơi
    }

    private void OnSpawnColectable()
    {
        // Sinh ra vật phẩm tại vị trí của kẻ thù
    }

    private void OnDisable()
    {
        OnDead.RemoveListener(OnSpawnColectable); 
        OnDead.RemoveListener(OnAddXpToPlayer); 
    }
}
