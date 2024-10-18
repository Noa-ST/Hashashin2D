using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int _minBonus;
    [SerializeField] private int _maxBonus;
    protected int _bonus;
    protected PlayerController _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (_player != null)
        {
            _bonus = Random.Range(_minBonus, _maxBonus) * _player.PlayerStats.level;
        }
    }
}

