using TMPro;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{
    [SerializeField] private FingerController thumb;
    [SerializeField] private FingerController index;
    [SerializeField] private FingerController middle;
    [SerializeField] private FingerController ring;
    [SerializeField] private FingerController pinky;

    [SerializeField] private TMP_Text gestureText;

    public GripType CurrentGrip { get; private set; }


    void Update()
    {
        DetectGesture();

        if (gestureText != null)
            gestureText.text = CurrentGrip.ToString();
    }

    void DetectGesture()
    {
        if (thumb.IsCurled &&
            index.IsCurled &&
            !middle.IsCurled &&
            !ring.IsCurled &&
            !pinky.IsCurled)
        {
            CurrentGrip = GripType.PrecisionPinch;
        }
        else if (thumb.IsCurled &&
                 index.IsCurled &&
                 middle.IsCurled &&
                 !ring.IsCurled &&
                 !pinky.IsCurled)
        {
            CurrentGrip = GripType.TripodGrip;
        }
        else if (thumb.IsCurled &&
                 index.IsCurled &&
                 middle.IsCurled &&
                 ring.IsCurled &&
                 pinky.IsCurled)
        {
            CurrentGrip = GripType.PowerGrip;
        }
        else
        {
            CurrentGrip = GripType.None;
        }
    }
}