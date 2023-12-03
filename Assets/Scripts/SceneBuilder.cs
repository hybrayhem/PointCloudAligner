using System.Collections;
using System.Collections.Generic;
using System.IO;

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

        readPointCloud("octahedron14");
        readPointCloud("octahedron6");
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void readPointCloud(string filename) {
        Debug.Log("Reading point cloud " + filename);

        // Get text from file
        string filePath = Application.dataPath + "/PointClouds/" + filename + ".xyz";
        if (File.Exists(filePath)) {
            string data = File.ReadAllText(filePath);
            Debug.Log(data);
        } else {
            Debug.Log("Can't find file at " + filePath);
        }

        // Parse into vector3 array

    }
}
