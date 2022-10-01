using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHighlighter : MonoBehaviour
{
    public Renderer skinRenderer;

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
        skinRenderer.material = outlineNone;
    }

    private void OnMouseOver()
    {
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
