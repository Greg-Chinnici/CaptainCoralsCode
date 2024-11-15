using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceX : MonoBehaviour
{
    [SerializeField] private float distance = 0;

    [SerializeField] private float speed = 1.0f;
    

    private float originalxPos;
    
    // Start is called before the first frame update
    void Start()
    {
        originalxPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        
            pos.x = originalxPos + (Mathf.Sin(Time.time * speed) * distance);

            transform.position = pos;
    }
}
