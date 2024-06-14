using System.Collections;
using UnityEngine;

public class Swinger : MonoBehaviour
{
    [SerializeField] private HingeJoint _hingeJoint;
    [SerializeField] private float _velocity;
    [SerializeField] private float _delayRocking;

    private Coroutine _coroutine;

    public void Shake()
    {
        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Sway());
    }

    private IEnumerator Sway()
    {
        var wait = new WaitForSeconds(_delayRocking);
        JointMotor jointMotor = _hingeJoint.motor;

        for (float currentVelocity = _velocity; currentVelocity >= -_velocity; currentVelocity -= _velocity)
        {
            jointMotor.targetVelocity = currentVelocity;
            _hingeJoint.motor = jointMotor;
            yield return wait;
        }

        jointMotor.targetVelocity = 0f;
        _hingeJoint.motor = jointMotor;
        _coroutine = null;
    }
}
