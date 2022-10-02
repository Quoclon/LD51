using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // ~TEST TRANSITION
    [Header("Transition Timer")]
    public float fadeDuration;

    [Header("Turn Management")]
    public TimerManager timeManager;
    public GamePhase currentGamePhase;
    private bool loadNextPhase;
    private bool isFadingTransiton;

    [Header("Canvas Transition")]
    public CanvasGroup transitionCanvas;

    [Header("Prefabs")]
    public GameObject chickenPrefab;
    public GameObject sheepPrefab;
    public GameObject cowPrefab;

    [Header("Spawning")]
    public Transform defaultSpawnPos;

    [Header("Tracking Animals")]
    public List<GameObject> animalList;
    public List<GameObject> tempPerRoundSpawnList;
    public List<GameObject> tempAnimalDeathList;

    [Header("Economy - Produce")]
    public float eggValue;
    public float woolValue;
    public float milkValue;

    [Header("Economy - Produce")]
    public float chickenValue;
    public float sheepValue;
    public float cowValue;

    [Header("Spawning Testing")]
    public int testingNumSpawn;

    [Header("Spawning Num of Prefabs")]
    public int chickenToSpawn;
    public int sheepToSpawn;
    public int cowToSpawn;

    [Header("Inventory")]
    public float gold;
    public float eggs;
    public float wool;
    public float milk;

    [Header("UI Elements")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI phaseText;

 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < testingNumSpawn; i++)
        {
            SpawnAnimal(sheepPrefab);
        }

        // Start with Selection Scene, Load Next Scene False
        isFadingTransiton = true;
        loadNextPhase = false;

        // Phase Detauls
        currentGamePhase = GamePhase.Selection;
        phaseText.text = "Selection";
        goldText.text = "0";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OutcomesPhase();

        //TransitionFade(transitionCanvas, transitionCanvas.alpha, 1);
        //if(Input.GetKeyDown(KeyCode.P))
            //StartCoroutine(ToggleTransition(transitionCanvas, 1, 0, fadeDuration));

        if (timeManager.roundOver)
        {
            StartCoroutine(ToggleTransition(transitionCanvas, 0.5f, 1, fadeDuration));
            timeManager.roundOver = false;
        }

    }

    IEnumerator ToggleTransition(CanvasGroup _transitionCanvas, float start, float end, float _duration)
    {
        float counter = 0f;
        while (counter < _duration)
        {
            counter += Time.deltaTime;
            _transitionCanvas.alpha = Mathf.Lerp(start, end, counter / _duration);

            // If we're moving back to Alpha 0, we're about to return into the game
            if (end == 0 && _transitionCanvas.alpha == 0)
            {
                NextPhase();
            }

            // Transitin Back to Scene
            if (end == 1 && _transitionCanvas.alpha == 1)
            {
                StartCoroutine(ToggleTransition(_transitionCanvas, 1, 0, fadeDuration));
            }

            yield return null;
        }
    }

    public void NextPhase()
    {
        switch (currentGamePhase)
        {
            case GamePhase.Selection:              
                // Next Phase/Text
                OutcomesPhase();
                phaseText.text = "Outcomes";
                currentGamePhase = GamePhase.Outcomes;
                timeManager.StartTimer(timeManager.timerMax / 2);
                break;

            case GamePhase.Outcomes:
                // Next Phase/Text
                SelectAnimalPhase();
                phaseText.text = "Selection";            
                currentGamePhase = GamePhase.Selection;
                timeManager.StartTimer(timeManager.timerMax);
                break;

            case GamePhase.EndTurn:
                phaseText.text = "EndTurn";
                //EndTurnPhase();
                //currentGamePhase = GamePhase.Selection;
                //currentGamePhase = GamePhase.Events;
                break;

            case GamePhase.Events:
                phaseText.text = "Events";
                //currentGamePhase = GamePhase.Upgrades;
                break;

            case GamePhase.Upgrades:
                phaseText.text = "Upgrdaes";
                //currentGamePhase = GamePhase.Selection;
                break;

            case GamePhase.Transition:
                phaseText.text = "Transition";
                //currentGamePhase = GamePhase.Outcomes;
                break;
            default:
                break;
        }

        Debug.Log("NextPhase Started: " + currentGamePhase);
    }

    public void SelectAnimalPhase()
    {
        foreach (var animal in animalList)
        {
            AnimalStats animalStats = animal.GetComponent<AnimalStats>();
            AnimalController animalController = animal.GetComponent<AnimalController>();
            animalController.currentBuilding = Buildings.None;
            animalController.SelectUnit(false);          
        }

        // Reset Animals to the field
        ResetAnimalsToField();


    }

    #region Outcome Phase
    public void OutcomesPhase()
    {

        if (animalList.Count <= 0)
            Debug.Log("YOU LOSE");

        // Reset Animals to the field
        ResetAnimalsToField();

        foreach (var animal in animalList)
        {
            AnimalStats animalStats = animal.GetComponent<AnimalStats>();
            AnimalController animalController = animal.GetComponent<AnimalController>();
            Debug.Log("Ran: " + animalStats.animalName.text);

            switch (animalController.currentBuilding)
            {
                case Buildings.None:
                    // DO NOTHING
                    break;
                case Buildings.Breed:
                    Breed(animalStats);
                    break;
                case Buildings.Feed:
                    Feed(animalStats);
                    break;
                case Buildings.Produce:
                    Produce(animalStats);
                    break;
                case Buildings.Sell:
                    Sell(animalStats);
                    break;
                default:
                    break;
            }
        }


        // ~ Should we embed the EndTurnPhase here or it's own Phase
        foreach (var animal in animalList)
        {
            AnimalStats animalStats = animal.GetComponent<AnimalStats>();
            EndTurnPhase(animalStats);
        }

        // Handle "Death" and "Selling" from 'Sell' and 'EndTurn'
        foreach (var animal in tempAnimalDeathList)
        {
            if (animalList.Contains(animal))
            {
                animalList.Remove(animal);
            }
        }

        foreach (var animal in tempAnimalDeathList)
        {
            Destroy(animal.gameObject);
        }



        // Spawning Animals
        HandleSpawningAnimals(sheepToSpawn, cowToSpawn, chickenToSpawn);
        ResetAnimalToSpawnNumbers();

        // Gold Output
        goldText.text = gold.ToString("F0");
        //Debug.Log("Gold After Round: " + gold);
    }

   
    public void ResetAnimalToSpawnNumbers()
    {
        sheepToSpawn = 0;
        cowToSpawn = 0;
        chickenToSpawn = 0;
    }

    public void HandleSpawningAnimals(int sheepToSpawn, int cowToSpawn, int chickenToSpawn )
    {
        for (int i = 0; i < sheepToSpawn; i++)
        {
            SpawnAnimal(sheepPrefab);
        }

        for (int i = 0; i < cowToSpawn; i++)
        {
            SpawnAnimal(cowPrefab);
        }

        for (int i = 0; i < chickenToSpawn; i++)
        {
            SpawnAnimal(chickenPrefab);
        }
    }

    public void Feed(AnimalStats animal)
    {
        // Young
        if (animal.currentAge <= animal.youngAge)
        {
            animal.cngHealth = Random.Range(animal.youngStats.FeedOutcomeMin, animal.youngStats.FeedOutcomeMax);
            animal.health += animal.cngHealth;
        }

        // Adult
        else if (animal.currentAge > animal.youngAge && animal.currentAge < animal.adultAge)
        {
            animal.cngHealth = Random.Range(animal.adultStats.FeedOutcomeMin, animal.adultStats.FeedOutcomeMax);
            animal.health += animal.cngHealth;
        }

        // Senior
        else if (animal.currentAge >= animal.adultAge)
        {
            animal.cngHealth = Random.Range(animal.seniorStats.FeedOutcomeMin, animal.seniorStats.FeedOutcomeMax);
            animal.health += animal.cngHealth;
        }

        // Cap Health
        if (animal.health > animal.healthMax)
        {
            animal.health = animal.healthMax;
        }

        // Adjust SLider
        animal.healthBar.SetSliderValue(animal.health);
    }

    public void Breed(AnimalStats animal)
    { 
        if (Random.Range(0f, 1f) < animal.breed)
        {
            switch (animal.animalType)
            {
                case AnimalType.Sheep:
                    sheepToSpawn++;
                    break;
                case AnimalType.Cow:
                    cowToSpawn++;
                    break;
                case AnimalType.Chicken:
                    chickenToSpawn++;
                    break;
                default:
                    break;
            }
        }
        
        if (animal.currentAge >= animal.youngAge)
        {     
            animal.cngBreeding = Random.Range(animal.youngStats.BreedOutcomeMin, animal.youngStats.BreedOutcomeMax);
            animal.breed += animal.cngBreeding;
            animal.breedingBar.SetSliderValue(animal.breed);
            return;
        }

        if (animal.currentAge > animal.adultAge)
        {
            animal.cngBreeding = Random.Range(animal.adultStats.BreedOutcomeMin, animal.adultStats.BreedOutcomeMax);
            animal.breed += animal.cngBreeding;
            animal.breedingBar.SetSliderValue(animal.breed);
            return;
        }

    }
    
    public void Produce(AnimalStats animal)
    {
        if (animal.currentAge < animal.adultAge)
        {
            gold += (animal.produce * woolValue) * (animal.health / animal.healthMod);
            animal.cngProduction = Random.Range(-.05f, -.2f);
            animal.produce += animal.cngProduction;
        }

        else if (animal.currentAge >= animal.adultAge)
        {
            gold += (animal.produce * woolValue) * (animal.health / animal.healthMod);
            animal.cngProduction = Random.Range(-.1f, -.3f);
            animal.produce += animal.cngProduction;
        }

        animal.productionBar.SetSliderValue(animal.produce);
    }

    public void Sell(AnimalStats animal)
    {
        switch (animal.animalType)
        {
            case AnimalType.Chicken:
                gold += chickenValue * animal.health / 100;
                break;
            case AnimalType.Sheep:
                gold += sheepValue * animal.health / 100;
                break;
            case AnimalType.Cow:
                gold += cowValue * animal.health / 100;
                break;
            default:
                break;
        }

        Debug.Log("Animal Sold");
        tempAnimalDeathList.Add(animal.gameObject);
    }
    #endregion


    public void EndTurnPhase(AnimalStats animal)
    {
        if (animal.currentAge <= animal.youngAge)
        {
            animal.cngHealth = Random.Range(animal.youngStats.HealthChangeEndTurnMin, animal.youngStats.HealthChangeEndTurnMax);
            animal.cngBreeding = Random.Range(animal.youngStats.BreedEndTurnMin, animal.youngStats.BreedEndTurnMax) * (animal.health / animal.healthMod);
            animal.cngProduction = Random.Range(animal.youngStats.ProduceEndTurnMin, animal.youngStats.ProduceEndTurnMax) * (animal.health / animal.healthMod);
        }

        else if (animal.currentAge > animal.youngAge && animal.currentAge < animal.adultAge)
        {
            animal.cngHealth = Random.Range(animal.adultStats.HealthChangeEndTurnMin, animal.adultStats.HealthChangeEndTurnMax);
            animal.cngBreeding = Random.Range(animal.adultStats.BreedEndTurnMin, animal.adultStats.BreedEndTurnMax) * (animal.health / animal.healthMod);
            animal.cngProduction = Random.Range(animal.adultStats.ProduceEndTurnMin, animal.adultStats.ProduceEndTurnMax) * (animal.health / animal.healthMod);
        }

        else if (animal.currentAge >= animal.adultAge)
        {
            animal.cngHealth = Random.Range(animal.seniorStats.HealthChangeEndTurnMin, animal.seniorStats.HealthChangeEndTurnMax);
            animal.cngBreeding = Random.Range(animal.seniorStats.BreedEndTurnMin, animal.seniorStats.BreedEndTurnMax) * (animal.health / animal.healthMod);
            animal.cngProduction = Random.Range(animal.seniorStats.ProduceEndTurnMin, animal.seniorStats.ProduceEndTurnMax) * (animal.health / animal.healthMod);
        }

        // Update Stats
        animal.health += animal.cngHealth;
        animal.breed += animal.cngBreeding;
        animal.produce += animal.cngProduction;

        // Update Bars
        animal.healthBar.SetSliderValue(animal.health);
        animal.breedingBar.SetSliderValue(animal.breed);
        animal.productionBar.SetSliderValue(animal.produce);

        animal.currentAge += 1;

        // Animal 'death' list (same as sell list for now, just dissapear)
        if (animal.health <= 0)
        {
            Debug.Log("Animal Died");
            tempAnimalDeathList.Add(animal.gameObject);
        }

    }

    public void ResetAnimalsToField()
    {
        foreach (var animal in animalList)
        {
            AnimalController animalController = animal.GetComponent<AnimalController>();
            animalController.SetAnimalVisuals(true);
        }
    }


    public void EventPhase()
    {

    }


    public void UpgradesPhase()
    {

    }

    public void SpawnAnimal(GameObject prefab)
    {
        Vector3 randomSpawnPosition;
        randomSpawnPosition = new Vector3(defaultSpawnPos.position.x + Random.Range(-8, 8), defaultSpawnPos.position.y, defaultSpawnPos.position.z + Random.Range(-6, 6));
        GameObject spawnedPrefab = Instantiate(prefab, randomSpawnPosition, Quaternion.identity);
        animalList.Add(spawnedPrefab);
        //tempPerRoundSpawnList.Add(spawnedPrefab);
    }

}

public enum GamePhase
{
    Selection,
    Outcomes,
    EndTurn,
    Events,
    Upgrades,
    Transition
}
