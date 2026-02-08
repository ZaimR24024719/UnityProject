using UnityEngine;

public class BoulderTriggerFall : MonoBehaviour
{
    public Rigidbody boulderRigidbody; // assign Boulder Rigidbody in Inspector
    private bool hasFallen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasFallen) return;

        if (other.CompareTag("Player"))
        {
            boulderRigidbody.isKinematic = false; // enable physics
            hasFallen = true;
            Debug.Log("Trigger fired! Boulder should fall."); // for testing
        }
    }
}