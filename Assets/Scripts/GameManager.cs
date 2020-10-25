using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Script References")]
    public ItemDetection itemDetection;
    public PlayerController playerController;
    public HealthManager healthManager;

    [Header("Delivery Mechanism")]
    public GameObject[] deliveryLocations;
    public GameObject deliveryLocationsGO;
    public int numberOfDeliveryLocations;
    public int indexOfActivatedDL = 0;          //DL stands for 'Delivery Location'. This variable will help in checking if the player delvered the secret to the right house.
    public GameObject grabbedItem;

    [Header("Secrets Mechanism")]
    public GameObject[] secrets;
    public GameObject secretsCollectionGO;
    public int numberOfSecrets;

    [Header("Enemies mechanism")]
    public GameObject[] earGuardsSets;
    public GameObject earGuardsCollectionGO;
    public int numberOfEarGuardsSets;

    [Header("Victory and Defeat Mechanism")]
    public GameObject gameOverScreen;
    public bool pause = false;
    public GameObject winScreen;
    public TextMeshProUGUI causeOfLosingText;
    public TextMeshProUGUI deliveryProgressText;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip audioSecretGrabbed;
    public AudioClip audioSecretDelivered;

    [Header("Arrow")]
    public GameObject arrow;

    [Header("Timer Settings")]
    public int totalMinutes;
    public int totalSeconds;
    public TextMeshProUGUI timerText;
    float secondsRemaining;

    void Start() {
        indexOfActivatedDL = 0;

        if (Time.timeScale == 0) Time.timeScale = 1;

        numberOfDeliveryLocations = deliveryLocationsGO.transform.childCount;
        deliveryLocations = new GameObject[numberOfDeliveryLocations];
        AssignDeliveryLocationsToArray();

        numberOfSecrets = secretsCollectionGO.transform.childCount;
        secrets = new GameObject[numberOfSecrets];
        AssignSecrets();

        numberOfEarGuardsSets = earGuardsCollectionGO.transform.childCount;
        earGuardsSets = new GameObject[numberOfEarGuardsSets];
        AssignEarGuardSets();

        secondsRemaining = totalSeconds + (totalMinutes * 60);
    }

    
    void Update() {
        if(playerController.currentHealth <= 0) GameOver("The ears got to you!");

        arrow.transform.LookAt(deliveryLocations[indexOfActivatedDL].transform);

        if ((secondsRemaining -= Time.deltaTime)<=0)
        {
            timerText.text = "00:00";
            GameOver("Time's up!!!");
        }
        else
        {
            timerText.text = TimeSpan.FromSeconds(secondsRemaining).ToString(@"mm\:ss");
        }
    }

    void GameOver(string causeOfLosing) {
        gameOverScreen.SetActive(true);
        causeOfLosingText.text = causeOfLosing;
        Time.timeScale = 0;
        Cursor.visible = true;
        FindObjectOfType<CameraController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void CheckIfWon(int totalSecDeli)
    {
        if (totalSecDeli == numberOfSecrets)
        {
            //PLAYER WON
            winScreen.SetActive(true);
            //Time.timeScale = 0;
            Cursor.visible = true;
            FindObjectOfType<CameraController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PlayAgain()
    {
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
        arrow.SetActive(true);
        //indexOfActivatedDL = Random.Range(0,numberOfDeliveryLocations);            //temporarily it is random. Will be commented later on.
    }

    public bool CheckIfDeliveredToRightLocation(GameObject currentLocation)
    {
        if (indexOfActivatedDL<=deliveryLocations.Length)
        {
            if (currentLocation == deliveryLocations[indexOfActivatedDL])           //delivered to right location
            {
                ItemDetection.totalSecretsDelivered++;
                int totalSecDeli = ItemDetection.totalSecretsDelivered;
                deliveryProgressText.text = "Secrets delivered : " + totalSecDeli + "/" + numberOfSecrets;
                //Debug.Log(ItemDetection.totalSecretsDelivered);
                //Debug.Log("delivered to right location");
                grabbedItem.SetActive(false);
                ActivateNextSecret(totalSecDeli);
                itemDetection.isItemPickedUp = false;

                PlaySoundEffects(2);
                arrow.SetActive(false);
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

    public void PlaySoundEffects(int soundEffect)
    {
        switch (soundEffect)
        {
            case 1:                                                    // 1 : If secret is grabbed
                audioSource.clip = audioSecretGrabbed;
                break;
            case 2:                                                    // 2 : If secret is delivered
                audioSource.clip = audioSecretDelivered;
                break;
            default:
                audioSource.clip = audioSecretGrabbed;
                break;
        }
        audioSource.Play();
    }
    public void DeactivateSecret(int secretNum)
    {
        secrets[secretNum].SetActive(false);
        Debug.Log("Called me?");
    }
    void ActivateNextSecret(int totalSecDeli)
    {
        if (totalSecDeli < numberOfSecrets)
        {
            secrets[totalSecDeli].SetActive(true);
            earGuardsSets[totalSecDeli].SetActive(true);
        }
    }
}
