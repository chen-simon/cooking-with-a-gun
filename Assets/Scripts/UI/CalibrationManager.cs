// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;

// public class CalibrationManager : MonoBehaviour
// {
//     public static CalibrationManager Instance;
    
//     public GameObject calibrationPanel;
//     public TextMeshProUGUI instructionText;
//     public Button calibrateButton;

//     private bool isCalibrated = false;

//     void Awake()
//     {
//         if (Instance == null)
//         {
//             instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     private void Start()
//     {
//         ShowCalibrationScreen();
//         calibrateButton.onClick.AddListener(Calibrate);
//     }

//     public void ShowCalibrationScreen()
//     {
//         calibrationPanel.SetActive(true);
//         instructionText.text = "Please point the Joycon to the center and press A";
//     }

//     public void 
// }
