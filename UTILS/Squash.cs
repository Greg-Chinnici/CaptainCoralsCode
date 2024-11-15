using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : MonoBehaviour
{
    [SerializeField] private float scale = 1.0f ;

    [SerializeField] private float speed = 1.0f ;

    [SerializeField] private float[] clampAt = new float[2];
 
    private float originalScale; 
    
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sc = transform.localScale;
        sc.y = Mathf.Sin(Time.time * speed) * scale * originalScale;
        sc.y = Mathf.Abs(sc.y);
        sc.y = Mathf.Clamp(sc.y, clampAt[0], clampAt[1]);
        
        //Debug.Log(sc.y);
        
        transform.localScale = new Vector3( transform.localScale.x , sc.y , transform.localScale.z);
        
        //Debug.Log(transform.localScale);
    }
}


