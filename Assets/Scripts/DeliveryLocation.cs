using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryLocation : MonoBehaviour
{
    public GameManager gameManager;
    public ItemDetection itemDetection;

    public GameObject confettiCelebration;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemDetection.isItemPickedUp)
            {
                //Debug.Log("Player entered");
                bool isDeliveryLocationRight = gameManager.CheckIfDeliveredToRightLocation(this.gameObject);
                if (isDeliveryLocationRight)
                {
                    //Give some reward.
                    confettiCelebration.SetActive(true);
                    Debug.Log("Total secrets delivered: " + ItemDetection.totalSecretsDelivered);
                    gameManager.CheckIfWon(ItemDetection.totalSecretsDelivered);
                }
            }
        }
    }
}
