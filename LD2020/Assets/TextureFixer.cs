/*using System.Drawing;*/
using UnityEngine;

public class TextureFixer : MonoBehaviour
{
    /*public float factor = 0.1f;*/

    // Start is called before the first frame update
    void Start()
    {
        /*var mf = GetComponent<MeshFilter>();
        var renderer = GetComponent<Renderer>();
        if (mf != null)
        {
            var bounds = mf.mesh.bounds;

            var size = Vector3.Scale(bounds.size, transform.localScale);
            Debug.Log(size);
            size = size * factor;

            if (size.y < .001)
                size.y = size.z;

            Debug.Log(size);
            renderer.material.mainTextureScale = size;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
