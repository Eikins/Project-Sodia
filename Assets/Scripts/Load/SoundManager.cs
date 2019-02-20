using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        InitSounds();
    }

    void InitSounds() {

    }
}