using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private Transform _enterPoint;

    public Transform EnterPointPosition => _enterPoint;
}