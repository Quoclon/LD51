using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAgeStats : MonoBehaviour
{
    public AgeEnum age;

    //[Header("Health - Outcomes")]
    //public float HealthChangeFeedMin;
    //public float HealthChangeFeedMax;

    [Header("Feed - Outcomes")]
    public float FeedOutcomeMin;
    public float FeedOutcomeMax;

    [Header("Breed - Outcomes")]
    public float BreedOutcomeMin;
    public float BreedOutcomeMax;

    [Header("Produce - Outcomes")]
    public float ProduceOutcomeMin;
    public float ProduceOutcomeMax;


    [Header("Health - EndTurn")]
    public float HealthChangeEndTurnMin;
    public float HealthChangeEndTurnMax;

    [Header("Breed - EndTurn")]
    public float BreedEndTurnMin;
    public float BreedEndTurnMax;

    [Header("Produce - EndTurn")]
    public float ProduceEndTurnMin;
    public float ProduceEndTurnMax;

}

public enum AgeEnum
{
    Young,
    Adult,
    Senior
}
