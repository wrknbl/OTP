using System;
using System.Linq;
using System.Windows.Forms;
using Autofac;
using OTP.Repositories;

namespace OTP.Forms
{
    public class AppointmentsListForm : Form
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IContainer _container;
        private DataGridView _dataGridView;

        public AppointmentsListForm(IAppointmentRepository appointmentRepository, IContainer container)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _container = container ?? throw new ArgumentNullException(nameof(container));
            InitializeComponents();
            LoadList();
        }

        private void InitializeComponents()
        {
            Text = "Журнал приёмов";
            Size = new Size(800, 450);
            StartPosition = FormStartPosition.CenterParent;

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

            var panel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
            var btnAdd = new Button { Text = "Добавить приём", Left = 10, Top = 10, Width = 150, Height = 30 };
            btnAdd.Click += (s, e) =>
            {
                var form = _container.Resolve<AppointmentCreateForm>();
                if (form.ShowDialog() == DialogResult.OK) LoadList();
            };
            panel.Controls.Add(btnAdd);

            Controls.Add(_dataGridView);
            Controls.Add(panel);
        }

        private void LoadList()
        {
            _dataGridView.DataSource = null;
            _dataGridView.DataSource = _appointmentRepository.ReadAppointments().ToList();
        }
    }
}