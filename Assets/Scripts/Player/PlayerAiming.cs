using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAiming : CharacterAiming
{
    [SerializeField] private float _aimingSpeed = 10f;

    private Transform _aimTransform;
    

    private Camera _mainCamera;

    protected override void OnInit()
    {
        base.OnInit();
        _mainCamera = Camera.main;
        _aimTransform = FindAnyObjectByType<PlayerAim>().transform;
        InitWeaponAimings(_aimTransform);
    }
    
    private void FixedUpdate()
    {
        if (!IsActive)
        {
            return;
        }

        Aiming();
    }

    private void Aiming()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray findTargetRay = _mainCamera.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(findTargetRay, out RaycastHit hitInfo))
        {
            Vector3 lookDirection = (hitInfo.point - transform.position).normalized;
            lookDirection.y = 0;
            var newRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, _aimingSpeed * Time.fixedDeltaTime);

            _aimTransform.position = Vector3.Lerp(_aimTransform.position, hitInfo.point, _aimingSpeed * Time.fixedDeltaTime);
        }
    }
}
