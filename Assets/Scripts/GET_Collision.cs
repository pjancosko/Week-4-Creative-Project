using UnityEngine;

public class GET_Collision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            print("Trigger Entered");

            
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            print("Trigger Stay");
             // Destroy the object when the player stays the trigger
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TriggerObject"))
        {
            print("Trigger Exit");

           
        }
    }
}