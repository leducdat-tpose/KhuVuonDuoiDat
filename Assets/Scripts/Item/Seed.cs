using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Seed : MonoBehaviour, IFarmable
{
    public Product ProductPrefab {get; set;}

    public float DurationGrowth {get; set;} = 3f;

    public void Initialise()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
