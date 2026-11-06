using OTP.Entities;
using OTP.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class PatientEditForm : Form
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private Patient? _patientToEdit;

        private TextBox _txtFullName;
        private TextBox _txtPhone;
        private ComboBox _cmbDoctor;

        public PatientEditForm(IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            InitializeComponents();
            LoadDoctors();
        }

        private void InitializeComponents()
        {
            Text = "Добавление пациента";
            Size = new Size(400, 250);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblFullName = new Label { Text = "ФИО:", Left = 20, Top = 20, Width = 100 };
            _txtFullName = new TextBox { Left = 130, Top = 20, Width = 230 };

            var lblPhone = new Label { Text = "Телефон:", Left = 20, Top = 60, Width = 100 };
            _txtPhone = new TextBox { Left = 130, Top = 60, Width = 230 };

            var lblDoctor = new Label { Text = "Участковый врач:", Left = 20, Top = 100, Width = 100 };
            _cmbDoctor = new ComboBox { Left = 130, Top = 100, Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };

            var btnSave = new Button { Text = "Сохранить", Left = 180, Top = 150, Width = 90, Height = 30 };
            btnSave.Click += BtnSave_Click;
            var btnCancel = new Button { Text = "Отмена", Left = 280, Top = 150, Width = 90, Height = 30 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lblFullName, _txtFullName, lblPhone, _txtPhone, lblDoctor, _cmbDoctor, btnSave, btnCancel });
        }

        private void LoadDoctors()
        {
            _cmbDoctor.DataSource = _doctorRepository.ReadDoctors().ToList();
            _cmbDoctor.DisplayMember = nameof(Doctor.FullName);
            _cmbDoctor.ValueMember = nameof(Doctor.Id);
        }

        public void InitializeForEdit(Patient patient)
        {
            _patientToEdit = patient;
            _txtFullName.Text = patient.FullName;
            _txtPhone.Text = patient.Phone;
            _cmbDoctor.SelectedValue = patient.DoctorId;
            Text = "Редактирование пациента";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtFullName.Text)) throw new ArgumentException("Введите ФИО.");
                if (_cmbDoctor.SelectedValue == null) throw new ArgumentException("Выберите врача.");

                int doctorId = (int)_cmbDoctor.SelectedValue;

                if (_patientToEdit == null)
                    _patientRepository.CreatePatient(Patient.CreateEntity(0, _txtFullName.Text, _txtPhone.Text, doctorId));
                else
                    _patientRepository.UpdatePatient(Patient.CreateEntity(_patientToEdit.Id, _txtFullName.Text, _txtPhone.Text, doctorId));

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}