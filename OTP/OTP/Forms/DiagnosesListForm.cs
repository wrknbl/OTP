using Autofac;
using OTP.Entities;
using OTP.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class DiagnosesListForm : Form
    {
        private readonly IDiagnosisRepository _repository;
        private readonly IContainer _container;
        private DataGridView _dataGridView;

        public DiagnosesListForm(IDiagnosisRepository repository, IContainer container)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            InitializeComponents();
            LoadList();
        }

        private void InitializeComponents()
        {
            Text = "Справочник: Диагнозы";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterParent;

            _dataGridView = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            var panel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
            var btnAdd = new Button { Text = "Добавить", Left = 10, Top = 10, Width = 120, Height = 30 };
            btnAdd.Click += (s, e) => { var f = _container.Resolve<DiagnosisEditForm>(); if (f.ShowDialog() == DialogResult.OK) LoadList(); };
            var btnEdit = new Button { Text = "Редактировать", Left = 140, Top = 10, Width = 120, Height = 30 };
            btnEdit.Click += BtnEdit_Click;
            var btnDelete = new Button { Text = "Удалить", Left = 270, Top = 10, Width = 120, Height = 30 };
            btnDelete.Click += BtnDelete_Click;
            panel.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });
            Controls.Add(_dataGridView);
            Controls.Add(panel);
        }

        private void LoadList() { _dataGridView.DataSource = null; _dataGridView.DataSource = _repository.ReadDiagnoses().ToList(); }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;
            var item = _dataGridView.SelectedRows[0].DataBoundItem as Diagnosis;
            if (item == null) return;
            var f = _container.Resolve<DiagnosisEditForm>();
            f.InitializeForEdit(item);
            if (f.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0) return;
            var item = _dataGridView.SelectedRows[0].DataBoundItem as Diagnosis;
            if (item != null && MessageBox.Show($"Удалить {item.Name}?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _repository.DeleteDiagnosis(item.Id);
                LoadList();
            }
        }
    }
}