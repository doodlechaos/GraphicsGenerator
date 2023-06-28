using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTarget : MonoBehaviour
{

    public PieChart pieChart;
    public AudioClip updateChart; 

    public void ReceiveArrow(Color arrowColor)
    {
        if (pieChart == null)
            return;

        pieChart.AddValue(1, arrowColor);
        AudioController.inst.PlaySound(updateChart, GraphicsController.inst.pitch, GraphicsController.inst.pitch);         
    }
}
