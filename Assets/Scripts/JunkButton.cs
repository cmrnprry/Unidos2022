using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JunkButton : MonoBehaviour
{
    public RectTransform button;
    public float floor;
    public GameObject buttons;
    public GameObject zipper;

    private bool CanDrag = false;
    private Sequence seq = null;
    private bool done;

    private void Update()
    {
        if (Zipper.zipped)
        {
            buttons.SetActive(true);
            zipper.SetActive(false);
        }

        if (CanDrag && !done)
        {
            button.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        }
    }

    public void ClickButton()
    {
        CanDrag = true;
        seq.Kill();
    }

    public void CheckButton()
    {
        if (CanDrag)
        {
            DialogueController.instance.SetBool(false);
            done = true;
            UnClickButton();
        }
        else
        {
            Debug.Log("not here :/");
        }
    }

    public void UnClickButton()
    {
        CanDrag = false;

        if (!done)
        {
            seq = DOTween.Sequence();

            if (button.position.y > floor)
            {
                var pos = new Vector3(button.position.x, floor, 0f);
                seq.Append(button.DOMove(pos, 1f, false));
            }
        }

    }
}
