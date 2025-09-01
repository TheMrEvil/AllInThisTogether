using System;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Returns detailed information about the schema exception.</summary>
	// Token: 0x020005AC RID: 1452
	[Serializable]
	public class XmlSchemaException : SystemException
	{
		/// <summary>Constructs a new <see langword="XmlSchemaException" /> object with the given <see langword="SerializationInfo" /> and <see langword="StreamingContext" /> information that contains all the properties of the <see langword="XmlSchemaException" />.</summary>
		/// <param name="info">SerializationInfo.</param>
		/// <param name="context">StreamingContext.</param>
		// Token: 0x06003ADD RID: 15069 RVA: 0x0014DBF4 File Offset: 0x0014BDF4
		protected XmlSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
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
				this.message = XmlSchemaException.CreateMessage(this.res, this.args);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the <see langword="XmlSchemaException" /> properties into the <see langword="SerializationInfo" /> class for the given <see langword="StreamingContext" />.</summary>
		/// <param name="info">The <see langword="SerializationInfo" />. </param>
		/// <param name="context">The <see langword="StreamingContext" /> information. </param>
		// Token: 0x06003ADE RID: 15070 RVA: 0x0014DD08 File Offset: 0x0014BF08
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class.</summary>
		// Token: 0x06003ADF RID: 15071 RVA: 0x001337F3 File Offset: 0x001319F3
		public XmlSchemaException() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified.</summary>
		/// <param name="message">A <see langword="string" /> description of the error condition.</param>
		// Token: 0x06003AE0 RID: 15072 RVA: 0x001337FC File Offset: 0x001319FC
		public XmlSchemaException(string message) : this(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A <see langword="string" /> description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		// Token: 0x06003AE1 RID: 15073 RVA: 0x00133808 File Offset: 0x00131A08
		public XmlSchemaException(string message, Exception innerException) : this(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A <see langword="string" /> description of the error condition.</param>
		/// <param name="innerException">The original T:System.Exception object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x06003AE2 RID: 15074 RVA: 0x0014DD82 File Offset: 0x0014BF82
		public XmlSchemaException(string message, Exception innerException, int lineNumber, int linePosition) : this((message == null) ? "A schema error occurred." : "{0}", new string[]
		{
			message
		}, innerException, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x00133821 File Offset: 0x00131A21
		internal XmlSchemaException(string res, string[] args) : this(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x00133830 File Offset: 0x00131A30
		internal XmlSchemaException(string res, string arg) : this(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x00133848 File Offset: 0x00131A48
		internal XmlSchemaException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : this(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00133862 File Offset: 0x00131A62
		internal XmlSchemaException(string res, string sourceUri, int lineNumber, int linePosition) : this(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x00133872 File Offset: 0x00131A72
		internal XmlSchemaException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : this(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x0014DDA9 File Offset: 0x0014BFA9
		internal XmlSchemaException(string res, XmlSchemaObject source) : this(res, null, source)
		{
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x0014DDB4 File Offset: 0x0014BFB4
		internal XmlSchemaException(string res, string arg, XmlSchemaObject source) : this(res, new string[]
		{
			arg
		}, source)
		{
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x0014DDC8 File Offset: 0x0014BFC8
		internal XmlSchemaException(string res, string[] args, XmlSchemaObject source) : this(res, args, null, source.SourceUri, source.LineNumber, source.LinePosition, source)
		{
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x0014DDE8 File Offset: 0x0014BFE8
		internal XmlSchemaException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source) : base(XmlSchemaException.CreateMessage(res, args), innerException)
		{
			base.HResult = -2146231999;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
			this.sourceSchemaObject = source;
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x0014DE3C File Offset: 0x0014C03C
		internal static string CreateMessage(string res, string[] args)
		{
			string result;
			try
			{
				result = Res.GetString(res, args);
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x0014DE7C File Offset: 0x0014C07C
		internal string GetRes
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x0014DE84 File Offset: 0x0014C084
		internal string[] Args
		{
			get
			{
				return this.args;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) location of the schema that caused the exception.</summary>
		/// <returns>The URI location of the schema that caused the exception.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x0014DE8C File Offset: 0x0014C08C
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x0014DE94 File Offset: 0x0014C094
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x0014DE9C File Offset: 0x0014C09C
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>The <see langword="XmlSchemaObject" /> that produced the <see langword="XmlSchemaException" />.</summary>
		/// <returns>A valid object instance represents a structural validation error in the XML Schema Object Model (SOM).</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x0014DEA4 File Offset: 0x0014C0A4
		public XmlSchemaObject SourceSchemaObject
		{
			get
			{
				return this.sourceSchemaObject;
			}
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x0014DEAC File Offset: 0x0014C0AC
		internal void SetSource(string sourceUri, int lineNumber, int linePosition)
		{
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x0014DEC3 File Offset: 0x0014C0C3
		internal void SetSchemaObject(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x0014DECC File Offset: 0x0014C0CC
		internal void SetSource(XmlSchemaObject source)
		{
			this.sourceSchemaObject = source;
			this.sourceUri = source.SourceUri;
			this.lineNumber = source.LineNumber;
			this.linePosition = source.LinePosition;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x0014DEF9 File Offset: 0x0014C0F9
		internal void SetResourceId(string resourceId)
		{
			this.res = resourceId;
		}

		/// <summary>Gets the description of the error condition of this exception.</summary>
		/// <returns>The description of the error condition of this exception.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x0014DF02 File Offset: 0x0014C102
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

		// Token: 0x04002B40 RID: 11072
		private string res;

		// Token: 0x04002B41 RID: 11073
		private string[] args;

		// Token: 0x04002B42 RID: 11074
		private string sourceUri;

		// Token: 0x04002B43 RID: 11075
		private int lineNumber;

		// Token: 0x04002B44 RID: 11076
		private int linePosition;

		// Token: 0x04002B45 RID: 11077
		[NonSerialized]
		private XmlSchemaObject sourceSchemaObject;

		// Token: 0x04002B46 RID: 11078
		private string message;
	}
}
