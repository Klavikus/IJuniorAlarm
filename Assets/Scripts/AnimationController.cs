using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private float _idleSpeedTreshold;

    private static readonly int IsRunning = Animator.StringToHash("isWalking");

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Thief _thief;
    private Vector3 _previousPosition;

    public void Init(Thief thief)
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _previousPosition = transform.position;
        _thief = thief;
        _thief.StartLooting += OnStartLooting;
        _thief.EndLooting += OnEndLooting;
    }

    private void Update()
    {
        Vector3 currentSpeed = transform.position - _previousPosition;

        _spriteRenderer.flipX = currentSpeed.x < 0;

        _animator.SetBool(IsRunning, currentSpeed.magnitude / Time.deltaTime > _idleSpeedTreshold);

        _previousPosition = transform.position;
    }

    private void OnDisable()
    {
        _thief.StartLooting -= OnStartLooting;
        _thief.EndLooting -= OnEndLooting;
    }

    private void OnStartLooting() => 
        _spriteRenderer.enabled = false;

    private void OnEndLooting() => 
        _spriteRenderer.enabled = true;
}