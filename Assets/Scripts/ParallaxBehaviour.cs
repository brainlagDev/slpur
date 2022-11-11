using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private List<ParallaxItem> ParallaxItems;
    private Transform Target;

    void Start()
    {
        Target = Camera.main.transform;
    }

    void Update()
    {
        for (int i = 0; i < ParallaxItems.Count; i++)
        {
            ParallaxItems[i].Move(Target);
        }
    }
}
