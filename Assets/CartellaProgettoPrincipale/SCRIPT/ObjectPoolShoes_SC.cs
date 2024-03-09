using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PoolShoes : MonoBehaviour
{
    [SerializeField] List<GameObject> shoesRed;
    [SerializeField] List<GameObject> shoesBlue;
    [SerializeField] List<GameObject> shoesYellow;
    [SerializeField] List<GameObject> shoesGrey;
    [SerializeField] List<Transform> spawnPointer;
    [SerializeField] GameObject baseShoes;
    [SerializeField] Transform containerShoes;
    public int timeSpawn;

    public int maxShoesRound;

    private int tempCountShoesSpawned;

    private void Start()
    {
        var list = GameObject.FindGameObjectsWithTag("Scarpa");
        foreach (var item in list)
        {
            switch (LayerMask.LayerToName(item.layer))
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

    public GameObject GetShoes(string layer)
    {
        GameObject tempShoes = null;
        switch (layer)
        {
            case "Blue":
                if(shoesBlue.Count == 0) { SpawnNewShoes(layer); }
                tempShoes = shoesBlue[0];
                shoesBlue.RemoveAt(0);
                return tempShoes;
            case "Yellow":
                if (shoesYellow.Count == 0) { SpawnNewShoes(layer); }
                tempShoes = shoesYellow[0];
                shoesYellow.RemoveAt(0);
                return tempShoes;
            case "Red":
                if (shoesRed.Count == 0) { SpawnNewShoes(layer); }
                tempShoes = shoesRed[0];
                shoesRed.RemoveAt(0);
                return tempShoes;
            case "Grey":
                if (shoesGrey.Count == 0) { SpawnNewShoes(layer); }
                tempShoes = shoesGrey[0];
                shoesGrey.RemoveAt(0);
                return tempShoes;
            default:
                Debug.LogError("layer non disponibile");
                return null;
        }
    }

    public void ReturnShoes(GameObject item)
    {
        switch (LayerMask.LayerToName(item.layer))
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
    }

    //call this when the list is empty
    private void SpawnNewShoes(string layer)
    {
        var tempGameObject= Instantiate(baseShoes,containerShoes.transform.position,Quaternion.Euler(0,0,0),containerShoes);
        tempGameObject.layer = LayerMask.NameToLayer(layer);

        switch (layer)
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
    }

    IEnumerator SpawnShoes()
    {
        while(true) 
        { 
           yield return new WaitForSeconds(timeSpawn);

           LogicChooseShoes();
        
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
