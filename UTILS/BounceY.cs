using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceY : MonoBehaviour
{
    [SerializeField] private float distance = 0;

    [SerializeField] private float speed = 1.0f;
    
    [SerializeField] private bool onlyUp = false;

    private float originalYPos;
    
    // Start is called before the first frame update
    void Start()
    {
        originalYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (onlyUp)
        {
           pos.y = originalYPos + Mathf.Abs(Mathf.Sin(Time.time * speed) * distance);
        }
        else
        {
            pos.y = originalYPos + (Mathf.Sin(Time.time * speed) * distance);
        }

        transform.position = pos;
    }
}
