using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CalibrationUI : MonoBehaviour
{
    public static CalibrationUI Instance;

    public GameObject calibrationPanel;
    public TextMeshProUGUI instructionText;
    public Image triangleTemplate;
    public Transform triangleParent;

    private bool isCalibrated = false;
    private float bounceSpeed = 2f;
    private float bounceDistance = 20f;
    private RectTransform[] triangles;
    private Vector2[] originalPositions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        triangleTemplate.gameObject.SetActive(false);
    }

    private void CreateTriangles()
    {
        // Clear out any existing triangles
        foreach (Transform child in triangleParent)
        {
            Destroy(child.gameObject);
        }

        triangles = new RectTransform[4];
        originalPositions = new Vector2[4];

        float scale = 0.3f;
        float distanceFromCenterVertical = 250f; // Distance for top and bottom triangles
        float distanceFromCenterHorizontal = 500f; // Distance for left and right triangles

        for (int i = 0; i < 4; i++)
        {
            Image newTriangle = Instantiate(triangleTemplate, triangleParent);
            newTriangle.gameObject.SetActive(true);
            triangles[i] = newTriangle.rectTransform;

            // Set position and rotation based on index
            float angle = i * 90f;
            triangles[i].localRotation = Quaternion.Euler(0, 0, angle - 90f); // Rotate the image itself

            // Position the triangle, using different distances for vertical and horizontal
            float distanceFromCenter = (i % 2 == 0) ? distanceFromCenterVertical : distanceFromCenterHorizontal;
            triangles[i].anchoredPosition = new Vector2(
                Mathf.Sin(angle * Mathf.Deg2Rad) * distanceFromCenter,
                Mathf.Cos(angle * Mathf.Deg2Rad) * distanceFromCenter
            );


            // Apply scale to the triangle, flipping horizontally for left and right triangles
            newTriangle.SetNativeSize();
            Vector3 triangleScale = Vector3.one * scale;
            if (i == 1 || i == 3) // Left and right triangles
            {
                triangleScale.x *= -1; // Flip horizontally
            }
            triangles[i].localScale = triangleScale;

            originalPositions[i] = triangles[i].anchoredPosition;
        }
    }

    private void AnimateTriangles()
    {
        float bounce = Mathf.Sin(Time.time * bounceSpeed) * bounceDistance;
        
        for (int i = 0; i < triangles.Length; i++)
        {
            Vector2 direction = (Vector2.zero - originalPositions[i]).normalized;
            triangles[i].anchoredPosition = originalPositions[i] + direction * bounce;
        }
    }

    public void ShowCalibrationScreen()
    {
        if (!isCalibrated)
        {
            isCalibrated = false;
            calibrationPanel.SetActive(true);
            instructionText.text = "Point the controller at the center of the screen and press A!";
            CreateTriangles();
        }
    }

    void Start()
    {
        ShowCalibrationScreen();
    }

    void Update()
    {
        if (!isCalibrated)
        {
            AnimateTriangles();
        }
    }

    private void ClearTriangles()
    {
        if (triangles != null)
        {
            foreach (var triangle in triangles)
            {
                if (triangle != null)
                {
                    Destroy(triangle.gameObject);
                }
            }
            triangles = null;
        }
    }

    public void Calibrate()
    {
        isCalibrated = true;
        calibrationPanel.SetActive(false);
        ClearTriangles();
    }

    public bool IsCalibrated()
    {
        return isCalibrated;
    }

    public void HideCalibrationManager()
    {
        if (isCalibrated)
        {
            return;
        }

        isCalibrated = true;
        calibrationPanel.SetActive(false);
        ClearTriangles();
    }
}