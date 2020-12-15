using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Fruit Data",menuName ="Fruit Data")]

public class FruitData : ScriptableObject
{
    public float maxDistanceWithinBasket;

    public GameObject fruitClickParticles;
    public GameObject wrongParticles;
    public GameObject correctParticles;
    public GameObject correctBasket;
    public AnimationCurve returnFruitToInitialPos;
}
