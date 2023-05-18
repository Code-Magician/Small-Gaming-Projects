using UnityEngine;
using System.Collections;

namespace mobilePhone
{
    public class button : MonoBehaviour
    {
        public static bool Spinning = false;
        [SerializeField] private float scaleLimit = 0.5f; // The scale limit to which the object should be scaled down
        [SerializeField] private float duration = 0.5f; // The duration of the scaling animation

        private Vector3 originalScale; // The original scale of the object
        private bool isScalingDown = false; // Flag to check if the object is currently scaling down

        [SerializeField] private Material numberKeys, ArrowKeys, AnimalKeys, musicKeys, alphabetKeys;

        private Phone.keyTypes currentType = Phone.keyTypes.animal;

        private void Start()
        {
            // Store the original scale of the object
            originalScale = transform.localScale;
        }

        public void Press()
        {
            if (!isScalingDown)
            {
                // Start scaling down the object
                isScalingDown = true;
                StartCoroutine(ScaleDownCoroutine());
            }
        }

        private IEnumerator ScaleDownCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Scale down the object
                float t = Mathf.Clamp01(elapsedTime / duration);
                float scale = Mathf.Lerp(originalScale.x, originalScale.x * scaleLimit, t);
                transform.localScale = new Vector3(scale, scale, scale);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Scale back up to the original size
            isScalingDown = false;
            StartCoroutine(ScaleUpCoroutine());
        }

        private IEnumerator ScaleUpCoroutine()
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Scale up the object
                float t = Mathf.Clamp01(elapsedTime / duration);
                float scale = Mathf.Lerp(transform.localScale.x, originalScale.x, t);
                transform.localScale = new Vector3(scale, scale, scale);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the scale to the original size
            transform.localScale = originalScale;
        }



        [SerializeField] private string id;
        [SerializeField] private Phone phone;

        public enum buttonIds
        {
            red, green, center, one, two, three, four, five, six, seven, eight, nine, zero
        }

        [SerializeField] private buttonIds myID;

        public void act()
        {
            phone.onButtonPressed(myID);
            Press();
        }


        public void checkKeys(Phone.keyTypes newtype)
        {
            if (currentType != newtype)
            {
                switch (newtype)
                {
                    case Phone.keyTypes.animal:
                        StartSpin(AnimalKeys);
                        break;

                    case Phone.keyTypes.numbers:
                        StartSpin(numberKeys);
                        break;

                    case Phone.keyTypes.arrows:
                        StartSpin(ArrowKeys);
                        break;

                    case Phone.keyTypes.music:
                        StartSpin(musicKeys);
                        break;
                    case Phone.keyTypes.Alphabet:
                        StartSpin(alphabetKeys);
                        break;
                }
            }
        }


        private void CHange(Material mat)
        {
            GetComponent<MeshRenderer>().material = mat;

        }

        public float spinSpeed = 10f; // The speed at which the object should spin
        private bool isSpinning = false; // Flag to check if the object is currently spinning
        private Coroutine spinCoroutine; // Reference to the coroutine used to spin the object
        private float currentRotation = 0f; // Current rotation of the object in degrees

        public void StartSpin(Material mat)
        {
            Spinning = false;
            spinCoroutine = StartCoroutine(SpinCoroutine(mat));
        }

        public void StopSpin()
        {
            if (spinCoroutine != null)
            {
                isSpinning = false;
                StopCoroutine(spinCoroutine);
                spinCoroutine = null;
                Spinning = false;
            }
        }


        private IEnumerator SpinCoroutine(Material mat)
        {
            yield return new WaitForSeconds(1);
            if (Spinning == false)
                Phone.instance.Spin();

            Spinning = true;


            yield return new WaitForSeconds(0.5f);
            GetComponent<MeshRenderer>().material = mat;

        }

    }



}