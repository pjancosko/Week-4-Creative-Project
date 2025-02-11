using UnityEngine;

public class GET_Collision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            Debug.Log("Trigger Entered");

            // Change the color to green
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            Debug.Log("Trigger Stay");
            // No action needed here since the object will be destroyed in OnTriggerExit
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            Debug.Log("Trigger Exit");

            // Destroy the object when the player exits the trigger
            Destroy(other.gameObject);
        }
    }
}