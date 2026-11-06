using OTP.Entities;
using OTP.Repositories;
using System;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class DoctorEditForm : Form
    {
        private readonly IDoctorRepository _doctorRepository;
        private Doctor? _doctorToEdit;
        private TextBox _txtFullName;
        private TextBox _txtPhone;

        public DoctorEditForm(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Добавление врача";
            Size = new Size(400, 200);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            // --- ФИО ---
            var lblFullName = new Label { Text = "ФИО:", Left = 20, Top = 20, Width = 100 };
            _txtFullName = new TextBox { Left = 130, Top = 20, Width = 230 };

            // --- Телефон ---
            var lblPhone = new Label { Text = "Телефон:", Left = 20, Top = 60, Width = 100 };
            _txtPhone = new TextBox { Left = 130, Top = 60, Width = 230 };

            // --- Кнопки ---
            var btnSave = new Button { Text = "Сохранить", Left = 180, Top = 110, Width = 90, Height = 30 };
            btnSave.Click += BtnSave_Click;

            var btnCancel = new Button { Text = "Отмена", Left = 280, Top = 110, Width = 90, Height = 30 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[]
            {
                lblFullName, _txtFullName, lblPhone, _txtPhone, btnSave, btnCancel
            });
        }

        public void InitializeForEdit(Doctor doctor)
        {
            _doctorToEdit = doctor;
            _txtFullName.Text = doctor.FullName;
            _txtPhone.Text = doctor.Phone;
            Text = "Редактирование врача";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtFullName.Text))
                    throw new ArgumentException("Введите ФИО врача.");

                if (_doctorToEdit == null)
                {
                    _doctorRepository.CreateDoctor(Doctor.CreateEntity(0, _txtFullName.Text, _txtPhone.Text));
                }
                else
                {
                    _doctorRepository.UpdateDoctor(Doctor.CreateEntity(_doctorToEdit.Id, _txtFullName.Text, _txtPhone.Text));
                }

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