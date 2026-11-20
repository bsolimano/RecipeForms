namespace RecipeForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTest = new Button();
            lblStatus = new Label();
            lblWho = new Label();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            btnRemoveIng = new Button();
            btnAddIng = new Button();
            btnIngSearch = new Button();
            lstMine = new ListBox();
            lstAll = new ListBox();
            txtIngSearch = new TextBox();
            lblMineHeader = new Label();
            lblAllHeader = new Label();
            lblSearchHelp = new Label();
            tabPage3 = new TabPage();
            txtDetails = new TextBox();
            btnViewRecipe = new Button();
            dgvRecipes = new DataGridView();
            btnFindMine = new Button();
            btnRecipeSearch = new Button();
            txtRecipeSearch = new TextBox();
            lblRecipeSearch = new Label();
            tabPage4 = new TabPage();
            btnRateDelete = new Button();
            dgvMyRatings = new DataGridView();
            btnRateSave = new Button();
            nudStars = new NumericUpDown();
            cboRecipe = new ComboBox();
            lblRateHeader = new Label();
            lblRateStars = new Label();
            lblRatePick = new Label();
            tabPage1 = new TabPage();
            lblEmail = new Label();
            lblLast = new Label();
            lblFirst = new Label();
            txtEmail = new TextBox();
            txtLast = new TextBox();
            txtFirst = new TextBox();
            btnLogout = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnLogin = new Button();
            btnCreate = new Button();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecipes).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyRatings).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStars).BeginInit();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(16, 601);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(141, 29);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test connection";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(163, 605);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(131, 20);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Not connected yet";
            // 
            // lblWho
            // 
            lblWho.AutoSize = true;
            lblWho.Location = new Point(12, 9);
            lblWho.Name = "lblWho";
            lblWho.Size = new Size(102, 20);
            lblWho.TabIndex = 2;
            lblWho.Text = "Not logged in";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Font = new Font("Segoe UI", 9F);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(894, 583);
            tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnRemoveIng);
            tabPage2.Controls.Add(btnAddIng);
            tabPage2.Controls.Add(btnIngSearch);
            tabPage2.Controls.Add(lstMine);
            tabPage2.Controls.Add(lstAll);
            tabPage2.Controls.Add(txtIngSearch);
            tabPage2.Controls.Add(lblMineHeader);
            tabPage2.Controls.Add(lblAllHeader);
            tabPage2.Controls.Add(lblSearchHelp);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(886, 550);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "My Ingredients";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnRemoveIng
            // 
            btnRemoveIng.Location = new Point(475, 444);
            btnRemoveIng.Name = "btnRemoveIng";
            btnRemoveIng.Size = new Size(200, 34);
            btnRemoveIng.TabIndex = 8;
            btnRemoveIng.Text = "Remove selected";
            btnRemoveIng.UseVisualStyleBackColor = true;
            btnRemoveIng.Click += btnRemoveIng_Click;
            // 
            // btnAddIng
            // 
            btnAddIng.Location = new Point(37, 444);
            btnAddIng.Name = "btnAddIng";
            btnAddIng.Size = new Size(200, 34);
            btnAddIng.TabIndex = 7;
            btnAddIng.Text = "Add to my ingredients";
            btnAddIng.UseVisualStyleBackColor = true;
            btnAddIng.Click += btnAddIng_Click;
            // 
            // btnIngSearch
            // 
            btnIngSearch.Location = new Point(681, 42);
            btnIngSearch.Name = "btnIngSearch";
            btnIngSearch.Size = new Size(94, 29);
            btnIngSearch.TabIndex = 6;
            btnIngSearch.Text = "Search";
            btnIngSearch.UseVisualStyleBackColor = true;
            btnIngSearch.Click += btnIngSearch_Click;
            // 
            // lstMine
            // 
            lstMine.FormattingEnabled = true;
            lstMine.Location = new Point(475, 120);
            lstMine.Name = "lstMine";
            lstMine.Size = new Size(300, 304);
            lstMine.TabIndex = 5;
            // 
            // lstAll
            // 
            lstAll.FormattingEnabled = true;
            lstAll.Location = new Point(37, 120);
            lstAll.Name = "lstAll";
            lstAll.Size = new Size(300, 304);
            lstAll.TabIndex = 4;
            // 
            // txtIngSearch
            // 
            txtIngSearch.Location = new Point(217, 39);
            txtIngSearch.Name = "txtIngSearch";
            txtIngSearch.Size = new Size(448, 27);
            txtIngSearch.TabIndex = 3;
            // 
            // lblMineHeader
            // 
            lblMineHeader.AutoSize = true;
            lblMineHeader.Location = new Point(475, 90);
            lblMineHeader.Name = "lblMineHeader";
            lblMineHeader.Size = new Size(110, 20);
            lblMineHeader.TabIndex = 2;
            lblMineHeader.Text = "My Ingredients:";
            // 
            // lblAllHeader
            // 
            lblAllHeader.AutoSize = true;
            lblAllHeader.Location = new Point(37, 90);
            lblAllHeader.Name = "lblAllHeader";
            lblAllHeader.Size = new Size(174, 20);
            lblAllHeader.TabIndex = 1;
            lblAllHeader.Text = "All Matching Ingredients:";
            // 
            // lblSearchHelp
            // 
            lblSearchHelp.AutoSize = true;
            lblSearchHelp.Location = new Point(37, 42);
            lblSearchHelp.Name = "lblSearchHelp";
            lblSearchHelp.Size = new Size(134, 20);
            lblSearchHelp.TabIndex = 0;
            lblSearchHelp.Text = "Search Ingredients:";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(txtDetails);
            tabPage3.Controls.Add(btnViewRecipe);
            tabPage3.Controls.Add(dgvRecipes);
            tabPage3.Controls.Add(btnFindMine);
            tabPage3.Controls.Add(btnRecipeSearch);
            tabPage3.Controls.Add(txtRecipeSearch);
            tabPage3.Controls.Add(lblRecipeSearch);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(886, 550);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Find Recipes";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtDetails
            // 
            txtDetails.Location = new Point(544, 124);
            txtDetails.Multiline = true;
            txtDetails.Name = "txtDetails";
            txtDetails.ReadOnly = true;
            txtDetails.ScrollBars = ScrollBars.Vertical;
            txtDetails.Size = new Size(327, 412);
            txtDetails.TabIndex = 6;
            // 
            // btnViewRecipe
            // 
            btnViewRecipe.Location = new Point(221, 77);
            btnViewRecipe.Name = "btnViewRecipe";
            btnViewRecipe.Size = new Size(189, 29);
            btnViewRecipe.TabIndex = 5;
            btnViewRecipe.Text = "View Selected Recipe";
            btnViewRecipe.UseVisualStyleBackColor = true;
            btnViewRecipe.Click += btnViewRecipe_Click;
            // 
            // dgvRecipes
            // 
            dgvRecipes.AllowUserToAddRows = false;
            dgvRecipes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRecipes.Location = new Point(22, 124);
            dgvRecipes.Name = "dgvRecipes";
            dgvRecipes.ReadOnly = true;
            dgvRecipes.RowHeadersVisible = false;
            dgvRecipes.RowHeadersWidth = 51;
            dgvRecipes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecipes.Size = new Size(497, 412);
            dgvRecipes.TabIndex = 4;
            // 
            // btnFindMine
            // 
            btnFindMine.Location = new Point(22, 77);
            btnFindMine.Name = "btnFindMine";
            btnFindMine.Size = new Size(172, 29);
            btnFindMine.TabIndex = 3;
            btnFindMine.Text = "Find recipes I can make";
            btnFindMine.UseVisualStyleBackColor = true;
            btnFindMine.Click += btnFindMine_Click;
            // 
            // btnRecipeSearch
            // 
            btnRecipeSearch.Location = new Point(509, 32);
            btnRecipeSearch.Name = "btnRecipeSearch";
            btnRecipeSearch.Size = new Size(156, 29);
            btnRecipeSearch.TabIndex = 2;
            btnRecipeSearch.Text = "Search Recipes";
            btnRecipeSearch.UseVisualStyleBackColor = true;
            btnRecipeSearch.Click += btnRecipeSearch_Click;
            // 
            // txtRecipeSearch
            // 
            txtRecipeSearch.Location = new Point(139, 32);
            txtRecipeSearch.Name = "txtRecipeSearch";
            txtRecipeSearch.Size = new Size(364, 27);
            txtRecipeSearch.TabIndex = 1;
            // 
            // lblRecipeSearch
            // 
            lblRecipeSearch.AutoSize = true;
            lblRecipeSearch.Location = new Point(22, 32);
            lblRecipeSearch.Name = "lblRecipeSearch";
            lblRecipeSearch.Size = new Size(111, 20);
            lblRecipeSearch.TabIndex = 0;
            lblRecipeSearch.Text = "Search Recipes:";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(btnRateDelete);
            tabPage4.Controls.Add(dgvMyRatings);
            tabPage4.Controls.Add(btnRateSave);
            tabPage4.Controls.Add(nudStars);
            tabPage4.Controls.Add(cboRecipe);
            tabPage4.Controls.Add(lblRateHeader);
            tabPage4.Controls.Add(lblRateStars);
            tabPage4.Controls.Add(lblRatePick);
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(886, 550);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Rate a Recipe";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnRateDelete
            // 
            btnRateDelete.Location = new Point(180, 480);
            btnRateDelete.Name = "btnRateDelete";
            btnRateDelete.Size = new Size(183, 29);
            btnRateDelete.TabIndex = 8;
            btnRateDelete.Text = "Delete My Rating";
            btnRateDelete.UseVisualStyleBackColor = true;
            btnRateDelete.Click += btnRateDelete_Click;
            // 
            // dgvMyRatings
            // 
            dgvMyRatings.AllowUserToAddRows = false;
            dgvMyRatings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyRatings.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dgvMyRatings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyRatings.Location = new Point(22, 155);
            dgvMyRatings.Name = "dgvMyRatings";
            dgvMyRatings.ReadOnly = true;
            dgvMyRatings.RowHeadersVisible = false;
            dgvMyRatings.RowHeadersWidth = 51;
            dgvMyRatings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyRatings.Size = new Size(424, 297);
            dgvMyRatings.TabIndex = 7;
            // 
            // btnRateSave
            // 
            btnRateSave.Location = new Point(22, 480);
            btnRateSave.Name = "btnRateSave";
            btnRateSave.Size = new Size(132, 29);
            btnRateSave.TabIndex = 5;
            btnRateSave.Text = "Save my rating";
            btnRateSave.UseVisualStyleBackColor = true;
            btnRateSave.Click += btnRateSave_Click;
            // 
            // nudStars
            // 
            nudStars.Location = new Point(147, 81);
            nudStars.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            nudStars.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudStars.Name = "nudStars";
            nudStars.Size = new Size(150, 27);
            nudStars.TabIndex = 4;
            nudStars.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // cboRecipe
            // 
            cboRecipe.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRecipe.FormattingEnabled = true;
            cboRecipe.Location = new Point(146, 36);
            cboRecipe.Name = "cboRecipe";
            cboRecipe.Size = new Size(151, 28);
            cboRecipe.TabIndex = 3;
            // 
            // lblRateHeader
            // 
            lblRateHeader.AutoSize = true;
            lblRateHeader.Location = new Point(22, 121);
            lblRateHeader.Name = "lblRateHeader";
            lblRateHeader.Size = new Size(81, 20);
            lblRateHeader.TabIndex = 2;
            lblRateHeader.Text = "My ratings:";
            // 
            // lblRateStars
            // 
            lblRateStars.AutoSize = true;
            lblRateStars.Location = new Point(22, 81);
            lblRateStars.Name = "lblRateStars";
            lblRateStars.Size = new Size(111, 20);
            lblRateStars.TabIndex = 1;
            lblRateStars.Text = "Your stars (1-5):";
            // 
            // lblRatePick
            // 
            lblRatePick.AutoSize = true;
            lblRatePick.Location = new Point(22, 36);
            lblRatePick.Name = "lblRatePick";
            lblRatePick.Size = new Size(118, 20);
            lblRatePick.TabIndex = 0;
            lblRatePick.Text = "Choose a recipe:";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(lblEmail);
            tabPage1.Controls.Add(lblLast);
            tabPage1.Controls.Add(lblFirst);
            tabPage1.Controls.Add(txtEmail);
            tabPage1.Controls.Add(txtLast);
            tabPage1.Controls.Add(txtFirst);
            tabPage1.Controls.Add(btnLogout);
            tabPage1.Controls.Add(btnDelete);
            tabPage1.Controls.Add(btnSave);
            tabPage1.Controls.Add(btnLogin);
            tabPage1.Controls.Add(btnCreate);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(886, 550);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Profile";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(37, 198);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(53, 20);
            lblEmail.TabIndex = 15;
            lblEmail.Text = "Email: ";
            // 
            // lblLast
            // 
            lblLast.AutoSize = true;
            lblLast.Location = new Point(37, 124);
            lblLast.Name = "lblLast";
            lblLast.Size = new Size(82, 20);
            lblLast.TabIndex = 14;
            lblLast.Text = "Last Name:";
            // 
            // lblFirst
            // 
            lblFirst.AutoSize = true;
            lblFirst.Location = new Point(37, 46);
            lblFirst.Name = "lblFirst";
            lblFirst.Size = new Size(83, 20);
            lblFirst.TabIndex = 13;
            lblFirst.Text = "First Name:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(136, 191);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(321, 27);
            txtEmail.TabIndex = 12;
            // 
            // txtLast
            // 
            txtLast.Location = new Point(136, 117);
            txtLast.Name = "txtLast";
            txtLast.Size = new Size(321, 27);
            txtLast.TabIndex = 11;
            // 
            // txtFirst
            // 
            txtFirst.Location = new Point(136, 43);
            txtFirst.Name = "txtFirst";
            txtFirst.Size = new Size(321, 27);
            txtFirst.TabIndex = 10;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(767, 504);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(94, 29);
            btnLogout.TabIndex = 9;
            btnLogout.Text = "Log out";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(37, 430);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(138, 29);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete my profile";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(37, 377);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(138, 29);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save changes";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(37, 330);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(138, 29);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Log in with email";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(38, 283);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(137, 29);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Create profile";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(954, 641);
            Controls.Add(lblStatus);
            Controls.Add(tabControl1);
            Controls.Add(lblWho);
            Controls.Add(btnTest);
            Name = "Form1";
            Text = "Recipe MATE";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecipes).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyRatings).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStars).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTest;
        private Label lblStatus;
        private Label lblWho;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Button btnLogout;
        private Button btnDelete;
        private Button btnSave;
        private Button btnLogin;
        private Button btnCreate;
        private TextBox txtEmail;
        private TextBox txtLast;
        private TextBox txtFirst;
        private Label lblEmail;
        private Label lblLast;
        private Label lblFirst;
        private Label lblMineHeader;
        private Label lblAllHeader;
        private Label lblSearchHelp;
        private TextBox txtIngSearch;
        private ListBox lstMine;
        private ListBox lstAll;
        private Button btnIngSearch;
        private Button btnAddIng;
        private Button btnRemoveIng;
        private Button btnFindMine;
        private Button btnRecipeSearch;
        private TextBox txtRecipeSearch;
        private Label lblRecipeSearch;
        private DataGridView dgvRecipes;
        private Button btnViewRecipe;
        private TextBox txtDetails;
        private NumericUpDown nudStars;
        private ComboBox cboRecipe;
        private Label lblRateHeader;
        private Label lblRateStars;
        private Label lblRatePick;
        private DataGridView dgvMyRatings;
        private Button btnRateSave;
        private Button btnRateDelete;
    }
}