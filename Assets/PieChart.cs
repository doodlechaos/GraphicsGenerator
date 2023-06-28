using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class PieChart : MonoBehaviour
{
    public Canvas canvas;
    public Transform Purgatory; 

    public Sprite circleSprite; 

    [SerializeField] private List<float> values = new List<float>();

    [SerializeField] private List<Image> chartImages = new List<Image>();

    [SerializeField] private bool isDirty = true;

    private Color nextColor = Color.white; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDirty)
            return;

        SetImageFillValues();

    }

    public void AddValue(float value, Color color)
    {
        values.Add(value); 
        nextColor = color;
        isDirty = true; 
    }

    private void SetImageFillValues()
    {
        if (values.Count <= 0)
        {
            GetComponent<Image>().enabled = true;
        }
        else
            GetComponent<Image>().enabled = false; 


        //Make sure we have one image for each value
        while(chartImages.Count < values.Count)
        {
            GameObject newImage = new GameObject();
            var img = newImage.AddComponent<Image>();
            img.sprite = circleSprite;
            img.type = Image.Type.Filled;
            img.fillMethod = Image.FillMethod.Radial360;
            img.color = nextColor; 
            newImage.transform.position = transform.position;
            newImage.transform.rotation = transform.rotation;
            newImage.transform.localScale = transform.localScale;
            newImage.transform.SetParent(canvas.transform);
            newImage.transform.SetAsFirstSibling();

            chartImages.Add(img);
        }

        //If we have too many, destroy the extra
        while(chartImages.Count > values.Count)
        {
            var img = chartImages[chartImages.Count - 1];
            img.transform.SetParent(Purgatory);
            img.gameObject.SetActive(false); 
            chartImages.Remove(img); 
        }


        float totalPercent = 0;
        for(int i = 0; i < values.Count; i++)
        {
            totalPercent += FindPercentages(values, i);
            chartImages[i].fillAmount = totalPercent;
        }
        isDirty = false;

    }

    private float FindPercentages(List<float> values, int index)
    {
        float valuesTotal = 0;
        for(int i = 0; i < values.Count; i++)
        {
            valuesTotal += values[i];
        }
        return values[index] / valuesTotal; 
    }
}
