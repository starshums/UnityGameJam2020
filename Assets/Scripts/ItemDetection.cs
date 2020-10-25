using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetection : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject pickUpItemUI;
    public bool isItemPickedUp;
    public bool canPickUp;
    public GameObject detectedItem;     //item(letter) in range to pickup
    public GameObject grabbedItem;

    public int secretNumber = -1;
    public static int totalSecretsDelivered = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isItemPickedUp)
        {
            pickUpItemUI.SetActive(false);
            canPickUp = false;                          //ability to pickup
            detectedItem = null;
        }
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                secretNumber++;
                pickUpItemUI.SetActive(false);
                isItemPickedUp = true;
                gameManager.DetermineDeliveryLocation(totalSecretsDelivered);
                if (detectedItem != null)
                {
                    grabbedItem.SetActive(true);
                    //gameManager.secrets[totalSecretsDelivered].SetActive(false);
                    gameManager.DeactivateSecret(totalSecretsDelivered);
                }
                Debug.Log("secret number is : " + secretNumber);
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
