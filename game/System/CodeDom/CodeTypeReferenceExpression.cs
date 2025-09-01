using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a data type.</summary>
	// Token: 0x0200033B RID: 827
	[Serializable]
	public class CodeTypeReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class.</summary>
		// Token: 0x06001A48 RID: 6728 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeTypeReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified type.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference.</param>
		// Token: 0x06001A49 RID: 6729 RVA: 0x000616E8 File Offset: 0x0005F8E8
		public CodeTypeReferenceExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type name.</summary>
		/// <param name="type">The name of the data type to reference.</param>
		// Token: 0x06001A4A RID: 6730 RVA: 0x000616F7 File Offset: 0x0005F8F7
		public CodeTypeReferenceExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type.</summary>
		/// <param name="type">An instance of the data type to reference.</param>
		// Token: 0x06001A4B RID: 6731 RVA: 0x0006170B File Offset: 0x0005F90B
		public CodeTypeReferenceExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x00061720 File Offset: 0x0005F920
		// (set) Token: 0x06001A4D RID: 6733 RVA: 0x0006174A File Offset: 0x0005F94A
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

		// Token: 0x04000DFC RID: 3580
		private CodeTypeReference _type;
	}
}
