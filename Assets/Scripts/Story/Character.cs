using System.Collections;
using Enums;
using Live2D.Cubism.Core;
using Live2D.Cubism.Rendering;
using UnityEngine;


public class Character : MonoBehaviour
{

    [SerializeField]
    private string characterName;
    private CubismModel model;

    private void Awake()
    {
        model = GetComponent<CubismModel>();
        SetExpression(ExpressionEnum.Neutral);
        gameObject.SetActive(true);
    }

    public void SetExpression(ExpressionEnum expression)
    {
        if (model == null)
        {
            return;
        }
        ResetModelParameters();
        switch (expression)
        {
            case ExpressionEnum.Neutral:
                SetModelParameter(ModelParameterEnum.HandonHip, 1.0f);
                break;
            case ExpressionEnum.Happy:
                SetModelParameter(ModelParameterEnum.HandonHip, 1.0f);
                SetModelParameter(ModelParameterEnum.AngleX, 5.0f);
                SetModelParameter(ModelParameterEnum.AngleZ, 15.0f);
                SetModelParameter(ModelParameterEnum.MouthForm, 1.0f);
                SetModelParameter(ModelParameterEnum.MouthOpenY, 1.0f);
                break;
            case ExpressionEnum.Sad:
                break;
            case ExpressionEnum.Angry:
                SetModelParameter(ModelParameterEnum.EyeLOpen, 0.5f);
                SetModelParameter(ModelParameterEnum.EyeROpen, 0.5f);
                SetModelParameter(ModelParameterEnum.BrowLY, -1.0f);
                SetModelParameter(ModelParameterEnum.BrowRY, -1.0f);
                SetModelParameter(ModelParameterEnum.MouthForm, -1.0f);
                SetModelParameter(ModelParameterEnum.MouthOpenY, 0.0f);
                SetModelParameter(ModelParameterEnum.AngleX, -30.0f);
                SetModelParameter(ModelParameterEnum.AngleY, -30.0f);
                SetModelParameter(ModelParameterEnum.ArmsCrossed, 1.0f);
                break;
            case ExpressionEnum.Surprised:
                break;
            case ExpressionEnum.Confused:
                break;
            default:
                break;
        }
    }

    private void ResetModelParameters()
    {
        if (model == null)
        {
            return;
        }
        SetModelParameter(ModelParameterEnum.AngleX, 0.0f);
        SetModelParameter(ModelParameterEnum.AngleY, 0.0f);
        SetModelParameter(ModelParameterEnum.AngleZ, 0.0f);
        SetModelParameter(ModelParameterEnum.EyeLOpen, 1.0f);
        SetModelParameter(ModelParameterEnum.EyeROpen, 1.0f);
        SetModelParameter(ModelParameterEnum.BrowLY, 0.0f);
        SetModelParameter(ModelParameterEnum.BrowRY, 0.0f);
        SetModelParameter(ModelParameterEnum.MouthForm, 0.2f);
        SetModelParameter(ModelParameterEnum.MouthOpenY, 0.2f);
        SetModelParameter(ModelParameterEnum.ArmsCrossed, 0.0f);
        SetModelParameter(ModelParameterEnum.HandonHip, 0.0f);
    }

    private void SetModelParameter(ModelParameterEnum modelParameter, float value)
    {
        string parameterId = "Param" + modelParameter.ToString();
        CubismParameter parameter = model.Parameters.FindById(parameterId);
        if (parameter != null)
        {
            parameter.Value = Mathf.Clamp(value, parameter.MinimumValue, parameter.MaximumValue);
        }
    }

    public string GetName()
    {
        return characterName;
    }

    public void SetDialogueState(DialogueStateEnum dialogueState)
    {
        bool highlight = false;
        if (DialogueStateEnum.Speaker == dialogueState)
        {
            highlight = true;
        }
        SetCharacterColor(highlight);
    }

    private void SetCharacterColor(bool highlight)
    {
        CubismRenderer[] renderers = GetComponentsInChildren<CubismRenderer>();

        foreach (CubismRenderer r in renderers)
        {
            if (highlight)
            {
                r.Color = Color.white;
            }
            else
            {
                r.Color = new Color(0.5f, 0.5f, 0.5f, 1f);
            }
        }
    }

}
