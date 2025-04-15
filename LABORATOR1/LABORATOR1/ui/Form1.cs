using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;


using System.Data.SqlClient;
using System.Drawing;
using LABORATOR1.repository;
using LABORATOR1.service;
using LABORATOR1.validator;
using System.ComponentModel.DataAnnotations;

namespace LABORATOR1.ui
{
    public partial class UI : Form
    {
        string connectionString = @"Server=ASUS_C_U\SQLEXPRESS; Database=STEAM; Integrated Security=true;";
        private readonly Service service;
        private readonly Repository repository;
        private readonly LABORATOR1.validator.Validator validator;

        private DataGridView gamesDataGridView;
        private DataGridView achievementsDataGridView;

        private TextBox achievement_nameTextBox;
        private TextBox descriptionTextBox;


        public UI()
        {
            InitializeComponent();
            this.repository = new Repository(connectionString);
            this.service = new Service(repository);
            this.validator = new LABORATOR1.validator.Validator();
            this.CenterToScreen();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            addButtons();
            addLabels();
            addTextBoxes();
        }

        /* This function is an event that is triggered when the form is loaded. */
        private void UI_Load(object sender, EventArgs e)
        {
            DataTable gamesData = service.GetAllGames();
            if (gamesData != null)
            {
                gamesDataGridView.DataSource = gamesData;
                if (gamesDataGridView.Rows.Count > 0)
                    gamesDataGridView.Rows[0].Selected = true;
            }
            else
            {
                MessageBox.Show("Failed to retrieve data from the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* This function is an event that is triggered when a row is selected in the parent table. */
        private void GamesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (gamesDataGridView.SelectedRows.Count > 0)
            {
                int selectedGameId = (int)gamesDataGridView.SelectedRows[0].Cells["id"].Value;
                DataTable relatedAchievements = service.GetAchievementsByGameId(selectedGameId);
                achievementsDataGridView.DataSource = relatedAchievements;
            }
        }

        /* This function is an event that is triggered when the form is loaded to set up all the controls. */
        private void InitializeComponent()
        {
            gamesDataGridView = new DataGridView();
            achievementsDataGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gamesDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)achievementsDataGridView).BeginInit();
            SuspendLayout();
            // 
            // gamesDataGridView
            // 
            gamesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gamesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gamesDataGridView.Font = new Font("Comic Sans MS", 12F);
            gamesDataGridView.Location = new Point(30, 250);
            gamesDataGridView.Name = "gamesDataGridView";
            gamesDataGridView.RowHeadersWidth = 62;
            gamesDataGridView.RowTemplate.Height = 24;
            gamesDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gamesDataGridView.Size = new Size(700, 200);
            gamesDataGridView.TabIndex = 0;
            gamesDataGridView.SelectionChanged += GamesDataGridView_SelectionChanged;
            // 
            // achievementsDataGridView
            // 
            achievementsDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            achievementsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            achievementsDataGridView.Font = new Font("Comic Sans MS", 12F);
            achievementsDataGridView.Location = new Point(30, 500);
            achievementsDataGridView.Name = "achievementsDataGridView";
            achievementsDataGridView.RowHeadersWidth = 62;
            achievementsDataGridView.RowTemplate.Height = 24;
            achievementsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            achievementsDataGridView.Size = new Size(1460, 250);
            achievementsDataGridView.TabIndex = 1;
            // 
            // UI
            // 
            BackColor = Color.LightBlue;
            ClientSize = new Size(1520, 800);
            Controls.Add(gamesDataGridView);
            Controls.Add(achievementsDataGridView);
            Name = "UI";
            WindowState = FormWindowState.Maximized;
            Load += UI_Load;
            ((System.ComponentModel.ISupportInitialize)gamesDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)achievementsDataGridView).EndInit();
            ResumeLayout(false);
        }

        /* Function for Button management */
        private void addButtons()
        {
            Button addButton = new Button();
            addButton.Location = new System.Drawing.Point(900, 400);
            addButton.Width = 100;
            addButton.Height = 50;
            addButton.Name = "addButton";
            addButton.TabIndex = 2;
            addButton.Text = "Add";
            addButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            addButton.UseVisualStyleBackColor = true;
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            Button removeButton = new Button();
            removeButton.Location = new System.Drawing.Point(1100, 400);
            removeButton.Width = 100;
            removeButton.Height = 50;
            removeButton.Name = "removeButton";
            removeButton.TabIndex = 3;
            removeButton.Text = "Remove";
            removeButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            removeButton.UseVisualStyleBackColor = true;
            removeButton.Click += RemoveButton_Click;
            this.Controls.Add(removeButton);

            Button updateButton = new Button();
            updateButton.Location = new System.Drawing.Point(1300, 400);
            updateButton.Width = 100;
            updateButton.Height = 50;
            updateButton.Name = "updateButton";
            updateButton.TabIndex = 4;
            updateButton.Text = "Update";
            updateButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += UpdateButton_Click;
            this.Controls.Add(updateButton);

            Button exitButton = new Button();
            exitButton.Location = new System.Drawing.Point(1350, 750);
            exitButton.Width = 100;
            exitButton.Height = 50;
            exitButton.Name = "exitButton";
            exitButton.TabIndex = 5;
            exitButton.Text = "Exit";
            exitButton.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);
        }

        /* Function for Label management */
        private void addLabels()
        {
            Label titluFereastra = new Label();
            titluFereastra.Location = new System.Drawing.Point(660, 20);
            titluFereastra.Width = 200;
            titluFereastra.Height = 30;
            titluFereastra.Name = "titluFereastra";
            titluFereastra.TabIndex = 1;
            titluFereastra.Text = "Aplicație Steam";
            titluFereastra.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            this.Controls.Add(titluFereastra);

            Label gamesLabel = new Label();
            gamesLabel.Location = new System.Drawing.Point(30, 220);
            gamesLabel.Width = 150;
            gamesLabel.Height = 30;
            gamesLabel.Name = "gamesLabel";
            gamesLabel.TabIndex = 5;
            gamesLabel.Text = "Games";
            gamesLabel.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            this.Controls.Add(gamesLabel);

            Label achievementsLabel = new Label();
            achievementsLabel.Location = new System.Drawing.Point(30, 470);
            achievementsLabel.Width = 150;
            achievementsLabel.Height = 30;
            achievementsLabel.Name = "achievementLabel";
            achievementsLabel.TabIndex = 6;
            achievementsLabel.Text = "Achievements";
            achievementsLabel.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            this.Controls.Add(achievementsLabel);

           

            Label functionalitatiLabel = new Label();
            functionalitatiLabel.Location = new System.Drawing.Point(950, 250);
            functionalitatiLabel.Width = 400;
            functionalitatiLabel.Height = 50;
            functionalitatiLabel.TextAlign = ContentAlignment.BottomCenter;
            functionalitatiLabel.Name = "functionalitatiLabel";
            functionalitatiLabel.TabIndex = 16;
            functionalitatiLabel.Text = "Funcționalități";
            functionalitatiLabel.Font = new Font("Comic Sans MS", 24, FontStyle.Regular);
            this.Controls.Add(functionalitatiLabel);
        }

        /* Function for TextBox management*/
        private void addTextBoxes()
        {
            achievement_nameTextBox = new TextBox();
            achievement_nameTextBox.Location = new System.Drawing.Point(30, 150);
            achievement_nameTextBox.Width = 187;
            achievement_nameTextBox.Height = 30;
            achievement_nameTextBox.Name = "achievement_nameTextBox";
            achievement_nameTextBox.TabIndex = 7;
            achievement_nameTextBox.Text = "Enter achievement name...";
            achievement_nameTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            achievement_nameTextBox.Tag = "Enter achievements name...";
            achievement_nameTextBox.ForeColor = SystemColors.GrayText;
            achievement_nameTextBox.GotFocus += TextBox_GotFocus;
            achievement_nameTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(achievement_nameTextBox);


            descriptionTextBox = new TextBox();
            descriptionTextBox.Location = new System.Drawing.Point(454, 150);
            descriptionTextBox.Width = 187;
            descriptionTextBox.Height = 30;
            descriptionTextBox.Name = "descriereTextBox";
            descriptionTextBox.TabIndex = 9;
            descriptionTextBox.Text = "Enter description...";
            descriptionTextBox.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
            descriptionTextBox.Tag = "Enter description...";
            descriptionTextBox.ForeColor = SystemColors.GrayText;
            descriptionTextBox.GotFocus += TextBox_GotFocus;
            descriptionTextBox.LostFocus += TextBox_LostFocus;
            this.Controls.Add(descriptionTextBox);


        }

        /* Event for adding an achievement in the child table when button Add is pressed */
        private void AddButton_Click(object sender, EventArgs e)
        {
            String achievement_name = this.achievement_nameTextBox.Text;
            String description = this.descriptionTextBox.Text;


            if (gamesDataGridView.SelectedRows.Count > 0 && validator.ValidateInputs(achievement_name, description))
            {
                int selectedGameId = (int)gamesDataGridView.SelectedRows[0].Cells["id"].Value;
                service.AddAchievement(achievement_name, description, selectedGameId);
                achievementsDataGridView.DataSource = service.GetAchievementsByGameId(selectedGameId);
            }
            else
            {
                MessageBox.Show("Please select a game from the parent table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Event for removing an achievement from the child table by its id when button Remove is pressed */
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            String selectedAchievement_name = (String)achievementsDataGridView.SelectedRows[0].Cells["achievement_name"].Value;
            service.DeleteAchievement(selectedAchievement_name);
            achievementsDataGridView.DataSource = service.GetAchievementsByGameId((int)gamesDataGridView.SelectedRows[0].Cells["id"].Value);
        }

        /* Event for updating a movie from the child table when button Update is pressed */
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            String achievement_name = (String)achievementsDataGridView.SelectedRows[0].Cells["achievement_name"].Value;
            String description = (String)achievementsDataGridView.SelectedRows[0].Cells["description"].Value;

            if (gamesDataGridView.SelectedRows.Count > 0 && validator.ValidateInputs(achievement_name, description))
            {
                int selectedGameId = (int)gamesDataGridView.SelectedRows[0].Cells["id"].Value;
                service.UpdateAchievement(achievement_name, description, selectedGameId);
                achievementsDataGridView.DataSource = service.GetAchievementsByGameId(selectedGameId);
            }
            else
            {
                MessageBox.Show("Please select a game from the parent table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Event for deleting the placeholder text when the TextBox is pressed */
        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == textBox.Tag as string)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
            }
        }

        /* Event for making a placeholder text that shows while the TextBox is not pressed */
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Tag as string;
                textBox.ForeColor = SystemColors.GrayText;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}