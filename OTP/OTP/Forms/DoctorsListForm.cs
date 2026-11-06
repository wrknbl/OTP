using Autofac;
using OTP.Entities;
using OTP.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OTP.Forms
{
    public class DoctorsListForm : Form
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IContainer _container;
        private DataGridView _dataGridView;

        public DoctorsListForm(IDoctorRepository doctorRepository, IContainer container)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            InitializeComponents();
            LoadList();
        }

        private void InitializeComponents()
        {
            Text = "Справочник: Врачи";
            Size = new Size(700, 450);
            StartPosition = FormStartPosition.CenterParent;

            // --- DataGridView ---
            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoGenerateColumns = true
            };
            _dataGridView.DataBindingComplete += (s, e) =>
            {
                // Скрываем Id в таблице (необязательно)
                if (_dataGridView.Columns["Id"] != null)
                    _dataGridView.Columns["Id"].Visible = false;
            };

            // --- Панель с кнопками ---
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
            _dataGridView.DataSource = _doctorRepository.ReadDoctors().ToList();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var form = _container.Resolve<DoctorEditForm>();
            if (form.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedDoctor(out Doctor? doctor)) return;

            var form = _container.Resolve<DoctorEditForm>();
            form.InitializeForEdit(doctor!);
            if (form.ShowDialog() == DialogResult.OK) LoadList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedDoctor(out Doctor? doctor)) return;

            if (MessageBox.Show($"Удалить врача {doctor!.FullName}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _doctorRepository.DeleteDoctor(doctor.Id);
                LoadList();
            }
        }

        private bool TryGetSelectedDoctor(out Doctor? doctor)
        {
            doctor = null;
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите запись в таблице.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            doctor = _dataGridView.SelectedRows[0].DataBoundItem as Doctor;
            return doctor != null;
        }
    }
}