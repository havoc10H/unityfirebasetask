using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineOrLocal : MonoBehaviour
{
    public static OnlineOrLocal Instance;
    public int mode = 0; // 0: local 1:online 2: ai
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }        
    }
}
