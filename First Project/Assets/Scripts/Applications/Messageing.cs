using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace mobilePhone
{
    public class Messageing : MonoBehaviour, Ibutton
    {
        [SerializeField] private bool only_ScreenMod;
        [SerializeField] private GameObject Contacts;
        [SerializeField] private GameObject ChatRoom;

        [SerializeField] private Transform[] contact_ids;
  



        private Sprite selectedContact_img;
        private string selectedContact_name;



        [SerializeField] private GameObject playerChatbox, sendersChatBox;
        [SerializeField] private Transform scroleViewContant;
        [SerializeField] private RectTransform scroleViewContant_t;
        [SerializeField] private Image[] Buttons;
        [SerializeField] private Sprite typing_dots;
        [SerializeField] private Sprite[] emotes;
 
    
        private Vector2 targetedSize;
        
    
        void Start()
        {
            Contacts.SetActive(true);
            ChatRoom.SetActive(false);
            targetedSize = scroleViewContant_t.sizeDelta;
        }

    
        void Update()
        {
            Vector2 velocity = Vector2.zero;
            scroleViewContant_t.sizeDelta = Vector2.SmoothDamp(scroleViewContant_t.sizeDelta,targetedSize, ref velocity, 5 * Time.deltaTime);
        }


        public void onContactSelected(int i)
        {
            selectedContact_img = contact_ids[i].GetChild(0).GetComponent<Image>().sprite;
            selectedContact_name = contact_ids[i].GetChild(0).GetComponent<Image>().name;

            Contacts.SetActive(false);
            ChatRoom.SetActive(true);

            Phone.instance.SetScreenMod(true);
        }









        public void addChat(int index)
        {
            GameObject chatbox = Instantiate(playerChatbox,scroleViewContant_t.position,Quaternion.identity);
            chatbox.transform.parent = scroleViewContant_t;
            chatbox.transform.localScale = Vector3.one;

            Image emote = chatbox.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            emote.sprite = Buttons[index].sprite;

            StartCoroutine(responseRutine());

             if (scroleViewContant_t.childCount >= 2)
            {
                targetedSize += new Vector2(0, 120);
            }


        }


        private IEnumerator responseRutine()
        {
            yield return new WaitForSeconds(1);

            GameObject chatbox = Instantiate(sendersChatBox,scroleViewContant_t.position,Quaternion.identity);
            chatbox.transform.parent = scroleViewContant_t;
            chatbox.transform.localScale = Vector3.one;

            TextMeshProUGUI name = chatbox.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            name.text = selectedContact_name;

            Image emote = chatbox.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            emote.sprite = typing_dots;

            if (scroleViewContant_t.childCount >= 2)
            {
                targetedSize += new Vector2(0, 120);
            }

            int t = Random.Range(2,5);
            yield return new WaitForSeconds(t);

            
            emote.sprite = emotes[Random.Range(0,emotes.Length)];
            

        }









        public void Intereact(button.buttonIds id)
        {
            switch(id)
            {
                case button.buttonIds.red:
    
                    Phone.instance.backToSelection();
                    break;
                
            }
        }

        [SerializeField] private Phone.keyTypes myKeyType;
        public Phone.keyTypes getMyKeyType()
        {
            return myKeyType;
        }
        public bool getScreenMod()
        {
            return only_ScreenMod;
        }
        public void closeApp()
        {
            gameObject.SetActive(false);
        }

    }
}
