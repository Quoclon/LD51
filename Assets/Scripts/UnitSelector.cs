using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public AnimalController? animalController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // Get the tag of the RayCast target
                string objectTag = hitInfo.collider.gameObject.tag;
                //Debug.Log(hitInfo.collider.gameObject.name);

                // If an animal is selected AND you've now clicked on something else
                if (animalController != null && objectTag != "Animal")
                {
                    // Select One of the 'Buildings'
                    if (animalController.isSelected)
                    {
                        //animalController.SetDestination(hitInfo.collider.gameObject.transform.position);
                        bool deactiveSelectedUnit = false;

                        if (objectTag == "Breed")
                        {
                            animalController.SetBuilding(Buildings.Breed);
                            deactiveSelectedUnit = true;                       
                        }

                        else if (objectTag == "Feed")
                        {
                            animalController.SetBuilding(Buildings.Feed);
                            deactiveSelectedUnit = true;
                        }

                        else if (objectTag == "Produce")
                        {
                            animalController.SetBuilding(Buildings.Produce);
                            deactiveSelectedUnit = true;
                        }

                        else if (objectTag == "Sell")
                        {
                            animalController.SetBuilding(Buildings.Sell);
                            deactiveSelectedUnit = true;
                        }

                        if (deactiveSelectedUnit)
                        {
                            animalController.SetAnimalVisuals(false);
                            animalController.SelectUnit(false);
                            animalController = null;
                        }
                    }
                }

                if (objectTag == "Animal")
                {
                    AnimalController tempAnimalController = hitInfo.collider.gameObject.GetComponent<AnimalController>();
                    
                    if (animalController != null)
                    {
                        if (animalController == tempAnimalController)
                        {
                            return;
                        }
                        else
                        {
                            animalController.SelectUnit(false);
                            animalController = tempAnimalController;
                            animalController.SelectUnit(true);
                        }
                    }

                    if (animalController == null)
                    {
                        animalController = tempAnimalController;
                        animalController.SelectUnit(true);
                    }                 
                }
            }
        }
    }


}
