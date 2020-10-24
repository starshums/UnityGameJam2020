using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ItemDetection itemDetection;

    public GameObject[] deliveryLocations;
    public GameObject deliveryLocationsGO;
    public int numberOfDeliveryLocations;
    public int indexOfActivatedDL = 100;          //DL stands for 'Delivery Location'. This variable will help in checking if the player delvered the secret to the right house.
    public GameObject grabbedItem;

    public GameObject[] secrets;
    public GameObject secretsCollectionGO;
    public int numberOfSecrets;

    void Start()
    {
        numberOfDeliveryLocations = deliveryLocationsGO.transform.childCount;
        deliveryLocations = new GameObject[numberOfDeliveryLocations];
        AssignDeliveryLocationsToArray();

        numberOfSecrets = secretsCollectionGO.transform.childCount;
        secrets = new GameObject[numberOfSecrets];
        AssignSecrets();
    }

    
    void Update()
    {
        
    }

    void AssignSecrets()
    {
        for (int i = 0; i < numberOfDeliveryLocations; i++)
        {
            secrets[i] = secretsCollectionGO.transform.GetChild(i).gameObject;
            secrets[i].SetActive(false);
            if (i == 0)
            {
                secrets[i].SetActive(true);
            }
        }
    }
    void AssignDeliveryLocationsToArray()
    {
        for (int i = 0; i < numberOfDeliveryLocations; i++)
        {
            deliveryLocations[i] = deliveryLocationsGO.transform.GetChild(i).gameObject;
        }
    }

    public void DetermineDeliveryLocation(int secretNumber)         //mapping of secret number with location number
    {
        switch (secretNumber)               //determines the Delivery Location number. By that way, we can control which secret number should go to which delivery location
        {
            case 0:
                indexOfActivatedDL = 0;         //indexOfActivatedDL is the index of Delivery Location Activated.
                break;
            case 1:
                indexOfActivatedDL = 1;
                break;
            case 2:
                indexOfActivatedDL = 2;
                break;
            case 3:
                indexOfActivatedDL = 3;
                break;
            default:
                break;
        }
        //indexOfActivatedDL = Random.Range(0,numberOfDeliveryLocations);            //temporarily it is random. Will be commented later on.
    }

    public bool CheckIfDeliveredToRightLocation(GameObject currentLocation)
    {
        if (indexOfActivatedDL<=deliveryLocations.Length)
        {
            if (currentLocation == deliveryLocations[indexOfActivatedDL])
            {
                ItemDetection.totalSecretsDelivered++;
                //Debug.Log(ItemDetection.totalSecretsDelivered);
                Debug.Log("delivered to right location");
                grabbedItem.SetActive(false);
                ActivateNextSecret();
                itemDetection.isItemPickedUp = false;
                return true;
            }
            else
            {
                Debug.Log("Wrong location mate!");
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }

    void ActivateNextSecret()
    {
        if (ItemDetection.totalSecretsDelivered<numberOfSecrets)
        {
            secrets[ItemDetection.totalSecretsDelivered].SetActive(true);
        }
    }
}
