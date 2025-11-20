using UnityEngine;

public class Enemy : MonoBehaviour
{
    private IBehaviour _idleBehaviour;
    private IBehaviour _reactionBehaviour;

    private IBehaviour _currentBehaviour;

    private bool _isInitialized = false;

    public void Initialize(IBehaviour idleBehaviour, IBehaviour reactionBehaviour)
    {
        _idleBehaviour = idleBehaviour;
        _reactionBehaviour = reactionBehaviour;

        _currentBehaviour = _idleBehaviour;
        
        _currentBehaviour.Enter();
        _isInitialized = true;
    }

    private void Update()
    {
        if (_isInitialized) 
            _currentBehaviour.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Hero>(out Hero hero))
            SwitchBehaviour(_reactionBehaviour);
        else
            SwitchBehaviour(_idleBehaviour);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, 2.5f);
    }

    private void SwitchBehaviour(IBehaviour behaviour)
    { 
        _currentBehaviour.Exit();
        _currentBehaviour = behaviour;
        _currentBehaviour.Enter();
    }
}
