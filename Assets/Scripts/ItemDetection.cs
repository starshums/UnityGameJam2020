using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject pickUpItemUI;
    public bool isItemPickedUp;
    public bool canPickUp;
    public GameObject detectedItem;
    public GameObject grabbedItem;

    public static int totalSecretsDelivered = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pickUpItemUI.SetActive(false);
                isItemPickedUp = true;
                gameManager.DetermineDeliveryLocation(totalSecretsDelivered);
                if (detectedItem != null)
                {
                    grabbedItem.SetActive(true);
                    gameManager.secrets[totalSecretsDelivered].SetActive(false);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            if (!isItemPickedUp)            //checking if item is already picked up or not
            {
                pickUpItemUI.SetActive(true);
                detectedItem = other.gameObject;
                canPickUp = true;                       //ability to pickup
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            pickUpItemUI.SetActive(false);
            canPickUp = false;                          //ability to pickup
            detectedItem = null;
        }
    }
}
