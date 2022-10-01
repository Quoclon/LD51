using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalUI : MonoBehaviour
{

    public float speed;
    public float maxHealth;
    public float minHealth;
    public float currentHealth;
    public float maxBreeding;
    public float minBreeding;
    public float currentBreeding;
    public float maxProduction;
    public float minProduction;
    public float currentProduction;

    public float cngHealth;
    public float cngBreeding;
    public float cngProduction;
    public float healthMod;

    public float sheep;
    public float sheepValue;
    public float wool;
    public float woolValue;
    public float gold;

    public TextMeshProUGUI animalName;
    public List<string> animalNamesList;

    public BarScript healthBar;
    public BarScript breedingBar;
    public BarScript productionBar;

    public int currentAge;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = minHealth;
        healthBar.SetSliderValueMax(100);
        healthBar.SetSliderValue(currentHealth);
        currentBreeding = minBreeding;
        breedingBar.SetSliderValueMax(.5f);
        breedingBar.SetSliderValue(currentBreeding);
        currentProduction = minProduction;
        productionBar.SetSliderValueMax(1);
        productionBar.SetSliderValue(currentProduction);

        wool = 0;
        currentAge = 0;

        



        int randomNumber = Random.Range(0, animalNamesList.Count);
        animalName.text = animalNamesList[randomNumber].ToString();
        //animalName.text = "Poly";
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
     if (Input.GetKeyDown(KeyCode.E))
        {
          EndTurn();
        }

     if (Input.GetKeyDown(KeyCode.F))
        {
            Feed();
        }

     if (Input.GetKeyDown(KeyCode.B))
        {
            Breeding();
        }

     if (Input.GetKeyDown(KeyCode.P))
        {
            Production();
        }

     if (Input.GetKeyDown(KeyCode.S))
        {
            SellAnimal();
        }




        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime);

    }

    void Feed()
    {
        if (currentAge <= 4)
        {
            cngHealth = Random.Range(4, 21);
            currentHealth += cngHealth;

        }

        if (currentAge >= 11)
        {

            cngHealth = Random.Range(5, 21);
            currentHealth += cngHealth;
        }

        if (currentAge > 4 && currentAge < 11)
        {
            cngHealth = Random.Range(10, 31);
            currentHealth += cngHealth;

        }

        if (currentHealth > 100)
        { 
         currentHealth = 100;
        }

        healthBar.SetSliderValue(currentHealth);

    }

    void Breeding()
    {

        if (currentAge >= 11)
        {
            if (Random.Range(0, 1) < currentBreeding)
            {
                //Need Instanciate Code
                sheep += 1;
            }

            cngBreeding = Random.Range(-.2f, -.45f);
            currentBreeding += cngBreeding;
        }

        if (currentAge < 11)
        {
            if (Random.Range(0, 1) < currentBreeding)
            {
                //Need Instanciate Code
                sheep += 1;
            }

            cngBreeding = Random.Range(-.35f, -.5f);
            currentBreeding += cngBreeding;

        }

        breedingBar.SetSliderValue(currentBreeding);

    }


    void Production()
    {
        if (currentAge < 11)
        {
            gold += (currentProduction * woolValue) * (currentHealth / healthMod);
            cngProduction = Random.Range(-.05f, -.2f);
            currentProduction += cngProduction;
        
        }

        if (currentAge >= 11)
        {
            gold += (currentProduction * woolValue) * (currentHealth / healthMod);
            cngProduction = Random.Range(-.1f, -.3f);
            currentProduction += cngProduction;


        }

        productionBar.SetSliderValue(currentProduction);

    }

    void SellAnimal()
    {
        if (sheep > 0)
        {
            gold += sheepValue * (currentHealth / healthMod);
            sheep -= 1;
        }
        
    
    }


    void EndTurn()
    {
        if (currentAge <= 4)
        {
            cngHealth = (Random.Range(1, 6));
            currentHealth += cngHealth;

            cngBreeding = (Random.Range(.02f, .05f) * (currentHealth / healthMod));
            currentBreeding += cngBreeding;

            cngProduction = (Random.Range(.1f, .2f) * (currentHealth / healthMod));
            currentProduction += cngProduction;

        
        }

        if (currentAge > 4 && currentAge < 11)
        {
            cngHealth = (Random.Range(-1, -16));
            currentHealth += cngHealth;

            cngBreeding = (Random.Range(.05f, .2f) * (currentHealth / healthMod));
            currentBreeding += cngBreeding;

            cngProduction = (Random.Range(.15f, .25f) * (currentHealth / healthMod));
            currentProduction += cngProduction;


        }

        if (currentAge > 11)
        {
            cngHealth = (Random.Range(-10, -25));
            currentHealth += cngHealth;

            cngBreeding = (Random.Range(.01f, .15f) * (currentHealth / healthMod));
            currentBreeding += cngBreeding;

            cngProduction = (Random.Range(.01f, .1f) * (currentHealth / healthMod));
            currentProduction += cngProduction;


        }

        healthBar.SetSliderValue(currentHealth);
        breedingBar.SetSliderValue(currentBreeding);
        productionBar.SetSliderValue(currentProduction);

        currentAge += 1;




    }





}
