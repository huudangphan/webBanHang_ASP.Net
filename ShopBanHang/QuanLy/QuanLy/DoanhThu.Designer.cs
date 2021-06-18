
namespace QuanLy.QuanLy
{
    partial class DoanhThu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoanhThu));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgvphat = new System.Windows.Forms.DataGridView();
            this.dtgvphieu = new System.Windows.Forms.DataGridView();
            this.dtgvtg = new System.Windows.Forms.DataGridView();
            this.dtgvoff = new System.Windows.Forms.DataGridView();
            this.dtgvonline = new System.Windows.Forms.DataGridView();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dtgvchi = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvphat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvphieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvonline)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvchi)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.barButtonItem1});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 3;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(964, 193);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Xem thống kê";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.Image")));
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 698);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(964, 30);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtgvphat);
            this.groupBox2.Controls.Add(this.dtgvphieu);
            this.groupBox2.Controls.Add(this.dtgvtg);
            this.groupBox2.Controls.Add(this.dtgvoff);
            this.groupBox2.Controls.Add(this.dtgvonline);
            this.groupBox2.Location = new System.Drawing.Point(12, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(362, 461);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tổng thu";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 399);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tiền phạt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tiền góp hàng tháng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Đơn hàng trả góp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Đơn hàng offline";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Đơn hàng online";
            // 
            // dtgvphat
            // 
            this.dtgvphat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvphat.Location = new System.Drawing.Point(194, 381);
            this.dtgvphat.Name = "dtgvphat";
            this.dtgvphat.RowHeadersWidth = 51;
            this.dtgvphat.RowTemplate.Height = 24;
            this.dtgvphat.Size = new System.Drawing.Size(162, 74);
            this.dtgvphat.TabIndex = 4;
            // 
            // dtgvphieu
            // 
            this.dtgvphieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvphieu.Location = new System.Drawing.Point(194, 300);
            this.dtgvphieu.Name = "dtgvphieu";
            this.dtgvphieu.RowHeadersWidth = 51;
            this.dtgvphieu.RowTemplate.Height = 24;
            this.dtgvphieu.Size = new System.Drawing.Size(162, 74);
            this.dtgvphieu.TabIndex = 3;
            // 
            // dtgvtg
            // 
            this.dtgvtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvtg.Location = new System.Drawing.Point(194, 212);
            this.dtgvtg.Name = "dtgvtg";
            this.dtgvtg.RowHeadersWidth = 51;
            this.dtgvtg.RowTemplate.Height = 24;
            this.dtgvtg.Size = new System.Drawing.Size(162, 74);
            this.dtgvtg.TabIndex = 2;
            // 
            // dtgvoff
            // 
            this.dtgvoff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvoff.Location = new System.Drawing.Point(194, 127);
            this.dtgvoff.Name = "dtgvoff";
            this.dtgvoff.RowHeadersWidth = 51;
            this.dtgvoff.RowTemplate.Height = 24;
            this.dtgvoff.Size = new System.Drawing.Size(162, 74);
            this.dtgvoff.TabIndex = 1;
            // 
            // dtgvonline
            // 
            this.dtgvonline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvonline.Location = new System.Drawing.Point(194, 46);
            this.dtgvonline.Name = "dtgvonline";
            this.dtgvonline.RowHeadersWidth = 51;
            this.dtgvonline.RowTemplate.Height = 24;
            this.dtgvonline.Size = new System.Drawing.Size(162, 74);
            this.dtgvonline.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dtgvchi);
            this.groupBox7.Controls.Add(this.label6);
            this.groupBox7.Location = new System.Drawing.Point(391, 420);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(529, 105);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Tổng chi";
            // 
            // dtgvchi
            // 
            this.dtgvchi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvchi.Location = new System.Drawing.Point(114, 30);
            this.dtgvchi.Name = "dtgvchi";
            this.dtgvchi.RowHeadersWidth = 51;
            this.dtgvchi.RowTemplate.Height = 24;
            this.dtgvchi.Size = new System.Drawing.Size(152, 69);
            this.dtgvchi.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tổng chi";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM";
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(391, 236);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(213, 40);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // DoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 728);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "DoanhThu";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "DoanhThu";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvphat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvphieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvoff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvonline)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvchi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgvphat;
        private System.Windows.Forms.DataGridView dtgvphieu;
        private System.Windows.Forms.DataGridView dtgvtg;
        private System.Windows.Forms.DataGridView dtgvoff;
        private System.Windows.Forms.DataGridView dtgvonline;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView dtgvchi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}