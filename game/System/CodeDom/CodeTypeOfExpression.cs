using System;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="typeof" /> expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x02000338 RID: 824
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06001A2C RID: 6700 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the <see langword="typeof" /> expression.</param>
		// Token: 0x06001A2D RID: 6701 RVA: 0x00061544 File Offset: 0x0005F744
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the <see langword="typeof" /> expression.</param>
		// Token: 0x06001A2E RID: 6702 RVA: 0x00061553 File Offset: 0x0005F753
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The data type of the data type of the <see langword="typeof" /> expression.</param>
		// Token: 0x06001A2F RID: 6703 RVA: 0x00061567 File Offset: 0x0005F767
		public CodeTypeOfExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the <see langword="typeof" /> expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the <see langword="typeof" /> expression. This property will never return <see langword="null" />, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0006157C File Offset: 0x0005F77C
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x000615A6 File Offset: 0x0005F7A6
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

		// Token: 0x04000DF7 RID: 3575
		private CodeTypeReference _type;
	}
}
