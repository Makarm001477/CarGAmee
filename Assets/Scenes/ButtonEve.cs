using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading

public class ButtonActions : MonoBehaviour
{
    // Name of the scene you want to load
    public string sceneToLoad = "SceneName"; // Replace with your scene name

    // Public method to load the scene
    public void LoadScene()
    {
        // Load the scene specified in the inspector
        SceneManager.LoadScene(sceneToLoad);
    }

    // Public method to exit the game
    public void ExitGame()
    {
        // Exit the application (will work in a built version of the game)
        Application.Quit();

        // If in the editor, stop playing (this won't work in the actual build)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
