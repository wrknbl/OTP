using System;
using System.Windows.Forms;
using Autofac;

namespace OTP.Forms
{
    public class MainForm : Form
    {
        private readonly ILifetimeScope _scope;

        private MenuStrip _menuStrip = null!;

        public MainForm(ILifetimeScope scope)
        {
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Поликлиника — Главное меню";
            this.Size = new System.Drawing.Size(450, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            _menuStrip = new MenuStrip();

            var menuDirectories = new ToolStripMenuItem("Справочники");

            var menuDoctors = new ToolStripMenuItem("Врачи");
            menuDoctors.Click += (s, e) => OpenForm<DoctorsListForm>();

            var menuPatients = new ToolStripMenuItem("Пациенты");
            menuPatients.Click += (s, e) => OpenForm<PatientsListForm>();

            var menuDiagnoses = new ToolStripMenuItem("Диагнозы");
            menuDiagnoses.Click += (s, e) => OpenForm<DiagnosesListForm>();

            var menuSpecializations = new ToolStripMenuItem("Специализации");
            menuSpecializations.Click += (s, e) => OpenForm<SpecializationsListForm>();

            menuDirectories.DropDownItems.AddRange(new ToolStripItem[]
            {
                menuDoctors, menuPatients, menuDiagnoses, menuSpecializations
            });

            var menuOperations = new ToolStripMenuItem("Операции");

            var menuAppointments = new ToolStripMenuItem("Приёмы");
            menuAppointments.Click += (s, e) => OpenForm<AppointmentsListForm>();

            menuOperations.DropDownItems.Add(menuAppointments);

            var menuReports = new ToolStripMenuItem("Отчеты");

            _menuStrip.Items.AddRange(new ToolStripItem[]
            {
                menuDirectories, menuOperations, menuReports
            });

            this.MainMenuStrip = _menuStrip;
            this.Controls.Add(_menuStrip);

            var label = new Label
            {
                Text = "Добро пожаловать в систему учёта пациентов поликлиники.\nВыберите раздел в меню сверху.",
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 12F)
            };
            this.Controls.Add(label);
        }

        private void OpenForm<T>() where T : Form
        {
            try
            {
                var form = _scope.Resolve<T>();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}