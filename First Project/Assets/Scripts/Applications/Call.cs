
using UnityEngine;
using TMPro;

namespace mobilePhone
{
    public class Call : MonoBehaviour, Ibutton
    {
        [SerializeField] private bool only_ScreenMod;


        [SerializeField] private TextMeshProUGUI number_desplay;
        [SerializeField] private TextMeshProUGUI callnumber_desplay;


        [SerializeField] private GameObject callerScreen;
        [SerializeField] private GameObject callConversations_prefabe;

        private GameObject currentConversation;

        private string input;



        void Start()
        {
            number_desplay.text = input;

        }

        public void Dail(string number)
        {
            if (!callerScreen.activeInHierarchy)
            {
                input += number;
                number_desplay.text = input;

                if (input != null)
                {
                    print("yes");
                    if (input.Length >= 5)
                    {
                        call();
                    }
                }
            }
        }


        public void call()
        {
            currentConversation = Instantiate(callConversations_prefabe);
            callerScreen.SetActive(true);
            callnumber_desplay.text = input;
        }


        public void Intereact(button.buttonIds ID)
        {


            switch (ID)
            {
                case button.buttonIds.red:

                    input = "";
                    number_desplay.text = "";

                    if (!callerScreen.activeInHierarchy)
                    {
                        gameObject.SetActive(false);
                    }
                    callerScreen.SetActive(false);
                    Phone.instance.backToSelection();


                    if (currentConversation != null)
                    {
                        Destroy(currentConversation);
                    }

                    break;

                case button.buttonIds.green:
                    if (input != null && input.Length == 5)
                    {
                        call();
                    }

                    break;

                case button.buttonIds.center:

                    break;

                case button.buttonIds.one:
                    Dail("1");
                    break;

                case button.buttonIds.two:
                    Dail("2");
                    break;

                case button.buttonIds.three:
                    Dail("3");
                    break;

                case button.buttonIds.four:
                    Dail("4");
                    break;

                case button.buttonIds.five:
                    Dail("5");
                    break;

                case button.buttonIds.six:
                    Dail("6");
                    break;

                case button.buttonIds.seven:
                    Dail("7");
                    break;

                case button.buttonIds.eight:
                    Dail("8");
                    break;

                case button.buttonIds.nine:
                    Dail("9");
                    break;
            }

            /*   if(id == "red")
               {

                   input = "";
                   number_desplay.text = "";
                   if(!callerScreen.activeInHierarchy)
                   {
                       gameObject.SetActive(false);
                   }


                   callerScreen.SetActive(false);

               }*/





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