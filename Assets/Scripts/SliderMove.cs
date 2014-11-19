using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;

public class SliderMove : MonoBehaviour {

    public UnityEngine.UI.Image RotationDevice;
    public UnityEngine.UI.Scrollbar slider;
    bool move = false;

	void Awake () {
        slider = GetComponent<UnityEngine.UI.Scrollbar>();

	}

    void Start()
    {
        StartCoroutine(waitAndMove());
        StartCoroutine(waitAndRotate());
    }
    
   public void MoveIt(bool val)
    {
        move = val;
    }

    float getValue()
    {
        float val = slider.value - .5f;
        val *= 5;
        return val;
    }

    public void DragIt(PointerEventData ed)
    {
        RotationDevice.rectTransform.position = ed.position;
    }
    
    IEnumerator waitAndMove()
    {
        while (true)
        {
            if (move)
                Movement.Instance.Move(Vector2.up * getValue());
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator waitAndRotate()
    {
         while (true)
        {
            var t = Camera.main.ScreenToWorldPoint(RotationDevice.rectTransform.position);
            Movement.Instance.Rotate(t);
            yield return new WaitForEndOfFrame();
        }
    }
}
