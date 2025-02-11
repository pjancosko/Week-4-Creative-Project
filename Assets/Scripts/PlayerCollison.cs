using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Enemy"))
    {
        Debug.Log("⚠️ Player hit an enemy! Losing score.");
        ScoreManager.Instance.DeductScore(5);
    }
}

}
