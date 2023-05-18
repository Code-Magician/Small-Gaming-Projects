using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text countDownText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] Image[] healthCells;
    [SerializeField] GameObject car;
    [SerializeField] RepeatBackground repeatBackground;
    [SerializeField] GameObject levelSelectPanel;
    public GameObject nextLevel;

    public float blinkTime = 0.05f;
    public float minAlpha = 0.1f;
    public float totalTime = 3;

    private int localHitCount = 0;




    private void Update()
    {
        switch (GameProps.hitCount)
        {
            case 0:
                if (localHitCount == 0)
                {
                    for (int i = 0; i < healthCells.Length; i++)
                        healthCells[i].gameObject.SetActive(true);
                    localHitCount++;
                }
                break;
            case 1:
                if (localHitCount == 1)
                {
                    StartCoroutine(BlinkHealthCell(2));
                    localHitCount++;
                }
                break;
            case 2:
                if (localHitCount == 2)
                {
                    StopCoroutine(BlinkHealthCell(2));
                    StartCoroutine(BlinkHealthCell(1));
                    localHitCount++;
                }
                break;
            case 3:
                if (localHitCount == 3)
                {
                    StopCoroutine(BlinkHealthCell(2));
                    StopCoroutine(BlinkHealthCell(1));
                    StartCoroutine(BlinkHealthCell(0));
                    localHitCount++;
                }
                break;
        }
    }

    public void IsLevelTypeGame(bool action)
    {
        levelSelectPanel.SetActive(false);
        GameProps.isLevelGame = action;

        if (!GameProps.isLevelGame)
            levelText.transform.parent.gameObject.SetActive(false);


        StartCoroutine(RunCountDown());
    }

    private IEnumerator BlinkHealthCell(int i)
    {
        for (int x = 0; x < totalTime; x++)
        {
            Color color;
            switch (x)
            {
                case 1:
                    color = Color.green;
                    break;
                case 2:
                    color = Color.yellow;
                    break;
                default:
                    color = Color.red;
                    break;
            }

            for (float t = 0.0f; t < blinkTime; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(minAlpha, 1.0f, t / blinkTime);

                color.a = alpha;
                healthCells[i].color = color;
                yield return null;
            }

            for (float t = 0.0f; t < blinkTime; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1.0f, minAlpha, t / blinkTime);

                color.a = alpha;
                healthCells[i].color = color;
                yield return null;
            }
        }

        healthCells[i].gameObject.SetActive(false);
    }

    private IEnumerator RunCountDown()
    {
        countDownText.gameObject.SetActive(true);
        for (int i = 5; i >= 1; i--)
        {
            countDownText.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }

        countDownText.gameObject.SetActive(false);
        GameProps.canPlay = true;
    }


    public void Reset()
    {
        Destroy(repeatBackground.finishLine);
        car.transform.position = new Vector2(-6, 0);
        levelText.text = $"Level : {GameProps.currentLevel}";
        repeatBackground.currentLevelObstacleCount = 0;

        StartCoroutine(RunCountDown());
        nextLevel.SetActive(false);
    }
}
