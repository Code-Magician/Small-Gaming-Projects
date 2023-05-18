using UnityEngine;

public class randomcall : MonoBehaviour
{
    [SerializeField] private AudioClip[] conversations;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = conversations[Random.Range(0,conversations.Length)];        
        source.Play();

        Destroy(gameObject,source.clip.length);
    }



}
