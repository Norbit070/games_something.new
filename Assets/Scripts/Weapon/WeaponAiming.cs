using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponAiming : MonoBehaviour
{
    private MultiAimConstraint[] _constraints;
    public void Init(Transform aim)
    {
        WeightedTransformArray constraintSourceObject = CreateConstraintSourceObject(aim);
        _constraints = GetComponentsInChildren<MultiAimConstraint>(true);

        for (int i = 0; i < _constraints.Length; i++)
        {
            _constraints[i].data.sourceObjects = constraintSourceObject;
        }
    }
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
    private WeightedTransformArray CreateConstraintSourceObject(Transform aim)
    {
        var constraintAimArray = new WeightedTransformArray(1);
        constraintAimArray[0] = new WeightedTransform(aim, 1);
        return constraintAimArray;
    }
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
