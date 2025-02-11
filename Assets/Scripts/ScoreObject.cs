using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    public int scoreValue = 5; // Set in Inspector: +5 for coins, -5 for enemies

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter Detected with: " + other.gameObject.name); // ✅ Debug log

        if (other.CompareTag("Player")) 
        {
            Debug.Log("✅ Player triggered with: " + gameObject.name);

            if (scoreValue > 0) // ✅ If it's a coin
            {
                Debug.Log("✅ Player collected a coin: +" + scoreValue);
                ScoreManager.Instance.AddScore(scoreValue);
                Destroy(gameObject); // ✅ Destroy the coin
            }
            else // ✅ If it's an enemy
            {
                Debug.Log("⚠️ Player hit an enemy! Losing: " + Mathf.Abs(scoreValue));
                ScoreManager.Instance.DeductScore(Mathf.Abs(scoreValue)); // Deduct score
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter Detected with: " + collision.gameObject.name); // ✅ Debug log

        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("⚠️ Collision detected with Enemy!");
            ScoreManager.Instance.DeductScore(Mathf.Abs(scoreValue)); // Deduct score
        }
    }
}
