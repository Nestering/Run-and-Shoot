using UnityEngine;

public class CharacterAnimPlayer : MonoBehaviour
{
    private Animator animator;
    private int isMoveHash;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        isMoveHash = Animator.StringToHash("isMove");
    }
    private void Update()
    {
        bool isMove = animator.GetBool(isMoveHash);
        if (!isMove && Player.Singleton.isMove )
        {
            animator.Play("idle");
            animator.SetBool(isMoveHash, true);
        }
        if(isMove && !Player.Singleton.isMove)
        {
            animator.SetBool(isMoveHash, false);
        }
        if (Player.Singleton.isShoot)
        {
            Player.Singleton.isShoot = false;
            animator.CrossFade("Shoot", 0f);
            animator.Play("Shoot");
        }

    }
}
