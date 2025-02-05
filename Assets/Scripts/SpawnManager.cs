using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] carPrefabs;
    
    // Posiciones de spawn para los carriles originales
    private float spawnXOriginal = 325.0f;
    private float[] spawnZPositionsOriginal = { 23.0f, 36.5f };
    
    // Posiciones de spawn para los carriles en sentido contrario
    private float spawnXOpposite = 673.0f;
    private float[] spawnZPositionsOpposite = { 74.6f, 61.6f };

    void Start()
    {
        // Inicia las corrutinas para ambos sentidos
        StartCoroutine(SpawnCars());
        StartCoroutine(SpawnCarsOpposite());
    }

    private IEnumerator SpawnCars()
    {
        while (true)
        {
            // Tiempo aleatorio entre 0.5 y 2.5 segundos para carriles originales
            float randomDelay = Random.Range(0.5f, 2.5f);
            yield return new WaitForSeconds(randomDelay);

            // Selección aleatoria de carril
            float spawnPosZ = spawnZPositionsOriginal[Random.Range(0, spawnZPositionsOriginal.Length)];
            Vector3 spawnPos = new Vector3(spawnXOriginal, 0, spawnPosZ);
            
            // Selección aleatoria de un coche
            int carIndex = Random.Range(0, carPrefabs.Length);
            
            // Instancia el coche en la posición seleccionada
            Instantiate(carPrefabs[carIndex], spawnPos, carPrefabs[carIndex].transform.rotation);
        }
    }

    private IEnumerator SpawnCarsOpposite()
    {
        while (true)
        {
            // Tiempo aleatorio entre 0.5 y 2.5 segundos para carriles opuestos
            float randomDelay = Random.Range(0.5f, 2.5f);
            yield return new WaitForSeconds(randomDelay);

            // Selección aleatoria de carril
            float spawnPosZ = spawnZPositionsOpposite[Random.Range(0, spawnZPositionsOpposite.Length)];
            Vector3 spawnPos = new Vector3(spawnXOpposite, 0, spawnPosZ);
            
            // Selección aleatoria de un coche
            int carIndex = Random.Range(0, carPrefabs.Length);
            
            // Instancia el coche en la posición seleccionada
            Quaternion spawnRotation = Quaternion.Euler(0, 270, 0);
            Instantiate(carPrefabs[carIndex], spawnPos, spawnRotation);
        }
    }
}
