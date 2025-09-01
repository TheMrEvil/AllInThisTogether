using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Utils;

namespace System.Xml.Xsl
{
	/// <summary>The exception that is thrown when an error occurs while processing an XSLT transformation.</summary>
	// Token: 0x0200034C RID: 844
	[Serializable]
	public class XsltException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="XsltException" /> class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The <see langword="SerializationInfo" /> object containing all the properties of an <see langword="XsltException" />. </param>
		/// <param name="context">The <see langword="StreamingContext" /> object. </param>
		// Token: 0x060022DA RID: 8922 RVA: 0x000DA750 File Offset: 0x000D8950
		protected XsltException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.sourceUri = (string)info.GetValue("sourceUri", typeof(string));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (serializationEntry.Name == "version")
				{
					text = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XsltException.CreateMessage(this.res, this.args, this.sourceUri, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the <see langword="XsltException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see langword="SerializationInfo" /> object. </param>
		/// <param name="context">The <see langword="StreamingContext" /> object. </param>
		// Token: 0x060022DB RID: 8923 RVA: 0x000DA874 File Offset: 0x000D8A74
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltException" /> class.</summary>
		// Token: 0x060022DC RID: 8924 RVA: 0x000DA8EE File Offset: 0x000D8AEE
		public XsltException() : this(string.Empty, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltException" /> class with a specified error message. </summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060022DD RID: 8925 RVA: 0x000DA8FC File Offset: 0x000D8AFC
		public XsltException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XsltException" /> class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> which threw the <see langword="XsltException" />, if any. This value can be <see langword="null" />. </param>
		// Token: 0x060022DE RID: 8926 RVA: 0x000DA906 File Offset: 0x000D8B06
		public XsltException(string message, Exception innerException) : this("{0}", new string[]
		{
			message
		}, null, 0, 0, innerException)
		{
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000DA921 File Offset: 0x000D8B21
		internal static XsltException Create(string res, params string[] args)
		{
			return new XsltException(res, args, null, 0, 0, null);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000DA92E File Offset: 0x000D8B2E
		internal static XsltException Create(string res, string[] args, Exception inner)
		{
			return new XsltException(res, args, null, 0, 0, inner);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000DA93B File Offset: 0x000D8B3B
		internal XsltException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, Exception inner) : base(XsltException.CreateMessage(res, args, sourceUri, lineNumber, linePosition), inner)
		{
			base.HResult = -2146231998;
			this.res = res;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		/// <summary>Gets the location path of the style sheet.</summary>
		/// <returns>The location path of the style sheet.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000DA97A File Offset: 0x000D8B7A
		public virtual string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets the line number indicating where the error occurred in the style sheet.</summary>
		/// <returns>The line number indicating where the error occurred in the style sheet.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000DA982 File Offset: 0x000D8B82
		public virtual int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred in the style sheet.</summary>
		/// <returns>The line position indicating where the error occurred in the style sheet.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000DA98A File Offset: 0x000D8B8A
		public virtual int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the formatted error message describing the current exception.</summary>
		/// <returns>The formatted error message describing the current exception.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000DA992 File Offset: 0x000D8B92
		public override string Message
		{
			get
			{
				if (this.message != null)
				{
					return this.message;
				}
				return base.Message;
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000DA9AC File Offset: 0x000D8BAC
		private static string CreateMessage(string res, string[] args, string sourceUri, int lineNumber, int linePosition)
		{
			string result;
			try
			{
				string text = XsltException.FormatMessage(res, args);
				if (res != "XSLT compile error at {0}({1},{2}). See InnerException for details." && lineNumber != 0)
				{
					text = text + " " + XsltException.FormatMessage("An error occurred at {0}({1},{2}).", new string[]
					{
						sourceUri,
						lineNumber.ToString(CultureInfo.InvariantCulture),
						linePosition.ToString(CultureInfo.InvariantCulture)
					});
				}
				result = text;
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000DAA38 File Offset: 0x000D8C38
		private static string FormatMessage(string key, params string[] args)
		{
			string text = Res.GetString(key);
			if (text != null && args != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, text, args);
			}
			return text;
		}

		// Token: 0x04001C52 RID: 7250
		private string res;

		// Token: 0x04001C53 RID: 7251
		private string[] args;

		// Token: 0x04001C54 RID: 7252
		private string sourceUri;

		// Token: 0x04001C55 RID: 7253
		private int lineNumber;

		// Token: 0x04001C56 RID: 7254
		private int linePosition;

		// Token: 0x04001C57 RID: 7255
		private string message;
	}
}
