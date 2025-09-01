using System;
using System.Globalization;
using System.Resources;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Xml
{
	/// <summary>Returns detailed information about the last exception.</summary>
	// Token: 0x0200023B RID: 571
	[Serializable]
	public class XmlException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="XmlException" /> class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The <see langword="SerializationInfo" /> object containing all the properties of an <see langword="XmlException" />. </param>
		/// <param name="context">The <see langword="StreamingContext" /> object containing the context information. </param>
		// Token: 0x0600154F RID: 5455 RVA: 0x00083A78 File Offset: 0x00081C78
		protected XmlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.res = (string)info.GetValue("res", typeof(string));
			this.args = (string[])info.GetValue("args", typeof(string[]));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
			this.sourceUri = string.Empty;
			string text = null;
			foreach (SerializationEntry serializationEntry in info)
			{
				string name = serializationEntry.Name;
				if (!(name == "sourceUri"))
				{
					if (name == "version")
					{
						text = (string)serializationEntry.Value;
					}
				}
				else
				{
					this.sourceUri = (string)serializationEntry.Value;
				}
			}
			if (text == null)
			{
				this.message = XmlException.CreateMessage(this.res, this.args, this.lineNumber, this.linePosition);
				return;
			}
			this.message = null;
		}

		/// <summary>Streams all the <see langword="XmlException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see langword="SerializationInfo" /> object. </param>
		/// <param name="context">The <see langword="StreamingContext" /> object. </param>
		// Token: 0x06001550 RID: 5456 RVA: 0x00083BA8 File Offset: 0x00081DA8
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("res", this.res);
			info.AddValue("args", this.args);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
			info.AddValue("sourceUri", this.sourceUri);
			info.AddValue("version", "2.0");
		}

		/// <summary>Initializes a new instance of the <see langword="XmlException" /> class.</summary>
		// Token: 0x06001551 RID: 5457 RVA: 0x00083C22 File Offset: 0x00081E22
		public XmlException() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XmlException" /> class with a specified error message.</summary>
		/// <param name="message">The error description. </param>
		// Token: 0x06001552 RID: 5458 RVA: 0x00083C2B File Offset: 0x00081E2B
		public XmlException(string message) : this(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XmlException" /> class.</summary>
		/// <param name="message">The description of the error condition. </param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that threw the <see langword="XmlException" />, if any. This value can be <see langword="null" />. </param>
		// Token: 0x06001553 RID: 5459 RVA: 0x00083C37 File Offset: 0x00081E37
		public XmlException(string message, Exception innerException) : this(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XmlException" /> class with the specified message, inner exception, line number, and line position.</summary>
		/// <param name="message">The error description. </param>
		/// <param name="innerException">The exception that is the cause of the current exception. This value can be <see langword="null" />. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred. </param>
		/// <param name="linePosition">The line position indicating where the error occurred. </param>
		// Token: 0x06001554 RID: 5460 RVA: 0x00083C43 File Offset: 0x00081E43
		public XmlException(string message, Exception innerException, int lineNumber, int linePosition) : this(message, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00083C54 File Offset: 0x00081E54
		internal XmlException(string message, Exception innerException, int lineNumber, int linePosition, string sourceUri) : base(XmlException.FormatUserMessage(message, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = ((message == null) ? "An XML error has occurred." : "{0}");
			this.args = new string[]
			{
				message
			};
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00083CB8 File Offset: 0x00081EB8
		internal XmlException(string res, string[] args) : this(res, args, null, 0, 0, null)
		{
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00083CC6 File Offset: 0x00081EC6
		internal XmlException(string res, string[] args, string sourceUri) : this(res, args, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00083CD4 File Offset: 0x00081ED4
		internal XmlException(string res, string arg) : this(res, new string[]
		{
			arg
		}, null, 0, 0, null)
		{
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00083CEB File Offset: 0x00081EEB
		internal XmlException(string res, string arg, string sourceUri) : this(res, new string[]
		{
			arg
		}, null, 0, 0, sourceUri)
		{
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00083D02 File Offset: 0x00081F02
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo) : this(res, new string[]
		{
			arg
		}, lineInfo, null)
		{
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00083D17 File Offset: 0x00081F17
		internal XmlException(string res, string arg, Exception innerException, IXmlLineInfo lineInfo) : this(res, new string[]
		{
			arg
		}, innerException, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, null)
		{
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00083D48 File Offset: 0x00081F48
		internal XmlException(string res, string arg, IXmlLineInfo lineInfo, string sourceUri) : this(res, new string[]
		{
			arg
		}, lineInfo, sourceUri)
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00083D5E File Offset: 0x00081F5E
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo) : this(res, args, lineInfo, null)
		{
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00083D6A File Offset: 0x00081F6A
		internal XmlException(string res, string[] args, IXmlLineInfo lineInfo, string sourceUri) : this(res, args, null, (lineInfo == null) ? 0 : lineInfo.LineNumber, (lineInfo == null) ? 0 : lineInfo.LinePosition, sourceUri)
		{
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00083D8F File Offset: 0x00081F8F
		internal XmlException(string res, int lineNumber, int linePosition) : this(res, null, null, lineNumber, linePosition)
		{
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00083D9C File Offset: 0x00081F9C
		internal XmlException(string res, string arg, int lineNumber, int linePosition) : this(res, new string[]
		{
			arg
		}, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00083DB4 File Offset: 0x00081FB4
		internal XmlException(string res, string arg, int lineNumber, int linePosition, string sourceUri) : this(res, new string[]
		{
			arg
		}, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00083DCD File Offset: 0x00081FCD
		internal XmlException(string res, string[] args, int lineNumber, int linePosition) : this(res, args, null, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00083DDC File Offset: 0x00081FDC
		internal XmlException(string res, string[] args, int lineNumber, int linePosition, string sourceUri) : this(res, args, null, lineNumber, linePosition, sourceUri)
		{
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00083DEC File Offset: 0x00081FEC
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition) : this(res, args, innerException, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00083DFC File Offset: 0x00081FFC
		internal XmlException(string res, string[] args, Exception innerException, int lineNumber, int linePosition, string sourceUri) : base(XmlException.CreateMessage(res, args, lineNumber, linePosition), innerException)
		{
			base.HResult = -2146232000;
			this.res = res;
			this.args = args;
			this.sourceUri = sourceUri;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00083E4C File Offset: 0x0008204C
		private static string FormatUserMessage(string message, int lineNumber, int linePosition)
		{
			if (message == null)
			{
				return XmlException.CreateMessage("An XML error has occurred.", null, lineNumber, linePosition);
			}
			if (lineNumber == 0 && linePosition == 0)
			{
				return message;
			}
			return XmlException.CreateMessage("{0}", new string[]
			{
				message
			}, lineNumber, linePosition);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00083E80 File Offset: 0x00082080
		private static string CreateMessage(string res, string[] args, int lineNumber, int linePosition)
		{
			string result;
			try
			{
				string @string;
				if (lineNumber == 0)
				{
					@string = Res.GetString(res, args);
				}
				else
				{
					string text = lineNumber.ToString(CultureInfo.InvariantCulture);
					string text2 = linePosition.ToString(CultureInfo.InvariantCulture);
					@string = Res.GetString(res, args);
					string name = "{0} Line {1}, position {2}.";
					object[] array = new string[]
					{
						@string,
						text,
						text2
					};
					@string = Res.GetString(name, array);
				}
				result = @string;
			}
			catch (MissingManifestResourceException)
			{
				result = "UNKNOWN(" + res + ")";
			}
			return result;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00083F0C File Offset: 0x0008210C
		internal static string[] BuildCharExceptionArgs(string data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < data.Length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00083F31 File Offset: 0x00082131
		internal static string[] BuildCharExceptionArgs(char[] data, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data, data.Length, invCharIndex);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00083F3D File Offset: 0x0008213D
		internal static string[] BuildCharExceptionArgs(char[] data, int length, int invCharIndex)
		{
			return XmlException.BuildCharExceptionArgs(data[invCharIndex], (invCharIndex + 1 < length) ? data[invCharIndex + 1] : '\0');
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00083F58 File Offset: 0x00082158
		internal static string[] BuildCharExceptionArgs(char invChar, char nextChar)
		{
			string[] array = new string[2];
			if (XmlCharType.IsHighSurrogate((int)invChar) && nextChar != '\0')
			{
				int num = XmlCharType.CombineSurrogateChar((int)nextChar, (int)invChar);
				array[0] = new string(new char[]
				{
					invChar,
					nextChar
				});
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", num);
			}
			else
			{
				if (invChar == '\0')
				{
					array[0] = ".";
				}
				else
				{
					array[0] = invChar.ToString(CultureInfo.InvariantCulture);
				}
				array[1] = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", (int)invChar);
			}
			return array;
		}

		/// <summary>Gets the line number indicating where the error occurred.</summary>
		/// <returns>The line number indicating where the error occurred.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00083FE4 File Offset: 0x000821E4
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		/// <summary>Gets the line position indicating where the error occurred.</summary>
		/// <returns>The line position indicating where the error occurred.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00083FEC File Offset: 0x000821EC
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		/// <summary>Gets the location of the XML file.</summary>
		/// <returns>The source URI for the XML data. If there is no source URI, this property returns <see langword="null" />.</returns>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x00083FF4 File Offset: 0x000821F4
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
		}

		/// <summary>Gets a message describing the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00083FFC File Offset: 0x000821FC
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00084013 File Offset: 0x00082213
		internal string ResString
		{
			get
			{
				return this.res;
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0008401B File Offset: 0x0008221B
		internal static bool IsCatchableException(Exception e)
		{
			return !(e is StackOverflowException) && !(e is OutOfMemoryException) && !(e is ThreadAbortException) && !(e is ThreadInterruptedException) && !(e is NullReferenceException) && !(e is AccessViolationException);
		}

		// Token: 0x040012D5 RID: 4821
		private string res;

		// Token: 0x040012D6 RID: 4822
		private string[] args;

		// Token: 0x040012D7 RID: 4823
		private int lineNumber;

		// Token: 0x040012D8 RID: 4824
		private int linePosition;

		// Token: 0x040012D9 RID: 4825
		[OptionalField]
		private string sourceUri;

		// Token: 0x040012DA RID: 4826
		private string message;
	}
}
