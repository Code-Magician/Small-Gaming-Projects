using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] Animator animationController;



    public string GetAnimationName(int currAnimation)
    {
        switch (currAnimation)
        {
            case 1:
                return "Plant";
            case 2:
                return "Flower";
            default:
                return "Grown";
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        animationController.SetTrigger(GetAnimationName(DragAndDrop.timesDropped));
    }
}
