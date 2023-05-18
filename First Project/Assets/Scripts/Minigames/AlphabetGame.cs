using UnityEngine;
using mobilePhone;
using System.Collections;
public class AlphabetGame : MonoBehaviour, Ibutton
{
    [SerializeField] private bool onlyScreenMod;
    [SerializeField] private Phone.keyTypes myKeyType;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private GameObject[] alphabetModels;
    [SerializeField] private Material[] alphabetMaterials;

    private int pageNumber = 0;

    private void Start()
    {
        // Phone.instance.OpenApplication(this.gameObject);
    }

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
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 0], alphabetModels[pageNumber * 9 + 0]));
                break;
            case button.buttonIds.two:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 1], alphabetModels[pageNumber * 9 + 1]));
                break;
            case button.buttonIds.three:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 2], alphabetModels[pageNumber * 9 + 2]));
                break;
            case button.buttonIds.four:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 3], alphabetModels[pageNumber * 9 + 3]));
                break;
            case button.buttonIds.five:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 4], alphabetModels[pageNumber * 9 + 4]));
                break;
            case button.buttonIds.six:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 5], alphabetModels[pageNumber * 9 + 5]));
                break;
            case button.buttonIds.seven:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 6], alphabetModels[pageNumber * 9 + 6]));
                break;
            case button.buttonIds.eight:
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 7], alphabetModels[pageNumber * 9 + 7]));
                break;
            case button.buttonIds.nine:
                if (pageNumber == 2) break;
                StartCoroutine(HandleGame(clips[pageNumber * 9 + 8], alphabetModels[pageNumber * 9 + 8]));
                break;
            case button.buttonIds.zero:
                pageNumber = (pageNumber + 1) % 3;
                Phone.instance.ChangeMaterial(alphabetMaterials[pageNumber]);
                break;
            case button.buttonIds.red:
                Phone.instance.backToSelection();
                Phone.instance.Spin();
                break;
        }
    }

    
    public IEnumerator HandleGame(AudioClip clip, GameObject model)
    {
        foreach (GameObject alphabet in alphabetModels) alphabet.SetActive(false);

        audioSource.Stop();
        model.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(clip);
    }
}
