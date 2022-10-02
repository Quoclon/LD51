using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectionHighlighter : MonoBehaviour
{
    GameManager gameManager;

    [Header("Appear/Dissapear Objets")]
    public CapsuleCollider capsuleCollider;
    public Canvas worldSpaceStatsCanvas;
    //public NavMeshAgent navmeshAgent;

    [Header("Renderers")]
    public Renderer skinRendererDefault;
    public Renderer skinRendererOutline;

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
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        skinRendererOutline.material = outlineNone;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RemoveVisualsAndClickable();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            AddVisualsAndClickable();
        }
    }

    private void OnMouseOver()
    {
        if (gameManager.currentGamePhase != GamePhase.Selection)
        {
            skinRendererOutline.material = outlineNone;
        }

        if(!animalController.isSelected)
            skinRendererOutline.material = outlineYellow;
    }

    private void OnMouseExit()
    {
        if (gameManager.currentGamePhase != GamePhase.Selection)
        {
            skinRendererOutline.material = outlineNone;
        }

        if (!animalController.isSelected)
            skinRendererOutline.material = outlineNone;
    }

    public void HighlightSelectedUnit()
    {
        skinRendererOutline.material = outlineGreen;
    }

    public void UnHighlighSelectedUnit()
    {
        skinRendererOutline.material = outlineNone;
    }

    public void RemoveVisualsAndClickable()
    {
        skinRendererDefault.gameObject.SetActive(false);
        skinRendererOutline.gameObject.SetActive(false);
        //navmeshAgent.ActivateCurrentOffMeshLink(false);
        capsuleCollider.enabled = false;
        worldSpaceStatsCanvas.rootCanvas.enabled = false;
        //navmeshAgent.gameObject.SetActive(false);
        //capsuleCollider.gameObject.SetActive(false);

    }

    public void AddVisualsAndClickable()
    {
        skinRendererDefault.gameObject.SetActive(true);
        skinRendererOutline.gameObject.SetActive(true);
        //navmeshAgent.ActivateCurrentOffMeshLink(true);
        capsuleCollider.enabled = true;
        //worldSpaceStatsCanvas.enabled = true;
        worldSpaceStatsCanvas.rootCanvas.enabled = true;


        //navmeshAgent.gameObject.SetActive(true);
        //capsuleCollider.gameObject.SetActive(true);
    }
}
