using DG.Tweening;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 rightOffset, leftOffset;
    private Vector3 right, left;
    [SerializeField] private float time;
    [SerializeField] private Ease ease;

    private void Awake()
    {

        right = transform.position + rightOffset;
        left = transform.position + leftOffset;
    }

    private void Start()
    {
        int _random = Random.Range(0,1) < 0.5f ? -1 : 1;

        MovePlatform(_random);

        
    }

    private void MovePlatform(int random)
    {
        if (random == -1)
        {
            transform.DOMove(right, time)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    transform.DOMove(left, time)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        MovePlatform(random);
                    });
                });
        }
        else
        {
            transform.DOMove(left, time)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    transform.DOMove(right, time)
                    .SetEase(ease)
                    .OnComplete(() =>
                    {
                        MovePlatform(random);
                    });
                });
        }
    }
}
