using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsBehaviour : IBehaviour
{
    private List<Transform> _patrolPoints;
    private Mover _mover;

    private Transform _currentTarget;

    private float _patrolSpeed = 1.2f;
    private float _minCheckDistance = 0.5f;

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
        _patrolPoints.Clear();
    }

    public void Update()
    {
        Vector3 direction = _currentTarget.position - _mover.GetMovingObjectTransform().position;
        float simpleDistance = direction.magnitude;
        
        ValidateTargetReached(simpleDistance);

        _mover.ProcessTranslatedMoveTo(direction, _patrolSpeed);
    }

    private void ValidateTargetReached(float distance)
    {
        if (distance <= _minCheckDistance)
            _currentTarget = GetNextRandomPoint();
    }

    private Transform GetNextRandomPoint()
    {
        return _patrolPoints[Random.Range(0, _patrolPoints.Count)];
    }
}
