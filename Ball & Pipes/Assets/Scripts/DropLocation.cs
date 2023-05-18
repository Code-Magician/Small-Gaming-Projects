using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropLocation : MonoBehaviour, IDropHandler
{
    [SerializeField] Type colorType;
    [SerializeField] new EdgeCollider2D collider;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Sprite onSprite, offSprite;
    [SerializeField] GameObject gateObject;
    [SerializeField] Image buttonImage;

    private bool isButtonOn = false;



    private void Awake()
    {
        collider = transform.parent.GetComponent<EdgeCollider2D>();
    }

    public void HandleButton()
    {
        isButtonOn = !isButtonOn;
        if (isButtonOn)
            buttonImage.sprite = onSprite;
        else
            buttonImage.sprite = offSprite;

        gateObject.SetActive(!isButtonOn);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject ballObject = eventData.pointerDrag;
        if (ballObject == null) return;

        DragNDrop dragNDrop = ballObject.GetComponent<DragNDrop>();
        if (dragNDrop == null) return;

        if (colorType == dragNDrop.colorType)
        {
            dragNDrop.rb.gravityScale = 9.8f;
            Debug.Log("Gravity : " + dragNDrop.rb.gravityScale);

            switch (dragNDrop.colorType)
            {
                case Type.BLUE:
                    GameProps.blueBallCnt++;
                    if (GameProps.blueBallCnt <= 3)
                    {
                        Instantiate(ballPrefab, dragNDrop.initialPosition, Quaternion.identity, ballObject.transform.parent);
                    }
                    break;
                case Type.YELLOW:
                    GameProps.yellowBallCnt++;
                    if (GameProps.yellowBallCnt <= 3)
                    {
                        Instantiate(ballPrefab, dragNDrop.initialPosition, Quaternion.identity, ballObject.transform.parent);
                    }
                    break;
                case Type.GREEN:
                    GameProps.greenBallCnt++;
                    if (GameProps.greenBallCnt <= 3)
                    {
                        Instantiate(ballPrefab, dragNDrop.initialPosition, Quaternion.identity, ballObject.transform.parent);
                    }
                    break;
            }
        }
        else
        {
            ballObject.transform.position = dragNDrop.initialPosition;
        }
    }
}
