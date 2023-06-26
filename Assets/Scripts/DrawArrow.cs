using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DrawArrow : MonoBehaviour
{
    public LineRenderer lineRenderer; 
    public Transform startTransform; 
    public Transform endTransform;


    [SerializeField] private float DrawDuration;

    [SerializeField] private float StartSeparation;
    [SerializeField] private float EndSeparation;

    [SerializeField] private bool IsDrawing;

    // Update is called once per frame
    void Update()
    {
        if (IsDrawing)
        {
            SetLinePositions(); 
        }
    }

    public void SetLinePositions()
    {
        Vector3 startPos = startTransform.position + (endTransform.position - startTransform.position).normalized * StartSeparation;
        Vector3 endPos = endTransform.position + (startTransform.position - endTransform.position).normalized * EndSeparation;

        //Vector3 arrow1 = new 

        //Vector3[] positions = new Vector3[] { startPos, endPos }; 
        //lineRenderer.SetPositions(positions);
    }

    public void StartDrawing()
    {

    }

    public void StopDrawing()
    {

    }


}
