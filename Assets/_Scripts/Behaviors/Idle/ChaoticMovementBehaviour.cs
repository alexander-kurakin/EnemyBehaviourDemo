using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChaoticMovementBehaviour : IBehaviour
{
    private Mover _mover;
    private Transform _centralPointTransform;
    private Vector3 _currentDirection;

    private float _speed = 0.5f;
    private float _pointsSpawnRadius = 10f;
    private float _arenaRadius = 8.5f;
    private float _timer;
    private bool _isEscapingBoundary;

    public ChaoticMovementBehaviour(Mover mover, Transform centralPointTransform)
    {
        _mover = mover;
        _centralPointTransform = centralPointTransform;
    }


    public void Enter()
    {
        _currentDirection = GetNextTarget();
        _mover.ProcessTranslatedMoveTo(_currentDirection, _speed);

        _timer = 1f;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            if (_isEscapingBoundary == false)
                _currentDirection = GetNextTarget();
            else 
                _isEscapingBoundary = false;
            
            _timer = 1f;
        }

        if ((_mover.GetMovingObjectTransform().position - _centralPointTransform.position).magnitude > _arenaRadius)
        {
            _timer = 2f;
            _mover.ProcessTranslatedMoveTo(_centralPointTransform.position - _mover.GetMovingObjectTransform().position, _speed);
            _isEscapingBoundary = true;
        }
        else
            _mover.ProcessTranslatedMoveTo(_currentDirection, _speed);
    }

    private Vector3 GetNextTarget()
    {
        Vector3 result = new Vector3(Random.Range(-_pointsSpawnRadius, _pointsSpawnRadius), 0, Random.Range(-_pointsSpawnRadius, _pointsSpawnRadius));
                
        return result;
    }
}
