using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController inst;
    public Vector3 Target;

    [SerializeField] private float lerpSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
    }

    public void SetTarget(Vector3 _target)
    {
        Target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;

        Vector3 nextPos = Vector3.Lerp(transform.position, Target, lerpSpeed * Time.deltaTime);
        transform.position = new Vector3(nextPos.x, nextPos.y, transform.position.z); 
    }
}
