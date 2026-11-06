using System;
using System.Windows.Forms;
using Autofac;
using OTP.Repositories;
using OTP.Repositories.Implementations;
using OTP.Forms; // <-- ОБЯЗАТЕЛЬНО, если формы лежат в папке Forms

namespace OTP
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var container = CreateContainer();

                // Разрешаем главную форму
                var mainForm = container.Resolve<MainForm>();

                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                // Показываем полную ошибку
                MessageBox.Show(ex.ToString(), "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            // 1. Создаем переменную и регистрируем её (чтобы формы могли её получить)
            IContainer container = null;
            builder.Register(ctx => container).As<IContainer>();

            // 2. Регистрация репозиториев
            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>().InstancePerDependency();
            builder.RegisterType<PatientRepository>().As<IPatientRepository>().InstancePerDependency();
            builder.RegisterType<DiagnosisRepository>().As<IDiagnosisRepository>().InstancePerDependency();
            builder.RegisterType<SpecializationRepository>().As<ISpecializationRepository>().InstancePerDependency();
            builder.RegisterType<AppointmentRepository>().As<IAppointmentRepository>().InstancePerDependency();
            builder.RegisterType<DoctorSpecializationRepository>().As<IDoctorSpecializationRepository>().InstancePerDependency();

            // 3. Регистрация форм (не забудь добавить using OTP.Forms; в самом верху файла)
            builder.RegisterType<MainForm>().AsSelf();
            builder.RegisterType<DoctorsListForm>().AsSelf();
            builder.RegisterType<DoctorEditForm>().AsSelf();
            builder.RegisterType<PatientsListForm>().AsSelf();
            builder.RegisterType<PatientEditForm>().AsSelf();
            builder.RegisterType<DiagnosesListForm>().AsSelf();
            builder.RegisterType<DiagnosisEditForm>().AsSelf();
            builder.RegisterType<SpecializationsListForm>().AsSelf();
            builder.RegisterType<SpecializationEditForm>().AsSelf();
            builder.RegisterType<AppointmentsListForm>().AsSelf();
            builder.RegisterType<AppointmentCreateForm>().AsSelf();

            // 4. Собираем контейнер (ОБРАТИ ВНИМАНИЕ: без слова var!)
            container = builder.Build();

            return container;
        }
    }
}