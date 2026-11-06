using Autofac;
using OTP.Entities;
using OTP.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class PatientsListForm : Form
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IContainer _container;
        private DataGridView _dataGridView;

        public PatientsListForm(IPatientRepository patientRepository, IContainer container)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            InitializeComponents();
            LoadList();
        }

        private void InitializeComponents()
        {
            Text = "Справочник: Пациенты";
            Size = new Size(800, 450);
            StartPosition = FormStartPosition.CenterParent;

            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

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
            _dataGridView.DataSource = _patientRepository.ReadPatients().ToList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = _container.Resolve<PatientEditForm>();
            if (form.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedPatient(out Patient? patient)) return;
            var form = _container.Resolve<PatientEditForm>();
            form.InitializeForEdit(patient!);
            if (form.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedPatient(out Patient? patient)) return;
            if (MessageBox.Show($"Удалить пациента {patient!.FullName}?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _patientRepository.DeletePatient(patient.Id);
                LoadList();
            }
        }

        private bool TryGetSelectedPatient(out Patient? patient)
        {
            patient = null;
            if (_dataGridView.SelectedRows.Count == 0) return false;
            patient = _dataGridView.SelectedRows[0].DataBoundItem as Patient;
            return patient != null;
        }
    }
}