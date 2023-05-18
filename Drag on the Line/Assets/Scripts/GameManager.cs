using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject level1, level2, level3;

    int currentLevel = 1;

    private void Update()
    {
        if (GameStats.collisionCount > 1)
        {
            ClearAll();

            currentLevel = (currentLevel + 1) % 4;
            if (currentLevel == 0) currentLevel = 1;

            switch (currentLevel)
            {
                case 1:
                    level1.SetActive(true);
                    break;
                case 2:
                    level2.SetActive(true);
                    break;
                case 3:
                    level3.SetActive(true);
                    break;
            }

            GameStats.collisionCount = 0;
        }
    }

    private void ClearAll()
    {
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
    }
}
