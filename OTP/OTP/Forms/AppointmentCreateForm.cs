using OTP.Entities;
using OTP.Entities.Enums;
using OTP.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class AppointmentCreateForm : Form
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDiagnosisRepository _diagnosisRepository;

        private ComboBox _cmbPatient, _cmbDoctor, _cmbDiagnosis, _cmbResult;
        private CheckedListBox _checkedServices;

        public AppointmentCreateForm(
            IAppointmentRepository appointmentRepository,
            IPatientRepository patientRepository,
            IDoctorRepository doctorRepository,
            IDiagnosisRepository diagnosisRepository)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _diagnosisRepository = diagnosisRepository ?? throw new ArgumentNullException(nameof(diagnosisRepository));
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            Text = "Новый приём";
            Size = new Size(500, 450);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Пациент
            var lblPatient = new Label { Text = "Пациент:", Left = 20, Top = 20, Width = 120 };
            _cmbPatient = new ComboBox { Left = 150, Top = 20, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };

            // Врач
            var lblDoctor = new Label { Text = "Врач:", Left = 20, Top = 55, Width = 120 };
            _cmbDoctor = new ComboBox { Left = 150, Top = 55, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };

            // Диагноз
            var lblDiagnosis = new Label { Text = "Диагноз:", Left = 20, Top = 90, Width = 120 };
            _cmbDiagnosis = new ComboBox { Left = 150, Top = 90, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };

            // Результат
            var lblResult = new Label { Text = "Результат:", Left = 20, Top = 125, Width = 120 };
            _cmbResult = new ComboBox { Left = 150, Top = 125, Width = 320, DropDownStyle = ComboBoxStyle.DropDownList };

            // Услуги
            var lblServices = new Label { Text = "Услуги на приёме:", Left = 20, Top = 160, Width = 150 };
            _checkedServices = new CheckedListBox { Left = 150, Top = 160, Width = 320, Height = 150 };

            // Кнопки
            var btnSave = new Button { Text = "Сохранить", Left = 280, Top = 330, Width = 90, Height = 30 };
            btnSave.Click += BtnSave_Click;

            var btnCancel = new Button { Text = "Отмена", Left = 380, Top = 330, Width = 90, Height = 30 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[]
            {
                lblPatient, _cmbPatient,
                lblDoctor, _cmbDoctor,
                lblDiagnosis, _cmbDiagnosis,
                lblResult, _cmbResult,
                lblServices, _checkedServices,
                btnSave, btnCancel
            });
        }

        private void LoadData()
        {
            _cmbPatient.DataSource = _patientRepository.ReadPatients().ToList();
            _cmbPatient.DisplayMember = nameof(Patient.FullName);
            _cmbPatient.ValueMember = nameof(Patient.Id);

            _cmbDoctor.DataSource = _doctorRepository.ReadDoctors().ToList();
            _cmbDoctor.DisplayMember = nameof(Doctor.FullName);
            _cmbDoctor.ValueMember = nameof(Doctor.Id);

            _cmbDiagnosis.DataSource = _diagnosisRepository.ReadDiagnoses().ToList();
            _cmbDiagnosis.DisplayMember = nameof(Diagnosis.Name);
            _cmbDiagnosis.ValueMember = nameof(Diagnosis.Id);

            _cmbResult.DataSource = Enum.GetValues(typeof(AppointmentResult));

            _checkedServices.Items.Clear();
            foreach (AppointmentServices s in Enum.GetValues(typeof(AppointmentServices)))
            {
                if (s != AppointmentServices.None) _checkedServices.Items.Add(s);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cmbPatient.SelectedItem == null) throw new ArgumentException("Выберите пациента.");
                if (_cmbDoctor.SelectedItem == null) throw new ArgumentException("Выберите врача.");
                if (_cmbDiagnosis.SelectedItem == null) throw new ArgumentException("Выберите диагноз.");

                var patient = (Patient)_cmbPatient.SelectedItem;
                var doctor = (Doctor)_cmbDoctor.SelectedItem;
                var diagnosis = (Diagnosis)_cmbDiagnosis.SelectedItem;
                var result = (AppointmentResult)_cmbResult.SelectedItem;

                var services = AppointmentServices.None;
                foreach (AppointmentServices s in _checkedServices.CheckedItems)
                    services |= s;

                var appointment = Appointment.CreateOperation(
                    0, patient.Id, doctor.Id, diagnosis.Id, result, services);

                _appointmentRepository.CreateAppointment(appointment);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}