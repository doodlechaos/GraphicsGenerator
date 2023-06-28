using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
public class DrawArrow : MonoBehaviour
{
    public SpriteShapeRenderer ssr; 
    public SpriteShapeController ssc; 
    public Transform startTransform; 
    public ArrowTarget arrowTarget;

    [SerializeField] private float DrawDuration;

    [SerializeField] private float StartSeparation;
    [SerializeField] private float EndSeparation;

    [SerializeField] public float stemWidth;
    [SerializeField] public float tipLength;
    [SerializeField] public float tipWidth;

    private float currStartSeparation;
    private float currEndSeparation;
    private float currStemWidth;
    private float currTipLength;
    private float currtipWidth;

    public float animateTimer;

    [SerializeField] public bool disable;

    private bool reachedTarget = false; 

    // Update is called once per frame
    void Update()
    {
        if (disable)
            return;

        if(animateTimer < DrawDuration)
        {
            float t = animateTimer / DrawDuration; 
            //Fade in 
            Color currColor = ssr.color;
            currColor.a = Mathf.Lerp(0, 1, t);
            ssr.color = currColor;

            float length = Vector3.Distance(startTransform.position, arrowTarget.transform.position);

            currEndSeparation = Mathf.Lerp(length - 2.5f, EndSeparation, t);
        }
        else
        {
            if (!reachedTarget)
            {
                reachedTarget = true;
                arrowTarget.ReceiveArrow(ssr.color);
            }
        }
        SetCornerPositions();
        animateTimer += Time.deltaTime; 
    }

    public void SetCornerPositions()
    {
        Vector3 direction = (arrowTarget.transform.position - startTransform.position).normalized;
        Vector3 tangent = Vector3.Cross(direction, Vector3.forward).normalized; 
        Vector3 startPos = startTransform.position + direction * StartSeparation;
        Vector3 endPos = arrowTarget.transform.position - direction * currEndSeparation;

        //Stem positions
        //Stem by start pos
        Vector3 rightBase = startPos + tangent * stemWidth;
        Vector3 leftBase = startPos - tangent * stemWidth; 

        Vector3 stemTopRight = endPos - (direction * tipLength) + tangent * stemWidth;
        Vector3 stemTopLeft = endPos - (direction * tipLength) - tangent * stemWidth;

        Vector3 tipOutRight = endPos - (direction * tipLength) + tangent * stemWidth + tangent * tipWidth;
        Vector3 tipOutLeft = endPos - (direction * tipLength) - tangent * stemWidth - tangent * tipWidth;

        Vector3 arrowTip = endPos;

        ssc.spline.SetPosition(0, leftBase);
        ssc.spline.SetPosition(1, rightBase);
        ssc.spline.SetPosition(2, stemTopRight);
        ssc.spline.SetPosition(3, tipOutRight);
        ssc.spline.SetPosition(4, arrowTip);
        ssc.spline.SetPosition(5, tipOutLeft);
        ssc.spline.SetPosition(6, stemTopLeft);

    }

}
