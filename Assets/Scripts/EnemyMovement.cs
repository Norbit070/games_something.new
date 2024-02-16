using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    private const string MovementHorizontalKey = "Horizontal";
    private const string MovementVerticalKey = "Vertical";
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;
    private Vector3 _prevPosition;

    protected override void OnInit()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTransform = FindAnyObjectByType<Player>().transform;
        _prevPosition = transform.position;
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }

        SetTargetPosition(_playerTransform.position);
        RefreshAnimation();
    }

    private void SetTargetPosition(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }

    private void RefreshAnimation()
    {
        Vector3 curPosition = transform.position;
        Vector3 deltaMove = curPosition - _prevPosition;
        _prevPosition = curPosition;
        deltaMove.Normalize();
        float relatedX = Vector3.Dot(deltaMove, transform.right);
        float relatedY = Vector3.Dot(deltaMove, transform.forward);
        _animator.SetFloat(MovementHorizontalKey, relatedX);
        _animator.SetFloat(MovementVerticalKey, relatedY);
    }
    protected override void OnStop()
    {
        _navMeshAgent.enabled = false;
        RefreshAnimation();
    }
}    
