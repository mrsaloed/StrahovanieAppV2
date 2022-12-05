using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace StrahovanieAppV2
{
    public partial class AuthorizationForm : MaterialForm
    {

        private string login;
        private string password;
        public AuthorizationForm()
        {
            InitializeComponent();
        }

        //Обработчик нажатия кнопки "Войти"
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (SQL.GetInstance().IsConnected())
            {
                if (ValidateChildren(ValidationConstraints.Enabled) && SQL.GetInstance().IsConnected())
                {
                    int returnCode = SQL.GetInstance().Autent(login, password);
                    Employee employee = SQL.GetInstance().GetEmployee(returnCode);
                    if (returnCode != -1 && employee != null)
                    {
                        new Main(employee).Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Проверьте правильность введенных данных", "Ошибка", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Нет соединения с базой данных", "Ошибка подключения к БД", MessageBoxButtons.OK);
            }
        }

        //Валидация текстбокса с логином
        private void LoginTextBox_Validating(object sender, CancelEventArgs e)
        {
            string input = LoginTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(LoginTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(LoginTextBox, String.Empty);
                e.Cancel = false;
            }
        }


        //Валидация текстбокса пароль
        private void PasswordTextBox_Validating(object sender, CancelEventArgs e)
        {
            string input = PasswordTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(PasswordTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(PasswordTextBox, String.Empty);
                e.Cancel = false;
            }
        }
    
        //Действие после валидации текстбокса логина
        private void LoginTextBox_Validated(object sender, EventArgs e)
        {
            login = LoginTextBox.Text;
        }

        //Действие после валидации текстбокса пароль
        private void PasswordTextBox_Validated(object sender, EventArgs e)
        {
            password = PasswordTextBox.Text;
        }

        //Действие при нажатии на клавиатуру
        private void AuthorizationForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginBtn.PerformClick();
            }
        }

        //Действие при закрытии формы
        private void AuthorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
