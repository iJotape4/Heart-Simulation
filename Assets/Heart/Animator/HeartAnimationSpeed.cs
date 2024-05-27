using UnityEngine;

public class HeartAnimationSpeed : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        UIManager.Instance.FCM.onValueChanged.AddListener(delegate { SetSpeed(UIManager.Instance.FCM.value); });
    }

    public void SetSpeed(float FCM)
    {
       float speed = (1f/60f) * FCM;
        animator.speed = speed;
    }
}