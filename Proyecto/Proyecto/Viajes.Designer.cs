
namespace Proyecto
{
    partial class Viajes
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Lunes = new System.Windows.Forms.TabPage();
            this.dataL = new System.Windows.Forms.DataGridView();
            this.Martes = new System.Windows.Forms.TabPage();
            this.dataM = new System.Windows.Forms.DataGridView();
            this.Miercoles = new System.Windows.Forms.TabPage();
            this.dataX = new System.Windows.Forms.DataGridView();
            this.Jueves = new System.Windows.Forms.TabPage();
            this.dataJ = new System.Windows.Forms.DataGridView();
            this.Viernes = new System.Windows.Forms.TabPage();
            this.dataV = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.Lunes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataL)).BeginInit();
            this.Martes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataM)).BeginInit();
            this.Miercoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataX)).BeginInit();
            this.Jueves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataJ)).BeginInit();
            this.Viernes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataV)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Lunes);
            this.tabControl1.Controls.Add(this.Martes);
            this.tabControl1.Controls.Add(this.Miercoles);
            this.tabControl1.Controls.Add(this.Jueves);
            this.tabControl1.Controls.Add(this.Viernes);
            this.tabControl1.Location = new System.Drawing.Point(45, 22);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(614, 362);
            this.tabControl1.TabIndex = 0;
            // 
            // Lunes
            // 
            this.Lunes.Controls.Add(this.dataL);
            this.Lunes.Location = new System.Drawing.Point(4, 22);
            this.Lunes.Name = "Lunes";
            this.Lunes.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Lunes.Size = new System.Drawing.Size(606, 336);
            this.Lunes.TabIndex = 0;
            this.Lunes.Text = "Lunes";
            this.Lunes.UseVisualStyleBackColor = true;
            // 
            // dataL
            // 
            this.dataL.AllowUserToAddRows = false;
            this.dataL.AllowUserToDeleteRows = false;
            this.dataL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataL.Location = new System.Drawing.Point(19, 11);
            this.dataL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataL.Name = "dataL";
            this.dataL.ReadOnly = true;
            this.dataL.RowHeadersWidth = 51;
            this.dataL.RowTemplate.Height = 24;
            this.dataL.Size = new System.Drawing.Size(570, 316);
            this.dataL.TabIndex = 1;
            // 
            // Martes
            // 
            this.Martes.Controls.Add(this.dataM);
            this.Martes.Location = new System.Drawing.Point(4, 22);
            this.Martes.Name = "Martes";
            this.Martes.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Martes.Size = new System.Drawing.Size(606, 336);
            this.Martes.TabIndex = 1;
            this.Martes.Text = "Martes";
            this.Martes.UseVisualStyleBackColor = true;
            // 
            // dataM
            // 
            this.dataM.AllowUserToAddRows = false;
            this.dataM.AllowUserToDeleteRows = false;
            this.dataM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataM.Location = new System.Drawing.Point(19, 11);
            this.dataM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataM.Name = "dataM";
            this.dataM.ReadOnly = true;
            this.dataM.RowHeadersWidth = 51;
            this.dataM.RowTemplate.Height = 24;
            this.dataM.Size = new System.Drawing.Size(570, 316);
            this.dataM.TabIndex = 1;
            // 
            // Miercoles
            // 
            this.Miercoles.Controls.Add(this.dataX);
            this.Miercoles.Location = new System.Drawing.Point(4, 22);
            this.Miercoles.Name = "Miercoles";
            this.Miercoles.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Miercoles.Size = new System.Drawing.Size(606, 336);
            this.Miercoles.TabIndex = 2;
            this.Miercoles.Text = "Miercoles";
            this.Miercoles.UseVisualStyleBackColor = true;
            // 
            // dataX
            // 
            this.dataX.AllowUserToAddRows = false;
            this.dataX.AllowUserToDeleteRows = false;
            this.dataX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataX.Location = new System.Drawing.Point(19, 12);
            this.dataX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataX.Name = "dataX";
            this.dataX.ReadOnly = true;
            this.dataX.RowHeadersWidth = 51;
            this.dataX.RowTemplate.Height = 24;
            this.dataX.Size = new System.Drawing.Size(570, 316);
            this.dataX.TabIndex = 1;
            // 
            // Jueves
            // 
            this.Jueves.Controls.Add(this.dataJ);
            this.Jueves.Location = new System.Drawing.Point(4, 22);
            this.Jueves.Name = "Jueves";
            this.Jueves.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Jueves.Size = new System.Drawing.Size(606, 336);
            this.Jueves.TabIndex = 3;
            this.Jueves.Text = "Jueves";
            this.Jueves.UseVisualStyleBackColor = true;
            // 
            // dataJ
            // 
            this.dataJ.AllowUserToAddRows = false;
            this.dataJ.AllowUserToDeleteRows = false;
            this.dataJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataJ.Location = new System.Drawing.Point(19, 11);
            this.dataJ.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataJ.Name = "dataJ";
            this.dataJ.ReadOnly = true;
            this.dataJ.RowHeadersWidth = 51;
            this.dataJ.RowTemplate.Height = 24;
            this.dataJ.Size = new System.Drawing.Size(570, 316);
            this.dataJ.TabIndex = 1;
            // 
            // Viernes
            // 
            this.Viernes.Controls.Add(this.dataV);
            this.Viernes.Location = new System.Drawing.Point(4, 22);
            this.Viernes.Name = "Viernes";
            this.Viernes.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Viernes.Size = new System.Drawing.Size(606, 336);
            this.Viernes.TabIndex = 4;
            this.Viernes.Text = "Viernes";
            this.Viernes.UseVisualStyleBackColor = true;
            // 
            // dataV
            // 
            this.dataV.AllowUserToAddRows = false;
            this.dataV.AllowUserToDeleteRows = false;
            this.dataV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataV.Location = new System.Drawing.Point(19, 11);
            this.dataV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataV.Name = "dataV";
            this.dataV.ReadOnly = true;
            this.dataV.RowHeadersWidth = 51;
            this.dataV.RowTemplate.Height = 24;
            this.dataV.Size = new System.Drawing.Size(570, 316);
            this.dataV.TabIndex = 1;
            this.dataV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataV_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(544, 390);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 32);
            this.button1.TabIndex = 1;
            this.button1.Text = "Avanzar semana";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 410);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 32);
            this.button2.TabIndex = 2;
            this.button2.Text = "Restaurar datos";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(674, 54);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 88);
            this.button3.TabIndex = 3;
            this.button3.Text = "Gestionar datos";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Viajes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 453);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Viajes";
            this.Text = "Viajes";
            this.Load += new System.EventHandler(this.Viajes_Load);
            this.tabControl1.ResumeLayout(false);
            this.Lunes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataL)).EndInit();
            this.Martes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataM)).EndInit();
            this.Miercoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataX)).EndInit();
            this.Jueves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataJ)).EndInit();
            this.Viernes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Jueves;
        private System.Windows.Forms.TabPage Viernes;
        private System.Windows.Forms.TabPage Lunes;
        private System.Windows.Forms.TabPage Martes;
        private System.Windows.Forms.TabPage Miercoles;
        private System.Windows.Forms.DataGridView dataM;
        private System.Windows.Forms.DataGridView dataX;
        private System.Windows.Forms.DataGridView dataJ;
        private System.Windows.Forms.DataGridView dataV;
        private System.Windows.Forms.DataGridView dataL;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

