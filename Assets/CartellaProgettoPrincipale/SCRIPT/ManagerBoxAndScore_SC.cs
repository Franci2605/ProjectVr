using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBoxAndScore_SC : MonoBehaviour
{
    [SerializeField] CheckBox_SC box;
    [SerializeField] Vector3 spawnPointer;

    private void Start()
    {
        SpawnBox();
    }

    private void SpawnBox()
    {
        Instantiate(box.gameObject,spawnPointer,Quaternion.Euler(0,90,0));
        box.managerBoxAndScore = this;
    }

    public void BoxComplete()
    {
        //animation
        //add point
        SpawnBox();
        print("point");
    }

}
