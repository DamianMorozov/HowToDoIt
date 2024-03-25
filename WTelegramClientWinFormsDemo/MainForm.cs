// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WTelegram;

namespace WTelegramClientWinFormsDemo;

public partial class MainForm : Form
{
	private Client _client;

	public MainForm()
	{
		InitializeComponent();
		Helpers.Log = (l, s) => Debug.WriteLine(s);
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		_client?.Dispose();
	}

	private async void buttonLogin_Click(object sender, EventArgs e)
	{
		try
		{
			labelException.Text = string.Empty;
			buttonLogin.Enabled = false;
			listBox.Items.Add("Connecting & login into Telegram servers...");
			_client = new Client(int.Parse(textBoxApiID.Text), textBoxApiHash.Text);
			await DoLogin(textBoxPhone.Text);
		}
		catch (Exception ex)
		{
			labelException.Text =
				ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
		}
	}

	private async Task DoLogin(string loginInfo)
	{
		string what = await _client.Login(loginInfo);
		if (what != null)
		{
			listBox.Items.Add($"A {what} is required...");
			labelCode.Text = what;
			textBoxCode.Text = "";
			labelCode.Visible = textBoxCode.Visible = buttonSendCode.Enabled = true;
			textBoxCode.Focus();
			return;
		}
		buttonGetChats.Enabled = true;
		buttonGetDialogs.Enabled = true;
		buttonGetMembers.Enabled = true;
		buttonSendMsg.Enabled = true;
		listBox.Items.Add($"We are now connected as {_client.User}");
	}

	private async void buttonSendCode_Click(object sender, EventArgs e)
	{
		try
		{
			labelException.Text = string.Empty;
			labelCode.Visible = textBoxCode.Visible = buttonSendCode.Enabled = false;
			await DoLogin(textBoxCode.Text);
		}
		catch (Exception ex)
		{
			labelException.Text =
				ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
		}
	}

	private async void buttonGetChats_Click(object sender, EventArgs e)
	{
		try
		{
			labelException.Text = string.Empty;
			if (_client.User == null)
			{
				MessageBox.Show(@"You must login first.");
				return;
			}
			Messages_Chats chats = await _client.Messages_GetAllChats();
			listBox.Items.Clear();
			foreach (ChatBase chat in chats.chats.Values)
				if (chat.IsActive)
					listBox.Items.Add(chat);
		}
		catch (Exception ex)
		{
			labelException.Text =
				ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
		}
	}

	private async void buttonGetMembers_Click(object sender, EventArgs e)
	{
		try
		{
			labelException.Text = string.Empty;
			if (listBox.SelectedItem is not ChatBase chat)
			{
				MessageBox.Show(@"You must select a chat in the list first");
				return;
			}
			Dictionary<long, User> users = chat is Channel channel
				? (await _client.Channels_GetAllParticipants(channel)).users
				: (await _client.Messages_GetFullChat(chat.ID)).users;
			listBox.Items.Clear();
			foreach (User user in users.Values)
				listBox.Items.Add(user);
		}
		catch (Exception ex)
		{
			labelException.Text =
				ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
		}
	}

	private async void buttonGetDialogs_Click(object sender, EventArgs e)
	{
		try
		{
			labelException.Text = string.Empty;
			if (_client.User == null)
			{
				MessageBox.Show(@"You must login first.");
				return;
			}
			Messages_Dialogs dialogs = await _client.Messages_GetAllDialogs();
			listBox.Items.Clear();
			foreach (Dialog dialog in dialogs.dialogs)
			{
				IPeerInfo peer = dialogs.UserOrChat(dialog);
				if (peer.IsActive)
					listBox.Items.Add(peer);
			}
		}
		catch (Exception ex)
		{
			labelException.Text =
				ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
		}
	}

	private async void buttonSendMsg_Click(object sender, EventArgs e)
	{
		try
		{
			labelException.Text = string.Empty;
			string msg = Interaction.InputBox("Type some text to send to ourselves\n(Saved Messages)", "Send to self");
			if (!string.IsNullOrEmpty(msg))
			{
				msg = "_Here is your *saved message*:_\n" + Markdown.Escape(msg);
				MessageEntity[] entities = _client.MarkdownToEntities(ref msg);
				await _client.SendMessageAsync(InputPeer.Self, msg, entities: entities);
			}
		}
		catch (Exception ex)
		{
			labelException.Text =
				ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
		}
	}
}