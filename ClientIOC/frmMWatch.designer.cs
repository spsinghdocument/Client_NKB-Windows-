namespace Client
{
    partial class frmMWatch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMWatch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addBlankRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameWatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDELToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sELLF2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bUYF1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openTokenInLadderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.combo_Exchange = new System.Windows.Forms.ToolStripComboBox();
            this.comboB_OrderType = new System.Windows.Forms.ToolStripComboBox();
            this.comboBInstType = new System.Windows.Forms.ToolStripComboBox();
            this.comboB_Symbol = new System.Windows.Forms.ToolStripComboBox();
            this.combo_Exoiry = new System.Windows.Forms.ToolStripComboBox();
            this.combo_OptionType = new System.Windows.Forms.ToolStripComboBox();
            this.combo_StrikePrice = new System.Windows.Forms.ToolStripComboBox();
            this.btnsaveMktwatch = new System.Windows.Forms.ToolStripButton();
            this.btnLoadMktWatch = new System.Windows.Forms.ToolStripButton();
            this.btnprofile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this._formulaBar = new System.Windows.Forms.Panel();
            this._txtFormula = new System.Windows.Forms.TextBox();
            this._lblFunctions = new System.Windows.Forms.Label();
            this._lblAddress = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteCtrlVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DGV = new Client.DataGridCalc();
            this.button1 = new System.Windows.Forms.Button();
            this.context.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this._formulaBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // addBlankRowToolStripMenuItem
            // 
            this.addBlankRowToolStripMenuItem.Name = "addBlankRowToolStripMenuItem";
            this.addBlankRowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)));
            this.addBlankRowToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.addBlankRowToolStripMenuItem.Text = "Add Blank Row";
            // 
            // renameWatchToolStripMenuItem
            // 
            this.renameWatchToolStripMenuItem.Name = "renameWatchToolStripMenuItem";
            this.renameWatchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.renameWatchToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.renameWatchToolStripMenuItem.Text = "Rename Watch";
            // 
            // removeDELToolStripMenuItem
            // 
            this.removeDELToolStripMenuItem.Name = "removeDELToolStripMenuItem";
            this.removeDELToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.removeDELToolStripMenuItem.Text = "Remove Token (DEL)";
            // 
            // sELLF2ToolStripMenuItem
            // 
            this.sELLF2ToolStripMenuItem.Name = "sELLF2ToolStripMenuItem";
            this.sELLF2ToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.sELLF2ToolStripMenuItem.Text = "Sell (F2)";
            // 
            // bUYF1ToolStripMenuItem
            // 
            this.bUYF1ToolStripMenuItem.Name = "bUYF1ToolStripMenuItem";
            this.bUYF1ToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.bUYF1ToolStripMenuItem.Text = "Buy (F1)";
            // 
            // context
            // 
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bUYF1ToolStripMenuItem,
            this.sELLF2ToolStripMenuItem,
            this.removeDELToolStripMenuItem,
            this.renameWatchToolStripMenuItem,
            this.openTokenInLadderToolStripMenuItem,
            this.addBlankRowToolStripMenuItem});
            this.context.Name = "context";
            this.context.Size = new System.Drawing.Size(232, 136);
            // 
            // openTokenInLadderToolStripMenuItem
            // 
            this.openTokenInLadderToolStripMenuItem.Name = "openTokenInLadderToolStripMenuItem";
            this.openTokenInLadderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.openTokenInLadderToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.openTokenInLadderToolStripMenuItem.Text = "Open Token in Ladder";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.combo_Exchange,
            this.comboB_OrderType,
            this.comboBInstType,
            this.comboB_Symbol,
            this.combo_Exoiry,
            this.combo_OptionType,
            this.combo_StrikePrice,
            this.btnsaveMktwatch,
            this.btnLoadMktWatch,
            this.btnprofile,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1151, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // combo_Exchange
            // 
            this.combo_Exchange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Exchange.Name = "combo_Exchange";
            this.combo_Exchange.Size = new System.Drawing.Size(121, 25);
            this.combo_Exchange.Sorted = true;
            this.combo_Exchange.ToolTipText = "Instrument Name";
            this.combo_Exchange.SelectedIndexChanged += new System.EventHandler(this.combo_Exchange_SelectedIndexChanged);
            // 
            // comboB_OrderType
            // 
            this.comboB_OrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboB_OrderType.Name = "comboB_OrderType";
            this.comboB_OrderType.Size = new System.Drawing.Size(121, 25);
            this.comboB_OrderType.Sorted = true;
            this.comboB_OrderType.ToolTipText = "Symbol";
            this.comboB_OrderType.SelectedIndexChanged += new System.EventHandler(this.comboB_OrderType_SelectedIndexChanged);
            // 
            // comboBInstType
            // 
            this.comboBInstType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBInstType.Name = "comboBInstType";
            this.comboBInstType.Size = new System.Drawing.Size(121, 25);
            this.comboBInstType.Sorted = true;
            this.comboBInstType.ToolTipText = "Option Type";
            this.comboBInstType.SelectedIndexChanged += new System.EventHandler(this.comboBInstType_SelectedIndexChanged);
            this.comboBInstType.Click += new System.EventHandler(this.comboBInstType_Click);
            // 
            // comboB_Symbol
            // 
            this.comboB_Symbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboB_Symbol.Name = "comboB_Symbol";
            this.comboB_Symbol.Size = new System.Drawing.Size(121, 25);
            this.comboB_Symbol.Sorted = true;
            this.comboB_Symbol.ToolTipText = "Strike Price";
            this.comboB_Symbol.SelectedIndexChanged += new System.EventHandler(this.comboB_Symbol_SelectedIndexChanged);
            this.comboB_Symbol.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboB_Symbol_KeyDown);
            this.comboB_Symbol.Click += new System.EventHandler(this.comboB_Symbol_Click);
            // 
            // combo_Exoiry
            // 
            this.combo_Exoiry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Exoiry.Name = "combo_Exoiry";
            this.combo_Exoiry.Size = new System.Drawing.Size(121, 25);
            this.combo_Exoiry.ToolTipText = "Seriex/Expiry Date";
            this.combo_Exoiry.SelectedIndexChanged += new System.EventHandler(this.combo_Exoiry_SelectedIndexChanged);
            this.combo_Exoiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combo_Exoiry_KeyDown);
            this.combo_Exoiry.Click += new System.EventHandler(this.combo_Exoiry_Click);
            // 
            // combo_OptionType
            // 
            this.combo_OptionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_OptionType.Name = "combo_OptionType";
            this.combo_OptionType.Size = new System.Drawing.Size(121, 25);
            this.combo_OptionType.SelectedIndexChanged += new System.EventHandler(this.combo_OptionType_SelectedIndexChanged);
            // 
            // combo_StrikePrice
            // 
            this.combo_StrikePrice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_StrikePrice.Name = "combo_StrikePrice";
            this.combo_StrikePrice.Size = new System.Drawing.Size(121, 25);
            this.combo_StrikePrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.combo_Exoiry_KeyDown);
            // 
            // btnsaveMktwatch
            // 
            this.btnsaveMktwatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnsaveMktwatch.Image = ((System.Drawing.Image)(resources.GetObject("btnsaveMktwatch.Image")));
            this.btnsaveMktwatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnsaveMktwatch.Name = "btnsaveMktwatch";
            this.btnsaveMktwatch.Size = new System.Drawing.Size(23, 22);
            this.btnsaveMktwatch.Text = "Save Market Watch";
            this.btnsaveMktwatch.ToolTipText = "Save Market Watch";
            this.btnsaveMktwatch.Click += new System.EventHandler(this.btnsaveMktwatch_Click);
            // 
            // btnLoadMktWatch
            // 
            this.btnLoadMktWatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadMktWatch.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadMktWatch.Image")));
            this.btnLoadMktWatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadMktWatch.Name = "btnLoadMktWatch";
            this.btnLoadMktWatch.Size = new System.Drawing.Size(23, 22);
            this.btnLoadMktWatch.Text = "Load Market Watch";
            this.btnLoadMktWatch.Click += new System.EventHandler(this.btnLoadMktWatch_Click);
            // 
            // btnprofile
            // 
            this.btnprofile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnprofile.Image = ((System.Drawing.Image)(resources.GetObject("btnprofile.Image")));
            this.btnprofile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnprofile.Name = "btnprofile";
            this.btnprofile.Size = new System.Drawing.Size(23, 22);
            this.btnprofile.Text = "Create/Load Profile";
            this.btnprofile.Click += new System.EventHandler(this.btnprofile_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton3.Text = "change";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // _formulaBar
            // 
            this._formulaBar.Controls.Add(this._txtFormula);
            this._formulaBar.Controls.Add(this._lblFunctions);
            this._formulaBar.Controls.Add(this._lblAddress);
            this._formulaBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._formulaBar.Location = new System.Drawing.Point(0, 25);
            this._formulaBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._formulaBar.Name = "_formulaBar";
            this._formulaBar.Size = new System.Drawing.Size(1151, 27);
            this._formulaBar.TabIndex = 6;
            // 
            // _txtFormula
            // 
            this._txtFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtFormula.Location = new System.Drawing.Point(157, 0);
            this._txtFormula.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._txtFormula.Name = "_txtFormula";
            this._txtFormula.Size = new System.Drawing.Size(994, 20);
            this._txtFormula.TabIndex = 5;
            // 
            // _lblFunctions
            // 
            this._lblFunctions.Dock = System.Windows.Forms.DockStyle.Left;
            this._lblFunctions.Font = new System.Drawing.Font("Times New Roman", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblFunctions.Location = new System.Drawing.Point(98, 0);
            this._lblFunctions.Name = "_lblFunctions";
            this._lblFunctions.Size = new System.Drawing.Size(59, 27);
            this._lblFunctions.TabIndex = 4;
            this._lblFunctions.Text = "fx";
            this._lblFunctions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblAddress
            // 
            this._lblAddress.BackColor = System.Drawing.SystemColors.Window;
            this._lblAddress.Dock = System.Windows.Forms.DockStyle.Left;
            this._lblAddress.Location = new System.Drawing.Point(0, 0);
            this._lblAddress.Name = "_lblAddress";
            this._lblAddress.Size = new System.Drawing.Size(98, 27);
            this._lblAddress.TabIndex = 3;
            this._lblAddress.Text = "A1";
            this._lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this._lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 347);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1151, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1095, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // _lblStatus
            // 
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(39, 17);
            this._lblStatus.Text = "Ready";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteCtrlVToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteCtrlVToolStripMenuItem
            // 
            this.pasteCtrlVToolStripMenuItem.Name = "pasteCtrlVToolStripMenuItem";
            this.pasteCtrlVToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteCtrlVToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteCtrlVToolStripMenuItem.Text = "Paste";
            this.pasteCtrlVToolStripMenuItem.Click += new System.EventHandler(this.pasteCtrlVToolStripMenuItem_Click);
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(843, 47);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(100, 20);
            this.txt.TabIndex = 9;
            this.txt.Visible = false;
            this.txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(167, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem1.Text = "New Row";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // DGV
            // 
            this.DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.DGV.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DGV.DataContext = null;
            this.DGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.DGV.Location = new System.Drawing.Point(4, 73);
            this.DGV.Name = "DGV";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV.RowHeadersWidth = 50;
            this.DGV.Size = new System.Drawing.Size(1143, 271);
            this.DGV.TabIndex = 7;
            this.DGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellClick);
            this.DGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellContentClick_1);
            this.DGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellEndEdit);
            this.DGV.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellEnter);
            this.DGV.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellLeave);
            this.DGV.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_CellMouseClick);
            this.DGV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGV_CellValueChanged);
            this.DGV.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGV_ColumnHeaderMouseClick);
            this.DGV.CurrentCellDirtyStateChanged += new System.EventHandler(this.DGV_CurrentCellDirtyStateChanged);
            this.DGV.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DGV_DataError);
            this.DGV.DragDrop += new System.Windows.Forms.DragEventHandler(this.DGV_DragDrop);
            this.DGV.DragEnter += new System.Windows.Forms.DragEventHandler(this.DGV_DragEnter);
            this.DGV.DragOver += new System.Windows.Forms.DragEventHandler(this.DGV_DragOver);
            this.DGV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGV_KeyDown_1);
            this.DGV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DGV_MouseClick);
            this.DGV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DGV_MouseDown);
            this.DGV.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DGV_MouseMove);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1055, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Export To Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 369);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.DGV);
            this.Controls.Add(this._formulaBar);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmMWatch";
            this.Text = "Market Watch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMktWatch_FormClosing);
            this.Load += new System.EventHandler(this.frmMktWatch_Load);
            this.context.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this._formulaBar.ResumeLayout(false);
            this._formulaBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem addBlankRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameWatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeDELToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sELLF2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bUYF1ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip context;
        public System.Windows.Forms.ToolStripMenuItem openTokenInLadderToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox combo_Exchange;
        private System.Windows.Forms.ToolStripComboBox comboB_OrderType;
        private System.Windows.Forms.ToolStripComboBox comboBInstType;
        private System.Windows.Forms.ToolStripComboBox comboB_Symbol;
        private System.Windows.Forms.ToolStripComboBox combo_Exoiry;
        private System.Windows.Forms.ToolStripButton btnsaveMktwatch;
        private System.Windows.Forms.ToolStripButton btnLoadMktWatch;
        private System.Windows.Forms.ToolStripButton btnprofile;
        private System.Windows.Forms.ToolStripComboBox combo_OptionType;
        private System.Windows.Forms.ToolStripComboBox combo_StrikePrice;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel _formulaBar;
        private System.Windows.Forms.TextBox _txtFormula;
        private System.Windows.Forms.Label _lblFunctions;
        private System.Windows.Forms.Label _lblAddress;
        private DataGridCalc DGV;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _lblStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteCtrlVToolStripMenuItem;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Button button1;
    }
}