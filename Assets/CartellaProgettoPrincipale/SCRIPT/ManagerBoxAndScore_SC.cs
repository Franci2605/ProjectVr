using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerBoxAndScore_SC : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] Transform spawnPointer;
    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] TextMeshProUGUI textTimer;
    [SerializeField] float maxTimerInSecond;
    public bool timerOn = false;
    int currentScore;

    [Tooltip("Set generic variable for gameplay")]
    [SerializeField] int timeForSpawn_NewBox;
    [SerializeField] int amountScoreShoes_Equal;
    [SerializeField] int amountScoreShoes_NoEqual;



    private void Start()
    {
        SpawnBox();

        timerOn = true;
    }

    private void SpawnBox()
    {
        GameObject tempObjectSpawn = Instantiate(box,spawnPointer.transform.position,Quaternion.Euler(0,90,0),spawnPointer);
        CheckBox_SC tempCheckBox = tempObjectSpawn.GetComponent<CheckBox_SC>();
        tempCheckBox.managerBoxAndScore = this;
    }

    public void BoxComplete(bool equalLayer)
    {
        currentScore = equalLayer ==  true ? currentScore+50 : currentScore+25;
        textScore.text = currentScore.ToString();

        StartCoroutine(BoxCompleteIEnumerator());
    }

    private void Update()
    {
        if(timerOn)
        {
            if(maxTimerInSecond > 0) 
            { 
               maxTimerInSecond -= Time.deltaTime;
               updateTimer(maxTimerInSecond);
            }
            else
            {
                maxTimerInSecond = 0;
                timerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(maxTimerInSecond / 60);
        float seconds = Mathf.FloorToInt(maxTimerInSecond % 60);

        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
