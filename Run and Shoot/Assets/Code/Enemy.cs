using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Image HPBar;
    [SerializeField] private GameObject canvas;
    private CapsuleCollider capsuleCollider;
    private Animator animator;

    [Space()]

    [Header("Setting Enemy")]
    [SerializeField] private int hp = 100;
    private int maxHp;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
        hp *= GameController.Singleton.level;
        maxHp = hp;
    }
    private void FixedUpdate()
    {
        if (canvas.activeInHierarchy)
        {
            HPBar.fillAmount = (float)hp / (float)maxHp;
            transform.LookAt(Player.Singleton.transform.position);
        }
        
    }
    public void TakeDamage(int damage)
    {
        hp = (hp > 0 && hp - damage > 0) ? hp - damage : 0;

        if (hp <= 0)
        {
            GameController.Singleton.RemoveListEnemy(this);
            capsuleCollider.enabled = false;
            animator.enabled = false;
            canvas.SetActive(false);
        }
    }

}
