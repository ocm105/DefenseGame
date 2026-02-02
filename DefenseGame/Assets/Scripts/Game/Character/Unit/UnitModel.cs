using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitModel : MonoBehaviour
{
    public Animator animator { get; private set; }
    public bool isAttack { get; private set; }

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        animator.Rebind();
        this.gameObject.SetActive(true);
    }
    public void Delete()
    {
        animator.Rebind();
        this.gameObject.SetActive(false);
    }

    public void Attack_Event(int isOn)
    {
        isAttack = isOn == 1 ? true : false;
    }
}
