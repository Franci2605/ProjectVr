using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox_SC : MonoBehaviour
{
    [SerializeField] PlacePoint placePointOne;
    [SerializeField] PlacePoint placePointTwo;
    [SerializeField] HingeJoint jointCheck;
    public ManagerBoxAndScore_SC managerBoxAndScore;
    private bool boxComplete = false;

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
                if(CheckBoxIsFull() && !boxComplete) 
                {
                    boxComplete = true;
                    managerBoxAndScore.BoxComplete();
                    Destroy(placePointTwo.placedObject.gameObject);
                    Destroy(placePointOne.placedObject.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
