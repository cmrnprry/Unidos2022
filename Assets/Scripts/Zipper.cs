using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipper : MonoBehaviour
{
    public Animator anim;
    private bool CanDrag = false;
    private bool CanProgress = false;
    private int ProgressState = 0;
    public RectTransform zipper;
    public RectTransform tag;

    [Header("Zipper Bounds")]
    public Vector3 CLOSED;
    public Vector3 OPEN;
    private float Current;
    public float StateOne;
    public float StateTwo;
    public float StateThree;

    public static bool zipped = false;
    private Sequence seqMovvement = null;
    private Sequence seqRotation = null;

    [SerializeField]
    AudioSource[] zipSounds;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        anim.speed = 0;
        Current = StateOne;
        StartCoroutine(ZipperLoop());
    }

    private void Update()
    {
        //check if reached end
        if (Current == CLOSED.y && zipper.position.y >= CLOSED.y - 10f)
        {
            zipped = true;
            DialogueController.instance.SetBool(false);
        }
        else
        {
            if (CanDrag && (Input.mousePosition.y > OPEN.y && Input.mousePosition.y <= Current))
            {
                Resistance(ProgressState);
                int x = Random.Range(0,2);//ff
                  if(!zipSounds[0].isPlaying && !zipSounds[1].isPlaying){
                    zipSounds[x].pitch = zipSounds[x].pitch + ((float) Random.Range(-1,2) / 10f);
                    zipSounds[x].Play();
                  }//ff
                zipper.position = new Vector3(zipper.position.x, Input.mousePosition.y, 1f);
            }

            if (CanDrag)
            {
                //look at mouse
                var dir = Input.mousePosition - tag.position;
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                tag.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            Animation(zipper.position.y / CLOSED.y);
        }
    }

    IEnumerator ZipperLoop()
    {
        yield return new WaitUntil(() => Input.mousePosition.y >= Current);

        CanProgress = true;
        yield return new WaitUntil(() => CheckState());

        if (ProgressState < 3)
        {
            ProgressState += 1;
        }

        yield return new WaitForFixedUpdate();
        StartCoroutine(ZipperLoop());
    }

    private void Resistance(int state)
    {
        //first time
        if (state == 0)
        {
            Current = StateOne;
        }
        else if (state == 1)
        {
            Current = StateTwo;
        }
        else if (state == 2)
        {
            Current = StateThree;
        }
        else
        {
            Current = CLOSED.y;
        }
    }

    private bool CheckState()
    {
        if (CanProgress && Input.mousePosition.y <= OPEN.y)
        {
            CanProgress = false;
            return true;
        }

        return false;
    }

    public void ClickZipper()
    {
        CanDrag = true;
        if (seqMovvement != null)
            seqMovvement.Kill();
        if (seqRotation != null)
            seqRotation.Kill();
    }

    public void UnClickZipper()
    {
        CanDrag = false;

            Unzip();
    }

    private void Unzip()
    {
        seqMovvement = DOTween.Sequence();
        seqRotation = DOTween.Sequence();

        if (zipper.position.y < CLOSED.y && zipper.anchoredPosition.y > -413f && !zipped)
        {
            var pos = new Vector3(zipper.position.x, zipper.position.y - 25, 0f);
            seqMovvement.Append(zipper.DOMove(pos, 2, false));
        }

        if (tag.rotation.z != 0f)
        {
            seqRotation.Append(tag.DORotate(Vector3.zero, 2));
        }
    }

    public void Animation(float value)
    {
        //float time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        anim.Play("zipper", 0, value);
        anim.speed = 0;
    }
}
