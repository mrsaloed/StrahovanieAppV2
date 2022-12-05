using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace StrahovanieAppV2
{
    public partial class Main : MaterialForm
    {
        
        private Employee employee;//Переменная сотрудника. Заполняется при открытии формы. Хранит в себе объект Сотрудника, который работает с системой
        private Position position;//Переменная должности. Заполняется при открытии формы. Хранит в себе объект Должность, который предоствляет доступ к разделам системы
        private string clientLastname;
        private string clientFirstname;
        private string clientSurnname;
        private string clientPassportSeries;
        private string clientPassportNumber;
        private string clientDriverLicenseNumber;
        private string carSeries;
        private string carNumber;
        private string policySeries;
        private string policyNumber;
        private SQL sql = SQL.GetInstance(); //Переменная класса SQL. Предназначена для работы с базой данных.

        //Конструктор формы. В нем заполняются переменные сотрудника и должности, а так же задаютсяначальные значения.
        public Main(Employee employee)
        {
            this.employee = employee;
            this.position = sql.GetPosition(employee);
            InitializeComponent();
            SetTabs(position);
            UpdateDataGrid();
            ClientClearButton.Enabled = false;
            EditClientButton.Enabled = false;
            InsurerComboBox.Items.AddRange(sql.GetAllInsurerNames());
            //ClientDateOfBirthPicker.MaxDate = DateTime.Now;
            //ClientPassportDateTimePicker.MaxDate = DateTime.Now;
            //ClientDateLicensePicker.MaxDate = DateTime.Now;
            //ClientLicenseEndDateTimePicker.MinDate = DateTime.Now;
            //CarDateIssueTimePicker.MaxDate = DateTime.Now;
            //CarInspectionDateTimePicker.MaxDate = DateTime.Now;
        }
        //Действие, происходяещее после закрытия приложения
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        //Метод для изменения доступа к вкладкам в зависимости от должности пользователя
        private void SetTabs(Position position)
        {
            if (position.ID == 1)
            {
                MainlTabControl.TabPages.Remove(PaymasterTab);
            }
            if (position.ID == 2)
            {
                MainlTabControl.TabPages.Remove(PaymasterTab);
                MainlTabControl.TabPages.Remove(MangerTab);
            }
            if (position.ID == 3)
            {
                MainlTabControl.TabPages.Remove(PaymasterTab);
                MainlTabControl.TabPages.Remove(MangerTab);
                EditClientButton.Hide();
            }
            if (position.ID == 4)
            {
                MainlTabControl.TabPages.Remove(MangerTab);
                MainlTabControl.TabPages.Remove(AddPolicyTab);
                MainlTabControl.TabPages.Remove(NewCarTab);
                MainlTabControl.TabPages.Remove(NewClientTab);
            }
        }
        
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        //Обработчик кнопки "добавить" вкладки "клиент". Добавляет нового клиента в БД. 
        private void AddClientButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    int result = sql.AddClient(ClientLastnameTextbox.Text,
                                  ClientFirstnameTextBox.Text,
                                  ClientSurnameTextBox.Text,
                                  ClientDateOfBirthPicker.Value,
                                  ClientPassportSeriesTextBox.Text,
                                  ClientPassportNumberTextBox.Text,
                                  ClientPassportDateTimePicker.Value,
                                  ClientDriverLicenseNumberTextBox.Text,
                                  ClientDateLicensePicker.Value,
                                  ClientLicenseEndDateTimePicker.Value);
                    if (result == 0)
                    {
                        UpdateDataGrid();
                        AddClientButton.Enabled = false;
                        ClientClearButton.Enabled = true;
                    }
                    if (result == -1) MessageBox.Show("Ошибка в паспортных данных!", "Ошибка", MessageBoxButtons.OK);
                    if (result == -2) MessageBox.Show("Ошибка в данных ВУ!", "Ошибка", MessageBoxButtons.OK);
                    if (result == -3) MessageBox.Show("Ошибка при добавлении клиента!", "Ошибка", MessageBoxButtons.OK);

                }
                catch (Exception)
                {
                    MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else 
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK);
            }
        }
        //Обработчик кнопки "очистить" вкладки клиент. Очищает заполненные поля и активирует кнопку добавить
        private void ClientClearButton_Click(object sender, EventArgs e)
        {
            AddClientButton.Enabled = true;
            ClientClearButton.Enabled = false;
            EditClientButton.Enabled = false;
            ClientLastnameTextbox.Text = "";
            ClientFirstnameTextBox.Text = "";
            ClientSurnameTextBox.Text = "";
            ClientPassportSeriesTextBox.Text = "";
            ClientPassportNumberTextBox.Text = "";
            ClientDriverLicenseNumberTextBox.Text = "";
        }
        //Обработчик нажатия на таблицу "клиент". Выбирает клиента из таблицы и заносит его данные в поля.
        private void ClientDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ClientDataGridView.SelectedCells.Count > 0)
            {
                AddClientButton.Enabled = false;
                ClientClearButton.Enabled = true;
                EditClientButton.Enabled = true;
                var i = ClientDataGridView.SelectedCells[0].OwningRow.Index;
                ClientLastnameTextbox.Text = (string)ClientDataGridView[1, i].Value;
                ClientFirstnameTextBox.Text = (string)ClientDataGridView[2, i].Value;
                ClientSurnameTextBox.Text = (string)ClientDataGridView[3, i].Value;
                ClientDateOfBirthPicker.Value = (DateTime)ClientDataGridView[4, i].Value;
                ClientPassportSeriesTextBox.Text = (string)ClientDataGridView[5, i].Value;
                ClientPassportNumberTextBox.Text = (string)ClientDataGridView[6, i].Value;
                ClientPassportDateTimePicker.Value = (DateTime)ClientDataGridView[7, i].Value;
                ClientDriverLicenseNumberTextBox.Text = ClientDataGridView[8, i].Value.ToString();
                ClientDateLicensePicker.Value = (DateTime)ClientDataGridView[9, i].Value;
                ClientLicenseEndDateTimePicker.Value = (DateTime)ClientDataGridView[10, i].Value;
            }
        }

        //Обработчик кнопки "изменить" вкладки "клиент". Вносит изменения клиента в БД.
        private void EditClientButton_Click(object sender, EventArgs e)
        {
            try
            {
                var i = ClientDataGridView.SelectedCells[0].OwningRow.Index;
                int result = sql.UpdateClient(
                            ClientDataGridView[0, i].Value.ToString(),
                            ClientLastnameTextbox.Text,
                              ClientFirstnameTextBox.Text,
                              ClientSurnameTextBox.Text,
                              ClientDateOfBirthPicker.Value,
                              ClientPassportSeriesTextBox.Text,
                              ClientPassportNumberTextBox.Text,
                              ClientPassportDateTimePicker.Value,
                              ClientDriverLicenseNumberTextBox.Text,
                              ClientDateLicensePicker.Value,
                              ClientLicenseEndDateTimePicker.Value);

                if (result == 0) UpdateDataGrid();
                if (result == -1) MessageBox.Show("Ошибка в паспортных данных!", "Ошибка", MessageBoxButtons.OK);
                if (result == -2) MessageBox.Show("Ошибка в данных ВУ!", "Ошибка", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
            }
        }

        //Обработчик кнопки "добавить" вкладки "авто". Добавляет в БД новый авотомобиль.
        private void CarAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                int result = sql.AddCar(employee.ID,
                                        CarSeriesTextBox.Text,
                                        CarNumberTextBox.Text,
                                        CarDateIssueTimePicker.Value,
                                        CarSpecialMarksTextBox.Text,
                                        CarInspectionResultComboBox1.SelectedItem.ToString());

                if (result == 0) UpdateDataGrid();
                if (result == -1) MessageBox.Show("Ошибка в данных авто!", "Ошибка", MessageBoxButtons.OK);
                if (result == -2) MessageBox.Show("Ошибка в данных осмотра!", "Ошибка", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
            }
        }

        //Метод для обновления данных в таблицах.
        private void UpdateDataGrid()
        {
            CarDataGridView.DataSource = sql.GetAllCar();
            ClientDataGridView.DataSource = sql.GetAllClients();
            ClientChooseDataGridView.DataSource = sql.FindClient();
            CarChooseDataGridView.DataSource = sql.FindCar();
            PolicyDataGridView.DataSource = sql.GetAllPolicy();
        }

        //Обработчик поля "фамилия" вкладки "полис". Ищет клиента в таблице
        private void FindClientlTextBox_TextChanged(object sender, EventArgs e)
        {
            ClientChooseDataGridView.ClearSelection();
            if (string.IsNullOrWhiteSpace(FindClientlTextBox.Text)) return;

            var values = FindClientlTextBox.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < ClientChooseDataGridView.RowCount; i++)
            {
                foreach (string value in values)
                {
                    var row = ClientChooseDataGridView.Rows[i];

                    if (row.Cells["Фамилия"].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        //Обработчик поля "стс" вкладки "полис". Ищет авто в таблице
        private void CarFindTextBox_TextChanged(object sender, EventArgs e)
        {
            CarChooseDataGridView.ClearSelection();
            if (string.IsNullOrWhiteSpace(CarFindTextBox.Text)) return;

            var values = CarFindTextBox.Text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < CarChooseDataGridView.RowCount; i++)
            {
                foreach (string value in values)
                {
                    var row = CarChooseDataGridView.Rows[i];

                    if (row.Cells["Серия"].Value.ToString().Contains(value))
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        //Обработчик кнопки "добавить" вкладки "клиент". Добавляет новыйполис в БД.
        private void AddPolicyButton_Click(object sender, EventArgs e)
        {
            try
            {
                var i = ClientChooseDataGridView.SelectedCells[0].OwningRow.Index;
                var j = CarChooseDataGridView.SelectedCells[0].OwningRow.Index;
                int result = sql.AddPolicy(
                            employee.ID,
                            (int)ClientDataGridView[0, i].Value,
                           (int)CarChooseDataGridView[0, j].Value,
                            InsurerComboBox.SelectedIndex + 1,
                              PolicySeriesTextBox.Text,
                              PolicyNumberTextBox.Text);

                if (result == 0) UpdateDataGrid();
                if (result == -1) MessageBox.Show("Ошибка при добавлении!", "Ошибка", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка!", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void ClientLastnameTextbox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = ClientLastnameTextbox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(ClientLastnameTextbox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(ClientLastnameTextbox, String.Empty);
                e.Cancel = false;
            }
        }

        private void ClientFirstnameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = ClientFirstnameTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(ClientFirstnameTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(ClientFirstnameTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void ClientSurnameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = ClientSurnameTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(ClientSurnameTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(ClientSurnameTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void ClientPassportSeriesTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = ClientPassportSeriesTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(ClientPassportSeriesTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 4)
            {
                errorProvider1.SetError(ClientPassportSeriesTextBox, "- Серия паспорта должна содержать 4 символа.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(ClientPassportSeriesTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void ClientPassportNumberTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = ClientPassportNumberTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(ClientPassportNumberTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 6)
            {
                errorProvider1.SetError(ClientPassportNumberTextBox, "- Номер паспорта должен содержать 6 символов.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(ClientPassportNumberTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void ClientDriverLicenseNumberTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = ClientDriverLicenseNumberTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(ClientDriverLicenseNumberTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 10)
            {
                errorProvider1.SetError(ClientDriverLicenseNumberTextBox, "- Номер ВУ должен содержать 10 символов.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(ClientDriverLicenseNumberTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void ClientLastnameTextbox_Validated(object sender, EventArgs e)
        {
            clientLastname = ClientLastnameTextbox.Text;
        }

        private void ClientFirstnameTextBox_Validated(object sender, EventArgs e)
        {
            clientFirstname = ClientFirstnameTextBox.Text;
        }

        private void ClientSurnameTextBox_Click(object sender, EventArgs e)
        {
            clientSurnname = ClientSurnameTextBox.Text;
        }

        private void ClientPassportSeriesTextBox_Validated(object sender, EventArgs e)
        {
            clientPassportSeries = ClientPassportSeriesTextBox.Text;
        }

        private void ClientPassportNumberTextBox_Validated(object sender, EventArgs e)
        {
            clientPassportNumber = ClientPassportNumberTextBox.Text;
        }

        private void ClientDriverLicenseNumberTextBox_Validated(object sender, EventArgs e)
        {
            clientDriverLicenseNumber = ClientDriverLicenseNumberTextBox.Text;
        }

        private void CarSeriesTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = CarSeriesTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(CarSeriesTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 4)
            {
                errorProvider1.SetError(CarSeriesTextBox, "- Серия СТС должна содержать 4 символа.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(CarSeriesTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void CarNumberTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = CarNumberTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(CarNumberTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 6)
            {
                errorProvider1.SetError(CarNumberTextBox, "- Номер СТС должен содержать 6 символов.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(CarNumberTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void CarSeriesTextBox_Validated(object sender, EventArgs e)
        {
            carSeries = CarSeriesTextBox.Text;
        }

        private void CarNumberTextBox_Validated(object sender, EventArgs e)
        {
            carNumber = CarNumberTextBox.Text;
        }

        private void PolicySeriesTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = PolicySeriesTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(PolicySeriesTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 3)
            {
                errorProvider1.SetError(PolicySeriesTextBox, "- Серия полиса должна содержать 3 символа.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(PolicySeriesTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void PolicyNumberTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = PolicyNumberTextBox.Text.Trim();
            if (String.IsNullOrEmpty(input))
            {
                errorProvider1.SetError(PolicyNumberTextBox, "- Это поле является обязательным к заполнению.");
                e.Cancel = true;
            }
            else if (input.Length != 10)
            {
                errorProvider1.SetError(PolicyNumberTextBox, "- Номер полиса должен содержать 10 символов.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(PolicyNumberTextBox, String.Empty);
                e.Cancel = false;
            }
        }

        private void PolicySeriesTextBox_Validated(object sender, EventArgs e)
        {
            policySeries = PolicySeriesTextBox.Text;
        }

        private void PolicyNumberTextBox_Validated(object sender, EventArgs e)
        {
            policyNumber = PolicyNumberTextBox.Text;
        }
    }
}
