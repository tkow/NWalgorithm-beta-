namespace NWandSW
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.selectrange = new System.Windows.Forms.ComboBox();
            this.sequencelength1 = new System.Windows.Forms.TextBox();
            this.sequencelength2 = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.optscore = new System.Windows.Forms.Label();
            this.alignmentX = new System.Windows.Forms.Label();
            this.alignmentY = new System.Windows.Forms.Label();
            this.sequenceproductcb = new System.Windows.Forms.ComboBox();
            this.selectrangelabel = new System.Windows.Forms.Label();
            this.sequenceproduct = new System.Windows.Forms.Label();
            this.seqXlength = new System.Windows.Forms.Label();
            this.seqYlength = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.costdset = new System.Windows.Forms.Label();
            this.costeset = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // selectrange
            // 
            this.selectrange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectrange.FormattingEnabled = true;
            this.selectrange.Items.AddRange(new object[] {
            "global",
            "rocal",
            "grobal -affinegapcost",
            "rocal -afeinegapcost"});
            this.selectrange.Location = new System.Drawing.Point(21, 40);
            this.selectrange.Margin = new System.Windows.Forms.Padding(2);
            this.selectrange.Name = "selectrange";
            this.selectrange.Size = new System.Drawing.Size(106, 20);
            this.selectrange.TabIndex = 0;
            this.selectrange.SelectedIndexChanged += new System.EventHandler(this.selectrange_SelectedIndexChanged);
            // 
            // sequencelength1
            // 
            this.sequencelength1.Location = new System.Drawing.Point(39, 118);
            this.sequencelength1.Margin = new System.Windows.Forms.Padding(2);
            this.sequencelength1.Name = "sequencelength1";
            this.sequencelength1.Size = new System.Drawing.Size(62, 19);
            this.sequencelength1.TabIndex = 1;
            // 
            // sequencelength2
            // 
            this.sequencelength2.Location = new System.Drawing.Point(39, 158);
            this.sequencelength2.Margin = new System.Windows.Forms.Padding(2);
            this.sequencelength2.Name = "sequencelength2";
            this.sequencelength2.Size = new System.Drawing.Size(62, 19);
            this.sequencelength2.TabIndex = 2;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(41, 197);
            this.start.Margin = new System.Windows.Forms.Padding(2);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(60, 25);
            this.start.TabIndex = 3;
            this.start.Text = "start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(154, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 200);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // optscore
            // 
            this.optscore.AutoSize = true;
            this.optscore.Location = new System.Drawing.Point(269, 302);
            this.optscore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.optscore.Name = "optscore";
            this.optscore.Size = new System.Drawing.Size(55, 12);
            this.optscore.TabIndex = 5;
            this.optscore.Text = "最適スコア";
            // 
            // alignmentX
            // 
            this.alignmentX.AutoSize = true;
            this.alignmentX.Location = new System.Drawing.Point(258, 257);
            this.alignmentX.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.alignmentX.Name = "alignmentX";
            this.alignmentX.Size = new System.Drawing.Size(66, 12);
            this.alignmentX.TabIndex = 6;
            this.alignmentX.Text = "アライメント１:";
            // 
            // alignmentY
            // 
            this.alignmentY.AutoSize = true;
            this.alignmentY.Location = new System.Drawing.Point(260, 278);
            this.alignmentY.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.alignmentY.Name = "alignmentY";
            this.alignmentY.Size = new System.Drawing.Size(64, 12);
            this.alignmentY.TabIndex = 7;
            this.alignmentY.Text = "アライメント2:";
            // 
            // sequenceproductcb
            // 
            this.sequenceproductcb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sequenceproductcb.FormattingEnabled = true;
            this.sequenceproductcb.Items.AddRange(new object[] {
            "Manual",
            "Auto",
            "Auto-TimeIncrement",
            "Readfile"});
            this.sequenceproductcb.Location = new System.Drawing.Point(21, 79);
            this.sequenceproductcb.Name = "sequenceproductcb";
            this.sequenceproductcb.Size = new System.Drawing.Size(106, 20);
            this.sequenceproductcb.TabIndex = 8;
            // 
            // selectrangelabel
            // 
            this.selectrangelabel.AutoSize = true;
            this.selectrangelabel.Location = new System.Drawing.Point(19, 22);
            this.selectrangelabel.Name = "selectrangelabel";
            this.selectrangelabel.Size = new System.Drawing.Size(64, 12);
            this.selectrangelabel.TabIndex = 9;
            this.selectrangelabel.Text = "selectrange";
            // 
            // sequenceproduct
            // 
            this.sequenceproduct.AutoSize = true;
            this.sequenceproduct.Location = new System.Drawing.Point(19, 64);
            this.sequenceproduct.Name = "sequenceproduct";
            this.sequenceproduct.Size = new System.Drawing.Size(91, 12);
            this.sequenceproduct.TabIndex = 10;
            this.sequenceproduct.Text = "sequenceproduct";
            // 
            // seqXlength
            // 
            this.seqXlength.AutoSize = true;
            this.seqXlength.Location = new System.Drawing.Point(19, 104);
            this.seqXlength.Name = "seqXlength";
            this.seqXlength.Size = new System.Drawing.Size(68, 12);
            this.seqXlength.TabIndex = 11;
            this.seqXlength.Text = "seq1 length ";
            // 
            // seqYlength
            // 
            this.seqYlength.AutoSize = true;
            this.seqYlength.Location = new System.Drawing.Point(19, 144);
            this.seqYlength.Name = "seqYlength";
            this.seqYlength.Size = new System.Drawing.Size(68, 12);
            this.seqYlength.TabIndex = 12;
            this.seqYlength.Text = "seq2 length ";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(103, 254);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 19);
            this.textBox1.TabIndex = 13;
            this.textBox1.Text = "2";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(103, 292);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 19);
            this.textBox2.TabIndex = 14;
            this.textBox2.Text = "1";
            // 
            // costdset
            // 
            this.costdset.AutoSize = true;
            this.costdset.Location = new System.Drawing.Point(47, 259);
            this.costdset.Name = "costdset";
            this.costdset.Size = new System.Drawing.Size(37, 12);
            this.costdset.TabIndex = 15;
            this.costdset.Text = "cost d";
            // 
            // costeset
            // 
            this.costeset.AutoSize = true;
            this.costeset.Location = new System.Drawing.Point(47, 292);
            this.costeset.Name = "costeset";
            this.costeset.Size = new System.Drawing.Size(37, 12);
            this.costeset.TabIndex = 16;
            this.costeset.Text = "cost e";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 356);
            this.Controls.Add(this.costeset);
            this.Controls.Add(this.costdset);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.seqYlength);
            this.Controls.Add(this.seqXlength);
            this.Controls.Add(this.sequenceproduct);
            this.Controls.Add(this.selectrangelabel);
            this.Controls.Add(this.sequenceproductcb);
            this.Controls.Add(this.alignmentY);
            this.Controls.Add(this.alignmentX);
            this.Controls.Add(this.optscore);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.sequencelength2);
            this.Controls.Add(this.sequencelength1);
            this.Controls.Add(this.selectrange);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectrange;
        private System.Windows.Forms.TextBox sequencelength1;
        private System.Windows.Forms.TextBox sequencelength2;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label optscore;
        private System.Windows.Forms.Label alignmentX;
        private System.Windows.Forms.Label alignmentY;
        private System.Windows.Forms.ComboBox sequenceproductcb;
        private System.Windows.Forms.Label selectrangelabel;
        private System.Windows.Forms.Label sequenceproduct;
        private System.Windows.Forms.Label seqXlength;
        private System.Windows.Forms.Label seqYlength;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label costdset;
        private System.Windows.Forms.Label costeset;
        private System.Windows.Forms.TextBox textBox1;
    }
}

