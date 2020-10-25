using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject[] earGuardsSets;
    public GameObject earGuardsCollectionGO;
    public int numberOfEarGuardsSets;

    public HealthManager healthManager;
    public GameObject gameOverScreen;
    public bool pause = false;

    void Start() {
        if(Time.timeScale == 0) Time.timeScale = 1;

        numberOfDeliveryLocations = deliveryLocationsGO.transform.childCount;
        deliveryLocations = new GameObject[numberOfDeliveryLocations];
        AssignDeliveryLocationsToArray();

        numberOfSecrets = secretsCollectionGO.transform.childCount;
        secrets = new GameObject[numberOfSecrets];
        AssignSecrets();

        numberOfEarGuardsSets = earGuardsCollectionGO.transform.childCount;
        earGuardsSets = new GameObject[numberOfEarGuardsSets];
        AssignEarGuardSets();
    }

    
    void Update() {
        if(healthManager.slider.value == 0) GameOver();
    }

    void GameOver() {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        FindObjectOfType<CameraController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void AssignEarGuardSets()
    {
        for (int i = 0; i < numberOfEarGuardsSets; i++)
        {
            earGuardsSets[i] = earGuardsCollectionGO.transform.GetChild(i).gameObject;
            earGuardsSets[i].SetActive(false);
            if (i == 0)
            {
                earGuardsSets[i].SetActive(true);
            }
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
                //Debug.Log("delivered to right location");
                grabbedItem.SetActive(false);
                ActivateNextSecret();
                itemDetection.isItemPickedUp = false;
                return true;
            }
            else
            {
                //Debug.Log("Wrong location mate!");
                return false;
            }
        }
        else
        {
            return false;
        }
        
    }

    public void DeactivateSecret(int secretNum)
    {
        secrets[secretNum].SetActive(false);
        Debug.Log("Called me?");
    }
    void ActivateNextSecret()
    {
        if (ItemDetection.totalSecretsDelivered<numberOfSecrets)
        {
            secrets[ItemDetection.totalSecretsDelivered].SetActive(true);
            earGuardsSets[ItemDetection.totalSecretsDelivered].SetActive(true);
        }
    }
}
