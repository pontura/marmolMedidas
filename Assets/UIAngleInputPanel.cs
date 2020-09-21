using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAngleInputPanel : MonoBehaviour
{
    public GameObject panel;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject result;
    public int id;
    public Image progressBar;
    public Text  field;
    public Text debugField;
    public Text resultField;

    public Text result1Field;
    public Text result2Field;

    int angleID;
    public InputField inputField;
    float originalValue;
    VerticeAngle verticeAngle;
    float speed = 2;
    int firstWallPressed;
    int secondWallPressed;
    float totalTime = 5;
    Quaternion angle1;
    Quaternion angle2;
    public states state;
    public enum states
    {
        IDLE,
        CALCULATING_1,
        DONE_1,
        CALCULATING_2,
        DONE_2
    }

    void Start()
    {
        panel.SetActive(false);
        Input.gyro.enabled = false;
        inputField.onEndEdit.AddListener(val =>
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                SetValueByField();
        });
    }
    void Close()
    {
        panel.SetActive(false);
    }
    private void Reset()
    {
        progressBarValue = 0;
        progressBar.fillAmount = 0;
        lastGyroValue = 0;
        resultField.text = "";
        debugField.text = "";
    }
    public void Init(VerticeAngle verticeAngle)//int angleID, float _originalValue)
    {
        state = states.IDLE;
        result.SetActive(false);
        Reset();
        this.verticeAngle = verticeAngle;
        this.originalValue = verticeAngle.angle;
        inputField.text = Utils.RoundNumber(verticeAngle.angle, 2).ToString();
        inputField.Select();
        this.angleID = verticeAngle.id;
        panel.SetActive(true);
        SetActivePanel();
    }
    
    public void CaculatePressed(int id)
    {
        progressBarValue = 0;
        if (state == states.IDLE)
        {
            firstWallPressed = id;
            Input.gyro.enabled = true;
            state = states.CALCULATING_1;
            Reset();
        }
        else
        {
            secondWallPressed = id;
            state = states.CALCULATING_2;
        }
        SetActivePanel();
    }
    void SetActivePanel()
    {
        switch (state)
        {
            case states.IDLE:
                field.text = "Coloca el ipad sobre alguna de estas 2 caras.";
                panel1.SetActive(true);
                panel2.SetActive(false);
                break;
            case states.CALCULATING_1:
                field.text = "Calculando";
                panel1.SetActive(true);
                panel2.SetActive(false);
                break;
            case states.DONE_1:
                field.text = "Ahora sobre sobre alguna de estas 2 caras.";
                panel2.SetActive(true);
                panel1.SetActive(false);
                break;
            case states.CALCULATING_2:
                field.text = "Calculando...";
                panel2.SetActive(true);
                panel1.SetActive(false);
                break;
            case states.DONE_2:
                field.text = "Elegir ángulo interno o externo";
                panel2.SetActive(false);
                panel1.SetActive(false);
                break;
        }
    }

    float lastGyroValue = 0;
    float progressBarValue = 0;

    private void Update()
    {
        if (!Input.gyro.enabled)
            return;
        debugField.text = Utils.RoundNumber(Input.gyro.attitude.eulerAngles.z, 3) + "°";
        if (state != states.CALCULATING_1 && state != states.CALCULATING_2)
            return;
        float newAngle = Utils.RoundNumber(Input.gyro.attitude.eulerAngles.z, 1);
        if (lastGyroValue == newAngle || lastGyroValue == 0)
        {
            progressBarValue += Time.deltaTime * speed;
            lastGyroValue = newAngle;
        }
        else
        {
            if (state == states.CALCULATING_1)
                state = states.IDLE;
            if (state == states.CALCULATING_2)
                state = states.DONE_1;
            progressBarValue = 0;
            lastGyroValue = 0;
        }
        progressBar.fillAmount = progressBarValue / totalTime;
        if (progressBar.fillAmount >= 1)
            Done();
        
    }
    public void SetNewAngle(float value)
    {

        MappingManager.Instance.verticeAngleManager.ChangeAngle(angleID, originalValue, value);
        MappingManager.Instance.verticeAngleManager.ConfirmAngle(verticeAngle.id - 1);

        MappingManager.Instance.confirmations.SetNextConfirm();
        Close();
    }
    public void SetValueByField()
    {
        float value;
        if(float.TryParse(inputField.text, out value))
            SetNewAngle(value);
    }
    void Done()
    {
        progressBar.fillAmount = 0;
        if (state == states.CALCULATING_1)
        {
            angle1 = Input.gyro.attitude;
            debugField.text = "Cara 1 = " + Input.gyro.attitude.eulerAngles.z + "°";
            state = states.DONE_1;
        }
        else
        {
            angle2 = Input.gyro.attitude;
            //float value_angle1 = 90 + Quaternion.Angle(angle1, angle2);
            float diff = Quaternion.Angle(angle1, angle2);
            if ((firstWallPressed == 1 && secondWallPressed == 2)
                ||
                (firstWallPressed == 2 && secondWallPressed == 1))
                diff *= -1;

            float value_angle1 = 90 + diff;
            float value_angle2 = 360 - value_angle1;

            result1Field.text = value_angle1.ToString();
            result2Field.text = value_angle2.ToString();
            resultField.text = "p1: " + firstWallPressed + " p2: "  + secondWallPressed + "  a1:" + angle1.eulerAngles.z + " a2:" + angle2.eulerAngles.z;
           // resultField.text = Quaternion.Angle(angle1, angle2) + "_" + Quaternion.Angle(angle2, angle1);
            // resultField.text = "Ángulos [" + value + "°]" + " [" + value2 + "°]";
            inputField.text = value_angle1.ToString();
            state = states.DONE_2;
            Results();
        }
        SetActivePanel();
    }
    void Results()
    {
        Input.gyro.enabled = false;
        result.SetActive(true);
        panel2.SetActive(false);
    }
    public void ClickResult(int id)
    {
        if (id == 1)
            inputField.text = result1Field.text;
        else
            inputField.text = result2Field.text;
        SetValueByField();
    }
}
