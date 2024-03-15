using UnityEngine;
using UnityEngine.Animations.Rigging;
public class EnemyAiming : CharacterAiming
{
    [SerializeField] private float _aimingSpeed = 10f;
    [SerializeField] private Vector3 _aimDeltaPosition = Vector3.up;
    [SerializeField] private float _aimingRange = 20f;
    
    private Transform _aimTransform;
    private Transform _targetTransform;
    private bool _isTargetInRange;
    protected override void OnInit()
    {
        base.OnInit();
        _aimTransform = CreateAim().transform;
        _targetTransform = FindAnyObjectByType<Player>().transform;
        InitWeaponAimings(_aimTransform);
        SetDefaultAimPosition();
    }
    

    private GameObject CreateAim()
    {
        GameObject aim = new GameObject("EnemyAim");
        aim.transform.SetParent(transform);
        return aim;
    }
    private void SetDefaultAimPosition()
    {
        _aimTransform.position = transform.position + transform.forward + _aimDeltaPosition;
    }
    
    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        Aiming();
    }

    private void Aiming()
    {
        if (CheckTargetInRange())
        {
            _isTargetInRange = true;
            _aimTransform.position = Vector3.Lerp(_aimTransform.position, _targetTransform.position + _aimDeltaPosition, _aimingSpeed * Time.deltaTime);
        }
        else
        {
            if (_isTargetInRange)
            {
                _isTargetInRange = false;

                SetDefaultAimPosition();
            }
        }
        Vector3 lookDirection = (_aimTransform.position - transform.position).normalized;

        lookDirection.y = 0;

        var newRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _aimingSpeed * Time.deltaTime);
    }
    private bool CheckTargetInRange()
    {
        return (_targetTransform.position - transform.position).magnitude <= _aimingRange;
    }

}
