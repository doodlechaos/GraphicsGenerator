using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsController : MonoBehaviour
{
    public static GraphicsController inst;
    public GameObject PlayerPrefab;
    public ArrowTarget arrowTarget;
    public GameObject ArrowPrefab;

    private float colorSeek = 0;

    public AudioClip spawnPlayer;
    public AudioClip spawnArrow;

    public float pitch = 0.8f;

    [SerializeField] public Vector3 separation;

    public Vector3 spawnPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnPlayer();
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnPlayerAndController();
        }
    }

    public void SpawnPlayer()
    {
        Color color = GetRandomBrightColor();

        //Spawn the player,
        GameObject newPlayer = Instantiate(PlayerPrefab);
        newPlayer.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        newPlayer.GetComponent<SpriteRenderer>().color = color;

        AudioController.inst.PlaySound(spawnPlayer, pitch, pitch); 

        //Spawn a 
        GameObject newArrow = Instantiate(ArrowPrefab);
        DrawArrow da = newArrow.GetComponent<DrawArrow>();
        da.disable = false; 
        da.startTransform = newPlayer.transform;
        da.arrowTarget = arrowTarget;
        da.animateTimer = 0;
        da.ssr.color = color;

        AudioController.inst.PlaySound(spawnArrow, pitch, pitch);

        pitch += 0.03f; 
    }

    public void SpawnPlayerAndController()
    {
        Color color = GetRandomBrightColor();

        //Spawn the player,
        GameObject newPlayer = Instantiate(PlayerPrefab);
        newPlayer.transform.position = new Vector3(newPlayer.transform.position.x, spawnPos.y, newPlayer.transform.position.z);
        newPlayer.GetComponent<SpriteRenderer>().color = color;

        //Spawn the controller
        GameObject newController = Instantiate(arrowTarget.gameObject);
        newController.transform.position = new Vector3(newController.transform.position.x, spawnPos.y, newController.transform.position.z);
        AudioController.inst.PlaySound(spawnPlayer, pitch, pitch);

        //Spawn the arrow
        //Spawn a 
        GameObject newArrow = Instantiate(ArrowPrefab);
        DrawArrow da = newArrow.GetComponent<DrawArrow>();
        da.disable = false;
        da.startTransform = newPlayer.transform;
        da.arrowTarget = newController.GetComponent<ArrowTarget>();
        da.animateTimer = 0;
        da.ssr.color = color;

        AudioController.inst.PlaySound(spawnArrow, pitch, pitch);

        spawnPos += separation;
        CamController.inst.SetTarget(spawnPos); 
    }

    private Color GetRandomBrightColor()
    {
        colorSeek = (colorSeek + Random.Range(0.1f, 0.2f)) % 1;
        float hue = colorSeek; // Random hue value between 0 and 1
        float saturation = Random.Range(0.7f, 1f); // Random saturation value between 0.7 and 1
        float value = 1f; // Set the value to 1 for maximum brightness

        Color randomColor = Color.HSVToRGB(hue, saturation, value);
        randomColor.a = 1f; // Set the alpha value to 1 (fully opaque)

        return randomColor;
    }
}
