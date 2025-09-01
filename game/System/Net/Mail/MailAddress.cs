using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the address of an electronic mail sender or recipient.</summary>
	// Token: 0x0200081D RID: 2077
	public class MailAddress
	{
		// Token: 0x06004216 RID: 16918 RVA: 0x000E46C8 File Offset: 0x000E28C8
		internal MailAddress(string displayName, string userName, string domain)
		{
			this._host = domain;
			this._userName = userName;
			this._displayName = displayName;
			this._displayNameEncoding = Encoding.GetEncoding("utf-8");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an email address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.</exception>
		// Token: 0x06004217 RID: 16919 RVA: 0x000E46F5 File Offset: 0x000E28F5
		public MailAddress(string address) : this(address, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address and display name.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an email address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />. This parameter can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.  
		/// -or-  
		/// <paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06004218 RID: 16920 RVA: 0x000E4700 File Offset: 0x000E2900
		public MailAddress(string address, string displayName) : this(address, displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address, display name, and encoding.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an email address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />.</param>
		/// <param name="displayNameEncoding">The <see cref="T:System.Text.Encoding" /> that defines the character set used for <paramref name="displayName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="displayName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").  
		/// -or-  
		/// <paramref name="displayName" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.  
		/// -or-  
		/// <paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06004219 RID: 16921 RVA: 0x000E470C File Offset: 0x000E290C
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "address"), "address");
			}
			this._displayNameEncoding = (displayNameEncoding ?? Encoding.GetEncoding("utf-8"));
			this._displayName = (displayName ?? string.Empty);
			if (!string.IsNullOrEmpty(this._displayName))
			{
				this._displayName = MailAddressParser.NormalizeOrThrow(this._displayName);
				if (this._displayName.Length >= 2 && this._displayName[0] == '"' && this._displayName[this._displayName.Length - 1] == '"')
				{
					this._displayName = this._displayName.Substring(1, this._displayName.Length - 2);
				}
			}
			MailAddress mailAddress = MailAddressParser.ParseAddress(address);
			this._host = mailAddress._host;
			this._userName = mailAddress._userName;
			if (string.IsNullOrEmpty(this._displayName))
			{
				this._displayName = mailAddress._displayName;
			}
		}

		/// <summary>Gets the display name composed from the display name and address information specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display name; otherwise, <see cref="F:System.String.Empty" /> ("") if no display name information was specified when this instance was created.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x000E4828 File Offset: 0x000E2A28
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		/// <summary>Gets the user information from the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user name portion of the <see cref="P:System.Net.Mail.MailAddress.Address" />.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x000E4830 File Offset: 0x000E2A30
		public string User
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000E4838 File Offset: 0x000E2A38
		private string GetUser(bool allowUnicode)
		{
			if (!allowUnicode && !MimeBasePart.IsAscii(this._userName, true))
			{
				throw new SmtpException(SR.Format("The client or server is only configured for E-mail addresses with ASCII local-parts: {0}.", this.Address));
			}
			return this._userName;
		}

		/// <summary>Gets the host portion of the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the host computer that accepts email for the <see cref="P:System.Net.Mail.MailAddress.User" /> property.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x000E4867 File Offset: 0x000E2A67
		public string Host
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x000E4870 File Offset: 0x000E2A70
		private string GetHost(bool allowUnicode)
		{
			string text = this._host;
			if (!allowUnicode && !MimeBasePart.IsAscii(text, true))
			{
				IdnMapping idnMapping = new IdnMapping();
				try
				{
					text = idnMapping.GetAscii(text);
				}
				catch (ArgumentException innerException)
				{
					throw new SmtpException(SR.Format("The address has an invalid host name: {0}.", this.Address), innerException);
				}
			}
			return text;
		}

		/// <summary>Gets the email address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the email address.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x000E48CC File Offset: 0x000E2ACC
		public string Address
		{
			get
			{
				return this._userName + "@" + this._host;
			}
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x000E48E4 File Offset: 0x000E2AE4
		private string GetAddress(bool allowUnicode)
		{
			return this.GetUser(allowUnicode) + "@" + this.GetHost(allowUnicode);
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x000E48FE File Offset: 0x000E2AFE
		private string SmtpAddress
		{
			get
			{
				return "<" + this.Address + ">";
			}
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x000E4915 File Offset: 0x000E2B15
		internal string GetSmtpAddress(bool allowUnicode)
		{
			return "<" + this.GetAddress(allowUnicode) + ">";
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the contents of this <see cref="T:System.Net.Mail.MailAddress" />.</returns>
		// Token: 0x06004223 RID: 16931 RVA: 0x000E492D File Offset: 0x000E2B2D
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				return this.Address;
			}
			return "\"" + this.DisplayName + "\" " + this.SmtpAddress;
		}

		/// <summary>Compares two mail addresses.</summary>
		/// <param name="value">A <see cref="T:System.Net.Mail.MailAddress" /> instance to compare to the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if the two mail addresses are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004224 RID: 16932 RVA: 0x000E495E File Offset: 0x000E2B5E
		public override bool Equals(object value)
		{
			return value != null && this.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>Returns a hash value for a mail address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06004225 RID: 16933 RVA: 0x000B73A2 File Offset: 0x000B55A2
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x000E4978 File Offset: 0x000E2B78
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this._displayName))
			{
				if (MimeBasePart.IsAscii(this._displayName, false) || allowUnicode)
				{
					text = "\"" + this._displayName + "\"";
				}
				else
				{
					IEncodableStream encoderForHeader = MailAddress.s_encoderFactory.GetEncoderForHeader(this._displayNameEncoding, false, charsConsumed);
					byte[] bytes = this._displayNameEncoding.GetBytes(this._displayName);
					encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
					text = encoderForHeader.GetEncodedString();
				}
				text = text + " " + this.GetSmtpAddress(allowUnicode);
			}
			else
			{
				text = this.GetAddress(allowUnicode);
			}
			return text;
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000E4A14 File Offset: 0x000E2C14
		// Note: this type is marked as 'beforefieldinit'.
		static MailAddress()
		{
		}

		// Token: 0x04002812 RID: 10258
		private readonly Encoding _displayNameEncoding;

		// Token: 0x04002813 RID: 10259
		private readonly string _displayName;

		// Token: 0x04002814 RID: 10260
		private readonly string _userName;

		// Token: 0x04002815 RID: 10261
		private readonly string _host;

		// Token: 0x04002816 RID: 10262
		private static readonly EncodedStreamFactory s_encoderFactory = new EncodedStreamFactory();
	}
}
