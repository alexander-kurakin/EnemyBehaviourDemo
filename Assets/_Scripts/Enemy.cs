using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float InteractRadius = 2.5f;

    [SerializeField] private ParticleSystem _destroyEffect;

    private IBehaviour _idleBehaviour;
    private IBehaviour _reactionBehaviour;
    private IBehaviour _currentBehaviour;

    private Hero _collidedHero;
    private Color _currentGizmosColor;
    private Color _transparentRed = new Color(1f, 0f, 0f, 0.25f);
    private Color _transparentGreen = new Color(0f, 1f, 0f, 0.25f);

    private bool _isInitialized = false;


    public void Initialize(IBehaviour idleBehaviour, IBehaviour reactionBehaviour)
    {
        _currentGizmosColor = _transparentRed;
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
        {
            _collidedHero = hero;
            SwitchBehaviour(_reactionBehaviour);
            _currentGizmosColor = _transparentGreen;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Hero>(out Hero hero))
        {
            _currentGizmosColor = _transparentRed;
            SwitchBehaviour(_idleBehaviour);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _currentGizmosColor;
        Gizmos.DrawSphere(transform.position, InteractRadius);
    }

    private void SwitchBehaviour(IBehaviour behaviour)
    { 
        _currentBehaviour.Exit();
        _currentBehaviour = behaviour;
        _currentBehaviour.Enter();
    }

    public void DestroyEnemy()
    {
        Instantiate(_destroyEffect, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }

    public Hero GetCollidedHero()
    {
        return _collidedHero;
    }
}
