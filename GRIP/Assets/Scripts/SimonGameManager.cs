using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimonGameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text objectText;
    [SerializeField] private TMP_Text timerText;

    [Header("Settings")]
    [SerializeField] private float assessmentTime = 5f;

    [Header("Collection")]
    [SerializeField] private Transform[] collectionSlots;
    [SerializeField] private float collectDuration = 0.5f;

    private int nextCollectionSlot = 0;

    private readonly List<ObjectType> objectQueue = new()
    {
        ObjectType.Apple,
        ObjectType.Coin,
        ObjectType.Key,
        ObjectType.Mug
    };

    private int currentIndex;

    private bool assessing;
    private bool collecting;

    private Coroutine assessmentCoroutine;

    public ObjectType CurrentTarget => objectQueue[currentIndex];

    private void Start()
    {
        ShowCurrentObject();
    }

    private void ShowCurrentObject()
    {
        objectText.text = $"Grab: {CurrentTarget}";
        timerText.text = "";
    }

    public void BeginAssessment(GrabbableObject obj)
    {
        if (assessing || collecting)
            return;

        // Wrong object? Fail immediately.
        if (obj.ObjectType != CurrentTarget)
        {
            objectText.text = "GAME OVER";
            timerText.text = "";
            return;
        }

        assessing = true;
        assessmentCoroutine = StartCoroutine(AssessmentRoutine(obj));
    }

    public void CancelAssessment()
    {
        if (collecting)
            return;

        if (!assessing)
            return;

        assessing = false;

        if (assessmentCoroutine != null)
        {
            StopCoroutine(assessmentCoroutine);
            assessmentCoroutine = null;
        }

        timerText.text = "";
        ShowCurrentObject();
    }

    private IEnumerator AssessmentRoutine(GrabbableObject obj)
    {
        float timer = assessmentTime;

        while (timer > 0f)
        {
            if (!assessing)
                yield break;

            timer -= Time.deltaTime;

            timerText.text = $"Assessing: {timer:0.0}s";

            yield return null;
        }

        assessing = false;
        collecting = true;

        yield return StartCoroutine(CollectObject(obj));

        collecting = false;

        currentIndex++;

        if (currentIndex >= objectQueue.Count)
        {
            objectText.text = "YOU WIN!";
            timerText.text = "";
            yield break;
        }

        ShowCurrentObject();
    }

    private IEnumerator CollectObject(GrabbableObject obj)
    {
        if (nextCollectionSlot >= collectionSlots.Length)
            yield break;

        Transform target = collectionSlots[nextCollectionSlot];

        Rigidbody rb = obj.Rigidbody;

        bool oldKinematic = rb.isKinematic;
        rb.isKinematic = true;

        // Disable all colliders while moving
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        Vector3 startPos = obj.transform.position;
        Quaternion startRot = obj.transform.rotation;

        Vector3 endPos = target.position;
        Quaternion endRot = target.rotation;

        float elapsed = 0f;

        // Height of the arc
        float arcHeight = 0.2f;

        while (elapsed < collectDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / collectDuration);

            // Smooth ease in/out
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // Base movement
            Vector3 pos = Vector3.Lerp(startPos, endPos, smoothT);

            // Arc
            pos.y += Mathf.Sin(smoothT * Mathf.PI) * arcHeight;

            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.Slerp(startRot, endRot, smoothT);

            yield return null;
        }

        obj.transform.position = endPos;
        obj.transform.rotation = endRot;

        // Re-enable colliders
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        rb.isKinematic = oldKinematic;

        nextCollectionSlot++;
    }
}