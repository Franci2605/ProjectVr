using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBoxAndScore_SC : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] Vector3 spawnPointer;

    private void Start()
    {
        SpawnBox();
    }

    private void SpawnBox()
    {
        GameObject tempObjectSpawn = Instantiate(box,spawnPointer,Quaternion.Euler(0,90,0));
        CheckBox_SC tempCheckBox = tempObjectSpawn.GetComponent<CheckBox_SC>();
        tempCheckBox.managerBoxAndScore = this;
    }

    public void BoxComplete()
    {
        StartCoroutine(BoxCompleteIEnumerator());
    }

    public IEnumerator BoxCompleteIEnumerator()
    {
        yield return new WaitForSeconds(3);
        //animation
        //add point
        SpawnBox();
        print("point");
    }


}
