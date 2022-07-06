using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI[] scoreLabels;
    [SerializeField] private TextMeshProUGUI allLinesLabel;
    [SerializeField] private float animationDelay;

    public void DisplayStatisticsToUI()
    {
        nameLabel.text = "Hello, " + Player.playerName;
        for (int i = 0; i < scoreLabels.Length; i++)
            StartCoroutine(IncAnimation(scoreLabels[i], Player.bestScores[i]));

        allLinesLabel.text = Player.allLinesCount.ToString("### ### ### ##0 lines cleared");
    }

    IEnumerator IncAnimation(TextMeshProUGUI label, int value)
    {
        if(value != 0)
        {

            int temp = 0;
            int inc = value / 100;
            while (temp < value)
            {
                temp += inc;
                label.text = temp.ToString("### ### ### ##0");
                yield return new WaitForSeconds(animationDelay);
            }
        }
        else
            label.text = value.ToString();
    }
}
