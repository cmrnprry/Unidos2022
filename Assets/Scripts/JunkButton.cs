using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JunkButton : MonoBehaviour
{
    public RectTransform button;
    public float floor;
    private bool CanDrag = false;
    private Sequence seq = null;

    private void Update()
    {
        if (CanDrag)
        {
            button.position = new Vector3(button.position.x, Input.mousePosition.y, 1f);
        }
    }

    public void ClickButton()
    {
        CanDrag = true;
    }

    public void UnClickButton()
    {
        CanDrag = false;
        seq = DOTween.Sequence();

        if (button.position.y > floor)
        {
            var pos = new Vector3(button.position.x, floor, 0f);
            seq.Append(button.DOMove(pos, 2, false));
        }
    }
}
