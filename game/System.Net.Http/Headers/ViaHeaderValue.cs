using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Headers
{
	/// <summary>Represents the value of a Via header.</summary>
	// Token: 0x0200006B RID: 107
	public class ViaHeaderValue : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		// Token: 0x060003CF RID: 975 RVA: 0x0000D157 File Offset: 0x0000B357
		public ViaHeaderValue(string protocolVersion, string receivedBy)
		{
			Parser.Token.Check(protocolVersion);
			Parser.Uri.Check(receivedBy);
			this.ProtocolVersion = protocolVersion;
			this.ReceivedBy = receivedBy;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		/// <param name="protocolName">The protocol name of the received protocol.</param>
		// Token: 0x060003D0 RID: 976 RVA: 0x0000D179 File Offset: 0x0000B379
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName) : this(protocolVersion, receivedBy)
		{
			if (!string.IsNullOrEmpty(protocolName))
			{
				Parser.Token.Check(protocolName);
				this.ProtocolName = protocolName;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> class.</summary>
		/// <param name="protocolVersion">The protocol version of the received protocol.</param>
		/// <param name="receivedBy">The host and port that the request or response was received by.</param>
		/// <param name="protocolName">The protocol name of the received protocol.</param>
		/// <param name="comment">The comment field used to identify the software of the recipient proxy or gateway.</param>
		// Token: 0x060003D1 RID: 977 RVA: 0x0000D198 File Offset: 0x0000B398
		public ViaHeaderValue(string protocolVersion, string receivedBy, string protocolName, string comment) : this(protocolVersion, receivedBy, protocolName)
		{
			if (!string.IsNullOrEmpty(comment))
			{
				Parser.Token.CheckComment(comment);
				this.Comment = comment;
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000022B8 File Offset: 0x000004B8
		private ViaHeaderValue()
		{
		}

		/// <summary>Gets the comment field used to identify the software of the recipient proxy or gateway.</summary>
		/// <returns>The comment field used to identify the software of the recipient proxy or gateway.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000D1BB File Offset: 0x0000B3BB
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000D1C3 File Offset: 0x0000B3C3
		public string Comment
		{
			[CompilerGenerated]
			get
			{
				return this.<Comment>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Comment>k__BackingField = value;
			}
		}

		/// <summary>Gets the protocol name of the received protocol.</summary>
		/// <returns>The protocol name.</returns>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		public string ProtocolName
		{
			[CompilerGenerated]
			get
			{
				return this.<ProtocolName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProtocolName>k__BackingField = value;
			}
		}

		/// <summary>Gets the protocol version of the received protocol.</summary>
		/// <returns>The protocol version.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000D1DD File Offset: 0x0000B3DD
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000D1E5 File Offset: 0x0000B3E5
		public string ProtocolVersion
		{
			[CompilerGenerated]
			get
			{
				return this.<ProtocolVersion>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ProtocolVersion>k__BackingField = value;
			}
		}

		/// <summary>Gets the host and port that the request or response was received by.</summary>
		/// <returns>The host and port that the request or response was received by.</returns>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000D1EE File Offset: 0x0000B3EE
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000D1F6 File Offset: 0x0000B3F6
		public string ReceivedBy
		{
			[CompilerGenerated]
			get
			{
				return this.<ReceivedBy>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ReceivedBy>k__BackingField = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</summary>
		/// <returns>A copy of the current instance.</returns>
		// Token: 0x060003DB RID: 987 RVA: 0x00006AEE File Offset: 0x00004CEE
		object ICloneable.Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003DC RID: 988 RVA: 0x0000D200 File Offset: 0x0000B400
		public override bool Equals(object obj)
		{
			ViaHeaderValue viaHeaderValue = obj as ViaHeaderValue;
			return viaHeaderValue != null && (string.Equals(viaHeaderValue.Comment, this.Comment, StringComparison.Ordinal) && string.Equals(viaHeaderValue.ProtocolName, this.ProtocolName, StringComparison.OrdinalIgnoreCase) && string.Equals(viaHeaderValue.ProtocolVersion, this.ProtocolVersion, StringComparison.OrdinalIgnoreCase)) && string.Equals(viaHeaderValue.ReceivedBy, this.ReceivedBy, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Serves as a hash function for an <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060003DD RID: 989 RVA: 0x0000D26C File Offset: 0x0000B46C
		public override int GetHashCode()
		{
			int num = this.ProtocolVersion.ToLowerInvariant().GetHashCode();
			num ^= this.ReceivedBy.ToLowerInvariant().GetHashCode();
			if (!string.IsNullOrEmpty(this.ProtocolName))
			{
				num ^= this.ProtocolName.ToLowerInvariant().GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.Comment))
			{
				num ^= this.Comment.GetHashCode();
			}
			return num;
		}

		/// <summary>Converts a string to an <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</summary>
		/// <param name="input">A string that represents via header value information.</param>
		/// <returns>A <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> instance.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is a <see langword="null" /> reference.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> is not valid via header value information.</exception>
		// Token: 0x060003DE RID: 990 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public static ViaHeaderValue Parse(string input)
		{
			ViaHeaderValue result;
			if (ViaHeaderValue.TryParse(input, out result))
			{
				return result;
			}
			throw new FormatException(input);
		}

		/// <summary>Determines whether a string is valid <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> information.</summary>
		/// <param name="input">The string to validate.</param>
		/// <param name="parsedValue">The <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> version of the string.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> is valid <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003DF RID: 991 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public static bool TryParse(string input, out ViaHeaderValue parsedValue)
		{
			Token token;
			if (ViaHeaderValue.TryParseElement(new Lexer(input), out parsedValue, out token) && token == Token.Type.End)
			{
				return true;
			}
			parsedValue = null;
			return false;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000D328 File Offset: 0x0000B528
		internal static bool TryParse(string input, int minimalCount, out List<ViaHeaderValue> result)
		{
			return CollectionParser.TryParse<ViaHeaderValue>(input, minimalCount, new ElementTryParser<ViaHeaderValue>(ViaHeaderValue.TryParseElement), out result);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000D340 File Offset: 0x0000B540
		private static bool TryParseElement(Lexer lexer, out ViaHeaderValue parsedValue, out Token t)
		{
			parsedValue = null;
			t = lexer.Scan(false);
			if (t != Token.Type.Token)
			{
				return false;
			}
			Token token = lexer.Scan(false);
			ViaHeaderValue viaHeaderValue = new ViaHeaderValue();
			if (token == Token.Type.SeparatorSlash)
			{
				token = lexer.Scan(false);
				if (token != Token.Type.Token)
				{
					return false;
				}
				viaHeaderValue.ProtocolName = lexer.GetStringValue(t);
				viaHeaderValue.ProtocolVersion = lexer.GetStringValue(token);
				token = lexer.Scan(false);
			}
			else
			{
				viaHeaderValue.ProtocolVersion = lexer.GetStringValue(t);
			}
			if (token != Token.Type.Token)
			{
				return false;
			}
			if (lexer.PeekChar() == 58)
			{
				lexer.EatChar();
				t = lexer.Scan(false);
				if (t != Token.Type.Token)
				{
					return false;
				}
			}
			else
			{
				t = token;
			}
			viaHeaderValue.ReceivedBy = lexer.GetStringValue(token, t);
			string comment;
			if (lexer.ScanCommentOptional(out comment, out t))
			{
				t = lexer.Scan(false);
			}
			viaHeaderValue.Comment = comment;
			parsedValue = viaHeaderValue;
			return true;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.Http.Headers.ViaHeaderValue" /> object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060003E2 RID: 994 RVA: 0x0000D448 File Offset: 0x0000B648
		public override string ToString()
		{
			string text = (this.ProtocolName != null) ? string.Concat(new string[]
			{
				this.ProtocolName,
				"/",
				this.ProtocolVersion,
				" ",
				this.ReceivedBy
			}) : (this.ProtocolVersion + " " + this.ReceivedBy);
			if (this.Comment == null)
			{
				return text;
			}
			return text + " " + this.Comment;
		}

		// Token: 0x04000150 RID: 336
		[CompilerGenerated]
		private string <Comment>k__BackingField;

		// Token: 0x04000151 RID: 337
		[CompilerGenerated]
		private string <ProtocolName>k__BackingField;

		// Token: 0x04000152 RID: 338
		[CompilerGenerated]
		private string <ProtocolVersion>k__BackingField;

		// Token: 0x04000153 RID: 339
		[CompilerGenerated]
		private string <ReceivedBy>k__BackingField;
	}
}
