using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform guardPos;
    public Transform nextWalkPoint;

    public GameObject[] walkPoints;
    public int numberOfPatrolPoints;
    public GameObject walkPointCollectionGO;
    public bool isNextWalkPointSet;
    
    // Start is called before the first frame update
    void Start()
    {
        guardPos = transform;
        nextWalkPoint = guardPos;

        numberOfPatrolPoints = walkPointCollectionGO.transform.childCount;
        walkPoints = new GameObject[numberOfPatrolPoints];
        AssignPatrolPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNextWalkPointSet)
        {
            Patrol();
        }
        else
        {
            GetNextWalkPoint();
        }
    }

    void AssignPatrolPoints()
    {
        for (int i = 0; i < numberOfPatrolPoints; i++)
        {
            walkPoints[i] = walkPointCollectionGO.transform.GetChild(i).gameObject;
        }
    }

    void GetNextWalkPoint()
    {
        nextWalkPoint = walkPoints[Random.Range(0, numberOfPatrolPoints)].transform;
        isNextWalkPointSet = true;
    }
    
    void Patrol()
    {
        agent.SetDestination(nextWalkPoint.position);

        if (Vector3.Distance(transform.position, nextWalkPoint.position) < 5)
        {
            isNextWalkPointSet = false;
        }
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered");
            nextWalkPoint = other.transform;
            isNextWalkPointSet = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Exited");
            isNextWalkPointSet = false;
        }
    }
    /*
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Hit Player");
            other.gameObject.GetComponent<PlayerController>().Damage(10);
        }
    }
    */
}
