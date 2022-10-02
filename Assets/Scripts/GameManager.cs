using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Turn Management")]
    public TimerManager timeManager;
    public GamePhase currentGamePhase;
    public bool loadNextPhase;
    public bool isFadingTransiton;
    public float transitionTimer;
    public float transitionTimerMax;

    [Header("Canvas Transition")]
    public CanvasGroup transitionCanvas;

    [Header("Prefabs")]
    public GameObject chickenPrefab;
    public GameObject cowPrefab;
    public GameObject sheepPrefab;

    [Header("Spawning")]
    public Transform defaultSpawnPos;

    [Header("Tracking Animals")]
    public List<GameObject> animalList;
    public List<GameObject> tempPerRoundSpawnList;
    public List<GameObject> tempAnimalDeathList;

    [Header("Inventory")]
    public float gold;
    public float wool;
    public float milk;
    public float eggs;


    [Header("Economy")]
    public float woolValue;
    public float milkValue;
    public float eggValue;

    [Header("Spawning Testing")]
    public int testingNumSpawn;

    [Header("Spawning Prefabs")]
    public int sheepToSpawn;
    public int cowToSpawn;
    public int chickenToSpawn;

    // ~TEST TRANSITION
    float fadeDuration = 4f;

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

        transitionTimerMax = 5f;
        transitionTimer = transitionTimerMax;

        currentGamePhase = GamePhase.Selection;

        //TransitionFade(transitionCanvas, transitionCanvas.alpha, 1);
        
        //StartCoroutine(ToggleTransition(transitionCanvas, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OutcomesPhase();

        //TransitionFade(transitionCanvas, transitionCanvas.alpha, 1);
        if(Input.GetKeyDown(KeyCode.P))
            StartCoroutine(ToggleTransition(transitionCanvas, 1, 0));

        if (timeManager.roundOver)
        {
            StartCoroutine(ToggleTransition(transitionCanvas, 0, 1));
        }

    }

    IEnumerator ToggleTransition(CanvasGroup _transitionCanvas, float start, float end)
    {
        float counter = 0f;
        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            _transitionCanvas.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            // If we're moving back to Alpha 0, we're about to return into the game
            if (end == 0 && _transitionCanvas.alpha == 0)
            {
                NextPhase();
                timeManager.StartTimer();
            }

            else if (end == 1 && _transitionCanvas.alpha == 1)
            {
                StartCoroutine(ToggleTransition(_transitionCanvas, 1, 0));
            }

            yield return null;

        }

    }

    public void NextPhase()
    {
        switch (currentGamePhase)
        {
            case GamePhase.Selection:
                currentGamePhase = GamePhase.Outcomes;
                break;
            case GamePhase.Outcomes:
                currentGamePhase = GamePhase.EndTurn;
                break;
            case GamePhase.EndTurn:
                currentGamePhase = GamePhase.Events;
                break;
            case GamePhase.Events:
                currentGamePhase = GamePhase.Upgrades;
                break;
            case GamePhase.Upgrades:
                currentGamePhase = GamePhase.Selection;
                break;
            case GamePhase.Transition:
                //currentGamePhase = GamePhase.Outcomes;
                break;
            default:
                break;
        }
        currentGamePhase++;
        Debug.Log("NextPhase Started: " + currentGamePhase);
        timeManager.StartTimer();
    }
  
    public void SelectAnimalPhase()
    {

    }

    #region Outcome Phase
    public void OutcomesPhase()
    {
        foreach (var animal in animalList)
        {
            AnimalStats animalStats = animal.GetComponent<AnimalStats>();
            Debug.Log("Ran: " + animalStats.animalName.text);

            int random = Random.Range(0, 6);
            if(random > 1)
                Feed(animalStats);

            if (random == 0)
                Breed(animalStats);

            if (random == 1)
                Produce(animalStats);
        }

        foreach (var animal in animalList)
        {
            AnimalStats animalStats = animal.GetComponent<AnimalStats>();
            EndTurnPhase(animalStats);
        }

        /*
        foreach (var spawnedAnimal in tempPerRoundSpawnList)
        {
            animalList.Add(spawnedAnimal);
        }
        */

        HandleSpawningAnimals(sheepToSpawn, cowToSpawn, chickenToSpawn);

        // Reset Temp Variables
        //tempPerRoundSpawnList.Clear();
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
                case AnimalStats.AnimalType.Sheep:
                    sheepToSpawn++;
                    break;
                case AnimalStats.AnimalType.Cow:
                    cowToSpawn++;
                    break;
                case AnimalStats.AnimalType.Chicken:
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

    public void Sell()
    {

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
