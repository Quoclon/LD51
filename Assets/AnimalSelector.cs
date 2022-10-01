using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSelector : MonoBehaviour
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

                Debug.Log(hitInfo.collider.gameObject.name);

                string objectTag = hitInfo.collider.gameObject.tag;

                if (animalController != null && objectTag != "Animal")
                {
                    if (animalController.isSelected)
                    {
                        if(objectTag == "Breed")
                        {
                            animalController.SetBuilding(Buildings.Breed);
                            Debug.Log("Breed: " + hitInfo.collider.gameObject.tag);
                            animalController.SetDestination(hitInfo.collider.gameObject.transform.position);
                        }

                        else if (objectTag == "Feed")
                        {
                            animalController.SetBuilding(Buildings.Feed);
                            Debug.Log("Feed: " + hitInfo.collider.gameObject.tag);
                            animalController.SetDestination(hitInfo.collider.gameObject.transform.position);

                        }

                        else if (objectTag == "Produce")
                        {
                            animalController.SetBuilding(Buildings.Produce);
                            Debug.Log("Produce: " + hitInfo.collider.gameObject.tag);
                            animalController.SetDestination(hitInfo.collider.gameObject.transform.position);
                        }

                        // Send animal toard building
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
