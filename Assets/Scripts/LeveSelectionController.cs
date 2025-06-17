using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveSelectionController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevel(string levelName)
    {
        SceneManager.LoadScene(levelName); // Load the specified level scene
    }
}
