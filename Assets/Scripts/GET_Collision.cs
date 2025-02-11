using UnityEngine;

public class GET_Collision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("🪙 Coin Collected!");
            Destroy(other.gameObject, 0f); // ✅ Destroy the coin immediately (no delay)
        }
    }
}
