using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    SelectionHighlighter selectionHighlighter;

    [Header("Building for Action Phase")]
    public Buildings currentBuilding;

    [Header("Status Bools")]
    public bool isSelected;

    [Header("Random Movement")]
    NavMeshAgent agent;
    public float walkRadius;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        selectionHighlighter = GetComponent<SelectionHighlighter>();
        isSelected = false;

        // Start by moving Animal
        agent.SetDestination(RandomNavMeshLocation());
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }

        if (gameManager.currentGamePhase != GamePhase.Selection)
        {
            isSelected = false;
            selectionHighlighter.UnHighlighSelectedUnit();
        }
    }

    public Vector3 RandomNavMeshLocation()
    {
        // ~ Used to be in Start() could be break things
        walkRadius = Random.Range(4f, 12f);

        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;

        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }


    public void SelectUnit(bool unitSelected)
    {

        if (gameManager.currentGamePhase != GamePhase.Selection)
        {
            unitSelected = false;
            selectionHighlighter.UnHighlighSelectedUnit();
            return;
        }

        isSelected = unitSelected;
        if (isSelected)
            selectionHighlighter.HighlightSelectedUnit();
        if(!isSelected)
            selectionHighlighter.UnHighlighSelectedUnit();
    }

    public void SetBuilding(Buildings building)
    {
        if (gameManager.currentGamePhase != GamePhase.Selection)
            return;

        currentBuilding = building;
    }

    public void SetAnimalVisuals(bool turnOnVisuals)
    {
        if (turnOnVisuals)
            selectionHighlighter.AddVisualsAndClickable();
        if (!turnOnVisuals)
            selectionHighlighter.RemoveVisualsAndClickable();
    }
}

public enum Buildings
{
    None,
    Breed,
    Feed,
    Produce,
    Sell
}
