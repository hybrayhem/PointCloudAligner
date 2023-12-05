using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

// Read and visualize point cloud
public class SceneBuilder : MonoBehaviour
{
    // Scene data
    public Vector3[] pointCloud1;
    public Vector3[] pointCloud2;
    public GameObject pointCloudGroup;

    // Scene parameters //
    Vector3 sceneOrigin = new Vector3(0, 0, 0.3f); // 30cm away from camera
    Vector3 sceneRotation = new Vector3(0, 0, 0);
    float sceneScale = 1f/50;
    float pointScale = 1.0f;
    bool updateScene = false;
    //                  //

    // Start is called before the first frame update
    void Start()
    {
        pointCloudGroup = new GameObject("PointCloudGroup");

        string pcName = "octahedron6";
        Vector3[] pcData = readPointCloud(pcName);
        pcData = applyTRSPointCloud(pcData, translation: new Vector3(0, 0, 0));
        instantiatePointCloud(pointCloudGroup, pcData, pcName);

        pcName = "octahedron6";
        pcData = readPointCloud(pcName);
        pcData = applyTRSPointCloud(pcData, translation: new Vector3(15, 5, 0), rotation: new Vector3(0, 0, 90));
        instantiatePointCloud(pointCloudGroup, pcData, pcName + "_t");

        fitPointCloudGroupToCameraView();

        findTransformation();
    }

    // Update is called once per frame
    void Update() {
        if (updateScene) {
            fitPointCloudGroupToCameraView();
            updateScene = false;
        }
    }

    
    
    void fitPointCloudGroupToCameraView() {
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
        obj.transform.localScale *= scale;
        obj.transform.Rotate(rotation);
        obj.transform.position += translation;
    }

    void applyTRSAbsolute(GameObject obj, 
             Vector3 translation = default(Vector3), 
             Vector3 rotation = default(Vector3), 
             float scale = 1.0f) 
    {
        obj.transform.localScale = Vector3.one * scale;
        obj.transform.rotation = Quaternion.Euler(rotation);
        obj.transform.position = translation;
    }

    // Transform Points one by one
    Vector3[] applyTRSPointCloud(Vector3[] pointCloud, 
             Vector3 translation = default(Vector3), 
             Vector3 rotation = default(Vector3), 
             float scale = 1.0f) 
    {
        Vector3[] transformedPointCloud = new Vector3[pointCloud.Length];

        for(int i = 0; i < pointCloud.Length; i++) {
            Vector3 scaledPoint = pointCloud[i] * scale;
            Vector3 rotatedPoint = Quaternion.Euler(rotation) * scaledPoint;
            Vector3 translatedPoint = rotatedPoint + translation;

            transformedPointCloud[i] = translatedPoint;
        }

        return transformedPointCloud;
    }

    void printPointCloud(Vector3[] pointCloud) {
        foreach (var point in pointCloud) {
            Debug.Log(point);
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

}
