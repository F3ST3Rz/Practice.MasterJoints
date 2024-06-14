using System.Collections;
using UnityEngine;

public class Catapulter : MonoBehaviour
{
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private float _maxSpring;
    [SerializeField] private float _minSpring;
    [SerializeField] private Rigidbody _ScoopRigidbody;
    [SerializeField] private float _delayRecharge;
    [SerializeField] private Projectile _prefab;
    [SerializeField] private Transform _pointSpawn;

    private bool _isStarted = false;
    private Coroutine _coroutine;

    private void Start()
    {
        AddProjectile();
    }

    public void Launch()
    {
        if (_isStarted)
            return;

        _ScoopRigidbody.WakeUp();
        _springJoint.spring = _maxSpring;
        _isStarted = true;
    }

    public void Recharge()
    {
        if (_isStarted == false)
            return;

        if (_coroutine != null)
            return;

        _coroutine = StartCoroutine(Recharging());
    }

    private IEnumerator Recharging()
    {
        _ScoopRigidbody.WakeUp();
        _springJoint.spring = _minSpring;

        yield return new WaitForSeconds(_delayRecharge);

        AddProjectile();

        _isStarted = false;
        _coroutine = null;
    }

    private void AddProjectile()
    {
        Projectile projectile = Instantiate(_prefab);
        projectile.transform.position = _pointSpawn.position;
    }
}
