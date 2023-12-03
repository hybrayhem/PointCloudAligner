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

        pointCloud = readPointCloud("octahedron14");
        pointCloud = readPointCloud("octahedron6");

        printPointCloud(pointCloud);
        visualizePointCloud(pointCloud);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    Vector3[] readPointCloud(string filename) {
        Vector3[] vectors;

        // Get text from file
        string data;
        string filePath = Application.dataPath + "/PointClouds/" + filename + ".xyz";
        
        if (File.Exists(filePath)) {
            data = File.ReadAllText(filePath);
            Debug.Log(data);
        } else {
            Debug.Log("Can't find file at " + filePath);
            return null;
        }

        // Parse text into vector3 array
        var lines = data.Split('\n');
        vectors = new Vector3[lines.Length];

        for (int i = 0; i < lines.Length; i++) {
            var line = lines[i];
            var coordinates = line.Split(' ');

            var x = float.Parse(coordinates[0]);
            var y = float.Parse(coordinates[1]);
            var z = float.Parse(coordinates[2]);

            vectors[i] = new Vector3(x, y, z);
        }

        return vectors;
    }

    void printPointCloud(Vector3[] pointCloud) {
        foreach (var point in pointCloud) {
            Debug.Log(point);
        }
    }

    void visualizePointCloud(Vector3[] pointCloud) {
        // To fit point cloud in camera view
        var visualizationOrigin = new Vector3(0, 0, 7);
        var downscaleRatio = 0.1f;
        var pointSize = 0.1f;

        Vector3[] newPointCloud;
        newPointCloud = scalePointCloud(pointCloud, downscaleRatio);
        newPointCloud = translatePointCloud(newPointCloud, visualizationOrigin);

        foreach (var point in newPointCloud) {
            // Instantiate a sphere
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = point;
            sphere.transform.localScale = new Vector3(pointSize, pointSize, pointSize);
        }
    }

    Vector3[] scalePointCloud(Vector3[] pointCloud, float scale) {
        Vector3[] scaledPointCloud = new Vector3[pointCloud.Length];

        for(int i = 0; i < pointCloud.Length; i++) {
            scaledPointCloud[i] = pointCloud[i] * scale;
        }
        return scaledPointCloud;
    }

    Vector3[] translatePointCloud(Vector3[] pointCloud, Vector3 translation) {
        Vector3[] translatedPointCloud = new Vector3[pointCloud.Length];

        for(int i = 0; i < pointCloud.Length; i++) {
            translatedPointCloud[i] = pointCloud[i] + translation;
        }

        return translatedPointCloud;
    }
}
