using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squish : MonoBehaviour
{
    [SerializeField] private float scale = 1.0f ;

    [SerializeField] private float speed = 1.0f ;

    [SerializeField] private float[] clampAt = new float[2];
 
    private float originalScale; 
    
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Sin(Time.time * speed) * scale * originalScale;
        localScale.x = Mathf.Clamp(localScale.x, clampAt[0], clampAt[1]);
        transform.localScale = new Vector3( localScale.x , transform.localScale.y , transform.localScale.z);
    }
}
