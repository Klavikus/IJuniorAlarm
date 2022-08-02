using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PerceptionField2D : MonoBehaviour
{
    public Action<Collider2D> OnEnter;
    public Action<Collider2D> OnExit;

    private void OnTriggerEnter2D(Collider2D collider) => OnEnter?.Invoke(collider);
    private void OnTriggerExit2D(Collider2D collider) => OnExit?.Invoke(collider);
}