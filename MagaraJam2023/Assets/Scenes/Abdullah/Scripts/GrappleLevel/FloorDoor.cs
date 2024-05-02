using DG.Tweening;
using UnityEngine;

public class FloorDoor : MonoBehaviour
{
    public bool isOpen;
    public Vector3 direction;
    public float distance;
    public float time;
    public Ease ease;

    private void Start()
    {
        isOpen = false;
    }

    public void Open()
    {
        isOpen=true;

        Vector3 _finalPos = transform.position + (direction * distance);
        transform.DOMove(_finalPos, time).SetEase(ease).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
