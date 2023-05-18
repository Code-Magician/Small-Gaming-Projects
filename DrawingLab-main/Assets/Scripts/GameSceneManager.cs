using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }


    public void backbuttonpressed(LinesDrawer lineDrawer)
    {
        StartCoroutine(backpressed(lineDrawer, "LevelSelection"));
    }

    public void dustbinpressed(LinesDrawer lineDrawer)
    {
        StartCoroutine(backpressed(lineDrawer, "GameScene"));
    }


    IEnumerator backpressed(LinesDrawer lineDrawer,string level)
    {
        yield return StartCoroutine(lineDrawer.savegameobject.SaveChildrenIenum());
        yield return null;
        yield return StartCoroutine(testScreenshot.instance.CaptureScreen());
        yield return null;
        SceneManager.LoadSceneAsync(level);
    }
}
