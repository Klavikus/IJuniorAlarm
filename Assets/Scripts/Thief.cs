using System;
using System.Collections;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private House _targetHouse;
    [SerializeField] private Transform _basePosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _minimalStopDistance;
    [SerializeField] private float _lootTime;

    public event Action StartLooting;
    public event Action EndLooting;

    private IEnumerator Start()
    {
        _animationController.Init(thief: this);

        yield return LootingTargetHouse(_targetHouse);
    }

    private IEnumerator LootingTargetHouse(House targetHouse)
    {
        yield return MoveToPoint(targetHouse.EnterPointPosition);
        StartLooting?.Invoke();
        yield return new WaitForSeconds(_lootTime);
        EndLooting?.Invoke();
        yield return MoveToPoint(_basePosition);
    }

    private IEnumerator MoveToPoint(Transform targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition.position) > _minimalStopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, Time.deltaTime * _speed);
            yield return null;
        }
    }
}