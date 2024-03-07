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
    [SerializeField] List<Vector3> spawnPointer;


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
    }

    public GameObject GetShoes(string layer)
    {
        switch (layer)
        {
            case "Blue":
                return shoesBlue[0];
            case "Yellow":
                return shoesYellow[0];
            case "Red":
                return shoesRed[0];
            case "Grey":
                return shoesGrey[0];
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

    //IEnumerator SpawnShoes()
    //{

    //}

}
