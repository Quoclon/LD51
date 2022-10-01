using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    NavMeshAgent agent;
    public Buildings currentBuilding;
    public bool isSelected;

    SelectionHighlighter selectionHighlighter;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selectionHighlighter = GetComponent<SelectionHighlighter>();
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDestination(Vector3 destination)
    {
        isSelected = false;
        agent.SetDestination(destination);
        selectionHighlighter.UnHighlighSelectedUnit();
    }

    public void SelectUnit(bool unitSelected)
    {
        isSelected = unitSelected;
        selectionHighlighter.HighlightSelectedUnit();
    }

    public void SetBuilding(Buildings building)
    {
        currentBuilding = building;
    }

    /*
    private void OnMouseOver()
    {
        Debug.Log("Mouse Hovering Over: " + this.name);
    }
    */


}

public enum Buildings
{
    None,
    Breed,
    Feed,
    Produce,
    Truck
}
