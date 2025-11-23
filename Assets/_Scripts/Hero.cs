using UnityEngine;

public class Hero : MonoBehaviour
{
    private const string RunningAnimParam = "isRunning";
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const float DeadZone = 0.1f;

    [SerializeField] private Animator _animator;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;

    private SmoothRotator _smoothRotator = new SmoothRotator();
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
        Vector3 normalizedInput = input.normalized;

        if (input.magnitude <= DeadZone)
        {
            _animator.SetBool(RunningAnimParam, false);
            return;
        }

        _animator.SetBool(RunningAnimParam, true);
        _mover.ProcessCharacterMoveTo(normalizedInput, _speed);
        _smoothRotator.ProcessRotateTo(transform, _rotateSpeed, normalizedInput);
    }
}
