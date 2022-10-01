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

                if (animalController != null && hitInfo.collider.gameObject.tag != "Animal")
                {
                    if (animalController.isSelected)
                    {
                        animalController.SetDestination(hitInfo.collider.gameObject.transform.position);
                    }
                }

                if (hitInfo.collider.gameObject.tag == "Animal")
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


            

                    //Debug.Log("hit animal");
                    //animalController = tempAnimalController;
                    //animalController.SelectUnit(true);

                 
                }
                else
                {
                    //animalController.SelectUnit(false);
                    //animalController = null;
                }

            }
        }
    }
}
