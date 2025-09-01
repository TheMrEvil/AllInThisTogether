using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the value of the Content-Disposition header.</summary>
	// Token: 0x02000039 RID: 57
	public class ContentDispositionHeaderValue : ICloneable
	{
		// Token: 0x060001DA RID: 474 RVA: 0x000022B8 File Offset: 0x000004B8
		private ContentDispositionHeaderValue()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> class.</summary>
		/// <param name="dispositionType">A string that contains a <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.</param>
		// Token: 0x060001DB RID: 475 RVA: 0x00007C20 File Offset: 0x00005E20
		public ContentDispositionHeaderValue(string dispositionType)
		{
			this.DispositionType = dispositionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> class.</summary>
		/// <param name="source">A <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" />.</param>
		// Token: 0x060001DC RID: 476 RVA: 0x00007C30 File Offset: 0x00005E30
		protected ContentDispositionHeaderValue(ContentDispositionHeaderValue source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.dispositionType = source.dispositionType;
			if (source.parameters != null)
			{
				foreach (NameValueHeaderValue source2 in source.parameters)
				{
					this.Parameters.Add(new NameValueHeaderValue(source2));
				}
			}
		}

		/// <summary>The date at which   the file was created.</summary>
		/// <returns>The file creation date.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007CB8 File Offset: 0x00005EB8
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00007CC5 File Offset: 0x00005EC5
		public DateTimeOffset? CreationDate
		{
			get
			{
				return this.GetDateValue("creation-date");
			}
			set
			{
				this.SetDateValue("creation-date", value);
			}
		}

		/// <summary>The disposition type for a content body part.</summary>
		/// <returns>The disposition type.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007CD3 File Offset: 0x00005ED3
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00007CDB File Offset: 0x00005EDB
		public string DispositionType
		{
			get
			{
				return this.dispositionType;
			}
			set
			{
				Parser.Token.Check(value);
				this.dispositionType = value;
			}
		}

		/// <summary>A suggestion for how to construct a filename for   storing the message payload to be used if the entity is   detached and stored in a separate file.</summary>
		/// <returns>A suggested filename.</returns>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00007CEC File Offset: 0x00005EEC
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00007D11 File Offset: 0x00005F11
		public string FileName
		{
			get
			{
				string text = this.FindParameter("filename");
				if (text == null)
				{
					return null;
				}
				return ContentDispositionHeaderValue.DecodeValue(text, false);
			}
			set
			{
				if (value != null)
				{
					value = ContentDispositionHeaderValue.EncodeBase64Value(value);
				}
				this.SetValue("filename", value);
			}
		}

		/// <summary>A suggestion for how to construct filenames for   storing message payloads to be used if the entities are    detached and stored in a separate files.</summary>
		/// <returns>A suggested filename of the form filename*.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007D2C File Offset: 0x00005F2C
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00007D51 File Offset: 0x00005F51
		public string FileNameStar
		{
			get
			{
				string text = this.FindParameter("filename*");
				if (text == null)
				{
					return null;
				}
				return ContentDispositionHeaderValue.DecodeValue(text, true);
			}
			set
			{
				if (value != null)
				{
					value = ContentDispositionHeaderValue.EncodeRFC5987(value);
				}
				this.SetValue("filename*", value);
			}
		}

		/// <summary>The date at   which the file was last modified.</summary>
		/// <returns>The file modification date.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007D6A File Offset: 0x00005F6A
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00007D77 File Offset: 0x00005F77
		public DateTimeOffset? ModificationDate
		{
			get
			{
				return this.GetDateValue("modification-date");
			}
			set
			{
				this.SetDateValue("modification-date", value);
			}
		}

		/// <summary>The name for a content body part.</summary>
		/// <returns>The name for the content body part.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007D88 File Offset: 0x00005F88
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00007DAD File Offset: 0x00005FAD
		public string Name
		{
			get
			{
				string text = this.FindParameter("name");
				if (text == null)
				{
					return null;
				}
				return ContentDispositionHeaderValue.DecodeValue(text, false);
			}
			set
			{
				if (value != null)
				{
					value = ContentDispositionHeaderValue.EncodeBase64Value(value);
				}
				this.SetValue("name", value);
			}
		}

		/// <summary>A set of parameters included the Content-Disposition header.</summary>
		/// <returns>A collection of parameters.</returns>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007DC8 File Offset: 0x00005FC8
		public ICollection<NameValueHeaderValue> Parameters
		{
			get
			{
				List<NameValueHeaderValue> result;
				if ((result = this.parameters) == null)
				{
					result = (this.parameters = new List<NameValueHeaderValue>());
				}
				return result;
			}
		}

		/// <summary>The date the file was last read.</summary>
		/// <returns>The last read date.</returns>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007DED File Offset: 0x00005FED
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00007DFA File Offset: 0x00005FFA
		public DateTimeOffset? ReadDate
		{
			get
			{
				return this.GetDateValue("read-date");
			}
			set
			{
				this.SetDateValue("read-date", value);
			}
		}

		/// <summary>The approximate size, in bytes, of the file.</summary>
		/// <returns>The approximate size, in bytes.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007E08 File Offset: 0x00006008
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00007E3C File Offset: 0x0000603C
		public long? Size
		{
			get
			{
				long value;
				if (Parser.Long.TryParse(this.FindParameter("size"), out value))
				{
					return new long?(value);
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.SetValue("size", null);
					return;
				}
				long? num = value;
				long num2 = 0L;
				if (num.GetValueOrDefault() < num2 & num != null)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.SetValue("size", value.Value.ToString(CultureInfo.InvariantCulture));
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060001EE RID: 494 RVA: 0x00007EA2 File Offset: 0x000060A2
		object ICloneable.Clone()
		{
			return new ContentDispositionHeaderValue(this);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001EF RID: 495 RVA: 0x00007EAC File Offset: 0x000060AC
		public override bool Equals(object obj)
		{
			ContentDispositionHeaderValue contentDispositionHeaderValue = obj as ContentDispositionHeaderValue;
			return contentDispositionHeaderValue != null && string.Equals(contentDispositionHeaderValue.dispositionType, this.dispositionType, StringComparison.OrdinalIgnoreCase) && contentDispositionHeaderValue.parameters.SequenceEqual(this.parameters);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007EEC File Offset: 0x000060EC
		private string FindParameter(string name)
		{
			if (this.parameters == null)
			{
				return null;
			}
			foreach (NameValueHeaderValue nameValueHeaderValue in this.parameters)
			{
				if (string.Equals(nameValueHeaderValue.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return nameValueHeaderValue.Value;
				}
			}
			return null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007F60 File Offset: 0x00006160
		private DateTimeOffset? GetDateValue(string name)
		{
			string text = this.FindParameter(name);
			if (text == null || text == null)
			{
				return null;
			}
			if (text.Length < 3)
			{
				return null;
			}
			if (text[0] == '"')
			{
				text = text.Substring(1, text.Length - 2);
			}
			DateTimeOffset value;
			if (Lexer.TryGetDateValue(text, out value))
			{
				return new DateTimeOffset?(value);
			}
			return null;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007FD0 File Offset: 0x000061D0
		private static string EncodeBase64Value(string value)
		{
			bool flag = value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"';
			if (flag)
			{
				value = value.Substring(1, value.Length - 2);
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] > '\u007f')
				{
					Encoding utf = Encoding.UTF8;
					return string.Format("\"=?{0}?B?{1}?=\"", utf.WebName, Convert.ToBase64String(utf.GetBytes(value)));
				}
			}
			if (flag || !Lexer.IsValidToken(value))
			{
				return "\"" + value + "\"";
			}
			return value;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008078 File Offset: 0x00006278
		private static string EncodeRFC5987(string value)
		{
			Encoding utf = Encoding.UTF8;
			StringBuilder stringBuilder = new StringBuilder(value.Length + 11);
			stringBuilder.Append(utf.WebName);
			stringBuilder.Append('\'');
			stringBuilder.Append('\'');
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					foreach (byte b in utf.GetBytes(new char[]
					{
						c
					}))
					{
						stringBuilder.Append('%');
						stringBuilder.Append(b.ToString("X2"));
					}
				}
				else if (!Lexer.IsValidCharacter(c) || c == '*' || c == '?' || c == '%')
				{
					stringBuilder.Append(Uri.HexEscape(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000815C File Offset: 0x0000635C
		private static string DecodeValue(string value, bool extendedNotation)
		{
			if (value.Length < 2)
			{
				return value;
			}
			string[] array;
			Encoding encoding;
			if (value[0] == '"')
			{
				array = value.Split('?', StringSplitOptions.None);
				if (array.Length != 5 || array[0] != "\"=" || array[4] != "=\"" || (array[2] != "B" && array[2] != "b"))
				{
					return value;
				}
				try
				{
					encoding = Encoding.GetEncoding(array[1]);
					return encoding.GetString(Convert.FromBase64String(array[3]));
				}
				catch
				{
					return value;
				}
			}
			if (!extendedNotation)
			{
				return value;
			}
			array = value.Split('\'', StringSplitOptions.None);
			if (array.Length != 3)
			{
				return null;
			}
			try
			{
				encoding = Encoding.GetEncoding(array[0]);
			}
			catch
			{
				return null;
			}
			value = array[2];
			if (value.IndexOf('%') < 0)
			{
				return value;
			}
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = null;
			int num = 0;
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c == '%')
				{
					char c2 = c;
					c = Uri.HexUnescape(value, ref i);
					if (c != c2)
					{
						if (array2 == null)
						{
							array2 = new byte[value.Length - i + 1];
						}
						array2[num++] = (byte)c;
						continue;
					}
				}
				else
				{
					i++;
				}
				if (num != 0)
				{
					stringBuilder.Append(encoding.GetChars(array2, 0, num));
					num = 0;
				}
				stringBuilder.Append(c);
			}
			if (num != 0)
			{
				stringBuilder.Append(encoding.GetChars(array2, 0, num));
			}
			return stringBuilder.ToString();
		}

		/// <summary>Serves as a hash function for an  <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060001F5 RID: 501 RVA: 0x000082F4 File Offset: 0x000064F4
		public override int GetHashCode()
		{
			return this.dispositionType.ToLowerInvariant().GetHashCode() ^ HashCodeCalculator.Calculate<NameValueHeaderValue>(this.parameters);
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents content disposition header value information.</param>
		/// <returns>An <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid content disposition header value information.</exception>
		// Token: 0x060001F6 RID: 502 RVA: 0x00008314 File Offset: 0x00006514
		public static ContentDispositionHeaderValue Parse(string input)
		{
			ContentDispositionHeaderValue result;
			if (ContentDispositionHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008334 File Offset: 0x00006534
		private void SetDateValue(string key, DateTimeOffset? value)
		{
			this.SetValue(key, (value == null) ? null : ("\"" + value.Value.ToString("r", CultureInfo.InvariantCulture) + "\""));
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000837C File Offset: 0x0000657C
		private void SetValue(string key, string value)
		{
			if (this.parameters == null)
			{
				this.parameters = new List<NameValueHeaderValue>();
			}
			this.parameters.SetValue(key, value);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060001F9 RID: 505 RVA: 0x0000839E File Offset: 0x0000659E
		public override string ToString()
		{
			return this.dispositionType + this.parameters.ToString<NameValueHeaderValue>();
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ContentDispositionHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001FA RID: 506 RVA: 0x000083B8 File Offset: 0x000065B8
		public static bool TryParse(string input, out ContentDispositionHeaderValue parsedValue)
		{
			parsedValue = null;
			Lexer lexer = new Lexer(input);
			Token token = lexer.Scan(false);
			if (token.Kind != Token.Type.Token)
			{
				return false;
			}
			List<NameValueHeaderValue> list = null;
			string stringValue = lexer.GetStringValue(token);
			token = lexer.Scan(false);
			Token.Type kind = token.Kind;
			if (kind != Token.Type.End)
			{
				if (kind != Token.Type.SeparatorSemicolon)
				{
					return false;
				}
				if (!NameValueHeaderValue.TryParseParameters(lexer, out list, out token) || token != Token.Type.End)
				{
					return false;
				}
			}
			parsedValue = new ContentDispositionHeaderValue
			{
				dispositionType = stringValue,
				parameters = list
			};
			return true;
		}

		// Token: 0x040000EB RID: 235
		private string dispositionType;

		// Token: 0x040000EC RID: 236
		private List<NameValueHeaderValue> parameters;
	}
}
