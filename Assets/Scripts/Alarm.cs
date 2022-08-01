using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _alarmAudio;
    [SerializeField] private float _initialVolume;
    [SerializeField] private float _volumeTransitionTime;
    [SerializeField] private float _onEnterVolume;
    [SerializeField] private float _onExitVolume;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _alarmAudio;
        _initialVolume = 0;
        _audioSource.volume = _initialVolume;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Thief>())
        {
            _audioSource.Play();
            StartCoroutine(SmoothVolume(_onEnterVolume, _volumeTransitionTime));
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Thief>())
        {
            yield return SmoothVolume(_onExitVolume, _volumeTransitionTime);
            _audioSource.Stop();
        }
    }

    private IEnumerator SmoothVolume(float endValue, float transitionTime)
    {
        while (Mathf.Abs(_audioSource.volume - endValue) > 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, endValue, Time.deltaTime / transitionTime);
            yield return null;
        }
    }
}