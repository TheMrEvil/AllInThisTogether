using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression cast to a data type or interface.</summary>
	// Token: 0x020002FB RID: 763
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class.</summary>
		// Token: 0x06001861 RID: 6241 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeCastExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type of the cast.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast.</param>
		// Token: 0x06001862 RID: 6242 RVA: 0x0005F430 File Offset: 0x0005D630
		public CodeCastExpression(CodeTypeReference targetType, CodeExpression expression)
		{
			this.TargetType = targetType;
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The name of the destination type of the cast.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast.</param>
		// Token: 0x06001863 RID: 6243 RVA: 0x0005F446 File Offset: 0x0005D646
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The destination data type of the cast.</param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast.</param>
		// Token: 0x06001864 RID: 6244 RVA: 0x0005F461 File Offset: 0x0005D661
		public CodeCastExpression(Type targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Gets or sets the destination type of the cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type to cast to.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0005F47C File Offset: 0x0005D67C
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x0005F4A6 File Offset: 0x0005D6A6
		public CodeTypeReference TargetType
		{
			get
			{
				CodeTypeReference result;
				if ((result = this._targetType) == null)
				{
					result = (this._targetType = new CodeTypeReference(""));
				}
				return result;
			}
			set
			{
				this._targetType = value;
			}
		}

		/// <summary>Gets or sets the expression to cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code to cast.</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005F4AF File Offset: 0x0005D6AF
		// (set) Token: 0x06001868 RID: 6248 RVA: 0x0005F4B7 File Offset: 0x0005D6B7
		public CodeExpression Expression
		{
			[CompilerGenerated]
			get
			{
				return this.<Expression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Expression>k__BackingField = value;
			}
		}

		// Token: 0x04000D6B RID: 3435
		private CodeTypeReference _targetType;

		// Token: 0x04000D6C RID: 3436
		[CompilerGenerated]
		private CodeExpression <Expression>k__BackingField;
	}
}
