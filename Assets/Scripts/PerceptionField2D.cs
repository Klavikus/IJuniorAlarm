using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PerceptionField2D : MonoBehaviour
{
    public event Action<Collider2D> Entered;
    public event Action<Collider2D> Exited;

    private void OnTriggerEnter2D(Collider2D collider) => Entered?.Invoke(collider);
    private void OnTriggerExit2D(Collider2D collider) => Exited?.Invoke(collider);
}