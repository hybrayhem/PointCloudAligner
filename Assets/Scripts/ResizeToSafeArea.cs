using UnityEngine;

public class ResizeToSafeArea : MonoBehaviour {

    private Rect lastSafeArea;

    private void Update() {
        if (lastSafeArea != Screen.safeArea) {
            ApplySafeArea();
        }
    }

    private void ApplySafeArea() {
        Rect safeArea = Screen.safeArea;
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        // Apply the offsets
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        lastSafeArea = Screen.safeArea;
    }
}




















        // // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        // Vector2 anchorMin = safeAreaRect.position;
        // Vector2 anchorMax = safeAreaRect.position + safeAreaRect.size;
        // anchorMin.x /= Screen.width;
        // anchorMin.y /= Screen.height;
        // anchorMax.x /= Screen.width;
        // anchorMax.y /= Screen.height;

        // parentRectTransform.anchorMin = anchorMin;
        // parentRectTransform.anchorMax = anchorMax;

        // // float scaleRatio = parentRectTransform.rect.width / Screen.width;

        // // var left = safeAreaRect.xMin * scaleRatio;
        // // var right = -(Screen.width - safeAreaRect.xMax) * scaleRatio;
        // // var top = -safeAreaRect.yMin * scaleRatio;
        // // var bottom = (Screen.height - safeAreaRect.yMax) * scaleRatio;

        // // RectTransform rectTransform = GetComponent<RectTransform>();
        // // rectTransform.offsetMin = new Vector2(left, bottom);
        // // rectTransform.offsetMax = new Vector2(right, top);