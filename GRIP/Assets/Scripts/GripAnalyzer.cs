using UnityEngine;

public class GripAnalyzer : MonoBehaviour
{
    [SerializeField] private GestureRecognizer gestureRecognizer;

    public bool CanHold(GrabbableObject obj)
    {
        if (obj == null)
            return false;

        GripProfile profile = obj.GripProfile;

        if (gestureRecognizer.CurrentGrip != profile.requiredGrip)
            return false;

        int stability = CalculateStability(obj);

        return stability >= profile.stabilityRequired;
    }

    public int CalculateStability(GrabbableObject obj)
    {
        int score = 0;

        foreach (FingerRequirement requirement in obj.GripProfile.fingerRequirements)
        {
            if (obj.IsTouching(requirement.finger))
            {
                score += requirement.contribution;
            }
        }

        return Mathf.Clamp(score, 0, 100);
    }
}