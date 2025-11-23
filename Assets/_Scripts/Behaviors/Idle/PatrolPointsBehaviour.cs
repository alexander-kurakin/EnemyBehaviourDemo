using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsBehaviour : IBehaviour
{
    private const float PatrolSpeed = 1.2f;
    private const float MinProximityDistance = 0.5f;

    private List<Transform> _patrolPoints;
    private Mover _mover;
    private Transform _currentTarget;

    public PatrolPointsBehaviour(Mover mover, List<Transform> patrolPoints)
    {
        _patrolPoints = patrolPoints;
        _mover = mover;
    }

    public void Enter()
    {
        _currentTarget = GetNextRandomPoint();
    }

    public void Exit()
    {
    }

    public void Update()
    {
        Vector3 direction = _currentTarget.position - _mover.GetMovingObjectTransform().position;
        float distance = direction.magnitude;
        
        ValidateTargetReached(distance);

        _mover.ProcessTranslatedMoveTo(direction, PatrolSpeed);
    }

    private void ValidateTargetReached(float distance)
    {
        if (distance <= MinProximityDistance)
            _currentTarget = GetNextRandomPoint();
    }

    private Transform GetNextRandomPoint()
    {
        return _patrolPoints[Random.Range(0, _patrolPoints.Count)];
    }
}
