using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioSource audioSource;      // assign the Player's AudioSource
    public AudioClip[] footstepClips;    // array of footstep sounds
    public float stepDistance = 2f;      // how far player moves between steps

    private Vector3 lastPosition;
    private float accumulatedDistance = 0f;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        HandleFootsteps();
    }

    void HandleFootsteps()
    {
        Vector3 moveDelta = transform.position - lastPosition;
        accumulatedDistance += moveDelta.magnitude;

        if (accumulatedDistance >= stepDistance)
        {
            PlayFootstep();
            accumulatedDistance = 0f;
        }

        lastPosition = transform.position;
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0 || audioSource == null)
            return;

        // Pick a random clip for variety
        int index = Random.Range(0, footstepClips.Length);
        audioSource.PlayOneShot(footstepClips[index]);
    }
}