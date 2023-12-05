using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSolver : MonoBehaviour
{
    public (Matrix4x4, Vector3) findTransformation(Vector3[] P, Vector3[] Q) {
        // 1. Compute centroids of P and Q
        Vector3 centroid_P = findCentroid(P);
        Vector3 centroid_Q = findCentroid(Q);

        // 2. Subtract centroids from P and Q
        Vector3[] P_centered3 = substract(P, centroid_P);
        Vector3[] Q_centered3 = substract(Q, centroid_Q);
        
        Vector4[] Q_centered4 = new Vector4[3];
        for (int i = 0; i < 3; i++) {
            Q_centered4[i] = new Vector4(Q_centered3[i][0], Q_centered3[i][1], Q_centered3[i][2], 1);
        }

        // 3. Use KabschSolver to find the optimal rotation matrix
        KabschSolver kabschSolver = new KabschSolver();
        Matrix4x4 rotationMatrix = kabschSolver.SolveKabsch(P_centered3, Q_centered4);

        // 4. Compute the optimal translation vector
        Vector3 translationVector = centroid_Q - MultiplyPoint3x4(rotationMatrix, centroid_P);

        return (rotationMatrix, translationVector);
    }

    public Vector3[] substract(Vector3[] points, Vector3 centroid) {
        Vector3[] result = new Vector3[points.Length];

        for (int i = 0; i < points.Length; i++) {
            result[i] = points[i] - centroid;
        }

        return result;
    }

    Vector3 findCentroid(Vector3[] points) {
        Vector3 centroid = new Vector3(0, 0, 0);

        foreach (var point in points) {
            centroid += point;
        }

        centroid /= points.Length;

        return centroid;
    }

    Vector3 MultiplyPoint3x4(Matrix4x4 matrix, Vector3 point) {
        return new Vector3(
            matrix.m00 * point.x + matrix.m01 * point.y + matrix.m02 * point.z + matrix.m03,
            matrix.m10 * point.x + matrix.m11 * point.y + matrix.m12 * point.z + matrix.m13,
            matrix.m20 * point.x + matrix.m21 * point.y + matrix.m22 * point.z + matrix.m23
        );
    }

    //------------------------------------------------------------//
    //-------------------- OLD IMPLEMENTATION --------------------//
    //------------------------------------------------------------//

    // public (Vector3, Vector3) findTransformationSVD(Vector3[] P, Vector3[] Q) { // (Matrix3x3 P, Matrix3x3 Q)
    //     Debug.Log("P: " + P[0] + ", " + P[1] + ", " + P[2]);
    //     Debug.Log("Q: " + Q[0] + ", " + Q[1] + ", " + Q[2]);

    //     // // 1. Find centroids
    //     Vector3 centroid_P = findCentroid(P);
    //     Vector3 centroid_Q = findCentroid(Q);
    //     Debug.Log("Centroid P: " + centroid_P);
    //     Debug.Log("Centroid Q: " + centroid_Q);
        

    //     // // 2. Find covariance matrix
    //     Matrix4x4 covarianceMatrix = findCovarianceMatrix(P, Q, centroid_P, centroid_Q);

    //     // // 3. Find SVD of covariance matrix
    //     Matrix4x4 svdMatrix = findSVD(covarianceMatrix);

    //     // // 4. Find rotation matrix
    //     // Matrix4x4 rotationMatrix = findRotationMatrix(svdMatrix);

    //     // // 5. Find translation vector
    //     // Vector3 translationVector = findTranslationVector(centroid1, centroid2, rotationMatrix);

    //     // // 6. Apply transformation
    //     // applyTRSAbsolute(obj: pointCloudGroup, translation: translationVector, rotation: rotationMatrix.eulerAngles, scale: sceneScale);

    //     return (new Vector3(0, 0, 0), new Vector3(0, 0, 0));
    // }

    // Matrix4x4 findSVD(Matrix4x4 covarianceMatrix) {
    //     Matrix4x4 svdMatrix = new Matrix4x4();

    //     // To be implemented...

    //     return svdMatrix;
    // }

    // Matrix4x4 findCovarianceMatrix(Vector3[] P, Vector3[] Q, Vector3 centroid_P, Vector3 centroid_Q) {
    //     Matrix4x4 covarianceMatrix = new Matrix4x4();

    //     Vector3[] P_mean = substract(P, centroid_P);
    //     Vector3[] Q_mean = substract(Q, centroid_Q);
    //     Debug.Log("P_mean: " + P_mean[0] + ", " + P_mean[1] + ", " + P_mean[2]);
    //     Debug.Log("Q_mean: " + Q_mean[0] + ", " + Q_mean[1] + ", " + Q_mean[2]);
        
    //     // Convert P_mean and Q_mean to Matrix4x4
    //     Matrix4x4 P_mean_matrix = vectorsToMatrix(P_mean);
    //     Matrix4x4 Q_mean_matrix = vectorsToMatrix(Q_mean);
    //     Debug.Log("P_mean_matrix: \n" + P_mean_matrix);
    //     Debug.Log("Q_mean_matrix: \n" + Q_mean_matrix);

    //     // Q_mean_transpose
    //     Matrix4x4 Q_mean_transpose = Q_mean_matrix.transpose;
    //     Debug.Log("Q_mean_transpose: \n" + Q_mean_transpose);

    //     // P_mean dot Q_mean_transpose
    //     covarianceMatrix = P_mean_matrix * Q_mean_transpose;
    //     Debug.Log("covarianceMatrix: \n" + covarianceMatrix);

    //     return covarianceMatrix;
    // }

    // Matrix4x4 vectorsToMatrix(Vector3[] vectors) {
    //     Matrix4x4 matrix = new Matrix4x4(
    //     new Vector4(vectors[0][0], vectors[0][1], vectors[0][2], 0),
    //     new Vector4(vectors[1][0], vectors[1][1], vectors[1][2], 0),
    //     new Vector4(vectors[2][0], vectors[2][1], vectors[2][2], 0),
    //     new Vector4(0, 0, 0, 1));

    //     return matrix;
    // }
    
}
