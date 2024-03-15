using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolShoes : Singleton_SC<PoolShoes>
{
    [SerializeField] List<GameObject> shoesRed;
    [SerializeField] List<GameObject> shoesBlue;
    [SerializeField] List<GameObject> shoesYellow;
    [SerializeField] List<GameObject> shoesGrey;
    [SerializeField] List<Transform> spawnPointer;
    [SerializeField] List<GameObject> AllShoes;
    [SerializeField] GameObject baseShoes;
    [SerializeField] Transform containerShoes;
    public int timeSpawn;

    public int maxShoesRound;

    private int tempCountShoesSpawned;



    private void Start()
    {
        foreach (var item in AllShoes)
        {
            switch (item.tag)
            {
                case "Blue":
                    shoesBlue.Add(item);
                    break;
                case "Yellow":
                    shoesYellow.Add(item);
                    break;
                case "Red":
                    shoesRed.Add(item);
                    break;
                case "Grey":
                    shoesGrey.Add(item);
                    break;
                default:
                    break;
            }
            item.gameObject.SetActive(false);
        }

        StartCoroutine(SpawnShoes());
    }

    public GameObject GetShoes(string tag)
    {
        GameObject tempShoes = null;
        switch (tag)
        {
            case "Blue":
                if(shoesBlue.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = shoesBlue[0];
                shoesBlue.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "Yellow":
                if (shoesYellow.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = shoesYellow[0];
                shoesYellow.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "Red":
                if (shoesRed.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = shoesRed[0];
                shoesRed.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "Grey":
                if (shoesGrey.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = shoesGrey[0];
                shoesGrey.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            default:
                Debug.LogError("layer non disponibile");
                return null;
        }
    }

    private Vector3 randomSpawnPosition()
    {
        return spawnPointer[Random.Range(0, spawnPointer.Count)].position;
    }

    public void ReturnShoes(GameObject item)
    {
        switch (item.tag)
        {
            case "Blue":
                shoesBlue.Add(item);
                break;
            case "Yellow":
                shoesYellow.Add(item);
                break;
            case "Red":
                shoesRed.Add(item);
                break;
            case "Grey":
                shoesGrey.Add(item);
                break;
            default:
                break;
        }

        item.SetActive(false);
        item.transform.position=containerShoes.transform.position;
    }

    //call this when the list is empty
    private void SpawnNewShoes(string tag)
    {
        var tempGameObject= Instantiate(baseShoes,containerShoes.transform.position,Quaternion.Euler(0,0,0),containerShoes);
        tempGameObject.tag = tag;

        switch (tag)
        {
            case "Blue":
                shoesBlue.Add(tempGameObject);
                break;
            case "Yellow":
                shoesYellow.Add(tempGameObject);
                break;
            case "Red":
                shoesRed.Add(tempGameObject);
                break;
            case "Grey":
                shoesGrey.Add(tempGameObject);
                break;
            default:
                break;
        }

        AllShoes.Add(tempGameObject);
    }

    IEnumerator SpawnShoes()
    {
        yield return new WaitForSeconds(timeSpawn);

        LogicChooseShoes();

        if(ManagerBoxAndScore_SC.Instance.timerOn)
        {
            StartCoroutine(SpawnShoes());
        }
        
    }

    private void LogicChooseShoes()
    {
        if(tempCountShoesSpawned> maxShoesRound)
        {
            tempCountShoesSpawned = 0;
        }

        int tempIndex = Random.Range(0, 4);
        string tempString;

        switch (tempIndex)
        {
            case 0:
                tempString= "Blue";
                break;
            case 1:
                tempString = "Yellow";
                break;
            case 2:
                tempString = "Red";
                break;
            case 3:
                tempString = "Grey";
                break;
            default:
                tempString = "";
                break;
        }

        GetShoes(tempString).SetActive(true);
    }

}
