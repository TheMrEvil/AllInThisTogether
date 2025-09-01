using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x0200031E RID: 798
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x06001964 RID: 6500 RVA: 0x0005F685 File Offset: 0x0005D885
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import.</param>
		// Token: 0x06001965 RID: 6501 RVA: 0x000606FC File Offset: 0x0005E8FC
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0006070B File Offset: 0x0005E90B
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x00060713 File Offset: 0x0005E913
		public CodeLinePragma LinePragma
		{
			[CompilerGenerated]
			get
			{
				return this.<LinePragma>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LinePragma>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0006071C File Offset: 0x0005E91C
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0006072D File Offset: 0x0005E92D
		public string Namespace
		{
			get
			{
				return this._nameSpace ?? string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		// Token: 0x04000DC1 RID: 3521
		private string _nameSpace;

		// Token: 0x04000DC2 RID: 3522
		[CompilerGenerated]
		private CodeLinePragma <LinePragma>k__BackingField;
	}
}
