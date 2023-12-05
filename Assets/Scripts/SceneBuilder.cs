using System.Collections;
using System.Collections.Generic;
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

    PointCloudProcessor pcProcessor = new PointCloudProcessor();
    TransformCalculator tfCalculator = new TransformCalculator();

    // Start is called before the first frame update
    void Start()
    {
        // Create PointCloudGroup and child point clouds
        pointCloudGroup = new GameObject("PointCloudGroup");

        string pcName = "octahedron6";
        Vector3[] pcData = pcProcessor.readPointCloud(pcName);
        // pcData = pcProcessor.applyTRSPointCloud(pcData, translation: new Vector3(0, 0, 0));
        pcProcessor.instantiatePointCloud(pointCloudGroup, pcData, pcName);

        pcName = "octahedron6";
        pcData = pcProcessor.readPointCloud(pcName);
        pcData = pcProcessor.applyTRSPointCloud(pcData, translation: new Vector3(15, 5, 0), rotation: new Vector3(0, 0, 90));
        pcProcessor.instantiatePointCloud(pointCloudGroup, pcData, pcName + "_t");

        // Fit point cloud group to camera view
        pcProcessor.fitPointCloudGroupToCameraView(pointScale, sceneScale, sceneOrigin, sceneRotation, pointCloudGroup);

        // Find transformation between two point clouds
        (Vector3 rotation, Vector3 translation) = tfCalculator.findTransformation();

        // Visaulize resulting transformation

        // 
    }

    // Update is called once per frame
    void Update() {
        if (updateScene) {
            pcProcessor.fitPointCloudGroupToCameraView(pointScale, sceneScale, sceneOrigin, sceneRotation, pointCloudGroup);
            updateScene = false;
        }
    }

}
