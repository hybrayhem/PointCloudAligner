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
    TransformSolver tfSolver = new TransformSolver();

    // Start is called before the first frame update
    void Start()
    {
        // Create PointCloudGroup and child point clouds
        pointCloudGroup = new GameObject("PointCloudGroup");

        string pcName1 = "octahedron6";
        Vector3[] pointCloud1 = pcProcessor.readPointCloud(pcName1);
        // pcData = pcProcessor.applyTRSPointCloud(pcData, translation: new Vector3(0, 0, 0));
        pcProcessor.instantiatePointCloud(pointCloudGroup, pointCloud1, pcName1);

        string pcName2 = "octahedron6";
        pointCloud2 = pcProcessor.readPointCloud(pcName2);
        pointCloud2 = pcProcessor.applyTRSPointCloud(pointCloud2, translation: new Vector3(15, 5, 0), rotation: new Vector3(0, 0, 90));
        pcProcessor.instantiatePointCloud(pointCloudGroup, pointCloud2, pcName2 + "_t");

        // Fit point cloud group to camera view
        pcProcessor.fitPointCloudGroupToCameraView(pointScale, sceneScale, sceneOrigin, sceneRotation, pointCloudGroup);

        // Find transformation between two point clouds
        (Vector3[] P, Vector3[] Q) = pcProcessor.getPandQ(pointCloud1, pointCloud2);

        (Matrix4x4 rotation, Vector3 translation) = tfSolver.findTransformation(P, Q);
        Debug.Log("Rotation: " + rotation);
        Debug.Log("Translation: " + translation);

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
