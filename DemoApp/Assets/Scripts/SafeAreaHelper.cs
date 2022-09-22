using UnityEngine;

public class SafeAreaHelper : MonoBehaviour
{

    private Rect lastSafeArea;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnGUI()
    {
        var safeArea = Screen.safeArea;

        if (lastSafeArea != safeArea)
        {
            ApplySafeArea(safeArea);
        }
    }

    private void ApplySafeArea(Rect safeAreaRect)
    {
        var anchorMin = safeAreaRect.position;
        var anchorMax = safeAreaRect.position + safeAreaRect.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

        lastSafeArea = safeAreaRect;
    }

}
