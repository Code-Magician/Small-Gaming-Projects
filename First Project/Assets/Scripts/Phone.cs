using UnityEngine;

namespace mobilePhone
{
    public class Phone : MonoBehaviour
    {
        private Animator anim;
        private Ibutton currentApp;

        [SerializeField] private GameObject selectionMenu;

        public static Phone instance;

        public enum keyTypes
        {
            animal, numbers, arrows, music, Alphabet
        }

        [SerializeField] private button[] Keys;

        private void Awake()
        {
            instance = this;
        }


        void Start()
        {
            anim = GetComponent<Animator>();
        }


        void Update()
        {


        }

        public void OpenApplication(GameObject app)
        {
            currentApp = app.GetComponent<Ibutton>();
            app.SetActive(true);
            print(currentApp.ToString());

            selectionMenu.SetActive(false);

            SetScreenMod(currentApp.getScreenMod());

            foreach (button b in Keys)
            {
                b.checkKeys(currentApp.getMyKeyType());
            }

        }

        public void onButtonPressed(button.buttonIds ID)
        {
            currentApp.Intereact(ID);
        }


        public void SetScreenMod(bool trigger)
        {
            anim.SetBool("onlyScreen", trigger);
        }

        public void backToSelection()
        {
            if (currentApp != null)
                currentApp.closeApp();
            selectionMenu.SetActive(true);
            SetScreenMod(true);
        }

        public void Spin()
        {
            anim.SetTrigger("Spin");
        }

        public void ChangeMaterial(Material mat)
        {
            foreach (button b in Keys)
            {
                b.StartSpin(mat);
            }
        }
    }
}
