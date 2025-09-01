using System;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when there is a syntax error in XML parsing. This class cannot be inherited.</summary>
	// Token: 0x020003CC RID: 972
	[Serializable]
	public sealed class XmlSyntaxException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with default properties.</summary>
		// Token: 0x0600285A RID: 10330 RVA: 0x00092A31 File Offset: 0x00090C31
		public XmlSyntaxException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected.</param>
		// Token: 0x0600285B RID: 10331 RVA: 0x00092A31 File Offset: 0x00090C31
		public XmlSyntaxException(int lineNumber)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and the line number where the exception was detected.</summary>
		/// <param name="lineNumber">The line number of the XML stream where the XML syntax error was detected.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600285C RID: 10332 RVA: 0x00092A31 File Offset: 0x00090C31
		public XmlSyntaxException(int lineNumber, string message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600285D RID: 10333 RVA: 0x00092A31 File Offset: 0x00090C31
		public XmlSyntaxException(string message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.XmlSyntaxException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600285E RID: 10334 RVA: 0x00092A31 File Offset: 0x00090C31
		public XmlSyntaxException(string message, Exception inner)
		{
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x00020A65 File Offset: 0x0001EC65
		private XmlSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
