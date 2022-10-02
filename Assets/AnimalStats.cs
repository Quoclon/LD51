using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimalStats : MonoBehaviour
{
    [Header("Animal Type")]
    public AnimalType animalType;

    [Header("Health Mod")]
    public float healthMod;

    [Header("Health")]
    public float healthMax;
    public float health;
    public float healthMin;
    public float cngHealth;

    [Header("Feeding")]
    public float feedMax;
    public float feed;
    public float feedMin;

    [Header("Breeding")]
    public float breedMax;
    public float breed;
    public float breedMin;
    public float cngBreeding;

    [Header("Production")]
    public float produceMax;
    public float produceMin;
    public float produce;
    public float cngProduction;

    [Header("Age")]
    public float currentAge;

    [Header("Age Definitions")]
    public int youngAge;
    public int adultAge;
    public int seniorAge;

    [Header("Age Stats")]
    public AnimalAgeStats youngStats;
    public AnimalAgeStats adultStats;
    public AnimalAgeStats seniorStats;

    [Header("Animal Names")]
    public TextMeshProUGUI animalName;
    public List<string> animalNamesList;

    [Header("Slider Bars")]
    public BarScript healthBar;
    public BarScript breedingBar;
    public BarScript productionBar;

    // Start is called before the first frame update
    void Start()
    {
        
        SetupBars();
        SetupNames();
    }

    void SetupStats()
    {
        

    }

    void SetupNames()
    {
        int randomNumber = Random.Range(0, animalNamesList.Count);
        animalName.text = animalNamesList[randomNumber].ToString();
    }

    void SetupBars()
    {
        health = healthMin;
        healthBar.SetSliderValueMax(healthMax);
        healthBar.SetSliderValue(health);

        breed = breedMin;
        breedingBar.SetSliderValueMax(breedMax);
        breedingBar.SetSliderValue(breed);

        produce = produceMin;
        productionBar.SetSliderValueMax(produceMax);
        productionBar.SetSliderValue(produce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCurrentHealth()
    {

    }

    public void GetCurrentFeeding()
    {

    }

    public void GetCurrentBreeding()
    {

    }

    public enum AnimalType
    {
        Sheep,
        Cow,
        Chicken
    }
}
