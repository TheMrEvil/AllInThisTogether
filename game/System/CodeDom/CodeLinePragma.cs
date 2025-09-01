using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a specific location within a specific file.</summary>
	// Token: 0x02000315 RID: 789
	[Serializable]
	public class CodeLinePragma
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		// Token: 0x0600190F RID: 6415 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeLinePragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLinePragma" /> class.</summary>
		/// <param name="fileName">The file name of the associated file.</param>
		/// <param name="lineNumber">The line number to store a reference to.</param>
		// Token: 0x06001910 RID: 6416 RVA: 0x0005FE10 File Offset: 0x0005E010
		public CodeLinePragma(string fileName, int lineNumber)
		{
			this.FileName = fileName;
			this.LineNumber = lineNumber;
		}

		/// <summary>Gets or sets the name of the associated file.</summary>
		/// <returns>The file name of the associated file.</returns>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0005FE26 File Offset: 0x0005E026
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x0005FE37 File Offset: 0x0005E037
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets the line number of the associated reference.</summary>
		/// <returns>The line number.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x0005FE40 File Offset: 0x0005E040
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x0005FE48 File Offset: 0x0005E048
		public int LineNumber
		{
			[CompilerGenerated]
			get
			{
				return this.<LineNumber>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LineNumber>k__BackingField = value;
			}
		}

		// Token: 0x04000D96 RID: 3478
		private string _fileName;

		// Token: 0x04000D97 RID: 3479
		[CompilerGenerated]
		private int <LineNumber>k__BackingField;
	}
}
