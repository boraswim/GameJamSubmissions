using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTween_Machine : DOTween_Animation
{
    public Transform parent, puzzleparent, cube, puzzle1, puzzle2, circle1, circle2;
    private void Start()
    {
        if (playOnStart)
            PlayMachineIdle();
    }
    public void PlayMachineIdle()
    {
        cube.DOLocalRotate(new Vector3(25, 25, 0), 0.4f, RotateMode.FastBeyond360)
         .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Incremental);
        /*
               puzzle1.DOLocalRotate(new Vector3(0, 0, 25), 0.4f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
               .SetLoops(-1, LoopType.Incremental);

              puzzle2.DOLocalRotate(new Vector3(0, 0, -25), 0.4f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
               .SetLoops(-1, LoopType.Incremental);
       */
        circle1.DOLocalRotate(new Vector3(25, 25, 0), 0.4f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
             .SetLoops(-1, LoopType.Incremental);

        circle2.DOLocalRotate(new Vector3(-25, 0, -25), 0.4f, RotateMode.FastBeyond360)
         .SetEase(Ease.Linear)
         .SetLoops(-1, LoopType.Incremental);

        puzzleparent.DOLocalRotate(new Vector3(35, 35, 0), 0.4f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
               .SetLoops(-1, LoopType.Incremental);

        parent.DOMoveY(transform.position.y + 0.1f, 0.4f)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Yoyo);

    }
}
