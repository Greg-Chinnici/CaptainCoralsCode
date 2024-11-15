using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceXUI : MonoBehaviour
{
    [SerializeField] private float distance = 0;

    [SerializeField] private float speed = 1.0f;


    [SerializeField] private RectTransform rectTransform;
    private float originalxPos;
    
    // Start is called before the first frame update
    void Start()
    {
        originalxPos = rectTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = rectTransform.position;
        
            pos.x = originalxPos + (Mathf.Sin(Time.time * speed) * distance);

            rectTransform.position = pos;
    }
}
