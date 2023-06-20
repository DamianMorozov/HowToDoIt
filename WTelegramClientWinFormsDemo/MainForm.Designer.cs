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
		listBox = new ListBox();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		labelCode = new Label();
		textBoxCode = new TextBox();
		buttonSendCode = new Button();
		textBoxPhone = new TextBox();
		textBoxApiHash = new TextBox();
		textBoxApiID = new TextBox();
		labelException = new LinkLabel();
		buttonSendMsg = new Button();
		buttonGetDialogs = new Button();
		buttonGetMembers = new Button();
		buttonGetChats = new Button();
		SuspendLayout();
		// 
		// buttonLogin
		// 
		buttonLogin.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		buttonLogin.Location = new Point(5, 130);
		buttonLogin.Margin = new Padding(3, 4, 3, 4);
		buttonLogin.Name = "buttonLogin";
		buttonLogin.Size = new Size(200, 30);
		buttonLogin.TabIndex = 4;
		buttonLogin.Text = "Connect/Login";
		buttonLogin.UseVisualStyleBackColor = true;
		buttonLogin.Click += buttonLogin_Click;
		// 
		// listBox
		// 
		listBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		listBox.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		listBox.FormattingEnabled = true;
		listBox.ItemHeight = 20;
		listBox.Location = new Point(5, 195);
		listBox.Margin = new Padding(3, 4, 3, 4);
		listBox.Name = "listBox";
		listBox.Size = new Size(616, 304);
		listBox.TabIndex = 12;
		// 
		// label1
		// 
		label1.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		label1.Location = new Point(5, 40);
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
		label2.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		label2.Location = new Point(5, 10);
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
		label3.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		label3.Location = new Point(5, 70);
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
		labelCode.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		labelCode.Location = new Point(5, 100);
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
		textBoxCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		textBoxCode.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxCode.Location = new Point(178, 100);
		textBoxCode.Margin = new Padding(3, 4, 3, 4);
		textBoxCode.Name = "textBoxCode";
		textBoxCode.Size = new Size(439, 25);
		textBoxCode.TabIndex = 3;
		textBoxCode.Visible = false;
		// 
		// buttonSendCode
		// 
		buttonSendCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		buttonSendCode.Enabled = false;
		buttonSendCode.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		buttonSendCode.Location = new Point(211, 130);
		buttonSendCode.Margin = new Padding(3, 4, 3, 4);
		buttonSendCode.Name = "buttonSendCode";
		buttonSendCode.Size = new Size(200, 30);
		buttonSendCode.TabIndex = 5;
		buttonSendCode.Text = "Verify";
		buttonSendCode.UseVisualStyleBackColor = true;
		buttonSendCode.Click += buttonSendCode_Click;
		// 
		// textBoxPhone
		// 
		textBoxPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		textBoxPhone.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxPhone.Location = new Point(178, 70);
		textBoxPhone.Margin = new Padding(3, 4, 3, 4);
		textBoxPhone.Name = "textBoxPhone";
		textBoxPhone.Size = new Size(439, 25);
		textBoxPhone.TabIndex = 2;
		textBoxPhone.Text = "+";
		// 
		// textBoxApiHash
		// 
		textBoxApiHash.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		textBoxApiHash.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxApiHash.Location = new Point(178, 10);
		textBoxApiHash.Margin = new Padding(3, 4, 3, 4);
		textBoxApiHash.Name = "textBoxApiHash";
		textBoxApiHash.Size = new Size(439, 25);
		textBoxApiHash.TabIndex = 0;
		// 
		// textBoxApiID
		// 
		textBoxApiID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		textBoxApiID.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		textBoxApiID.Location = new Point(178, 40);
		textBoxApiID.Margin = new Padding(3, 4, 3, 4);
		textBoxApiID.Name = "textBoxApiID";
		textBoxApiID.Size = new Size(439, 25);
		textBoxApiID.TabIndex = 1;
		// 
		// labelException
		// 
		labelException.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		labelException.BackColor = Color.Transparent;
		labelException.BorderStyle = BorderStyle.FixedSingle;
		labelException.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		labelException.Location = new Point(5, 505);
		labelException.Name = "labelException";
		labelException.Padding = new Padding(2);
		labelException.Size = new Size(616, 52);
		labelException.TabIndex = 13;
		labelException.Tag = "";
		labelException.TextAlign = ContentAlignment.MiddleCenter;
		// 
		// buttonSendMsg
		// 
		buttonSendMsg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		buttonSendMsg.Enabled = false;
		buttonSendMsg.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		buttonSendMsg.Location = new Point(417, 130);
		buttonSendMsg.Margin = new Padding(3, 4, 3, 4);
		buttonSendMsg.Name = "buttonSendMsg";
		buttonSendMsg.Size = new Size(200, 30);
		buttonSendMsg.TabIndex = 14;
		buttonSendMsg.Text = "Send to self...";
		buttonSendMsg.UseVisualStyleBackColor = true;
		buttonSendMsg.Click += buttonSendMsg_Click;
		// 
		// buttonGetDialogs
		// 
		buttonGetDialogs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		buttonGetDialogs.Enabled = false;
		buttonGetDialogs.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		buttonGetDialogs.Location = new Point(211, 160);
		buttonGetDialogs.Margin = new Padding(3, 4, 3, 4);
		buttonGetDialogs.Name = "buttonGetDialogs";
		buttonGetDialogs.Size = new Size(200, 30);
		buttonGetDialogs.TabIndex = 15;
		buttonGetDialogs.Text = "Get dialogs";
		buttonGetDialogs.UseVisualStyleBackColor = true;
		buttonGetDialogs.Click += buttonGetDialogs_Click;
		// 
		// buttonGetMembers
		// 
		buttonGetMembers.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		buttonGetMembers.Enabled = false;
		buttonGetMembers.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		buttonGetMembers.Location = new Point(417, 160);
		buttonGetMembers.Margin = new Padding(3, 4, 3, 4);
		buttonGetMembers.Name = "buttonGetMembers";
		buttonGetMembers.Size = new Size(200, 30);
		buttonGetMembers.TabIndex = 16;
		buttonGetMembers.Text = "Get members";
		buttonGetMembers.UseVisualStyleBackColor = true;
		buttonGetMembers.Click += buttonGetMembers_Click;
		// 
		// buttonGetChats
		// 
		buttonGetChats.Enabled = false;
		buttonGetChats.Font = new Font("Cascadia Code", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
		buttonGetChats.Location = new Point(5, 160);
		buttonGetChats.Margin = new Padding(3, 4, 3, 4);
		buttonGetChats.Name = "buttonGetChats";
		buttonGetChats.Size = new Size(200, 30);
		buttonGetChats.TabIndex = 17;
		buttonGetChats.Text = "Get chats";
		buttonGetChats.UseVisualStyleBackColor = true;
		buttonGetChats.Click += buttonGetChats_Click;
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(9F, 21F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(624, 561);
		Controls.Add(buttonGetChats);
		Controls.Add(buttonGetMembers);
		Controls.Add(buttonGetDialogs);
		Controls.Add(buttonSendMsg);
		Controls.Add(labelException);
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
		MinimumSize = new Size(640, 400);
		Name = "MainForm";
		Text = "WTelegramClientWinFormsDemo";
		FormClosing += MainForm_FormClosing;
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.Button buttonLogin;
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
	private LinkLabel labelException;
	private Button buttonSendMsg;
	private Button buttonGetDialogs;
	private Button buttonGetMembers;
	private Button buttonGetChats;
}
