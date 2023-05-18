using System.Collections;
using UnityEngine;
using mobilePhone;

public class AnimalGame : MonoBehaviour, Ibutton
{
    [SerializeField] private bool onlyScreenMod;
    [SerializeField] private Phone.keyTypes myKeyType;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private GameObject[] animalModels;


    public void closeApp()
    {
        gameObject.SetActive(false);
    }

    public Phone.keyTypes getMyKeyType()
    {
        return myKeyType;
    }

    public bool getScreenMod()
    {
        return onlyScreenMod;
    }

    public void Intereact(button.buttonIds buttonID)
    {
        switch (buttonID)
        {
            case button.buttonIds.one:
                StartCoroutine(HandleGame(clips[0], animalModels[0]));
                break;
            case button.buttonIds.two:
                StartCoroutine(HandleGame(clips[1], animalModels[1]));
                break;
            case button.buttonIds.three:
                StartCoroutine(HandleGame(clips[2], animalModels[2]));
                break;
            case button.buttonIds.four:
                StartCoroutine(HandleGame(clips[3], animalModels[3]));
                break;
            case button.buttonIds.five:
                StartCoroutine(HandleGame(clips[4], animalModels[4]));
                break;
            case button.buttonIds.six:
                StartCoroutine(HandleGame(clips[5], animalModels[5]));
                break;
            case button.buttonIds.seven:
                StartCoroutine(HandleGame(clips[6], animalModels[6]));
                break;
            case button.buttonIds.eight:
                StartCoroutine(HandleGame(clips[7], animalModels[7]));
                break;
            case button.buttonIds.nine:
                StartCoroutine(HandleGame(clips[8], animalModels[8]));
                break;
            case button.buttonIds.zero:
                StartCoroutine(HandleGame(clips[9], animalModels[9]));
                break;
            case button.buttonIds.red:
                Phone.instance.backToSelection();
                Phone.instance.Spin();
                break;
        }
    }
    public void BackToSelection()
    {
        Phone.instance.backToSelection();
    }

    public IEnumerator HandleGame(AudioClip clip, GameObject model)
    {
        foreach (GameObject animal in animalModels) animal.SetActive(false);

        audioSource.Stop();
        model.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(clip);
    }
}
