using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelScreenManager : MonoBehaviour
{
    public Transform buttons_Container;
    public string pathtofile;
    public GameObject extra_button, buttonprefab;
    int pagesCount;
    public void Start()
    {
        bool firsttime = true;
        string firsttimetext = Application.persistentDataPath + "/firsttimefile.txt";
        if (File.Exists(firsttimetext))
            firsttime = false;
        else
            File.WriteAllText(firsttimetext, "this file is generated for the first time only");

#if UNITY_EDITOR
        firsttime = true;
#endif

        string tempfilename;
        Transform temp;
        Sprite sprite;
        TextAsset texass;
        Texture2D tex2d;
        string json;

        string path = Application.persistentDataPath + "/levels.txt";
        if (File.Exists(path))
            pagesCount = int.Parse(File.ReadAllText(path));
        else
            pagesCount = buttons_Container.childCount - 1;

        for (int i = 0; i < pagesCount; i++)
        {
            pathtofile = Application.persistentDataPath + "/savefile_" + i;
            if (i < buttons_Container.childCount - 1)
                temp = buttons_Container.GetChild(i);
            else
            {
                temp = Instantiate(buttonprefab, buttons_Container).transform;
                extra_button.transform.SetAsLastSibling();
                Button tempbtn = temp.GetComponent<Button>();
                tempbtn.onClick.RemoveAllListeners();
                int n = i;
                tempbtn.onClick.AddListener(() => { onbuttonclicked(n); });
            }

            if (firsttime)
            {
                tempfilename = "savefile_" + i;
                texass = Resources.Load<TextAsset>(tempfilename);
                tex2d = Resources.Load<Texture2D>(tempfilename);

                if (!texass)
                    continue;
                json = texass.ToString();
                File.WriteAllText(pathtofile + ".json", json);
                if (!tex2d)
                    continue;
                File.WriteAllBytes(pathtofile + ".png", tex2d.EncodeToPNG());
            }

            Image img = temp.GetComponent<Image>();
            sprite = loadimage();
            if (sprite)
                img.sprite = sprite;
        }
    }

    Sprite loadimage()
    {
        string temppath = pathtofile + ".png";
        if (!File.Exists(temppath))
            return null;
        //print("loading image " + temppath);
        byte[] image = File.ReadAllBytes(temppath);
        Texture2D tex = new Texture2D(0, 0);
        tex.LoadImage(image);
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        return sprite;
    }

    public void onbuttonclicked(int n)
    {
        print(n);
        DataManager.instance.LeveltoLoad = n;
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void onextrabuttonClicked()
    {
        GameObject temp = Instantiate(buttonprefab, buttons_Container);
        extra_button.transform.SetAsLastSibling();
        Button tempbtn = temp.GetComponent<Button>();
        tempbtn.onClick.RemoveAllListeners();
        int n = pagesCount;
        tempbtn.onClick.AddListener(() => { onbuttonclicked(n); });
        pagesCount++;
    }

    public void onbackbuttonclicked()
    {
        SceneManager.LoadSceneAsync("HomeScene");
    }

    private void OnDestroy()
    {
        string path = Application.persistentDataPath + "/levels.txt";
        File.WriteAllText(path, pagesCount.ToString());
    }
}
