using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpecificButton : MonoBehaviour
{
    [SerializeField] private List<RuntimePlatform> showOnPlatforms;
    // Start is called before the first frame update
    void Start()
    {
        if (showOnPlatforms.Contains(Application.platform) == false)
        {
            gameObject.SetActive(false);
        }
       
    }
}
