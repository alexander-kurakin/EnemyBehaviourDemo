using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunawayBehaviour : IBehaviour
{
    private const float Speed = 1f;

    private Mover _mover;
    private Enemy _enemy;
    private Hero _hero;

    private Vector3 _currentTarget;
    

    public RunawayBehaviour(Enemy enemy, Mover mover)
    {
        _mover = mover;
        _enemy = enemy;
    }
    public void Enter()
    {
        _hero = _enemy.GetCollidedHero();
        _currentTarget = _hero.transform.position;
    }

    public void Exit()
    {
        _currentTarget = Vector3.zero;
    }

    public void Update()
    {
        _currentTarget = _hero.transform.position;

        Vector3 direction = new Vector3(_mover.transform.position.x - _currentTarget.x, 0, _mover.transform.position.z - _currentTarget.z);

        _mover.ProcessTranslatedMoveTo(direction, Speed);
    }
}
