using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    public TextMeshProUGUI animalType1;
    public TextMeshProUGUI animalType2;
    public TextMeshProUGUI animalType3;
    public List<string> animalTypeList;

    public TextMeshProUGUI description1;
    public TextMeshProUGUI description2;
    public TextMeshProUGUI description3;
    public List<string> descriptionList;

    public TextMeshProUGUI increase1;
    public TextMeshProUGUI increase2;
    public TextMeshProUGUI increase3;

    public TextMeshProUGUI cost1;
    public TextMeshProUGUI cost2;
    public TextMeshProUGUI cost3;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        int randomNumber1 = Random.Range(0, animalTypeList.Count);
        animalType1.text = animalTypeList[randomNumber1].ToString();

        int randomNumber2 = Random.Range(0, descriptionList.Count);
        description1.text = descriptionList[randomNumber2].ToString();

        int randomNumber3 = Random.Range(20, 50);
        increase1.text = randomNumber3.ToString() + "%";

        int randomNumber4 = Random.Range(200, 1001);
        cost1.text = "$" + randomNumber4.ToString();

        int randomNumber5 = Random.Range(0, animalTypeList.Count);
        animalType2.text = animalTypeList[randomNumber5].ToString();

        int randomNumber6 = Random.Range(0, descriptionList.Count);
        description2.text = descriptionList[randomNumber6].ToString();

        int randomNumber7 = Random.Range(20, 50);
        increase2.text = randomNumber7.ToString() + "%";

        int randomNumber8 = Random.Range(200, 1001);
        cost2.text = "$" + randomNumber8.ToString();

        int randomNumber9 = Random.Range(0, animalTypeList.Count);
        animalType3.text = animalTypeList[randomNumber9].ToString();

        int randomNumber10 = Random.Range(0, descriptionList.Count);
        description3.text = descriptionList[randomNumber10].ToString();

        int randomNumber11 = Random.Range(20, 50);
        increase3.text = randomNumber11.ToString() + "%";

        int randomNumber12 = Random.Range(200, 1001);
        cost3.text = "$" + randomNumber12.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeButton1()
    { 
    
    
    }

    public void UpgradeButton2()
    {


    }

    public void UpgradeButton3()
    {


    }





}
