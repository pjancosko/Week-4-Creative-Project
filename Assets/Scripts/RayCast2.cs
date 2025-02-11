using UnityEngine;

public class StableRaycast : MonoBehaviour
{
    public float range = 3f;
    public Camera playerCamera; // Assign the player's camera in the Inspector

    void Start()
    {
        Debug.Log("Raycast script has started");

        // Ensure the playerCamera is assigned
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
        }
    }

    void FixedUpdate()
    {
        if (playerCamera == null) return;

        // Use the camera's position and forward direction for the raycast
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        Debug.DrawRay(origin, direction * range, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            if (hit.collider.CompareTag("Static"))
            {
                Debug.Log("My raycast hit a STATIC object");
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("My raycast hit an ENEMY object");

                // Reduce the size of the enemy and add score
                ReduceSize(hit.collider.gameObject);
                ScoreManager.Instance.AddScore(2); // Add 2 points when the enemy shrinks
            }
        }
    }

    // Method to reduce the size of the enemy
    void ReduceSize(GameObject enemy)
    {
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        Collider col = enemy.GetComponent<Collider>();

        if (rb != null)
        {
            rb.isKinematic = true; // Disable physics interactions
        }

        enemy.transform.localScale *= 0.5f; // Reduce the size by half

        if (col != null)
        {
            // Recalculate collider size
            col.enabled = false;
            col.enabled = true;
        }

        if (rb != null)
        {
            rb.isKinematic = false; // Re-enable physics interactions
            rb.linearVelocity = Vector3.zero; // Reset velocity to prevent floating away
            rb.angularVelocity = Vector3.zero; // Reset angular velocity to prevent spinning
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; // Apply constraints
        }
    }
}