using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHighlighter : MonoBehaviour
{
    public Renderer skinRenderer;

    //public bool isSelected;

    [Header("Quibli Outline Materials")]
    public Material outlineNone;
    public Material outlineRed;
    public Material outlineYellow;
    public Material outlineGreen;

    [Header("Selection Status Reference")]
    public AnimalController animalController;

    // Start is called before the first frame update
    void Start()
    {
        //isSelected = false;
        //animalController.GetComponent<AnimalController>();
        skinRenderer.material = outlineNone;
    }

    private void OnMouseOver()
    {
        //Debug.Log("Hovered over: " + this.name);
        if(!animalController.isSelected)
            skinRenderer.material = outlineYellow;
    }

    private void OnMouseExit()
    {
        if(!animalController.isSelected)
            skinRenderer.material = outlineNone;
    }

    public void HighlightSelectedUnit()
    {
        skinRenderer.material = outlineGreen;
    }

    public void UnHighlighSelectedUnit()
    {
        skinRenderer.material = outlineNone;
    }
}
