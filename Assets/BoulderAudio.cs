using UnityEngine;
public class BoulderAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip rollingClip;     // Looping rolling sound
    public AudioClip impactClip;      // Sound when boulder hits ground or object

    [Header("Settings")]
    public float rollingThreshold = 0.1f; // Min velocity to play rolling sound
    public float impactThreshold = 1f;    // Min collision magnitude to play impact sound

    private Rigidbody rb;
    private AudioSource rollingSource;
    private AudioSource impactSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Setup rolling AudioSource
        rollingSource = gameObject.AddComponent<AudioSource>();
        rollingSource.clip = rollingClip;
        rollingSource.loop = true;
        rollingSource.spatialBlend = 1f; // 3D sound
        rollingSource.playOnAwake = false;

        // Setup impact AudioSource
        impactSource = gameObject.AddComponent<AudioSource>();
        impactSource.clip = impactClip;
        impactSource.loop = false;
        impactSource.spatialBlend = 1f; // 3D sound
        impactSource.playOnAwake = false;
    }

    void Update()
    {
        HandleRollingSound();
    }

    void HandleRollingSound()
    {
        if (rb.linearVelocity.magnitude > rollingThreshold)
        {
            if (!rollingSource.isPlaying)
                rollingSource.Play();
        }
        else
        {
            if (rollingSource.isPlaying)
                rollingSource.Pause(); // pause instead of Stop to keep loop position
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only play impact if velocity is significant
        if (rb.linearVelocity.magnitude > impactThreshold && impactSource.clip != null)
        {
            impactSource.Play();
        }
    }
}