using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    [SerializeField] int laneWidth;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpTime = 0.5f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float jumpTimer;
    private int canMoveCounter;

    private void Start()
    {
        Time.timeScale = 1;

        rb = GetComponent<Rigidbody>();

        canMoveCounter = 0;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMoveCounter > -1)
        {
            canMoveCounter--;
            transform.Translate(Vector3.left * laneWidth);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canMoveCounter < 1)
        {
            canMoveCounter++;
            transform.Translate(Vector3.right * laneWidth);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
            jumpTimer = Time.time;
        }

        if (!isGrounded && Time.time - jumpTimer > jumpTime)
        {
            rb.velocity = Vector3.zero;
            isGrounded = true;
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GroundTrigger"))
        {
            Destroy(other.gameObject);
            Instantiate(groundPrefab, new Vector3(0, 0, 100), Quaternion.identity);
        }
    }
}
