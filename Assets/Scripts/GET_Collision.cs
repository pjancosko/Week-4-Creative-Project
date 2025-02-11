using UnityEngine;

public class GET_Collision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected");

            // Destroy the coin
            Destroy(other.gameObject);
        }
    }
}