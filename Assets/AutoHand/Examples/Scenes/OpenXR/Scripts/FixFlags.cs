using System.Collections.Generic;
using UnityEngine;

public class FixFlags : MonoBehaviour
{
    public List<Texture> asset =  new List<Texture>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Texture texture in asset)
            texture.hideFlags = HideFlags.None;
    }
}
