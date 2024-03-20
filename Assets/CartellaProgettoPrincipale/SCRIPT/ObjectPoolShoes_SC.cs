using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolShoes : Singleton_SC<PoolShoes>
{
    [SerializeField] List<GameObject> ModelOne_V_1;
    [SerializeField] List<GameObject> ModelOne_V_2;
    [SerializeField] List<GameObject> ModelTwo_V_1;
    [SerializeField] List<GameObject> ModelTwo_V_2;
    [SerializeField] List<GameObject> ModelThree_V_1;
    [SerializeField] List<GameObject> ModelThree_V_2;
    [SerializeField] GameObject baseShoes;
    [SerializeField] List<GameObject> modelsShoes;
    [SerializeField] List<Transform> spawnPointer;
    [SerializeField] List<GameObject> AllShoes;
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
                case "ModelOne_V_1":
                    ModelOne_V_1.Add(item);
                    break;
                case "ModelOne_V_2":
                    ModelOne_V_2.Add(item);
                    break;
                case "ModelTwo_V_1":
                    ModelTwo_V_1.Add(item);
                    break;
                case "ModelTwo_V_2":
                    ModelTwo_V_2.Add(item);
                    break;
                case "ModelThree_V_1":
                    ModelThree_V_1.Add(item);
                    break;
                case "ModelThree_V_2":
                    ModelThree_V_2.Add(item);
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
            case "ModelOne_V_1":
                if(ModelOne_V_1.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = ModelOne_V_1[0];
                ModelOne_V_1.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "ModelOne_V_2":
                if (ModelOne_V_2.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = ModelOne_V_2[0];
                ModelOne_V_2.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "ModelTwo_V_1":
                if (ModelTwo_V_1.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = ModelTwo_V_1[0];
                ModelTwo_V_1.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "ModelTwo_V_2":
                if (ModelTwo_V_2.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = ModelTwo_V_2[0];
                ModelTwo_V_2.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "ModelThree_V_1":
                if (ModelThree_V_1.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = ModelThree_V_1[0];
                ModelThree_V_1.RemoveAt(0);
                tempShoes.transform.position = randomSpawnPosition();
                return tempShoes;
            case "ModelThree_V_2":
                if (ModelThree_V_2.Count == 0) { SpawnNewShoes(tag); }
                tempShoes = ModelThree_V_2[0];
                ModelThree_V_2.RemoveAt(0);
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
            case "ModelOne_V_1":
                ModelOne_V_1.Add(item);
                break;
            case "ModelOne_V_2":
                ModelOne_V_2.Add(item);
                break;
            case "ModelTwo_V_1":
                ModelTwo_V_1.Add(item);
                break;
            case "ModelTwo_V_2":
                ModelTwo_V_2.Add(item);
                break;
            case "ModelThree_V_1":
                ModelThree_V_1.Add(item);
                break;
            case "ModelThree_V_2":
                ModelThree_V_2.Add(item);
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
        int baseShoesIndex=0;

        switch (tag)
        {
            case "ModelOne_V_1":
                baseShoesIndex = 0;
                break;
            case "ModelOne_V_2":
                baseShoesIndex = 1;
                break;
            case "ModelTwo_V_1":
                baseShoesIndex = 2;
                break;
            case "ModelTwo_V_2":
                baseShoesIndex = 3;
                break;
            case "ModelThree_V_1":
                baseShoesIndex = 4;
                break;
            case "ModelThree_V_2":
                baseShoesIndex = 5;
                break;
            default:
                break;
        }

        GameObject tempGameObject= Instantiate(baseShoes,containerShoes.transform.position,Quaternion.Euler(0,0,0),containerShoes);
        tempGameObject.tag = tag;
        tempGameObject.GetComponent<MeshFilter>().mesh = modelsShoes[baseShoesIndex].GetComponent<MeshFilter>().sharedMesh;
        tempGameObject.GetComponent<MeshRenderer>().material = modelsShoes[baseShoesIndex].GetComponent<MeshRenderer>().sharedMaterial;
        tempGameObject.GetComponent<MeshCollider>().sharedMesh = modelsShoes[baseShoesIndex].GetComponent<MeshFilter>().sharedMesh;

        switch (tag)
        {
            case "ModelOne_V_1":
                ModelOne_V_1.Add(tempGameObject);
                break;
            case "ModelOne_V_2":
                ModelOne_V_2.Add(tempGameObject);
                break;
            case "ModelTwo_V_1":
                ModelTwo_V_1.Add(tempGameObject);
                break;
            case "ModelTwo_V_2":
                ModelTwo_V_2.Add(tempGameObject);
                break;
            case "ModelThree_V_1":
                ModelThree_V_1.Add(tempGameObject);
                break;
            case "ModelThree_V_2":
                ModelThree_V_2.Add(tempGameObject);
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

        int tempIndex = Random.Range(0, 6);
        string tempString;

        switch (tempIndex)
        {
            case 0:
                tempString= "ModelOne_V_1";
                break;
            case 1:
                tempString = "ModelOne_V_2";
                break;
            case 2:
                tempString = "ModelTwo_V_1";
                break;
            case 3:
                tempString = "ModelTwo_V_2";
                break;
            case 4:
                tempString = "ModelThree_V_1";
                break;
            case 5:
                tempString = "ModelThree_V_2";
                break;
            default:
                tempString = "";
                break;
        }

        GetShoes(tempString).SetActive(true);
    }

}
