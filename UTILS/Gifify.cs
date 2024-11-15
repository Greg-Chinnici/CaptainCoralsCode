using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gifify : MonoBehaviour
{
    
    [SerializeField] private Texture2D[] gifFrames;
    
    [SerializeField] private  float fps = 10.0f;

    private Material mat;

    void Start() {
        mat = GetComponent<Renderer> ().material;
    }

    void Update() {
        int index = (int)(Time.time * fps);
        index %= gifFrames.Length;
        mat.mainTexture = gifFrames[index];
        //GetComponent<RawImage> ().texture = frames [index];
    }
}
