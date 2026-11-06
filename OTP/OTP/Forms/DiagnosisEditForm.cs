using OTP.Entities;
using OTP.Repositories;
using System;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class DiagnosisEditForm : Form
    {
        private readonly IDiagnosisRepository _repository;
        private Diagnosis? _toEdit;
        private TextBox _txtName;

        public DiagnosisEditForm(IDiagnosisRepository repository)
        {
            _repository = repository;
            Text = "Диагноз";
            Size = new Size(350, 150);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            var lbl = new Label { Text = "Название:", Left = 20, Top = 20, Width = 80 };
            _txtName = new TextBox { Left = 100, Top = 20, Width = 200 };
            var btnSave = new Button { Text = "Сохранить", Left = 100, Top = 60, Width = 90, Height = 30 };
            btnSave.Click += BtnSave_Click;
            var btnCancel = new Button { Text = "Отмена", Left = 210, Top = 60, Width = 90, Height = 30 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lbl, _txtName, btnSave, btnCancel });
        }

        public void InitializeForEdit(Diagnosis item)
        {
            _toEdit = item; _txtName.Text = item.Name; Text = "Редактирование";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_txtName.Text)) throw new ArgumentException("Введите название.");
                if (_toEdit == null) _repository.CreateDiagnosis(Diagnosis.CreateEntity(0, _txtName.Text));
                else _repository.UpdateDiagnosis(Diagnosis.CreateEntity(_toEdit.Id, _txtName.Text));
                DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}