using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolShoes : MonoBehaviour
{
   Queue<GameObject> shoes = new Queue<GameObject>();
   [SerializeField] List<Vector3> spawnPointer;

    private void Start()
    {
        var list = GameObject.FindGameObjectsWithTag("Scarpa");
        foreach (var item in list)
        {
            shoes.Enqueue(item);
            item.gameObject.SetActive(false);
        }
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        for (int i = shoes.Count-1; i > 0; i--) 
        { 
            yield return new WaitForSeconds(1);
            var tempObject = shoes.Dequeue();
            tempObject.gameObject.SetActive(true);
        }
    }

}
