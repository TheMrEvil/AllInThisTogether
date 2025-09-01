using System;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Represents the root class for the Xml schema object model hierarchy and serves as a base class for classes such as the <see cref="T:System.Xml.Schema.XmlSchema" /> class.</summary>
	// Token: 0x020005CA RID: 1482
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class XmlSchemaObject
	{
		/// <summary>Gets or sets the line number in the file to which the <see langword="schema" /> element refers.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x0014E4BE File Offset: 0x0014C6BE
		// (set) Token: 0x06003B77 RID: 15223 RVA: 0x0014E4C6 File Offset: 0x0014C6C6
		[XmlIgnore]
		public int LineNumber
		{
			get
			{
				return this.lineNum;
			}
			set
			{
				this.lineNum = value;
			}
		}

		/// <summary>Gets or sets the line position in the file to which the <see langword="schema" /> element refers.</summary>
		/// <returns>The line position.</returns>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x0014E4CF File Offset: 0x0014C6CF
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x0014E4D7 File Offset: 0x0014C6D7
		[XmlIgnore]
		public int LinePosition
		{
			get
			{
				return this.linePos;
			}
			set
			{
				this.linePos = value;
			}
		}

		/// <summary>Gets or sets the source location for the file that loaded the schema.</summary>
		/// <returns>The source location (URI) for the file.</returns>
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x0014E4E0 File Offset: 0x0014C6E0
		// (set) Token: 0x06003B7B RID: 15227 RVA: 0x0014E4E8 File Offset: 0x0014C6E8
		[XmlIgnore]
		public string SourceUri
		{
			get
			{
				return this.sourceUri;
			}
			set
			{
				this.sourceUri = value;
			}
		}

		/// <summary>Gets or sets the parent of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</summary>
		/// <returns>The parent <see cref="T:System.Xml.Schema.XmlSchemaObject" /> of this <see cref="T:System.Xml.Schema.XmlSchemaObject" />.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x0014E4F1 File Offset: 0x0014C6F1
		// (set) Token: 0x06003B7D RID: 15229 RVA: 0x0014E4F9 File Offset: 0x0014C6F9
		[XmlIgnore]
		public XmlSchemaObject Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> to use with this schema object.</summary>
		/// <returns>The <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> property for the schema object.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x0014E502 File Offset: 0x0014C702
		// (set) Token: 0x06003B7F RID: 15231 RVA: 0x0014E51D File Offset: 0x0014C71D
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlSerializerNamespaces();
				}
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void OnAdd(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void OnRemove(XmlSchemaObjectCollection container, object item)
		{
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void OnClear(XmlSchemaObjectCollection container)
		{
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06003B83 RID: 15235 RVA: 0x0001DA42 File Offset: 0x0001BC42
		// (set) Token: 0x06003B84 RID: 15236 RVA: 0x0000B528 File Offset: 0x00009728
		[XmlIgnore]
		internal virtual string IdAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void SetUnhandledAttributes(XmlAttribute[] moreAttributes)
		{
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x0000B528 File Offset: 0x00009728
		internal virtual void AddAnnotation(XmlSchemaAnnotation annotation)
		{
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003B87 RID: 15239 RVA: 0x0001DA42 File Offset: 0x0001BC42
		// (set) Token: 0x06003B88 RID: 15240 RVA: 0x0000B528 File Offset: 0x00009728
		[XmlIgnore]
		internal virtual string NameAttribute
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003B89 RID: 15241 RVA: 0x0014E526 File Offset: 0x0014C726
		// (set) Token: 0x06003B8A RID: 15242 RVA: 0x0014E52E File Offset: 0x0014C72E
		[XmlIgnore]
		internal bool IsProcessing
		{
			get
			{
				return this.isProcessing;
			}
			set
			{
				this.isProcessing = value;
			}
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x0014E537 File Offset: 0x0014C737
		internal virtual XmlSchemaObject Clone()
		{
			return (XmlSchemaObject)base.MemberwiseClone();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaObject" /> class.</summary>
		// Token: 0x06003B8C RID: 15244 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlSchemaObject()
		{
		}

		// Token: 0x04002B81 RID: 11137
		private int lineNum;

		// Token: 0x04002B82 RID: 11138
		private int linePos;

		// Token: 0x04002B83 RID: 11139
		private string sourceUri;

		// Token: 0x04002B84 RID: 11140
		private XmlSerializerNamespaces namespaces;

		// Token: 0x04002B85 RID: 11141
		private XmlSchemaObject parent;

		// Token: 0x04002B86 RID: 11142
		private bool isProcessing;
	}
}
