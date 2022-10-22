using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally
{
    internal class PanelLogin : MonoBehaviour
    {
        //internal enum UserType
        //{
        //    None = 0,
        //    Teacher = 1,
        //    Student = 2,
        //}

        //[SerializeField] private Button btnSelectTeacher = null;
        //[SerializeField] private Button btnSelectStudent = null;
        [SerializeField] private Button btnContinue = null;
        [SerializeField] private TMP_InputField inputName = null;
        //private UserType currentUserType = UserType.None;
        private string username = string.Empty;

        internal void Init()
        {
            //this.btnSelectTeacher.onClick.AddListener(OnBtnSelectTeacher);
            //this.btnSelectStudent.onClick.AddListener(OnBtnSelectStudent);
            this.btnContinue.onClick.AddListener(OnBtnContinue);
            this.btnContinue.interactable = false;
            this.inputName.onValueChanged.AddListener(OnValidateInputName);
        }

        private void OnValidateInputName(string value)
        {
            this.btnContinue.interactable = this.inputName.text.Length > 2;
        }

        //private void OnBtnSelectTeacher()
        //{
        //    Debug.Log("ON BTN SELECT TEACHER!");
        //    this.btnContinue.interactable = true;
        //    btnSelectTeacher.interactable = false;
        //    btnSelectStudent.interactable = true;
        //    this.currentUserType = UserType.Teacher;
        //    TaskBarHandler.EventIn_SetTaskBarUsername.Invoke(this.currentUserType.ToString());
        //}

        //private void OnBtnSelectStudent()
        //{
        //    Debug.Log("ON BTN SELECT STUDENT!");
        //    this.btnContinue.interactable = true;
        //    btnSelectTeacher.interactable = true;
        //    btnSelectStudent.interactable = false;
        //    this.currentUserType = UserType.Student;
        //    TaskBarHandler.EventIn_SetTaskBarUsername.Invoke(this.currentUserType.ToString());
        //}

        private void OnBtnContinue()
        {
            Debug.Log("ONTINUE!");
            this.username = this.inputName.text;
            PanelsHandler.EventIn_SetPanelBody.Invoke(PanelsHandler.PanelType.RallySelection);
        }
    }
}
