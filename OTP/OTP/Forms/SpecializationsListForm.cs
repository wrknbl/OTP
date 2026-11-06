using Autofac;
using OTP.Entities;
using OTP.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class SpecializationsListForm : Form
    {
        private readonly ISpecializationRepository _repository;
        private readonly IContainer _container;
        private DataGridView _dataGridView;

        public SpecializationsListForm(ISpecializationRepository repository, IContainer container)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            InitializeComponents();
            LoadList();
        }

        private void InitializeComponents()
        {
            Text = "Справочник: Специализации";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterParent;

            // Таблица
            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            // Панель с кнопками
            var panel = new Panel { Dock = DockStyle.Bottom, Height = 50 };

            var btnAdd = new Button { Text = "Добавить", Left = 10, Top = 10, Width = 120, Height = 30 };
            btnAdd.Click += BtnAdd_Click;

            var btnEdit = new Button { Text = "Редактировать", Left = 140, Top = 10, Width = 120, Height = 30 };
            btnEdit.Click += BtnEdit_Click;

            var btnDelete = new Button { Text = "Удалить", Left = 270, Top = 10, Width = 120, Height = 30 };
            btnDelete.Click += BtnDelete_Click;

            panel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });

            Controls.Add(_dataGridView);
            Controls.Add(panel);
        }

        private void LoadList()
        {
            _dataGridView.DataSource = null;
            _dataGridView.DataSource = _repository.ReadSpecializations().ToList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = _container.Resolve<SpecializationEditForm>();
            if (form.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;

            var item = _dataGridView.SelectedRows[0].DataBoundItem as Specialization;
            if (item == null) return;

            var form = _container.Resolve<SpecializationEditForm>();
            form.InitializeForEdit(item);

            if (form.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;

            var item = _dataGridView.SelectedRows[0].DataBoundItem as Specialization;
            if (item == null) return;

            var result = MessageBox.Show(
                $"Удалить специализацию \"{item.Name}\"?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _repository.DeleteSpecialization(item.Id);
                LoadList();
            }
        }
    }
}