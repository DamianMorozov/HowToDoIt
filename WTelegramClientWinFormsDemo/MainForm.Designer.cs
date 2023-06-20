namespace WTelegramClientWinFormsDemo;

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
		buttonLogin = new Button();
		buttonGetChats = new Button();
		listBox = new ListBox();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		labelCode = new Label();
		textBoxCode = new TextBox();
		buttonSendCode = new Button();
		panelActions = new Panel();
		buttonGetDialogs = new Button();
		buttonSendMsg = new Button();
		buttonGetMembers = new Button();
		textBoxPhone = new TextBox();
		textBoxApiHash = new TextBox();
		textBoxApiID = new TextBox();
		labelException = new LinkLabel();
		panelActions.SuspendLayout();
		SuspendLayout();
		// 
		// buttonLogin
		// 
		buttonLogin.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		buttonLogin.Location = new Point(560, 15);
		buttonLogin.Margin = new Padding(3, 4, 3, 4);
		buttonLogin.Name = "buttonLogin";
		buttonLogin.Size = new Size(140, 40);
		buttonLogin.TabIndex = 4;
		buttonLogin.Text = "Connect/Login";
		buttonLogin.UseVisualStyleBackColor = true;
		buttonLogin.Click += buttonLogin_Click;
		// 
		// buttonGetChats
		// 
		buttonGetChats.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		buttonGetChats.Location = new Point(5, 5);
		buttonGetChats.Margin = new Padding(3, 4, 3, 4);
		buttonGetChats.Name = "buttonGetChats";
		buttonGetChats.Size = new Size(140, 50);
		buttonGetChats.TabIndex = 6;
		buttonGetChats.Text = "Get chats";
		buttonGetChats.UseVisualStyleBackColor = true;
		buttonGetChats.Click += buttonGetChats_Click;
		// 
		// listBox
		// 
		listBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		listBox.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		listBox.FormattingEnabled = true;
		listBox.ItemHeight = 21;
		listBox.Location = new Point(5, 228);
		listBox.Margin = new Padding(3, 4, 3, 4);
		listBox.Name = "listBox";
		listBox.Size = new Size(1000, 445);
		listBox.TabIndex = 12;
		// 
		// label1
		// 
		label1.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		label1.Location = new Point(5, 50);
		label1.Margin = new Padding(0);
		label1.Name = "label1";
		label1.Padding = new Padding(2);
		label1.Size = new Size(170, 26);
		label1.TabIndex = 0;
		label1.Text = "API ID";
		label1.TextAlign = ContentAlignment.MiddleRight;
		// 
		// label2
		// 
		label2.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		label2.Location = new Point(5, 15);
		label2.Margin = new Padding(0);
		label2.Name = "label2";
		label2.Padding = new Padding(2);
		label2.Size = new Size(170, 26);
		label2.TabIndex = 2;
		label2.Text = "API HASH";
		label2.TextAlign = ContentAlignment.MiddleRight;
		// 
		// label3
		// 
		label3.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		label3.Location = new Point(5, 85);
		label3.Margin = new Padding(0);
		label3.Name = "label3";
		label3.Padding = new Padding(2);
		label3.Size = new Size(170, 26);
		label3.TabIndex = 5;
		label3.Text = "Phone number";
		label3.TextAlign = ContentAlignment.MiddleRight;
		// 
		// labelCode
		// 
		labelCode.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelCode.Location = new Point(5, 120);
		labelCode.Margin = new Padding(0);
		labelCode.Name = "labelCode";
		labelCode.Padding = new Padding(2);
		labelCode.Size = new Size(170, 26);
		labelCode.TabIndex = 8;
		labelCode.Text = "Verification code";
		labelCode.TextAlign = ContentAlignment.MiddleRight;
		labelCode.Visible = false;
		// 
		// textBoxCode
		// 
		textBoxCode.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxCode.Location = new Point(178, 120);
		textBoxCode.Margin = new Padding(3, 4, 3, 4);
		textBoxCode.Name = "textBoxCode";
		textBoxCode.Size = new Size(330, 26);
		textBoxCode.TabIndex = 3;
		textBoxCode.Visible = false;
		// 
		// buttonSendCode
		// 
		buttonSendCode.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		buttonSendCode.Location = new Point(819, 15);
		buttonSendCode.Margin = new Padding(3, 4, 3, 4);
		buttonSendCode.Name = "buttonSendCode";
		buttonSendCode.Size = new Size(140, 40);
		buttonSendCode.TabIndex = 5;
		buttonSendCode.Text = "Verify";
		buttonSendCode.UseVisualStyleBackColor = true;
		buttonSendCode.Visible = false;
		buttonSendCode.Click += buttonSendCode_Click;
		// 
		// panelActions
		// 
		panelActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		panelActions.Controls.Add(buttonGetDialogs);
		panelActions.Controls.Add(buttonSendMsg);
		panelActions.Controls.Add(buttonGetMembers);
		panelActions.Controls.Add(buttonGetChats);
		panelActions.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		panelActions.Location = new Point(5, 160);
		panelActions.Margin = new Padding(3, 4, 3, 4);
		panelActions.Name = "panelActions";
		panelActions.Size = new Size(1000, 60);
		panelActions.TabIndex = 11;
		panelActions.Visible = false;
		// 
		// buttonGetDialogs
		// 
		buttonGetDialogs.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		buttonGetDialogs.Location = new Point(296, 5);
		buttonGetDialogs.Margin = new Padding(3, 4, 3, 4);
		buttonGetDialogs.Name = "buttonGetDialogs";
		buttonGetDialogs.Size = new Size(140, 50);
		buttonGetDialogs.TabIndex = 8;
		buttonGetDialogs.Text = "Get dialogs";
		buttonGetDialogs.UseVisualStyleBackColor = true;
		buttonGetDialogs.Click += buttonGetDialogs_Click;
		// 
		// buttonSendMsg
		// 
		buttonSendMsg.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		buttonSendMsg.Location = new Point(440, 5);
		buttonSendMsg.Margin = new Padding(3, 4, 3, 4);
		buttonSendMsg.Name = "buttonSendMsg";
		buttonSendMsg.Size = new Size(140, 50);
		buttonSendMsg.TabIndex = 9;
		buttonSendMsg.Text = "Send to self...";
		buttonSendMsg.UseVisualStyleBackColor = true;
		buttonSendMsg.Click += buttonSendMsg_Click;
		// 
		// buttonGetMembers
		// 
		buttonGetMembers.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		buttonGetMembers.Location = new Point(150, 5);
		buttonGetMembers.Margin = new Padding(3, 4, 3, 4);
		buttonGetMembers.Name = "buttonGetMembers";
		buttonGetMembers.Size = new Size(140, 50);
		buttonGetMembers.TabIndex = 7;
		buttonGetMembers.Text = "Get members";
		buttonGetMembers.UseVisualStyleBackColor = true;
		buttonGetMembers.Click += buttonGetMembers_Click;
		// 
		// textBoxPhone
		// 
		textBoxPhone.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxPhone.Location = new Point(178, 85);
		textBoxPhone.Margin = new Padding(3, 4, 3, 4);
		textBoxPhone.Name = "textBoxPhone";
		textBoxPhone.Size = new Size(330, 26);
		textBoxPhone.TabIndex = 2;
		textBoxPhone.Text = "+";
		// 
		// textBoxApiHash
		// 
		textBoxApiHash.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxApiHash.Location = new Point(178, 15);
		textBoxApiHash.Margin = new Padding(3, 4, 3, 4);
		textBoxApiHash.Name = "textBoxApiHash";
		textBoxApiHash.Size = new Size(330, 26);
		textBoxApiHash.TabIndex = 0;
		// 
		// textBoxApiID
		// 
		textBoxApiID.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxApiID.Location = new Point(178, 50);
		textBoxApiID.Margin = new Padding(3, 4, 3, 4);
		textBoxApiID.Name = "textBoxApiID";
		textBoxApiID.Size = new Size(330, 26);
		textBoxApiID.TabIndex = 1;
		// 
		// labelException
		// 
		labelException.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		labelException.BackColor = Color.Transparent;
		labelException.BorderStyle = BorderStyle.FixedSingle;
		labelException.Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		labelException.Location = new Point(5, 670);
		labelException.Name = "labelException";
		labelException.Padding = new Padding(2);
		labelException.Size = new Size(1000, 52);
		labelException.TabIndex = 13;
		labelException.Tag = "";
		labelException.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(9F, 21F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1008, 729);
		Controls.Add(labelException);
		Controls.Add(panelActions);
		Controls.Add(buttonSendCode);
		Controls.Add(labelCode);
		Controls.Add(textBoxCode);
		Controls.Add(label3);
		Controls.Add(textBoxPhone);
		Controls.Add(label2);
		Controls.Add(textBoxApiHash);
		Controls.Add(label1);
		Controls.Add(textBoxApiID);
		Controls.Add(listBox);
		Controls.Add(buttonLogin);
		Font = new Font("Cascadia Code", 12F, FontStyle.Regular, GraphicsUnit.Point);
		Margin = new Padding(3, 4, 3, 4);
		MinimumSize = new Size(1024, 768);
		Name = "MainForm";
		Text = "WTelegramClientWinFormsDemo";
		FormClosing += MainForm_FormClosing;
		panelActions.ResumeLayout(false);
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.Button buttonLogin;
	private System.Windows.Forms.Button buttonGetChats;
	private System.Windows.Forms.ListBox listBox;
	private System.Windows.Forms.TextBox textBoxApiID;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox textBoxApiHash;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.TextBox textBoxPhone;
	private System.Windows.Forms.Label labelCode;
	private System.Windows.Forms.TextBox textBoxCode;
	private System.Windows.Forms.Button buttonSendCode;
	private System.Windows.Forms.Panel panelActions;
	private System.Windows.Forms.Button buttonGetMembers;
	private System.Windows.Forms.Button buttonSendMsg;
	private System.Windows.Forms.Button buttonGetDialogs;
	private LinkLabel labelException;
}
