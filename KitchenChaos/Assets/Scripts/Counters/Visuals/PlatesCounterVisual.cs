using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform platePrefab, counterTopPoint;
    [SerializeField] private float spawnPointYOffset;
    private List<GameObject> spawnedPlatesList;

    private void Awake(){
        spawnedPlatesList = new List<GameObject>();
    }

    private void Start(){
        platesCounter.OnSpawnPlate += PlatesCounter_OnSpawnPlate;
        platesCounter.OnRemovePlate += PlatesCounter_OnRemovePlate;
    }

    private void PlatesCounter_OnRemovePlate(object sender, EventArgs e)
    {
        GameObject lastPlateInList = spawnedPlatesList[spawnedPlatesList.Count-1];
        spawnedPlatesList.Remove(lastPlateInList);
        Destroy(lastPlateInList);
    }

    private void PlatesCounter_OnSpawnPlate(object sender, EventArgs e)
    {
        Transform spawnedPlateVisual = Instantiate(platePrefab, counterTopPoint);
        spawnedPlateVisual.localPosition = new Vector3(0f, spawnPointYOffset * spawnedPlatesList.Count, 0f);
        spawnedPlatesList.Add(spawnedPlateVisual.gameObject);
    }
}
