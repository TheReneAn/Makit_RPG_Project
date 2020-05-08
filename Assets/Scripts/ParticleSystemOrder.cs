using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().sortingOrder = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
