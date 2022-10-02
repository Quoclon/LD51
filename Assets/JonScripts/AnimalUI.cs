using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalUI : MonoBehaviour
{

    public float speed;
    public float healthMax;
    public float healthMin;
    public float health;

    public float breedMax;
    public float breedMin;
    public float breed;

    public float produceMax;
    public float produceMin;
    public float produce;

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
        health = healthMin;
        healthBar.SetSliderValueMax(100);
        healthBar.SetSliderValue(health);
        breed = breedMin;
        breedingBar.SetSliderValueMax(.5f);
        breedingBar.SetSliderValue(breed);
        produce = produceMin;
        productionBar.SetSliderValueMax(1);
        productionBar.SetSliderValue(produce);

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

    }

    void Movement()
    {
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
            health += cngHealth;

        }

        if (currentAge >= 11)
        {

            cngHealth = Random.Range(5, 21);
            health += cngHealth;
        }

        if (currentAge > 4 && currentAge < 11)
        {
            cngHealth = Random.Range(10, 31);
            health += cngHealth;

        }

        if (health > 100)
        { 
         health = 100;
        }

        healthBar.SetSliderValue(health);
    }

    void Breeding()
    {

        if (currentAge >= 11)
        {
            if (Random.Range(0, 1) < breed)
            {
                //Need Instanciate Code
                sheep += 1;
            }

            cngBreeding = Random.Range(-.2f, -.45f);
            breed += cngBreeding;
        }

        if (currentAge < 11)
        {
            if (Random.Range(0, 1) < breed)
            {
                //Need Instanciate Code
                sheep += 1;
            }

            cngBreeding = Random.Range(-.35f, -.5f);
            breed += cngBreeding;

        }

        breedingBar.SetSliderValue(breed);
    }


    void Production()
    {
        if (currentAge < 11)
        {
            gold += (produce * woolValue) * (health / healthMod);
            cngProduction = Random.Range(-.05f, -.2f);
            produce += cngProduction;
        
        }

        if (currentAge >= 11)
        {
            gold += (produce * woolValue) * (health / healthMod);
            cngProduction = Random.Range(-.1f, -.3f);
            produce += cngProduction;
        }

        productionBar.SetSliderValue(produce);
    }

    void SellAnimal()
    {
        if (sheep > 0)
        {
            gold += sheepValue * (health / healthMod);
            sheep -= 1;
        }         
    }

    void EndTurn()
    {
        if (currentAge <= 4)
        {
            cngHealth = (Random.Range(1, 6));
            health += cngHealth;

            cngBreeding = (Random.Range(.02f, .05f) * (health / healthMod));
            breed += cngBreeding;

            cngProduction = (Random.Range(.1f, .2f) * (health / healthMod));
            produce += cngProduction;       
        }

        if (currentAge > 4 && currentAge < 11)
        {
            cngHealth = (Random.Range(-1, -16));
            health += cngHealth;

            cngBreeding = (Random.Range(.05f, .2f) * (health / healthMod));
            breed += cngBreeding;

            cngProduction = (Random.Range(.15f, .25f) * (health / healthMod));
            produce += cngProduction;
        }

        if (currentAge > 11)
        {
            cngHealth = (Random.Range(-10, -25));
            health += cngHealth;

            cngBreeding = (Random.Range(.01f, .15f) * (health / healthMod));
            breed += cngBreeding;

            cngProduction = (Random.Range(.01f, .1f) * (health / healthMod));
            produce += cngProduction;
        }

        healthBar.SetSliderValue(health);
        breedingBar.SetSliderValue(breed);
        productionBar.SetSliderValue(produce);

        currentAge += 1;
    }
}
