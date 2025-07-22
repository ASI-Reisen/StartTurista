using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace StartTurista
{
    public partial class MainForm : Form
    {
        private const string TURISTA_EXE_PATH = @"C:\ASI\Turista\Programm\turista-ve.exe";
        private const string TARGET_INI = @"C:\ASI\Turista\Daten\turista.ini";

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Text = "Turista Launcher";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create buttons
            var btnProd = new Button
            {
                Text = "Turista Prod",
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(50, 30),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                BackColor = System.Drawing.Color.FromArgb(0, 120, 215),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnProd.Click += (s, e) => LaunchTurista("prod");

            var btnStaging = new Button
            {
                Text = "Turista Staging",
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(50, 100),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                BackColor = System.Drawing.Color.FromArgb(255, 140, 0),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnStaging.Click += (s, e) => LaunchTurista("staging");

            var btnLocal = new Button
            {
                Text = "Turista Local",
                Size = new System.Drawing.Size(300, 50),
                Location = new System.Drawing.Point(50, 170),
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F),
                BackColor = System.Drawing.Color.FromArgb(34, 139, 34),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLocal.Click += (s, e) => LaunchTurista("local");

            // Add buttons to form
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
    }
}