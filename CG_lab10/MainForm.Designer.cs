
namespace CG_lab10
{
    partial class MainForm
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
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonScale = new System.Windows.Forms.Button();
            this.buttonMirrorZ = new System.Windows.Forms.Button();
            this.buttonMirrorY = new System.Windows.Forms.Button();
            this.buttonMirrorX = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGrid = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonOrt_634 = new System.Windows.Forms.Button();
            this.buttonOrt_45 = new System.Windows.Forms.Button();
            this.buttonOnePoint = new System.Windows.Forms.Button();
            this.buttonFront = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonMove
            // 
            this.buttonMove.Location = new System.Drawing.Point(6, 22);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(163, 23);
            this.buttonMove.TabIndex = 0;
            this.buttonMove.Text = "Переместить";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(6, 396);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(163, 23);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Очистить изменения";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown7);
            this.groupBox1.Controls.Add(this.numericUpDown8);
            this.groupBox1.Controls.Add(this.numericUpDown9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.numericUpDown4);
            this.groupBox1.Controls.Add(this.numericUpDown6);
            this.groupBox1.Controls.Add(this.numericUpDown5);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.buttonRotate);
            this.groupBox1.Controls.Add(this.buttonScale);
            this.groupBox1.Controls.Add(this.buttonMirrorZ);
            this.groupBox1.Controls.Add(this.buttonMirrorY);
            this.groupBox1.Controls.Add(this.buttonMirrorX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonGrid);
            this.groupBox1.Controls.Add(this.buttonMove);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 425);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Меню";
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown7.Location = new System.Drawing.Point(6, 242);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown7.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown7.TabIndex = 26;
            // 
            // numericUpDown8
            // 
            this.numericUpDown8.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown8.Location = new System.Drawing.Point(63, 242);
            this.numericUpDown8.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown8.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown8.TabIndex = 25;
            // 
            // numericUpDown9
            // 
            this.numericUpDown9.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown9.Location = new System.Drawing.Point(118, 242);
            this.numericUpDown9.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown9.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.numericUpDown9.Name = "numericUpDown9";
            this.numericUpDown9.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown9.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "Z";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(75, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 15);
            this.label10.TabIndex = 21;
            this.label10.Text = "X";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.DecimalPlaces = 1;
            this.numericUpDown4.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown4.Location = new System.Drawing.Point(6, 154);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown4.TabIndex = 20;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.DecimalPlaces = 1;
            this.numericUpDown6.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown6.Location = new System.Drawing.Point(118, 154);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown6.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown6.TabIndex = 18;
            this.numericUpDown6.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.DecimalPlaces = 1;
            this.numericUpDown5.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDown5.Location = new System.Drawing.Point(63, 154);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown5.TabIndex = 19;
            this.numericUpDown5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Z";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "X";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(119, 66);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown3.TabIndex = 11;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(63, 66);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown2.TabIndex = 10;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(6, 66);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(50, 23);
            this.numericUpDown1.TabIndex = 9;
            // 
            // buttonRotate
            // 
            this.buttonRotate.Location = new System.Drawing.Point(7, 198);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(162, 23);
            this.buttonRotate.TabIndex = 8;
            this.buttonRotate.Text = "Повернуть";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonScale
            // 
            this.buttonScale.Location = new System.Drawing.Point(6, 110);
            this.buttonScale.Name = "buttonScale";
            this.buttonScale.Size = new System.Drawing.Size(162, 23);
            this.buttonScale.TabIndex = 7;
            this.buttonScale.Text = "Масштабировать";
            this.buttonScale.UseVisualStyleBackColor = true;
            this.buttonScale.Click += new System.EventHandler(this.buttonScale_Click);
            // 
            // buttonMirrorZ
            // 
            this.buttonMirrorZ.Location = new System.Drawing.Point(119, 317);
            this.buttonMirrorZ.Name = "buttonMirrorZ";
            this.buttonMirrorZ.Size = new System.Drawing.Size(50, 23);
            this.buttonMirrorZ.TabIndex = 6;
            this.buttonMirrorZ.Text = "Z";
            this.buttonMirrorZ.UseVisualStyleBackColor = true;
            this.buttonMirrorZ.Click += new System.EventHandler(this.buttonMirrorZ_Click);
            // 
            // buttonMirrorY
            // 
            this.buttonMirrorY.Location = new System.Drawing.Point(63, 317);
            this.buttonMirrorY.Name = "buttonMirrorY";
            this.buttonMirrorY.Size = new System.Drawing.Size(50, 23);
            this.buttonMirrorY.TabIndex = 5;
            this.buttonMirrorY.Text = "Y";
            this.buttonMirrorY.UseVisualStyleBackColor = true;
            this.buttonMirrorY.Click += new System.EventHandler(this.buttonMirrorY_Click);
            // 
            // buttonMirrorX
            // 
            this.buttonMirrorX.Location = new System.Drawing.Point(7, 317);
            this.buttonMirrorX.Name = "buttonMirrorX";
            this.buttonMirrorX.Size = new System.Drawing.Size(50, 23);
            this.buttonMirrorX.TabIndex = 4;
            this.buttonMirrorX.Text = "X";
            this.buttonMirrorX.UseVisualStyleBackColor = true;
            this.buttonMirrorX.Click += new System.EventHandler(this.buttonMirrorX_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Отразить";
            // 
            // buttonGrid
            // 
            this.buttonGrid.Location = new System.Drawing.Point(7, 367);
            this.buttonGrid.Name = "buttonGrid";
            this.buttonGrid.Size = new System.Drawing.Size(162, 23);
            this.buttonGrid.TabIndex = 2;
            this.buttonGrid.Text = "Вкл/выкл сетку";
            this.buttonGrid.UseVisualStyleBackColor = true;
            this.buttonGrid.Click += new System.EventHandler(this.buttonGrid_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonOrt_634);
            this.groupBox2.Controls.Add(this.buttonOrt_45);
            this.groupBox2.Controls.Add(this.buttonOnePoint);
            this.groupBox2.Controls.Add(this.buttonFront);
            this.groupBox2.Location = new System.Drawing.Point(627, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 89);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Проекция";
            // 
            // buttonOrt_634
            // 
            this.buttonOrt_634.Location = new System.Drawing.Point(87, 50);
            this.buttonOrt_634.Name = "buttonOrt_634";
            this.buttonOrt_634.Size = new System.Drawing.Size(75, 23);
            this.buttonOrt_634.TabIndex = 3;
            this.buttonOrt_634.Text = "Орт 63,4";
            this.buttonOrt_634.UseVisualStyleBackColor = true;
            this.buttonOrt_634.Click += new System.EventHandler(this.buttonOrt_634_Click);
            // 
            // buttonOrt_45
            // 
            this.buttonOrt_45.Location = new System.Drawing.Point(6, 50);
            this.buttonOrt_45.Name = "buttonOrt_45";
            this.buttonOrt_45.Size = new System.Drawing.Size(75, 23);
            this.buttonOrt_45.TabIndex = 2;
            this.buttonOrt_45.Text = "Орт 45";
            this.buttonOrt_45.UseVisualStyleBackColor = true;
            this.buttonOrt_45.Click += new System.EventHandler(this.buttonOrt_45_Click);
            // 
            // buttonOnePoint
            // 
            this.buttonOnePoint.Location = new System.Drawing.Point(86, 21);
            this.buttonOnePoint.Name = "buttonOnePoint";
            this.buttonOnePoint.Size = new System.Drawing.Size(75, 23);
            this.buttonOnePoint.TabIndex = 1;
            this.buttonOnePoint.Text = "Одноточ";
            this.buttonOnePoint.UseVisualStyleBackColor = true;
            this.buttonOnePoint.Click += new System.EventHandler(this.buttonOnePoint_Click);
            // 
            // buttonFront
            // 
            this.buttonFront.Location = new System.Drawing.Point(7, 21);
            this.buttonFront.Name = "buttonFront";
            this.buttonFront.Size = new System.Drawing.Size(75, 23);
            this.buttonFront.TabIndex = 0;
            this.buttonFront.Text = "Спереди";
            this.buttonFront.UseVisualStyleBackColor = true;
            this.buttonFront.Click += new System.EventHandler(this.buttonFront_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "3D преобразования";
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonGrid;
        private System.Windows.Forms.Button buttonMirrorZ;
        private System.Windows.Forms.Button buttonMirrorY;
        private System.Windows.Forms.Button buttonMirrorX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonScale;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.NumericUpDown numericUpDown9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOrt_634;
        private System.Windows.Forms.Button buttonOrt_45;
        private System.Windows.Forms.Button buttonOnePoint;
        private System.Windows.Forms.Button buttonFront;
    }
}

