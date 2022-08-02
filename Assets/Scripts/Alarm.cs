using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private PerceptionField2D _perceptionField;
    [SerializeField] private AudioClip _alarmAudio;
    [SerializeField] private float _initialVolume;
    [SerializeField] private float _volumeTransitionTime;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _alarmAudio;
        _audioSource.volume = _initialVolume;
    }

    private void OnEnable()
    {
        _perceptionField.Entered += OnPerceptionFieldEntered;
        _perceptionField.Exited += OnPerceptionFieldExited;
    }

    private void OnDisable()
    {
        _perceptionField.Entered -= OnPerceptionFieldEntered;
        _perceptionField.Exited -= OnPerceptionFieldExited;
    }

    private void OnPerceptionFieldEntered(Collider2D collider)
    {
        if (collider.TryGetComponent(out Thief thief))
            StartCoroutine(SmoothVolume(_maxVolume, _volumeTransitionTime, OnStart: () => _audioSource.Play()));
    }

    private void OnPerceptionFieldExited(Collider2D collider)
    {
        if (collider.TryGetComponent(out Thief thief))
            StartCoroutine(SmoothVolume(_minVolume, _volumeTransitionTime, OnEnd: () => _audioSource.Stop()));
    }

    private IEnumerator SmoothVolume(float endValue, float transitionTime, Action OnStart = null, Action OnEnd = null)
    {
        OnStart?.Invoke();

        while (Mathf.Abs(_audioSource.volume - endValue) > 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, endValue, Time.deltaTime / transitionTime);
            yield return null;
        }

        OnEnd?.Invoke();
    }
}