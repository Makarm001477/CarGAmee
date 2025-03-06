using UnityEngine;
using TMPro; // For using TextMesh Pro
using UnityEngine.UI; // For using Button component

public class ButtonActivator : MonoBehaviour
{
    // Reference to the Button component
    public Button button;

    // References to the 3 GameObjects to be activated
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the button has a listener for clicks
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick); // Add click listener to the button
        }
    }

    // This method will be called when the button is clicked
    void OnButtonClick()
    {
        // Activate the three deactivated objects
        if (object1 != null) object1.SetActive(true);
        if (object2 != null) object2.SetActive(true);
        if (object3 != null) object3.SetActive(true);
    }
}
