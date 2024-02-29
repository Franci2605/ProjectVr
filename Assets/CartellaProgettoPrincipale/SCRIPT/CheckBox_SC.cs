using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox_SC : MonoBehaviour
{
    [SerializeField] PlacePoint placePointOne;
    [SerializeField] PlacePoint placePointTwo;
    [SerializeField] HingeJoint jointCheck;

    public bool CheckBoxIsFull()
    {
        if(placePointOne.placedObject != null && placePointTwo.placedObject != null) 
        {
            return true;
        }

        return false;
    }

    private void Update()
    {
        if(jointCheck != null) 
        {
            if(jointCheck.gameObject.transform.rotation.eulerAngles.z<5)
            {
                if(CheckBoxIsFull()) 
                {
                    print("win");
                }
            }
        }
    }
}
