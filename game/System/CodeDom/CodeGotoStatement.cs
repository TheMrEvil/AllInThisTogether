using System;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="goto" /> statement.</summary>
	// Token: 0x02000311 RID: 785
	[Serializable]
	public class CodeGotoStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class.</summary>
		// Token: 0x060018F6 RID: 6390 RVA: 0x0005F0D5 File Offset: 0x0005D2D5
		public CodeGotoStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label at which to continue program execution.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Label" /> is <see langword="null" />.</exception>
		// Token: 0x060018F7 RID: 6391 RVA: 0x0005FCB9 File Offset: 0x0005DEB9
		public CodeGotoStatement(string label)
		{
			this.Label = label;
		}

		/// <summary>Gets or sets the name of the label at which to continue program execution.</summary>
		/// <returns>A string that indicates the name of the label at which to continue program execution.</returns>
		/// <exception cref="T:System.ArgumentNullException">The label cannot be set because <paramref name="value" /> is <see langword="null" /> or an empty string.</exception>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x0005FCC8 File Offset: 0x0005DEC8
		// (set) Token: 0x060018F9 RID: 6393 RVA: 0x0005FCD0 File Offset: 0x0005DED0
		public string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this._label = value;
			}
		}

		// Token: 0x04000D8D RID: 3469
		private string _label;
	}
}
