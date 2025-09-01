using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Schema
{
	/// <summary>Represents the exception thrown when XML Schema Definition Language (XSD) schema validation errors and warnings are encountered in an XML document being validated. </summary>
	// Token: 0x020005E6 RID: 1510
	[Serializable]
	public class XmlSchemaValidationException : XmlSchemaException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects specified.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06003C78 RID: 15480 RVA: 0x001337DF File Offset: 0x001319DF
		protected XmlSchemaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Constructs a new <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> object with the given <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> information that contains all the properties of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <param name="info">
		///       <see cref="T:System.Runtime.Serialization.SerializationInfo" />
		///     </param>
		/// <param name="context">
		///       <see cref="T:System.Runtime.Serialization.StreamingContext" />
		///     </param>
		// Token: 0x06003C79 RID: 15481 RVA: 0x001337E9 File Offset: 0x001319E9
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class.</summary>
		// Token: 0x06003C7A RID: 15482 RVA: 0x001337F3 File Offset: 0x001319F3
		public XmlSchemaValidationException() : base(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message specified.</summary>
		/// <param name="message">A <see langword="string" /> description of the error condition.</param>
		// Token: 0x06003C7B RID: 15483 RVA: 0x001337FC File Offset: 0x001319FC
		public XmlSchemaValidationException(string message) : base(message, null, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message and original <see cref="T:System.Exception" /> object that caused this exception specified.</summary>
		/// <param name="message">A <see langword="string" /> description of the error condition.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" /> object that caused this exception.</param>
		// Token: 0x06003C7C RID: 15484 RVA: 0x00133808 File Offset: 0x00131A08
		public XmlSchemaValidationException(string message, Exception innerException) : base(message, innerException, 0, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaValidationException" /> class with the exception message specified, and the original <see cref="T:System.Exception" /> object, line number, and line position of the XML that cause this exception specified.</summary>
		/// <param name="message">A <see langword="string" /> description of the error condition.</param>
		/// <param name="innerException">The original <see cref="T:System.Exception" /> object that caused this exception.</param>
		/// <param name="lineNumber">The line number of the XML that caused this exception.</param>
		/// <param name="linePosition">The line position of the XML that caused this exception.</param>
		// Token: 0x06003C7D RID: 15485 RVA: 0x00133814 File Offset: 0x00131A14
		public XmlSchemaValidationException(string message, Exception innerException, int lineNumber, int linePosition) : base(message, innerException, lineNumber, linePosition)
		{
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x00133821 File Offset: 0x00131A21
		internal XmlSchemaValidationException(string res, string[] args) : base(res, args, null, null, 0, 0, null)
		{
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x00133830 File Offset: 0x00131A30
		internal XmlSchemaValidationException(string res, string arg) : base(res, new string[]
		{
			arg
		}, null, null, 0, 0, null)
		{
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x00133848 File Offset: 0x00131A48
		internal XmlSchemaValidationException(string res, string arg, string sourceUri, int lineNumber, int linePosition) : base(res, new string[]
		{
			arg
		}, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x00133862 File Offset: 0x00131A62
		internal XmlSchemaValidationException(string res, string sourceUri, int lineNumber, int linePosition) : base(res, null, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x00133872 File Offset: 0x00131A72
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition) : base(res, args, null, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003C83 RID: 15491 RVA: 0x001514EB File Offset: 0x0014F6EB
		internal XmlSchemaValidationException(string res, string[] args, Exception innerException, string sourceUri, int lineNumber, int linePosition) : base(res, args, innerException, sourceUri, lineNumber, linePosition, null)
		{
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x001514FD File Offset: 0x0014F6FD
		internal XmlSchemaValidationException(string res, string[] args, object sourceNode) : base(res, args, null, null, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x00151513 File Offset: 0x0014F713
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, object sourceNode) : base(res, args, null, sourceUri, 0, 0, null)
		{
			this.sourceNodeObject = sourceNode;
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x0015152A File Offset: 0x0014F72A
		internal XmlSchemaValidationException(string res, string[] args, string sourceUri, int lineNumber, int linePosition, XmlSchemaObject source, object sourceNode) : base(res, args, null, sourceUri, lineNumber, linePosition, source)
		{
			this.sourceNodeObject = sourceNode;
		}

		/// <summary>Gets the XML node that caused this <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</summary>
		/// <returns>The XML node that caused this <see cref="T:System.Xml.Schema.XmlSchemaValidationException" />.</returns>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x00151544 File Offset: 0x0014F744
		public object SourceObject
		{
			get
			{
				return this.sourceNodeObject;
			}
		}

		/// <summary>Sets the XML node that causes the error.</summary>
		/// <param name="sourceObject">The source object.</param>
		// Token: 0x06003C88 RID: 15496 RVA: 0x0015154C File Offset: 0x0014F74C
		protected internal void SetSourceObject(object sourceObject)
		{
			this.sourceNodeObject = sourceObject;
		}

		// Token: 0x04002BE1 RID: 11233
		private object sourceNodeObject;
	}
}
