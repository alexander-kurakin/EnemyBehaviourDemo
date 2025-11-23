using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChaoticMovementBehaviour : IBehaviour
{
    private const float Speed = 0.5f;
    private const float PointsSpawnRadius = 10f;
    private const float ArenaRadius = 8.5f;
    private const float ChaoticRunTime = 1f;
    private const float EscapeRunTime = 1f;

    private Mover _mover;
    private Transform _centralPointTransform;
    private Vector3 _currentDirection;

    private float _chaoticTimer;
    private float _escapeTimer;

    public ChaoticMovementBehaviour(Mover mover, Transform centralPointTransform)
    {
        _mover = mover;
        _centralPointTransform = centralPointTransform;
    }

    public void Enter()
    {
        _currentDirection = GetNextTarget();
        _mover.ProcessTranslatedMoveTo(_currentDirection, Speed);

        _chaoticTimer = ChaoticRunTime;
    }

    public void Exit()
    {
    }

    public void Update()
    {
        ProcessTimers();

        ResetChaoticPosition();

        ValidateOutOfBoundaries();

        ProcessMovement();
    }

    private void ProcessTimers()
    {
        _chaoticTimer -= Time.deltaTime;
        _escapeTimer -= Time.deltaTime;
    }

    private void ValidateOutOfBoundaries()
    {
        Vector3 directionFromCentralPoint = _mover.transform.position - _centralPointTransform.position;

        if (directionFromCentralPoint.magnitude > ArenaRadius)
            _escapeTimer = EscapeRunTime;
    }

    private void ProcessMovement()
    {
        if (_escapeTimer > 0f)
        {
            Vector3 directionToCentralPoint = _centralPointTransform.position - _mover.transform.position;
            _mover.ProcessTranslatedMoveTo(directionToCentralPoint, Speed);
        }
        else
        {
            _mover.ProcessTranslatedMoveTo(_currentDirection, Speed);
        }
    }

    private void ResetChaoticPosition()
    {
        if (_chaoticTimer <= 0f)
        {
            _currentDirection = GetNextTarget();
            _chaoticTimer = ChaoticRunTime;
        }
    }

    private Vector3 GetNextTarget()
    {
        Vector3 result = new Vector3(Random.Range(-PointsSpawnRadius, PointsSpawnRadius), 0, Random.Range(-PointsSpawnRadius, PointsSpawnRadius));
                
        return result;
    }
}
