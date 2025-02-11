using UnityEngine;

public class StableRaycast : MonoBehaviour
{
    public float range = 3f;
    public Camera playerCamera; // Assign the player's camera in the Inspector

    void Start()
    {
        print("Raycast script has started");

        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
        }
    }

    void FixedUpdate()
    {
        if (playerCamera == null) return;

        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        Debug.DrawRay(origin, direction * range, Color.red);

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            if (hit.collider.CompareTag("Static"))
            {
                print("My raycast hit a STATIC object");
            }
            else if (hit.collider.CompareTag("Enemy"))
            {
                print("My raycast hit an ENEMY object");

                // Reduce the size of the enemy **without breaking physics**
                ReduceSize(hit.collider.gameObject);
            }
        }
    }

    // ✅ Fix: Adjust ReduceSize() to Prevent Collision Issues
    void ReduceSize(GameObject enemy)
    {
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        Collider col = enemy.GetComponent<Collider>();

        if (rb != null)
        {
            rb.isKinematic = false; // ✅ Keep physics enabled for collision detection
        }

        enemy.transform.localScale *= 0.5f; // Reduce the size by half

        if (col != null)
        {
            col.enabled = false;
            col.enabled = true; // ✅ Collider refresh, but be careful!
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // ✅ Corrected from linearVelocity
            rb.angularVelocity = Vector3.zero; // ✅ Reset angular velocity
            rb.constraints = RigidbodyConstraints.FreezeRotation; // ✅ Allow movement, only freeze rotation
        }
    }
}
