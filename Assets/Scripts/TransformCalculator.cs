using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCalculator : MonoBehaviour
{

    void findTransformation() {
        // Find transformation between two point clouds
        // 1. Find centroids
        // 2. Find covariance matrix
        // 3. Find SVD of covariance matrix
        // 4. Find rotation matrix
        // 5. Find translation vector

        // // 1. Find centroids
        // Vector3 centroid1 = findCentroid(pointCloud1);
        // Vector3 centroid2 = findCentroid(pointCloud2);

        // // 2. Find covariance matrix
        // Matrix4x4 covarianceMatrix = findCovarianceMatrix(pointCloud1, pointCloud2, centroid1, centroid2);

        // // 3. Find SVD of covariance matrix
        // Matrix4x4 svdMatrix = findSVD(covarianceMatrix);

        // // 4. Find rotation matrix
        // Matrix4x4 rotationMatrix = findRotationMatrix(svdMatrix);

        // // 5. Find translation vector
        // Vector3 translationVector = findTranslationVector(centroid1, centroid2, rotationMatrix);

        // // 6. Apply transformation
        // applyTRSAbsolute(obj: pointCloudGroup, translation: translationVector, rotation: rotationMatrix.eulerAngles, scale: sceneScale);
    }

    // Vector3 findCentroid(Vector3[] pointCloud) {
    //     Vector3 centroid = new Vector3(0, 0, 0);

    //     foreach (var point in pointCloud) {
    //         centroid += point;
    //     }

    //     centroid /= pointCloud.Length;

    //     return centroid;
    // }

    // Matrix4x4 findCovarianceMatrix(Vector3[] pointCloud1, Vector3[] pointCloud2, Vector3 centroid1, Vector3 centroid2) {
    //     Matrix4x4 covarianceMatrix = new Matrix4x4();

    //     for (int i = 0; i < pointCloud1.Length; i++) {
    //         Vector3 point1 = pointCloud1[i];
    //         Vector3 point2 = pointCloud2[i];

    //         Vector3 point1Diff = point1 - centroid1;
    //         Vector3 point2Diff = point2 - centroid2;

    //         covarianceMatrix.m00 += point1Diff.x * point2Diff.x;
    //         covarianceMatrix.m01 += point1Diff.x * point2Diff.y;
    //         covarianceMatrix.m02 += point1Diff.x * point2Diff.z;
    //         covarianceMatrix.m03 += point1Diff.x;

    //         covarianceMatrix.m10 += point1Diff.y * point2Diff.x;
    //         covarianceMatrix.m11 += point1Diff.y * point2Diff.y;
    //         covarianceMatrix.m12 += point1Diff.y * point2Diff.z;
    //         covarianceMatrix.m13 += point1Diff.y;

    //         covarianceMatrix.m20 += point1Diff.z * point2Diff.x;
    //         covarianceMatrix.m21 += point1Diff.z * point2Diff.y;
    //         covarianceMatrix.m22 += point1Diff.z * point2Diff.z;
    //         covarianceMatrix.m23 += point1Diff.z;

    //         covarianceMatrix.m30 += point1Diff.x;
    //         covarianceMatrix.m31 += point1Diff.y;
    //         covarianceMatrix.m32 += point1Diff.z;
    //         covarianceMatrix.m33 += 1;
    //     }

    //     covarianceMatrix /= pointCloud1.Length;

    //     return covarianceMatrix;
    // }

    // Matrix4x4 findSVD(Matrix4x4 covarianceMatrix) {
    //     Matrix4x4 svdMatrix = new Matrix4x4();

    //     // Find SVD of covariance matrix
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C

    //     // 1. Find eigenvalues and eigenvectors
    //     // 2. Sort eigenvalues and eigenvectors
    //     // 3. Find SVD matrix

    //     // 1. Find eigenvalues and eigenvectors
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C

    //     // 2. Sort eigenvalues and eigenvectors
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C

    //     // 3. Find SVD matrix
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C
    //     // https://www.codeproject.com/Articles/11640/Eigenvalues-and-Eigenvectors-in-C

    //     return svdMatrix;
    // }

    
}
