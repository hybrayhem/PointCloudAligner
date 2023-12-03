using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// Read and visualize point cloud
public class SceneBuilder : MonoBehaviour
{
    public Vector3[] pointCloud;


    // Start is called before the first frame update
    void Start()
    {
        // Print Hello
        Debug.Log("Hello");

        readPointCloud("PointCloud/octahedron14");
        readPointCloud("PointCloud/octahedron6");
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void readPointCloud(string path) {
        // var pointCloudText = Resources.Load<TextAsset>(path);
        // Debug.Log(pointCloudText.text);

        TextAsset textAsset = (TextAsset) Resources.Load(path);
        Debug.Log(textAsset);
    }
}
