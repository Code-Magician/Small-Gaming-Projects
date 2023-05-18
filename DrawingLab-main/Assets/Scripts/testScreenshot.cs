using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections;

public class testScreenshot : MonoBehaviour
{
    public static testScreenshot instance;
    public Canvas can;
    public GameObject boundingbox;
    public string pathtofile;
    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        pathtofile = "/savefile_";
    }

    [Button]
    public void TakeScreenshot()
    {
        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen()
    {
        Time.timeScale = 0;
        boundingbox.SetActive(false);
        can.gameObject.SetActive(false);

        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        pathtofile += DataManager.instance.LeveltoLoad + ".png";
        ScreenCapture.CaptureScreenshot(pathtofile);

        yield return new WaitForEndOfFrame();
        //print("Saved screenshot to " + pathtofile);

        // Show UI after we're done
        can.gameObject.SetActive(true);
        boundingbox.SetActive(true);

        yield return null;

        Time.timeScale = 1;
    }
}
