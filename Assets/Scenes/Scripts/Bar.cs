using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] Gradient Gradient;
    [SerializeField] Material materialBase;
    [SerializeField] private MeshRenderer meshRenderer;

    public Transform Transform => gameObject.transform;
    
    public void SetColor(int min, int max)
    {
        Material instance = new Material(materialBase);
        instance.color = Gradient.Evaluate((float)min / max);
        meshRenderer.material = instance;
    }
    
}
