using UnityEngine;
using TMPro; // Import for TextMeshPro
using UnityEngine.SceneManagement; // For scene loading

public class LapCounter : MonoBehaviour
{
    public TextMeshProUGUI f1Text;       // Text for F1 laps
    public TextMeshProUGUI ambulanceText; // Text for Ambulance laps
    public TextMeshProUGUI tractorText;   // Text for Tractor laps

    private int f1Laps = 0;
    private int ambulanceLaps = 0;
    private int tractorLaps = 0;

    public int maxLaps = 3;

    // Scene names to load, assign in Inspector
    public string f1SceneName;        // Scene name for F1 when reaching max laps
    public string ambulanceSceneName; // Scene name for Ambulance and Tractor when reaching max laps

    // Assign layer tags to objects to detect them
    private string f1Tag = "F1";
    private string ambulanceTag = "Ambulance";
    private string tractorTag = "Tractor";

    void Start()
    {
        // Initialize the text
        UpdateLapTexts();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object is F1, Ambulance, or Tractor
        if (other.gameObject.CompareTag(f1Tag))
        {
            f1Laps++;
            if (f1Laps >= maxLaps)
            {
                // Load the F1-specific scene (set in Inspector)
                SceneManager.LoadScene(f1SceneName);
            }
        }
        else if (other.gameObject.CompareTag(ambulanceTag))
        {
            ambulanceLaps++;
            if (ambulanceLaps >= maxLaps)
            {
                // Load the Ambulance/Tractor-specific scene (set in Inspector)
                SceneManager.LoadScene(ambulanceSceneName);
            }
        }
        else if (other.gameObject.CompareTag(tractorTag))
        {
            tractorLaps++;
            if (tractorLaps >= maxLaps)
            {
                // Load the Ambulance/Tractor-specific scene (set in Inspector)
                SceneManager.LoadScene(ambulanceSceneName);
            }
        }

        // Update the text after each lap is counted
        UpdateLapTexts();
    }

    void UpdateLapTexts()
    {
        f1Text.text = $"F1 {f1Laps}/{maxLaps} laps";
        ambulanceText.text = $"Ambulance {ambulanceLaps}/{maxLaps} laps";
        tractorText.text = $"Tractor {tractorLaps}/{maxLaps} laps";
    }
}
