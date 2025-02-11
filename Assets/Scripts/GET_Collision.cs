using UnityEngine;

public class GET_Collision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected");
            ScoreManager.Instance.AddScore(5); // ✅ Add 5 points when collecting a coin
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit by Enemy!");
            ScoreManager.Instance.DeductScore(5); // ❌ Deduct 5 points when hitting an enemy
        }
    }
}
