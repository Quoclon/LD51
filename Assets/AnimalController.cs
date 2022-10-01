using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    NavMeshAgent agent;
    public Buildings currentBuilding;
    public bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void SelectUnit(bool unitSelected)
    {
        isSelected = unitSelected;
    }

    public void SetBuilding(Buildings building)
    {
        currentBuilding = building;
    }
}

public enum Buildings
{
    None,
    Breed,
    Feed,
    Produce,
    Truck
}
