using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a default value.</summary>
	// Token: 0x02000305 RID: 773
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class.</summary>
		// Token: 0x060018B1 RID: 6321 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeDefaultValueExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class using the specified code type reference.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that specifies the reference to a value type.</param>
		// Token: 0x060018B2 RID: 6322 RVA: 0x0005F967 File Offset: 0x0005DB67
		public CodeDefaultValueExpression(CodeTypeReference type)
		{
			this._type = type;
		}

		/// <summary>Gets or sets the data type reference for a default value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> object representing a data type that has a default value.</returns>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0005F978 File Offset: 0x0005DB78
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x0005F9A2 File Offset: 0x0005DBA2
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference result;
				if ((result = this._type) == null)
				{
					result = (this._type = new CodeTypeReference(""));
				}
				return result;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x04000D80 RID: 3456
		private CodeTypeReference _type;
	}
}
