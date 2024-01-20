using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Transform TowerPoint;

    private void Awake()
    {
        instance = this;
    }

}
