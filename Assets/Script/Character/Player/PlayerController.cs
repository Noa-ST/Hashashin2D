using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : Actor
{
    [Header("Player Setting:")]
    public float runSpeed;
    public float jumpForce;
    public float wallSlideSpeed = -1f;
    Vector2 _moveInput;
    TouchingDirections _touching;
    PlayerStats _playerStats;


    [Header("Player Event:")]
    public UnityEvent OnAddXp;
    public UnityEvent OnLevelUp; 

    public bool IsMoving { get; private set; }

    public bool isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            if (isFacingRight != value)
                transform.localScale *= new Vector2(-1, 1);  // Lật hướng của nhân vật
            isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return _amin.GetBool(AminConts.CAN_MOVE);
        }
    }

    public PlayerStats PlayerStats { get => _playerStats; set => _playerStats = value; }

    public override void Init()
    {
        LoadStats();
    }

    private void LoadStats()
    {
        if (statData == null) return;

        _playerStats = (PlayerStats)statData; 
        _playerStats.Load(); 
        CurHp = _playerStats.hp; 
    }

    protected override void Awake()
    {
        base.Awake();
        _touching = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            if (_touching.IsOnWall && !_touching.IsGround)
            {
                _rb.velocity = new Vector2(0, Mathf.Max(wallSlideSpeed, _rb.velocity.y));
            }
            else
            {
                _rb.velocity = new Vector2(_moveInput.x * runSpeed * Time.fixedDeltaTime, _rb.velocity.y);
            }

            _amin.SetBool(AminConts.PLAYER_RUN_ANIMATION, IsMoving);
        }
        else
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        _amin.SetFloat(AminConts.PLAYER_ISAIR, _rb.velocity.y);
    }

    // Phương thức xử lý di chuyển
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        IsMoving = _moveInput != Vector2.zero;
        SetFacingDirection(_moveInput);
    }

    // Đặt hướng của nhân vật dựa trên hướng di chuyển
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    // Phương thức xử lý nhảy
    public void OnJump(InputAction.CallbackContext context)
    {
        // Kiểm tra nếu bấm nhảy và nhân vật đang ở trên mặt đất
        if (context.started && _touching.IsGround)
        {
            _amin.SetTrigger(AminConts.PLAYER_JUMP_ANIMATION);
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    // Các phương thức tấn công
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _amin.SetTrigger(AminConts.PLAYER_ATTACK_ANIMATION);
        }
    }

    public void OnAttackSkill2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _amin.SetTrigger(AminConts.PLAYER_ATTACK2_ANIMATION);
        }
    }

    public void OnAttackSkill3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _amin.SetTrigger(AminConts.PLAYER_ATTACK3_ANIMATION);
        }
    }

    public void OnAttackSkillAir(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _amin.SetTrigger(AminConts.PLAYER_ATTACKAIR_ANIMATION);
        }
    }

    public void OnAttackSkillUntil(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _amin.SetTrigger(AminConts.PLAYER_ATTACKUNTIL_ANIMATION);
        }
    }

    public void AddXp(float xpBonus)
    {
        if (PlayerStats == null) return;

       _playerStats.xp += xpBonus;
        _playerStats.Upgrade(OnUpgradeStats);
        OnAddXp?.Invoke();
        _playerStats.Save();
    }

    private void OnUpgradeStats()
    {
        OnLevelUp?.Invoke();
    }

    public override void Takedamage(float damage)
    {
        base.Takedamage(damage);
        _amin.SetTrigger(AminConts.PLAYER_TAKE_HIT);
        OnTakeDamage?.Invoke();
        if (CurHp <= 0)
        {
            _amin.SetTrigger(AminConts.PLAYER_DEATH);
        }
    }

    public void PerformAttack(Collider2D collision, float attackDamage)
    {
        EnemyWalk damageAble = collision.GetComponent<EnemyWalk>();
        if (damageAble != null)
        {
             damageAble.Takedamage(attackDamage);
        }
    }
}
