using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitGrid : MonoBehaviour
{
    [SerializeField] Color baseColor;
    [SerializeField] Color dragColor;
    private SpriteRenderer sprite;
    public UnitInfo UnitInfo { get; set; }
    public string Ability { get; set; }

    public bool IsUnit { get { return UnitInfo != null; } }

    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    public void ChageColor(bool isDrag)
    {
        sprite.color = isDrag ? dragColor : baseColor;
    }

    public void UnitMove(UnitInfo unit)
    {
        UnitInfo = unit;
        unit.transform.parent = this.transform;
        Move(unit).Forget();
    }
    private async UniTask Move(UnitInfo unit)
    {
        while (Vector2.Distance(unit.transform.localPosition, Vector3.zero) > 0.05f)
        {
            unit.transform.localPosition = Vector2.MoveTowards(unit.transform.localPosition, Vector3.zero, Time.deltaTime * 3f);
            await UniTask.Yield();
        }
        unit.transform.localPosition = Vector2.zero;
    }

}
