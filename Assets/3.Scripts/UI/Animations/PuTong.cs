using UnityEngine;
using DG.Tweening;

public class PuTong : MonoBehaviour
{
    public Vector3 pos;
    void Start()
    {
        transform.localScale = transform.localScale / 2;
        transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }
}