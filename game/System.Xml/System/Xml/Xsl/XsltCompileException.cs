using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Xsl
{
	/// <summary>The exception that is thrown by the Load method when an error is found in the XSLT style sheet.</summary>
	// Token: 0x0200034D RID: 845
	[Serializable]
	public class XsltCompileException : XsltException
	{
		/// <summary>Initializes a new instance of the <see langword="XsltCompileException" /> class using the information in the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">The <see langword="SerializationInfo" /> object containing all the properties of an <see langword="XsltCompileException" />. </param>
		/// <param name="context">The <see langword="StreamingContext" /> object containing the context information. </param>
		// Token: 0x060022E8 RID: 8936 RVA: 0x000D91A7 File Offset: 0x000D73A7
		protected XsltCompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Streams all the <see langword="XsltCompileException" /> properties into the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class for the given <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The <see langword="SerializationInfo" /> object. </param>
		/// <param name="context">The <see langword="StreamingContext" /> object. </param>
		// Token: 0x060022E9 RID: 8937 RVA: 0x000DAA62 File Offset: 0x000D8C62
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class.</summary>
		// Token: 0x060022EA RID: 8938 RVA: 0x000DAA6C File Offset: 0x000D8C6C
		public XsltCompileException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060022EB RID: 8939 RVA: 0x000DAA74 File Offset: 0x000D8C74
		public XsltCompileException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltCompileException" /> class specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the error.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null" /> if no inner exception is specified. </param>
		// Token: 0x060022EC RID: 8940 RVA: 0x000DAA7D File Offset: 0x000D8C7D
		public XsltCompileException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XsltCompileException" /> class.</summary>
		/// <param name="inner">The <see cref="T:System.Exception" /> that threw the <see langword="XsltCompileException" />. </param>
		/// <param name="sourceUri">The location path of the style sheet. </param>
		/// <param name="lineNumber">The line number indicating where the error occurred in the style sheet. </param>
		/// <param name="linePosition">The line position indicating where the error occurred in the style sheet. </param>
		// Token: 0x060022ED RID: 8941 RVA: 0x000DAA88 File Offset: 0x000D8C88
		public XsltCompileException(Exception inner, string sourceUri, int lineNumber, int linePosition) : base((lineNumber != 0) ? "XSLT compile error at {0}({1},{2}). See InnerException for details." : "XSLT compile error.", new string[]
		{
			sourceUri,
			lineNumber.ToString(CultureInfo.InvariantCulture),
			linePosition.ToString(CultureInfo.InvariantCulture)
		}, sourceUri, lineNumber, linePosition, inner)
		{
		}
	}
}
