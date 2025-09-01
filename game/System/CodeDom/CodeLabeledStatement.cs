using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a labeled statement or a stand-alone label.</summary>
	// Token: 0x02000314 RID: 788
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class.</summary>
		// Token: 0x06001908 RID: 6408 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeLabeledStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label.</param>
		// Token: 0x06001909 RID: 6409 RVA: 0x0005FDC0 File Offset: 0x0005DFC0
		public CodeLabeledStatement(string label)
		{
			this._label = label;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name and statement.</summary>
		/// <param name="label">The name of the label.</param>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to associate with the label.</param>
		// Token: 0x0600190A RID: 6410 RVA: 0x0005FDCF File Offset: 0x0005DFCF
		public CodeLabeledStatement(string label, CodeStatement statement)
		{
			this._label = label;
			this.Statement = statement;
		}

		/// <summary>Gets or sets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x0005FDE5 File Offset: 0x0005DFE5
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0005FDF6 File Offset: 0x0005DFF6
		public string Label
		{
			get
			{
				return this._label ?? string.Empty;
			}
			set
			{
				this._label = value;
			}
		}

		/// <summary>Gets or sets the optional associated statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement associated with the label.</returns>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0005FDFF File Offset: 0x0005DFFF
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x0005FE07 File Offset: 0x0005E007
		public CodeStatement Statement
		{
			[CompilerGenerated]
			get
			{
				return this.<Statement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Statement>k__BackingField = value;
			}
		}

		// Token: 0x04000D94 RID: 3476
		private string _label;

		// Token: 0x04000D95 RID: 3477
		[CompilerGenerated]
		private CodeStatement <Statement>k__BackingField;
	}
}
