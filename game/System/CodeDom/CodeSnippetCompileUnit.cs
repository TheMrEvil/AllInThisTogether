using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x02000329 RID: 809
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		// Token: 0x060019BB RID: 6587 RVA: 0x00060CAB File Offset: 0x0005EEAB
		public CodeSnippetCompileUnit()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		/// <param name="value">The literal code fragment to represent.</param>
		// Token: 0x060019BC RID: 6588 RVA: 0x00060CB3 File Offset: 0x0005EEB3
		public CodeSnippetCompileUnit(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment to represent.</summary>
		/// <returns>The literal code fragment.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x00060CC2 File Offset: 0x0005EEC2
		// (set) Token: 0x060019BE RID: 6590 RVA: 0x00060CD3 File Offset: 0x0005EED3
		public string Value
		{
			get
			{
				return this._value ?? string.Empty;
			}
			set
			{
				this._value = value;
			}
		}

		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00060CDC File Offset: 0x0005EEDC
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x00060CE4 File Offset: 0x0005EEE4
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

		// Token: 0x04000DD6 RID: 3542
		private string _value;

		// Token: 0x04000DD7 RID: 3543
		[CompilerGenerated]
		private CodeLinePragma <LinePragma>k__BackingField;
	}
}
