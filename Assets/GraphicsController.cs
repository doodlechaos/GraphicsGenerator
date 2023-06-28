using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsController : MonoBehaviour
{

    public GameObject PlayerPrefab;
    public ArrowTarget arrowTarget;
    public GameObject ArrowPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnPlayer();
        }
    }

    public void SpawnPlayer()
    {
        Color color = GetRandomBrightColor();

        //Spawn the player,
        GameObject newPlayer = Instantiate(PlayerPrefab);
        newPlayer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        newPlayer.GetComponent<SpriteRenderer>().color = color; 

        //Spawn a 
        GameObject newArrow = Instantiate(ArrowPrefab);
        DrawArrow da = newArrow.GetComponent<DrawArrow>();
        da.disable = false; 
        da.startTransform = newPlayer.transform;
        da.arrowTarget = arrowTarget;
        da.animateTimer = 0;
        da.ssr.color = color; 

    }

    private Color GetRandomBrightColor()
    {
        float hue = Random.Range(0f, 1f); // Random hue value between 0 and 1
        float saturation = 1f; // Random saturation value between 0.7 and 1
        float value = 1f; // Set the value to 1 for maximum brightness

        Color randomColor = Color.HSVToRGB(hue, saturation, value);
        randomColor.a = 1f; // Set the alpha value to 1 (fully opaque)

        return randomColor;
    }
}
