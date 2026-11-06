using OTP.Entities;
using OTP.Repositories;
using System;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class SpecializationEditForm : Form
    {
        private readonly ISpecializationRepository _repository;
        private Specialization? _toEdit;
        private TextBox _txtName;

        public SpecializationEditForm(ISpecializationRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Добавление специализации";
            Size = new Size(350, 150);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lbl = new Label { Text = "Название:", Left = 20, Top = 20, Width = 80 };
            _txtName = new TextBox { Left = 100, Top = 20, Width = 200 };

            var btnSave = new Button { Text = "Сохранить", Left = 100, Top = 60, Width = 90, Height = 30 };
            btnSave.Click += BtnSave_Click;

            var btnCancel = new Button { Text = "Отмена", Left = 210, Top = 60, Width = 90, Height = 30 };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lbl, _txtName, btnSave, btnCancel });
        }

        public void InitializeForEdit(Specialization item)
        {
            _toEdit = item;
            _txtName.Text = item.Name;
            Text = "Редактирование специализации";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка на пустое поле (защита от null произойдет внутри CreateEntity)
                if (string.IsNullOrWhiteSpace(_txtName.Text))
                    throw new ArgumentException("Введите название специализации.");

                if (_toEdit == null)
                {
                    // Режим добавления
                    _repository.CreateSpecialization(Specialization.CreateEntity(0, _txtName.Text));
                }
                else
                {
                    // Режим редактирования
                    _repository.UpdateSpecialization(Specialization.CreateEntity(_toEdit.Id, _txtName.Text));
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