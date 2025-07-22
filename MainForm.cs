using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace StartTurista
{
    public partial class MainForm : Form
    {
        private const string TURISTA_EXE_PATH = @"C:\ASI\Turista\Programm\turista-ve.exe";
        private const string TURISTA_DATA_PATH = @"C:\ASI\Turista\Daten";
        private const string TARGET_INI = @"C:\ASI\Turista\Daten\turista.ini";

        private Label lblValidationTitle;
        private Label lblExeStatus;
        private Label lblDataStatus;
        private Button btnProd;
        private Button btnStaging;
        private Button btnLocal;

        public MainForm()
        {
            InitializeComponent();
            ValidateTuristaInstallation();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Text = "Turista Launcher";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create validation labels
            lblValidationTitle = new Label
            {
                Text = "Installation Validation:",
                Size = new System.Drawing.Size(400, 20),
                Location = new System.Drawing.Point(50, 20),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Black
            };

            lblExeStatus = new Label
            {
                Text = "Checking turista-ve.exe...",
                Size = new System.Drawing.Size(400, 20),
                Location = new System.Drawing.Point(50, 45),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F),
                ForeColor = System.Drawing.Color.Gray
            };

            lblDataStatus = new Label
            {
                Text = "Checking Daten folder...",
                Size = new System.Drawing.Size(400, 20),
                Location = new System.Drawing.Point(50, 70),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9F),
                ForeColor = System.Drawing.Color.Gray
            };

            // Create buttons
            btnProd = new Button
            {
                Text = "Turista Prod",
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(100, 120),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                BackColor = System.Drawing.Color.FromArgb(0, 120, 215),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnProd.Click += (s, e) => LaunchTurista("prod");

            btnStaging = new Button
            {
                Text = "Turista Staging",
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(100, 190),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                BackColor = System.Drawing.Color.FromArgb(255, 140, 0),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnStaging.Click += (s, e) => LaunchTurista("staging");

            btnLocal = new Button
            {
                Text = "Turista Local",
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(100, 260),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                BackColor = System.Drawing.Color.FromArgb(34, 139, 34),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLocal.Click += (s, e) => LaunchTurista("local");

            // Add controls to form
            this.Controls.Add(lblValidationTitle);
            this.Controls.Add(lblExeStatus);
            this.Controls.Add(lblDataStatus);
            this.Controls.Add(btnProd);
            this.Controls.Add(btnStaging);
            this.Controls.Add(btnLocal);

            this.ResumeLayout(false);
        }

        private void LaunchTurista(string environment)
        {
            try
            {
                // Generate ini file dynamically
                IniConfiguration.GenerateIniFile(environment, TARGET_INI);

                // Check if executable exists
                if (!File.Exists(TURISTA_EXE_PATH))
                {
                    MessageBox.Show($"Turista executable not found:\n{TURISTA_EXE_PATH}", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Start turista-ve.exe
                Process.Start(new ProcessStartInfo
                {
                    FileName = TURISTA_EXE_PATH,
                    UseShellExecute = true
                });

                // Show success message and close launcher
                //MessageBox.Show($"Turista {environment} started successfully!", 
                  //  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error launching Turista {environment}:\n{ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateTuristaInstallation()
        {
            bool isValid = true;

            // Check if turista-ve.exe exists
            if (File.Exists(TURISTA_EXE_PATH))
            {
                lblExeStatus.Text = "✅ C:\\ASI\\Turista\\Programm\\turista-ve.exe - File found";
                lblExeStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblExeStatus.Text = "❌ C:\\ASI\\Turista\\Programm\\turista-ve.exe - File not found";
                lblExeStatus.ForeColor = System.Drawing.Color.Red;
                isValid = false;
            }

            // Check if Daten folder exists
            if (Directory.Exists(TURISTA_DATA_PATH))
            {
                lblDataStatus.Text = "✅ C:\\ASI\\Turista\\Daten - Folder found";
                lblDataStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblDataStatus.Text = "❌ C:\\ASI\\Turista\\Daten - Folder not found";
                lblDataStatus.ForeColor = System.Drawing.Color.Red;
                isValid = false;
            }

            if (!isValid)
            {
                lblValidationTitle.Text = "Installation Validation: ❌ The Turista installation seems to be invalid.";
                lblValidationTitle.ForeColor = System.Drawing.Color.Red;
                
                // Disable all buttons when validation fails
                btnProd.Enabled = false;
                btnStaging.Enabled = false;
                btnLocal.Enabled = false;
            }
            else
            {
                lblValidationTitle.Text = "Installation Validation: ✅ Valid";
                lblValidationTitle.ForeColor = System.Drawing.Color.Green;
                
                // Enable all buttons when validation succeeds
                btnProd.Enabled = true;
                btnStaging.Enabled = true;
                btnLocal.Enabled = true;
            }
        }
    }
}