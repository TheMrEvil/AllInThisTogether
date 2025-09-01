using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace System.Net.Mail
{
	/// <summary>Allows applications to send email by using the Simple Mail Transfer Protocol (SMTP).</summary>
	// Token: 0x0200082F RID: 2095
	[Obsolete("SmtpClient and its network of types are poorly designed, we strongly recommend you use https://github.com/jstedfast/MailKit and https://github.com/jstedfast/MimeKit instead")]
	public class SmtpClient : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class by using configuration file settings.</summary>
		// Token: 0x060042B4 RID: 17076 RVA: 0x000E7EA7 File Offset: 0x000E60A7
		public SmtpClient() : this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends email by using the specified SMTP server.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host computer used for SMTP transactions.</param>
		// Token: 0x060042B5 RID: 17077 RVA: 0x000E7EB1 File Offset: 0x000E60B1
		public SmtpClient(string host) : this(host, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends email by using the specified SMTP server and port.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host used for SMTP transactions.</param>
		/// <param name="port">An <see cref="T:System.Int32" /> greater than zero that contains the port to be used on <paramref name="host" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> cannot be less than zero.</exception>
		// Token: 0x060042B6 RID: 17078 RVA: 0x000E7EBC File Offset: 0x000E60BC
		public SmtpClient(string host, int port)
		{
			SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			if (smtpSection != null)
			{
				this.host = smtpSection.Network.Host;
				this.port = smtpSection.Network.Port;
				this.enableSsl = smtpSection.Network.EnableSsl;
				this.TargetName = smtpSection.Network.TargetName;
				if (this.TargetName == null)
				{
					this.TargetName = "SMTPSVC/" + ((host != null) ? host : "");
				}
				if (smtpSection.Network.UserName != null)
				{
					string password = string.Empty;
					if (smtpSection.Network.Password != null)
					{
						password = smtpSection.Network.Password;
					}
					this.Credentials = new CCredentialsByHost(smtpSection.Network.UserName, password);
				}
				if (!string.IsNullOrEmpty(smtpSection.From))
				{
					this.defaultFrom = new MailAddress(smtpSection.From);
				}
			}
			if (!string.IsNullOrEmpty(host))
			{
				this.host = host;
			}
			if (port != 0)
			{
				this.port = port;
				return;
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
		}

		/// <summary>Specify which certificates should be used to establish the Secure Sockets Layer (SSL) connection.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, holding one or more client certificates. The default value is derived from the mail configuration attributes in a configuration file.</returns>
		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000E7FED File Offset: 0x000E61ED
		[MonoTODO("Client certificates not used")]
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the SPN to use for extended protection. The default value for this SPN is of the form "SMTPSVC/&lt;host&gt;" where &lt;host&gt; is the hostname of the SMTP mail server.</returns>
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x000E8008 File Offset: 0x000E6208
		// (set) Token: 0x060042B9 RID: 17081 RVA: 0x000E8010 File Offset: 0x000E6210
		public string TargetName
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetName>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the credentials used to authenticate the sender.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentialsByHost" /> that represents the credentials to use for authentication; or <see langword="null" /> if no credentials have been specified.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x000E8019 File Offset: 0x000E6219
		// (set) Token: 0x060042BB RID: 17083 RVA: 0x000E8021 File Offset: 0x000E6221
		public ICredentialsByHost Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.CheckState();
				this.credentials = value;
			}
		}

		/// <summary>Specifies how outgoing email messages will be handled.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpDeliveryMethod" /> that indicates how email messages are delivered.</returns>
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x000E8030 File Offset: 0x000E6230
		// (set) Token: 0x060042BD RID: 17085 RVA: 0x000E8038 File Offset: 0x000E6238
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
			set
			{
				this.CheckState();
				this.deliveryMethod = value;
			}
		}

		/// <summary>Specify whether the <see cref="T:System.Net.Mail.SmtpClient" /> uses Secure Sockets Layer (SSL) to encrypt the connection.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Net.Mail.SmtpClient" /> uses SSL; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x000E8047 File Offset: 0x000E6247
		// (set) Token: 0x060042BF RID: 17087 RVA: 0x000E804F File Offset: 0x000E624F
		public bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.CheckState();
				this.enableSsl = value;
			}
		}

		/// <summary>Gets or sets the name or IP address of the host used for SMTP transactions.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name or IP address of the computer to use for SMTP transactions.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x000E805E File Offset: 0x000E625E
		// (set) Token: 0x060042C1 RID: 17089 RVA: 0x000E8066 File Offset: 0x000E6266
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("An empty string is not allowed.", "value");
				}
				this.CheckState();
				this.host = value;
			}
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the pickup directory for mail messages.</returns>
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x000E809B File Offset: 0x000E629B
		// (set) Token: 0x060042C3 RID: 17091 RVA: 0x000E80A3 File Offset: 0x000E62A3
		public string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
			set
			{
				this.pickupDirectoryLocation = value;
			}
		}

		/// <summary>Gets or sets the port used for SMTP transactions.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the port number on the SMTP host. The default value is 25.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x000E80AC File Offset: 0x000E62AC
		// (set) Token: 0x060042C5 RID: 17093 RVA: 0x000E80B4 File Offset: 0x000E62B4
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.port = value;
			}
		}

		/// <summary>Gets or sets the delivery format used by <see cref="T:System.Net.Mail.SmtpClient" /> to send email.</summary>
		/// <returns>The delivery format used by <see cref="T:System.Net.Mail.SmtpClient" />.</returns>
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x000E80D2 File Offset: 0x000E62D2
		// (set) Token: 0x060042C7 RID: 17095 RVA: 0x000E80DA File Offset: 0x000E62DA
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return this.deliveryFormat;
			}
			set
			{
				this.CheckState();
				this.deliveryFormat = value;
			}
		}

		/// <summary>Gets the network connection used to transmit the email message.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that connects to the <see cref="P:System.Net.Mail.SmtpClient.Host" /> property used for SMTP.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" /> or the empty string ("").  
		/// -or-  
		/// <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060042C8 RID: 17096 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public ServicePoint ServicePoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> call times out.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that specifies the time-out value in milliseconds. The default value is 100,000 (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation was less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000E80E9 File Offset: 0x000E62E9
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x000E80F1 File Offset: 0x000E62F1
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>
		///   <see langword="true" /> if the default credentials are used; otherwise <see langword="false" />. The default value is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x060042CC RID: 17100 RVA: 0x000E810F File Offset: 0x000E630F
		public bool UseDefaultCredentials
		{
			get
			{
				return false;
			}
			[MonoNotSupported("no DefaultCredential support in Mono")]
			set
			{
				if (value)
				{
					throw new NotImplementedException("Default credentials are not supported");
				}
				this.CheckState();
			}
		}

		/// <summary>Occurs when an asynchronous email send operation completes.</summary>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x060042CD RID: 17101 RVA: 0x000E8128 File Offset: 0x000E6328
		// (remove) Token: 0x060042CE RID: 17102 RVA: 0x000E8160 File Offset: 0x000E6360
		public event SendCompletedEventHandler SendCompleted
		{
			[CompilerGenerated]
			add
			{
				SendCompletedEventHandler sendCompletedEventHandler = this.SendCompleted;
				SendCompletedEventHandler sendCompletedEventHandler2;
				do
				{
					sendCompletedEventHandler2 = sendCompletedEventHandler;
					SendCompletedEventHandler value2 = (SendCompletedEventHandler)Delegate.Combine(sendCompletedEventHandler2, value);
					sendCompletedEventHandler = Interlocked.CompareExchange<SendCompletedEventHandler>(ref this.SendCompleted, value2, sendCompletedEventHandler2);
				}
				while (sendCompletedEventHandler != sendCompletedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				SendCompletedEventHandler sendCompletedEventHandler = this.SendCompleted;
				SendCompletedEventHandler sendCompletedEventHandler2;
				do
				{
					sendCompletedEventHandler2 = sendCompletedEventHandler;
					SendCompletedEventHandler value2 = (SendCompletedEventHandler)Delegate.Remove(sendCompletedEventHandler2, value);
					sendCompletedEventHandler = Interlocked.CompareExchange<SendCompletedEventHandler>(ref this.SendCompleted, value2, sendCompletedEventHandler2);
				}
				while (sendCompletedEventHandler != sendCompletedEventHandler2);
			}
		}

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, and releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
		// Token: 0x060042CF RID: 17103 RVA: 0x000E8195 File Offset: 0x000E6395
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Sends a QUIT message to the SMTP server, gracefully ends the TCP connection, releases all resources used by the current instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class, and optionally disposes of the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to releases only unmanaged resources.</param>
		// Token: 0x060042D0 RID: 17104 RVA: 0x00003917 File Offset: 0x00001B17
		[MonoTODO("Does nothing at the moment.")]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000E819E File Offset: 0x000E639E
		private void CheckState()
		{
			if (this.messageInProcess != null)
			{
				throw new InvalidOperationException("Cannot set Timeout while Sending a message");
			}
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000E81B4 File Offset: 0x000E63B4
		private static string EncodeAddress(MailAddress address)
		{
			if (!string.IsNullOrEmpty(address.DisplayName))
			{
				string text = MailMessage.EncodeSubjectRFC2047(address.DisplayName, Encoding.UTF8);
				return string.Concat(new string[]
				{
					"\"",
					text,
					"\" <",
					address.Address,
					">"
				});
			}
			return address.ToString();
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000E8218 File Offset: 0x000E6418
		private static string EncodeAddresses(MailAddressCollection addresses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (MailAddress address in addresses)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SmtpClient.EncodeAddress(address));
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000E8288 File Offset: 0x000E6488
		private string EncodeSubjectRFC2047(MailMessage message)
		{
			return MailMessage.EncodeSubjectRFC2047(message.Subject, message.SubjectEncoding);
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000E829C File Offset: 0x000E649C
		private string EncodeBody(MailMessage message)
		{
			string body = message.Body;
			Encoding bodyEncoding = message.BodyEncoding;
			TransferEncoding contentTransferEncoding = message.ContentTransferEncoding;
			if (contentTransferEncoding == TransferEncoding.Base64)
			{
				return Convert.ToBase64String(bodyEncoding.GetBytes(body), Base64FormattingOptions.InsertLineBreaks);
			}
			if (contentTransferEncoding == TransferEncoding.SevenBit)
			{
				return body;
			}
			return this.ToQuotedPrintable(body, bodyEncoding);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000E82E0 File Offset: 0x000E64E0
		private string EncodeBody(AlternateView av)
		{
			byte[] array = new byte[av.ContentStream.Length];
			av.ContentStream.Read(array, 0, array.Length);
			TransferEncoding transferEncoding = av.TransferEncoding;
			if (transferEncoding == TransferEncoding.Base64)
			{
				return Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks);
			}
			if (transferEncoding == TransferEncoding.SevenBit)
			{
				return Encoding.ASCII.GetString(array);
			}
			return this.ToQuotedPrintable(array);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000E833A File Offset: 0x000E653A
		private void EndSection(string section)
		{
			this.SendData(string.Format("--{0}--", section));
			this.SendData(string.Empty);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000E8358 File Offset: 0x000E6558
		private string GenerateBoundary()
		{
			string result = SmtpClient.GenerateBoundary(this.boundaryIndex);
			this.boundaryIndex++;
			return result;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000E8374 File Offset: 0x000E6574
		private static string GenerateBoundary(int index)
		{
			return string.Format("--boundary_{0}_{1}", index, Guid.NewGuid().ToString("D"));
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000E83A3 File Offset: 0x000E65A3
		private bool IsError(SmtpClient.SmtpResponse status)
		{
			return status.StatusCode >= (SmtpStatusCode)400;
		}

		/// <summary>Raises the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains event data.</param>
		// Token: 0x060042DB RID: 17115 RVA: 0x000E83B8 File Offset: 0x000E65B8
		protected void OnSendCompleted(AsyncCompletedEventArgs e)
		{
			try
			{
				if (this.SendCompleted != null)
				{
					this.SendCompleted(this, e);
				}
			}
			finally
			{
				this.worker = null;
				this.user_async_state = null;
			}
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x000E83FC File Offset: 0x000E65FC
		private void CheckCancellation()
		{
			if (this.worker != null && this.worker.CancellationPending)
			{
				throw new SmtpClient.CancellationException();
			}
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x000E841C File Offset: 0x000E661C
		private SmtpClient.SmtpResponse Read()
		{
			byte[] array = new byte[512];
			int num = 0;
			bool flag = false;
			do
			{
				this.CheckCancellation();
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 <= 0)
				{
					break;
				}
				int num3 = num + num2 - 1;
				if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
				{
					int num4 = num3 - 3;
					while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
					{
						num4--;
					}
					flag = (array[num4 + 4] == 32);
				}
				num += num2;
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
			}
			while (!flag);
			if (num > 0)
			{
				return SmtpClient.SmtpResponse.Parse(new ASCIIEncoding().GetString(array, 0, num - 1));
			}
			throw new IOException("Connection closed");
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x000E84F0 File Offset: 0x000E66F0
		private void ResetExtensions()
		{
			this.authMechs = SmtpClient.AuthMechs.None;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000E84FC File Offset: 0x000E66FC
		private void ParseExtensions(string extens)
		{
			foreach (string text in extens.Split('\n', StringSplitOptions.None))
			{
				if (text.Length >= 4)
				{
					string text2 = text.Substring(4);
					if (text2.StartsWith("AUTH ", StringComparison.Ordinal))
					{
						string[] array2 = text2.Split(' ', StringSplitOptions.None);
						for (int j = 1; j < array2.Length; j++)
						{
							string a = array2[j].Trim();
							if (!(a == "LOGIN"))
							{
								if (a == "PLAIN")
								{
									this.authMechs |= SmtpClient.AuthMechs.Plain;
								}
							}
							else
							{
								this.authMechs |= SmtpClient.AuthMechs.Login;
							}
						}
					}
				}
			}
		}

		/// <summary>Sends the specified message to an SMTP server for delivery.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.MailMessage.From" /> is <see langword="null" />.  
		///  -or-  
		///  There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientException">The <paramref name="message" /> could not be delivered to one of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to two or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060042E0 RID: 17120 RVA: 0x000E85B0 File Offset: 0x000E67B0
		public void Send(MailMessage message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.Network && (this.Host == null || this.Host.Trim().Length == 0))
			{
				throw new InvalidOperationException("The SMTP host was not specified");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.PickupDirectoryFromIis)
			{
				throw new NotSupportedException("IIS delivery is not supported");
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
			this.mutex.WaitOne();
			try
			{
				this.messageInProcess = message;
				if (this.deliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
				{
					this.SendToFile(message);
				}
				else
				{
					this.SendInternal(message);
				}
			}
			catch (SmtpClient.CancellationException)
			{
			}
			catch (SmtpException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new SmtpException("Message could not be sent.", innerException);
			}
			finally
			{
				this.mutex.ReleaseMutex();
				this.messageInProcess = null;
			}
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000E86A4 File Offset: 0x000E68A4
		private void SendInternal(MailMessage message)
		{
			this.CheckCancellation();
			try
			{
				this.client = new TcpClient(this.host, this.port);
				this.stream = this.client.GetStream();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				this.SendCore(message);
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				if (this.reader != null)
				{
					this.reader.Close();
				}
				if (this.stream != null)
				{
					this.stream.Close();
				}
				if (this.client != null)
				{
					this.client.Close();
				}
			}
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x000E8768 File Offset: 0x000E6968
		private void SendToFile(MailMessage message)
		{
			if (!Path.IsPathRooted(this.pickupDirectoryLocation))
			{
				throw new SmtpException("Only absolute directories are allowed for pickup directory.");
			}
			string path = Path.Combine(this.pickupDirectoryLocation, Guid.NewGuid().ToString() + ".eml");
			try
			{
				this.writer = new StreamWriter(path);
				MailAddress from = message.From;
				if (from == null)
				{
					from = this.defaultFrom;
				}
				string text = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
				text = text.Remove(text.Length - 3, 1);
				this.SendHeader("Date", text);
				this.SendHeader("From", SmtpClient.EncodeAddress(from));
				this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
				if (message.CC.Count > 0)
				{
					this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
				}
				this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
				foreach (string name in message.Headers.AllKeys)
				{
					this.SendHeader(name, message.Headers[name]);
				}
				this.AddPriorityHeader(message);
				this.boundaryIndex = 0;
				if (message.Attachments.Count > 0)
				{
					this.SendWithAttachments(message);
				}
				else
				{
					this.SendWithoutAttachments(message, null, false);
				}
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				this.writer = null;
			}
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x000E8908 File Offset: 0x000E6B08
		private void SendCore(MailMessage message)
		{
			SmtpClient.SmtpResponse smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string hostName = Dns.GetHostName();
			try
			{
				hostName = Dns.GetHostEntry(hostName).HostName;
			}
			catch (SocketException)
			{
			}
			smtpResponse = this.SendCommand("EHLO " + hostName);
			if (this.IsError(smtpResponse))
			{
				smtpResponse = this.SendCommand("HELO " + hostName);
				if (this.IsError(smtpResponse))
				{
					throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
				}
			}
			else
			{
				string description = smtpResponse.Description;
				if (description != null)
				{
					this.ParseExtensions(description);
				}
			}
			if (this.enableSsl)
			{
				this.InitiateSecureConnection();
				this.ResetExtensions();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				smtpResponse = this.SendCommand("EHLO " + hostName);
				if (this.IsError(smtpResponse))
				{
					smtpResponse = this.SendCommand("HELO " + hostName);
					if (this.IsError(smtpResponse))
					{
						throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
					}
				}
				else
				{
					string description2 = smtpResponse.Description;
					if (description2 != null)
					{
						this.ParseExtensions(description2);
					}
				}
			}
			if (this.authMechs != SmtpClient.AuthMechs.None)
			{
				this.Authenticate();
			}
			MailAddress mailAddress = message.Sender;
			if (mailAddress == null)
			{
				mailAddress = message.From;
			}
			if (mailAddress == null)
			{
				mailAddress = this.defaultFrom;
			}
			smtpResponse = this.SendCommand("MAIL FROM:<" + mailAddress.Address + ">");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			List<SmtpFailedRecipientException> list = new List<SmtpFailedRecipientException>();
			for (int i = 0; i < message.To.Count; i++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.To[i].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.To[i].Address));
				}
			}
			for (int j = 0; j < message.CC.Count; j++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.CC[j].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.CC[j].Address));
				}
			}
			for (int k = 0; k < message.Bcc.Count; k++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.Bcc[k].Address + ">");
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.Bcc[k].Address));
				}
			}
			if (list.Count > 0)
			{
				throw new SmtpFailedRecipientsException("failed recipients", list.ToArray());
			}
			smtpResponse = this.SendCommand("DATA");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string text = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
			text = text.Remove(text.Length - 3, 1);
			this.SendHeader("Date", text);
			MailAddress from = message.From;
			if (from == null)
			{
				from = this.defaultFrom;
			}
			this.SendHeader("From", SmtpClient.EncodeAddress(from));
			this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
			if (message.CC.Count > 0)
			{
				this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
			}
			this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
			string value = "normal";
			switch (message.Priority)
			{
			case MailPriority.Normal:
				value = "normal";
				break;
			case MailPriority.Low:
				value = "non-urgent";
				break;
			case MailPriority.High:
				value = "urgent";
				break;
			}
			this.SendHeader("Priority", value);
			if (message.Sender != null)
			{
				this.SendHeader("Sender", SmtpClient.EncodeAddress(message.Sender));
			}
			if (message.ReplyToList.Count > 0)
			{
				this.SendHeader("Reply-To", SmtpClient.EncodeAddresses(message.ReplyToList));
			}
			foreach (string name in message.Headers.AllKeys)
			{
				this.SendHeader(name, MailMessage.EncodeSubjectRFC2047(message.Headers[name], message.HeadersEncoding));
			}
			this.AddPriorityHeader(message);
			this.boundaryIndex = 0;
			if (message.Attachments.Count > 0)
			{
				this.SendWithAttachments(message);
			}
			else
			{
				this.SendWithoutAttachments(message, null, false);
			}
			this.SendDot();
			smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			try
			{
				smtpResponse = this.SendCommand("QUIT");
			}
			catch (IOException)
			{
			}
		}

		/// <summary>Sends the specified email message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientException">The <paramref name="message" /> could not be delivered to one of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to two or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060042E4 RID: 17124 RVA: 0x000E8E34 File Offset: 0x000E7034
		public void Send(string from, string recipients, string subject, string body)
		{
			this.Send(new MailMessage(from, recipients, subject, body));
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is <see langword="null" />.</exception>
		// Token: 0x060042E5 RID: 17125 RVA: 0x000E8E48 File Offset: 0x000E7048
		public Task SendMailAsync(MailMessage message)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			SendCompletedEventHandler handler = null;
			handler = delegate(object s, AsyncCompletedEventArgs e)
			{
				SmtpClient.SendMailAsyncCompletedHandler(tcs, e, handler, this);
			};
			this.SendCompleted += handler;
			this.SendAsync(message, tcs);
			return tcs.Task;
		}

		/// <summary>Sends the specified message to an SMTP server for delivery as an asynchronous operation. . The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="recipients" /> is <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x060042E6 RID: 17126 RVA: 0x000E8EAA File Offset: 0x000E70AA
		public Task SendMailAsync(string from, string recipients, string subject, string body)
		{
			return this.SendMailAsync(new MailMessage(from, recipients, subject, body));
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x000E8EBC File Offset: 0x000E70BC
		private static void SendMailAsyncCompletedHandler(TaskCompletionSource<object> source, AsyncCompletedEventArgs e, SendCompletedEventHandler handler, SmtpClient client)
		{
			if (source != e.UserState)
			{
				return;
			}
			client.SendCompleted -= handler;
			if (e.Error != null)
			{
				source.SetException(e.Error);
				return;
			}
			if (e.Cancelled)
			{
				source.SetCanceled();
				return;
			}
			source.SetResult(null);
		}

		// Token: 0x060042E8 RID: 17128 RVA: 0x000E8EFA File Offset: 0x000E70FA
		private void SendDot()
		{
			this.writer.Write(".\r\n");
			this.writer.Flush();
		}

		// Token: 0x060042E9 RID: 17129 RVA: 0x000E8F18 File Offset: 0x000E7118
		private void SendData(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				this.writer.Write("\r\n");
				this.writer.Flush();
				return;
			}
			StringReader stringReader = new StringReader(data);
			bool flag = this.deliveryMethod == SmtpDeliveryMethod.Network;
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				this.CheckCancellation();
				if (flag && text.Length > 0 && text[0] == '.')
				{
					text = "." + text;
				}
				this.writer.Write(text);
				this.writer.Write("\r\n");
			}
			this.writer.Flush();
		}

		/// <summary>Sends the specified email message to an SMTP server for delivery. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="message" /> is <see langword="null" />.  
		/// -or-  
		/// <see cref="P:System.Net.Mail.MailMessage.From" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  There are no recipients specified in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" /> properties.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.  
		///  -or-  
		///  The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x060042EA RID: 17130 RVA: 0x000E8FB8 File Offset: 0x000E71B8
		public void SendAsync(MailMessage message, object userToken)
		{
			if (this.worker != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.worker = new BackgroundWorker();
			this.worker.DoWork += delegate(object o, DoWorkEventArgs ea)
			{
				try
				{
					this.user_async_state = ea.Argument;
					this.Send(message);
				}
				catch (Exception ex)
				{
					ea.Result = ex;
					throw ex;
				}
			};
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerCompleted += delegate(object o, RunWorkerCompletedEventArgs ea)
			{
				this.OnSendCompleted(new AsyncCompletedEventArgs(ea.Error, ea.Cancelled, this.user_async_state));
			};
			this.worker.RunWorkerAsync(userToken);
		}

		/// <summary>Sends an email message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the address that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="recipient" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.  
		/// -or-  
		/// <paramref name="recipient" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is <see langword="null" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" /> and <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero, a negative number, or greater than 65,535.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.  
		///  -or-  
		///  Authentication failed.  
		///  -or-  
		///  The operation timed out.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true" /> but the <see cref="P:System.Net.Mail.SmtpClient.DeliveryMethod" /> property is set to <see cref="F:System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory" /> or <see cref="F:System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis" />.  
		///  -or-  
		///  <see cref="P:System.Net.Mail.SmtpClient.EnableSsl" /> is set to <see langword="true," /> but the SMTP mail server did not advertise STARTTLS in the response to the EHLO command.  
		///  -or-  
		///  The message could not be delivered to one or more of the recipients in <paramref name="recipients" />.</exception>
		// Token: 0x060042EB RID: 17131 RVA: 0x000E903D File Offset: 0x000E723D
		public void SendAsync(string from, string recipients, string subject, string body, object userToken)
		{
			this.SendAsync(new MailMessage(from, recipients, subject, body), userToken);
		}

		/// <summary>Cancels an asynchronous operation to send an email message.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x060042EC RID: 17132 RVA: 0x000E9051 File Offset: 0x000E7251
		public void SendAsyncCancel()
		{
			if (this.worker == null)
			{
				throw new InvalidOperationException("SendAsync operation is not in progress");
			}
			this.worker.CancelAsync();
		}

		// Token: 0x060042ED RID: 17133 RVA: 0x000E9074 File Offset: 0x000E7274
		private void AddPriorityHeader(MailMessage message)
		{
			MailPriority priority = message.Priority;
			if (priority != MailPriority.Low)
			{
				if (priority == MailPriority.High)
				{
					this.SendHeader("Priority", "Urgent");
					this.SendHeader("Importance", "high");
					this.SendHeader("X-Priority", "1");
					return;
				}
			}
			else
			{
				this.SendHeader("Priority", "Non-Urgent");
				this.SendHeader("Importance", "low");
				this.SendHeader("X-Priority", "5");
			}
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x000E90F4 File Offset: 0x000E72F4
		private void SendSimpleBody(MailMessage message)
		{
			this.SendHeader("Content-Type", message.BodyContentType.ToString());
			if (message.ContentTransferEncoding != TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(message.ContentTransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(message));
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x000E9150 File Offset: 0x000E7350
		private void SendBodylessSingleAlternate(AlternateView av)
		{
			this.SendHeader("Content-Type", av.ContentType.ToString());
			if (av.TransferEncoding != TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(av.TransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(av));
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x000E91AC File Offset: 0x000E73AC
		private void SendWithoutAttachments(MailMessage message, string boundary, bool attachmentExists)
		{
			if (message.Body == null && message.AlternateViews.Count == 1)
			{
				this.SendBodylessSingleAlternate(message.AlternateViews[0]);
				return;
			}
			if (message.AlternateViews.Count > 0)
			{
				this.SendBodyWithAlternateViews(message, boundary, attachmentExists);
				return;
			}
			this.SendSimpleBody(message);
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x000E9204 File Offset: 0x000E7404
		private void SendWithAttachments(MailMessage message)
		{
			string text = this.GenerateBoundary();
			this.SendHeader("Content-Type", new ContentType
			{
				Boundary = text,
				MediaType = "multipart/mixed",
				CharSet = null
			}.ToString());
			this.SendData(string.Empty);
			Attachment attachment = null;
			if (message.AlternateViews.Count > 0)
			{
				this.SendWithoutAttachments(message, text, true);
			}
			else
			{
				attachment = Attachment.CreateAttachmentFromString(message.Body, null, message.BodyEncoding, message.IsBodyHtml ? "text/html" : "text/plain");
				message.Attachments.Insert(0, attachment);
			}
			try
			{
				this.SendAttachments(message, attachment, text);
			}
			finally
			{
				if (attachment != null)
				{
					message.Attachments.Remove(attachment);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x000E92D8 File Offset: 0x000E74D8
		private void SendBodyWithAlternateViews(MailMessage message, string boundary, bool attachmentExists)
		{
			AlternateViewCollection alternateViews = message.AlternateViews;
			string text = this.GenerateBoundary();
			ContentType contentType = new ContentType();
			contentType.Boundary = text;
			contentType.MediaType = "multipart/alternative";
			if (!attachmentExists)
			{
				this.SendHeader("Content-Type", contentType.ToString());
				this.SendData(string.Empty);
			}
			AlternateView alternateView = null;
			if (message.Body != null)
			{
				alternateView = AlternateView.CreateAlternateViewFromString(message.Body, message.BodyEncoding, message.IsBodyHtml ? "text/html" : "text/plain");
				alternateViews.Insert(0, alternateView);
				this.StartSection(boundary, contentType);
			}
			try
			{
				foreach (AlternateView alternateView2 in alternateViews)
				{
					string text2 = null;
					if (alternateView2.LinkedResources.Count > 0)
					{
						text2 = this.GenerateBoundary();
						ContentType contentType2 = new ContentType("multipart/related");
						contentType2.Boundary = text2;
						contentType2.Parameters["type"] = alternateView2.ContentType.ToString();
						this.StartSection(text, contentType2);
						this.StartSection(text2, alternateView2.ContentType, alternateView2);
					}
					else
					{
						ContentType contentType2 = new ContentType(alternateView2.ContentType.ToString());
						this.StartSection(text, contentType2, alternateView2);
					}
					switch (alternateView2.TransferEncoding)
					{
					case TransferEncoding.Unknown:
					case TransferEncoding.SevenBit:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Encoding.ASCII.GetString(array));
						break;
					}
					case TransferEncoding.QuotedPrintable:
					{
						byte[] array2 = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array2, 0, array2.Length);
						this.SendData(this.ToQuotedPrintable(array2));
						break;
					}
					case TransferEncoding.Base64:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
						break;
					}
					}
					if (alternateView2.LinkedResources.Count > 0)
					{
						this.SendLinkedResources(message, alternateView2.LinkedResources, text2);
						this.EndSection(text2);
					}
					if (!attachmentExists)
					{
						this.SendData(string.Empty);
					}
				}
			}
			finally
			{
				if (alternateView != null)
				{
					alternateViews.Remove(alternateView);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x000E9570 File Offset: 0x000E7770
		private void SendLinkedResources(MailMessage message, LinkedResourceCollection resources, string boundary)
		{
			foreach (LinkedResource linkedResource in resources)
			{
				this.StartSection(boundary, linkedResource.ContentType, linkedResource);
				switch (linkedResource.TransferEncoding)
				{
				case TransferEncoding.Unknown:
				case TransferEncoding.SevenBit:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				}
				case TransferEncoding.QuotedPrintable:
				{
					byte[] array2 = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array2, 0, array2.Length);
					this.SendData(this.ToQuotedPrintable(array2));
					break;
				}
				case TransferEncoding.Base64:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				}
			}
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x000E9684 File Offset: 0x000E7884
		private void SendAttachments(MailMessage message, Attachment body, string boundary)
		{
			foreach (Attachment attachment in message.Attachments)
			{
				ContentType contentType = new ContentType(attachment.ContentType.ToString());
				if (attachment.Name != null)
				{
					contentType.Name = attachment.Name;
					if (attachment.NameEncoding != null)
					{
						contentType.CharSet = attachment.NameEncoding.HeaderName;
					}
					attachment.ContentDisposition.FileName = attachment.Name;
				}
				this.StartSection(boundary, contentType, attachment, attachment != body);
				byte[] array = new byte[attachment.ContentStream.Length];
				attachment.ContentStream.Read(array, 0, array.Length);
				switch (attachment.TransferEncoding)
				{
				case TransferEncoding.Unknown:
				case TransferEncoding.SevenBit:
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				case TransferEncoding.QuotedPrintable:
					this.SendData(this.ToQuotedPrintable(array));
					break;
				case TransferEncoding.Base64:
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				this.SendData(string.Empty);
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x000E97B0 File Offset: 0x000E79B0
		private SmtpClient.SmtpResponse SendCommand(string command)
		{
			this.writer.Write(command);
			this.writer.Write("\r\n");
			this.writer.Flush();
			return this.Read();
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x000E97DF File Offset: 0x000E79DF
		private void SendHeader(string name, string value)
		{
			this.SendData(string.Format("{0}: {1}", name, value));
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x000E97F3 File Offset: 0x000E79F3
		private void StartSection(string section, ContentType sectionContentType)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendData(string.Empty);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x000E9824 File Offset: 0x000E7A24
		private void StartSection(string section, ContentType sectionContentType, AttachmentBase att)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(att.TransferEncoding));
			if (!string.IsNullOrEmpty(att.ContentId))
			{
				this.SendHeader("content-ID", "<" + att.ContentId + ">");
			}
			this.SendData(string.Empty);
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x000E98A4 File Offset: 0x000E7AA4
		private void StartSection(string section, ContentType sectionContentType, Attachment att, bool sendDisposition)
		{
			this.SendData(string.Format("--{0}", section));
			if (!string.IsNullOrEmpty(att.ContentId))
			{
				this.SendHeader("content-ID", "<" + att.ContentId + ">");
			}
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(att.TransferEncoding));
			if (sendDisposition)
			{
				this.SendHeader("content-disposition", att.ContentDisposition.ToString());
			}
			this.SendData(string.Empty);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000E993C File Offset: 0x000E7B3C
		private string ToQuotedPrintable(string input, Encoding enc)
		{
			byte[] bytes = enc.GetBytes(input);
			return this.ToQuotedPrintable(bytes);
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000E9958 File Offset: 0x000E7B58
		private string ToQuotedPrintable(byte[] bytes)
		{
			StringWriter stringWriter = new StringWriter();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("=", 3);
			byte b = 61;
			char c = '\0';
			int i = 0;
			while (i < bytes.Length)
			{
				byte b2 = bytes[i];
				int num2;
				if (b2 > 127 || b2 == b)
				{
					stringBuilder.Length = 1;
					stringBuilder.Append(Convert.ToString(b2, 16).ToUpperInvariant());
					num2 = 3;
					goto IL_7C;
				}
				c = Convert.ToChar(b2);
				if (c != '\r' && c != '\n')
				{
					num2 = 1;
					goto IL_7C;
				}
				stringWriter.Write(c);
				num = 0;
				IL_AC:
				i++;
				continue;
				IL_7C:
				num += num2;
				if (num > 75)
				{
					stringWriter.Write("=\r\n");
					num = num2;
				}
				if (num2 == 1)
				{
					stringWriter.Write(c);
					goto IL_AC;
				}
				stringWriter.Write(stringBuilder.ToString());
				goto IL_AC;
			}
			return stringWriter.ToString();
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000E9A28 File Offset: 0x000E7C28
		private static string GetTransferEncodingName(TransferEncoding encoding)
		{
			switch (encoding)
			{
			case TransferEncoding.QuotedPrintable:
				return "quoted-printable";
			case TransferEncoding.Base64:
				return "base64";
			case TransferEncoding.SevenBit:
				return "7bit";
			default:
				return "unknown";
			}
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000E9A58 File Offset: 0x000E7C58
		private void InitiateSecureConnection()
		{
			SmtpClient.SmtpResponse status = this.SendCommand("STARTTLS");
			if (this.IsError(status))
			{
				throw new SmtpException(SmtpStatusCode.GeneralFailure, "Server does not support secure connections.");
			}
			MobileTlsProvider providerInternal = Mono.Net.Security.MonoTlsProviderFactory.GetProviderInternal();
			MonoTlsSettings monoTlsSettings = MonoTlsSettings.CopyDefaultSettings();
			monoTlsSettings.UseServicePointManagerCallback = new bool?(true);
			SslStream sslStream = new SslStream(this.stream, false, providerInternal, monoTlsSettings);
			this.CheckCancellation();
			sslStream.AuthenticateAsClient(this.Host, this.ClientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, false);
			this.stream = sslStream;
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000E9AD4 File Offset: 0x000E7CD4
		private void Authenticate()
		{
			string userName;
			string password;
			if (this.UseDefaultCredentials)
			{
				userName = CredentialCache.DefaultCredentials.GetCredential(new Uri("smtp://" + this.host), "basic").UserName;
				password = CredentialCache.DefaultCredentials.GetCredential(new Uri("smtp://" + this.host), "basic").Password;
			}
			else
			{
				if (this.Credentials == null)
				{
					return;
				}
				userName = this.Credentials.GetCredential(this.host, this.port, "smtp").UserName;
				password = this.Credentials.GetCredential(this.host, this.port, "smtp").Password;
			}
			this.Authenticate(userName, password);
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000E9B9A File Offset: 0x000E7D9A
		private void CheckStatus(SmtpClient.SmtpResponse status, int i)
		{
			if (status.StatusCode != (SmtpStatusCode)i)
			{
				throw new SmtpException(status.StatusCode, status.Description);
			}
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000E9BB7 File Offset: 0x000E7DB7
		private void ThrowIfError(SmtpClient.SmtpResponse status)
		{
			if (this.IsError(status))
			{
				throw new SmtpException(status.StatusCode, status.Description);
			}
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000E9BD4 File Offset: 0x000E7DD4
		private void Authenticate(string user, string password)
		{
			if (this.authMechs == SmtpClient.AuthMechs.None)
			{
				return;
			}
			if ((this.authMechs & SmtpClient.AuthMechs.Login) != SmtpClient.AuthMechs.None)
			{
				SmtpClient.SmtpResponse status = this.SendCommand("AUTH LOGIN");
				this.CheckStatus(status, 334);
				status = this.SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(user)));
				this.CheckStatus(status, 334);
				status = this.SendCommand(Convert.ToBase64String(Encoding.UTF8.GetBytes(password)));
				this.CheckStatus(status, 235);
				return;
			}
			if ((this.authMechs & SmtpClient.AuthMechs.Plain) != SmtpClient.AuthMechs.None)
			{
				string text = string.Format("\0{0}\0{1}", user, password);
				text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
				SmtpClient.SmtpResponse status = this.SendCommand("AUTH PLAIN " + text);
				this.CheckStatus(status, 235);
				return;
			}
			throw new SmtpException("AUTH types PLAIN, LOGIN not supported by the server");
		}

		// Token: 0x0400286D RID: 10349
		private string host;

		// Token: 0x0400286E RID: 10350
		private int port;

		// Token: 0x0400286F RID: 10351
		private int timeout = 100000;

		// Token: 0x04002870 RID: 10352
		private ICredentialsByHost credentials;

		// Token: 0x04002871 RID: 10353
		private string pickupDirectoryLocation;

		// Token: 0x04002872 RID: 10354
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x04002873 RID: 10355
		private SmtpDeliveryFormat deliveryFormat;

		// Token: 0x04002874 RID: 10356
		private bool enableSsl;

		// Token: 0x04002875 RID: 10357
		private X509CertificateCollection clientCertificates;

		// Token: 0x04002876 RID: 10358
		private TcpClient client;

		// Token: 0x04002877 RID: 10359
		private Stream stream;

		// Token: 0x04002878 RID: 10360
		private StreamWriter writer;

		// Token: 0x04002879 RID: 10361
		private StreamReader reader;

		// Token: 0x0400287A RID: 10362
		private int boundaryIndex;

		// Token: 0x0400287B RID: 10363
		private MailAddress defaultFrom;

		// Token: 0x0400287C RID: 10364
		private MailMessage messageInProcess;

		// Token: 0x0400287D RID: 10365
		private BackgroundWorker worker;

		// Token: 0x0400287E RID: 10366
		private object user_async_state;

		// Token: 0x0400287F RID: 10367
		private SmtpClient.AuthMechs authMechs;

		// Token: 0x04002880 RID: 10368
		private Mutex mutex = new Mutex();

		// Token: 0x04002881 RID: 10369
		[CompilerGenerated]
		private string <TargetName>k__BackingField;

		// Token: 0x04002882 RID: 10370
		[CompilerGenerated]
		private SendCompletedEventHandler SendCompleted;

		// Token: 0x02000830 RID: 2096
		[Flags]
		private enum AuthMechs
		{
			// Token: 0x04002884 RID: 10372
			None = 0,
			// Token: 0x04002885 RID: 10373
			Login = 1,
			// Token: 0x04002886 RID: 10374
			Plain = 2
		}

		// Token: 0x02000831 RID: 2097
		private class CancellationException : Exception
		{
			// Token: 0x06004302 RID: 17154 RVA: 0x0000DC12 File Offset: 0x0000BE12
			public CancellationException()
			{
			}
		}

		// Token: 0x02000832 RID: 2098
		private struct HeaderName
		{
			// Token: 0x04002887 RID: 10375
			public const string ContentTransferEncoding = "Content-Transfer-Encoding";

			// Token: 0x04002888 RID: 10376
			public const string ContentType = "Content-Type";

			// Token: 0x04002889 RID: 10377
			public const string Bcc = "Bcc";

			// Token: 0x0400288A RID: 10378
			public const string Cc = "Cc";

			// Token: 0x0400288B RID: 10379
			public const string From = "From";

			// Token: 0x0400288C RID: 10380
			public const string Subject = "Subject";

			// Token: 0x0400288D RID: 10381
			public const string To = "To";

			// Token: 0x0400288E RID: 10382
			public const string MimeVersion = "MIME-Version";

			// Token: 0x0400288F RID: 10383
			public const string MessageId = "Message-ID";

			// Token: 0x04002890 RID: 10384
			public const string Priority = "Priority";

			// Token: 0x04002891 RID: 10385
			public const string Importance = "Importance";

			// Token: 0x04002892 RID: 10386
			public const string XPriority = "X-Priority";

			// Token: 0x04002893 RID: 10387
			public const string Date = "Date";
		}

		// Token: 0x02000833 RID: 2099
		private struct SmtpResponse
		{
			// Token: 0x06004303 RID: 17155 RVA: 0x000E9CA4 File Offset: 0x000E7EA4
			public static SmtpClient.SmtpResponse Parse(string line)
			{
				SmtpClient.SmtpResponse result = default(SmtpClient.SmtpResponse);
				if (line.Length < 4)
				{
					throw new SmtpException("Response is to short " + line.Length.ToString() + ".");
				}
				if (line[3] != ' ' && line[3] != '-')
				{
					throw new SmtpException("Response format is wrong.(" + line + ")");
				}
				result.StatusCode = (SmtpStatusCode)int.Parse(line.Substring(0, 3));
				result.Description = line;
				return result;
			}

			// Token: 0x04002894 RID: 10388
			public SmtpStatusCode StatusCode;

			// Token: 0x04002895 RID: 10389
			public string Description;
		}

		// Token: 0x02000834 RID: 2100
		[CompilerGenerated]
		private sealed class <>c__DisplayClass85_0
		{
			// Token: 0x06004304 RID: 17156 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass85_0()
			{
			}

			// Token: 0x06004305 RID: 17157 RVA: 0x000E9D2E File Offset: 0x000E7F2E
			internal void <SendMailAsync>b__0(object s, AsyncCompletedEventArgs e)
			{
				SmtpClient.SendMailAsyncCompletedHandler(this.tcs, e, this.handler, this.<>4__this);
			}

			// Token: 0x04002896 RID: 10390
			public TaskCompletionSource<object> tcs;

			// Token: 0x04002897 RID: 10391
			public SendCompletedEventHandler handler;

			// Token: 0x04002898 RID: 10392
			public SmtpClient <>4__this;
		}

		// Token: 0x02000835 RID: 2101
		[CompilerGenerated]
		private sealed class <>c__DisplayClass90_0
		{
			// Token: 0x06004306 RID: 17158 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c__DisplayClass90_0()
			{
			}

			// Token: 0x06004307 RID: 17159 RVA: 0x000E9D48 File Offset: 0x000E7F48
			internal void <SendAsync>b__0(object o, DoWorkEventArgs ea)
			{
				try
				{
					this.<>4__this.user_async_state = ea.Argument;
					this.<>4__this.Send(this.message);
				}
				catch (Exception ex)
				{
					ea.Result = ex;
					throw ex;
				}
			}

			// Token: 0x06004308 RID: 17160 RVA: 0x000E9D94 File Offset: 0x000E7F94
			internal void <SendAsync>b__1(object o, RunWorkerCompletedEventArgs ea)
			{
				this.<>4__this.OnSendCompleted(new AsyncCompletedEventArgs(ea.Error, ea.Cancelled, this.<>4__this.user_async_state));
			}

			// Token: 0x04002899 RID: 10393
			public SmtpClient <>4__this;

			// Token: 0x0400289A RID: 10394
			public MailMessage message;
		}
	}
}
