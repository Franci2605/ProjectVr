using System.Collections;
using TMPro;
using UnityEngine;

public class ManagerBoxAndScore_SC : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] Vector3 spawnPointer;
    [SerializeField] TextMeshPro textScore;
    int currentScore;

    [Tooltip("Set generic variable for gameplay")]
    [SerializeField] int timeForSpawn_NewBox;
    [SerializeField] int amountScoreShoes_Equal;
    [SerializeField] int amountScoreShoes_NoEqual;



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

    public void BoxComplete(bool equalLayer)
    {
        currentScore = equalLayer ==  true ? currentScore+50 : currentScore+25;
        textScore.text = currentScore.ToString();

        StartCoroutine(BoxCompleteIEnumerator());
    }

    public IEnumerator BoxCompleteIEnumerator()
    {
        yield return new WaitForSeconds(timeForSpawn_NewBox);
        //animation
        //add point
        SpawnBox();
        print("point");
    }


}
