using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipper : MonoBehaviour
{
    public Animator anim;
    private bool CanDrag = false;
    public RectTransform zipper;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (CanDrag && (Input.mousePosition.y > 0 && Input.mousePosition.y <= 500))
        {
            float normalized = Input.mousePosition.y / 500;
            zipper.position = new Vector3(zipper.position.x, Input.mousePosition.y, 1f) + offset;
            Animation(normalized);

        }
    }

    public void ClickZipper()
    {
        CanDrag = true;
    }

    public void UnClickZipper()
    {
        CanDrag = false;
    }

    public void Animation(float value)
    {
        //float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        anim.Play("test", 0, value);
        anim.speed = 0;
    }
}
