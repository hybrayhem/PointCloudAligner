using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

// Read and visualize point cloud
public class SceneBuilder : MonoBehaviour
{
    // Scene data
    public Vector3[] pointCloud;
    public GameObject pointCloudGroup;

    // Scene parameters //
    Vector3 sceneOrigin = new Vector3(0, 0, 7);
    Vector3 sceneRotation = new Vector3(0, 0, 0);
    float sceneScale = 0.1f;
    float pointScale = 1.0f;
    bool updateScene = false;
    //                  //

    // Start is called before the first frame update
    void Start()
    {
        pointCloudGroup = new GameObject("PointCloudGroup");

        string pcName = "octahedron6";
        Vector3[] pcData = readPointCloud(pcName);
        instantiatePointCloud(pointCloudGroup, pcData, pcName);

        pcName = "octahedron14";
        pcData = readPointCloud(pcName);
        pcData = translatePointCloud(pcData, new Vector3(0, 0, 10)); // TODO: remove this
        instantiatePointCloud(pointCloudGroup, pcData, pcName);

        transformPointCloudGroup();
    }

    // Update is called once per frame
    void Update() {
        sceneOrigin = new Vector3(-0.5f, 0, 3); // TODO: remove this
        updateScene = true;

        if (updateScene) {
            transformPointCloudGroup();
            updateScene = false;
        }
    }

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

    void transformPointCloudGroup() {
        // Scene adjustments to fit in camera view  
        GameObject[] points = GameObject.FindGameObjectsWithTag("PointTag");
        foreach (var point in points) {
            point.transform.localScale = Vector3.one * pointScale;
        }

        applyTRSAbsolute(obj: pointCloudGroup, translation: sceneOrigin, rotation: sceneRotation, scale: sceneScale);
    }

    GameObject instantiatePointCloud(GameObject parentGroup, Vector3[] pointCloud, string name) {
        GameObject pcObject = new GameObject(name);
        pcObject.transform.parent = parentGroup.transform; // Register to point cloud group
        
        foreach (var point in pointCloud) {
            // Instantiate sphere
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = point;
            sphere.tag = "PointTag";

            // Set sphere color
            sphere.GetComponent<Renderer>().material.color = Color.red;

            // Register sphere to parent as child
            sphere.transform.parent = pcObject.transform;
        }

        return pcObject;
    }

    // Transform GameObject
    void applyTRSRelative(GameObject obj, 
             Vector3 translation = default(Vector3), 
             Vector3 rotation = default(Vector3), 
             float scale = 1.0f) 
    {
        obj.transform.position += translation;
        obj.transform.Rotate(rotation);
        obj.transform.localScale *= scale;
    }

    void applyTRSAbsolute(GameObject obj, 
             Vector3 translation = default(Vector3), 
             Vector3 rotation = default(Vector3), 
             float scale = 1.0f) 
    {
        obj.transform.position = translation;
        obj.transform.rotation = Quaternion.Euler(rotation);
        obj.transform.localScale = Vector3.one * scale;
    }

    // Transform Points one by one
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
