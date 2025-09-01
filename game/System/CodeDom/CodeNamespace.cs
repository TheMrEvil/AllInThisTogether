using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.CodeDom
{
	/// <summary>Represents a namespace declaration.</summary>
	// Token: 0x0200031C RID: 796
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Comments" /> collection is accessed.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600194A RID: 6474 RVA: 0x000603F4 File Offset: 0x0005E5F4
		// (remove) Token: 0x0600194B RID: 6475 RVA: 0x0006042C File Offset: 0x0005E62C
		public event EventHandler PopulateComments
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.PopulateComments;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateComments, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.PopulateComments;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateComments, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Imports" /> collection is accessed.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600194C RID: 6476 RVA: 0x00060464 File Offset: 0x0005E664
		// (remove) Token: 0x0600194D RID: 6477 RVA: 0x0006049C File Offset: 0x0005E69C
		public event EventHandler PopulateImports
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.PopulateImports;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateImports, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.PopulateImports;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateImports, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Types" /> collection is accessed.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600194E RID: 6478 RVA: 0x000604D4 File Offset: 0x0005E6D4
		// (remove) Token: 0x0600194F RID: 6479 RVA: 0x0006050C File Offset: 0x0005E70C
		public event EventHandler PopulateTypes
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.PopulateTypes;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateTypes, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.PopulateTypes;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.PopulateTypes, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class.</summary>
		// Token: 0x06001950 RID: 6480 RVA: 0x00060541 File Offset: 0x0005E741
		public CodeNamespace()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class using the specified name.</summary>
		/// <param name="name">The name of the namespace being declared.</param>
		// Token: 0x06001951 RID: 6481 RVA: 0x0006056A File Offset: 0x0005E76A
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the collection of types that the namespace contains.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> that indicates the types contained in the namespace.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x0006059A File Offset: 0x0005E79A
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateTypes = this.PopulateTypes;
					if (populateTypes != null)
					{
						populateTypes(this, EventArgs.Empty);
					}
				}
				return this._classes;
			}
		}

		/// <summary>Gets the collection of namespace import directives used by the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> that indicates the namespace import directives used by the namespace.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x000605D1 File Offset: 0x0005E7D1
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateImports = this.PopulateImports;
					if (populateImports != null)
					{
						populateImports(this, EventArgs.Empty);
					}
				}
				return this._imports;
			}
		}

		/// <summary>Gets or sets the name of the namespace.</summary>
		/// <returns>The name of the namespace.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00060608 File Offset: 0x0005E808
		// (set) Token: 0x06001955 RID: 6485 RVA: 0x00060619 File Offset: 0x0005E819
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets the comments for the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the namespace.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00060622 File Offset: 0x0005E822
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateComments = this.PopulateComments;
					if (populateComments != null)
					{
						populateComments(this, EventArgs.Empty);
					}
				}
				return this._comments;
			}
		}

		// Token: 0x04000DB6 RID: 3510
		private string _name;

		// Token: 0x04000DB7 RID: 3511
		private readonly CodeNamespaceImportCollection _imports = new CodeNamespaceImportCollection();

		// Token: 0x04000DB8 RID: 3512
		private readonly CodeCommentStatementCollection _comments = new CodeCommentStatementCollection();

		// Token: 0x04000DB9 RID: 3513
		private readonly CodeTypeDeclarationCollection _classes = new CodeTypeDeclarationCollection();

		// Token: 0x04000DBA RID: 3514
		private int _populated;

		// Token: 0x04000DBB RID: 3515
		private const int ImportsCollection = 1;

		// Token: 0x04000DBC RID: 3516
		private const int CommentsCollection = 2;

		// Token: 0x04000DBD RID: 3517
		private const int TypesCollection = 4;

		// Token: 0x04000DBE RID: 3518
		[CompilerGenerated]
		private EventHandler PopulateComments;

		// Token: 0x04000DBF RID: 3519
		[CompilerGenerated]
		private EventHandler PopulateImports;

		// Token: 0x04000DC0 RID: 3520
		[CompilerGenerated]
		private EventHandler PopulateTypes;
	}
}
