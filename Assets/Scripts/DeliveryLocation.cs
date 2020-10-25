using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryLocation : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject confettiCelebration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered");
            bool isDeliveryLocationRight = gameManager.CheckIfDeliveredToRightLocation(this.gameObject);
            if (isDeliveryLocationRight)
            {
                //Give some reward.
                confettiCelebration.SetActive(true);
                Debug.Log("Total secrets delivered: "+ItemDetection.totalSecretsDelivered);
            }
        }
    }
}
