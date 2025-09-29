using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Motion;
using UnityEngine;
using Enums;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;


public class Character : MonoBehaviour
{

    [SerializeField]
    private string name;
    [SerializeField]
    private CubismModel model;
    [SerializeField]
    private AnimationClip[] animationClips;

    public void SetExpression(Enums.Expression expression)
    {
        CubismMotionController controller = model.GetComponent<CubismMotionController>();
        
        if (controller != null)
        {
            controller.PlayAnimation(GetExpressionAnimationClip(expression));
        }
    }

    private AnimationClip GetExpressionAnimationClip(Enums.Expression expression)
    {
        string expressionName = expression.ToString();
        AnimationClip clip = null;

        // TODO get clip by name

        return clip;
    }

}
