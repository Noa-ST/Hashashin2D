using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D cashFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    PolygonCollider2D _touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    Animator _amin;

    [SerializeField] private bool _isGrounded;
    public bool IsGround
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            _amin.SetBool(AminConts.PLAYER_ISGROUND, value);
        }
    }

    [SerializeField] private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            _amin.SetBool(AminConts.PLAYER_ISWALL, value);
        }
    }

    [SerializeField] private bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            _amin.SetBool(AminConts.PLAYER_ISCEILNG, value);
        }
    }

    private void Awake()
    {
        _touchingCol = GetComponent<PolygonCollider2D>();
        _amin = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGround = _touchingCol.Cast(Vector2.down, cashFilter, groundHits, groundDistance) > 0;
        IsOnWall = _touchingCol.Cast(wallCheckDirection, cashFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = _touchingCol.Cast(Vector2.up, cashFilter, ceilingHits, ceilingDistance) > 0;
    }
}
