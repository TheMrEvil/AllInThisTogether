using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an index of an array.</summary>
	// Token: 0x020002F1 RID: 753
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class.</summary>
		// Token: 0x0600181C RID: 6172 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeArrayIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class using the specified target object and indexes.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the array the indexer targets.</param>
		/// <param name="indices">The index or indexes to reference.</param>
		// Token: 0x0600181D RID: 6173 RVA: 0x0005F081 File Offset: 0x0005D281
		public CodeArrayIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.TargetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object of the array indexer.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the array being indexed.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0005F09C File Offset: 0x0005D29C
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x0005F0A4 File Offset: 0x0005D2A4
		public CodeExpression TargetObject
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetObject>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetObject>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the index or indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0005F0B0 File Offset: 0x0005D2B0
		public CodeExpressionCollection Indices
		{
			get
			{
				CodeExpressionCollection result;
				if ((result = this._indices) == null)
				{
					result = (this._indices = new CodeExpressionCollection());
				}
				return result;
			}
		}

		// Token: 0x04000D4B RID: 3403
		private CodeExpressionCollection _indices;

		// Token: 0x04000D4C RID: 3404
		[CompilerGenerated]
		private CodeExpression <TargetObject>k__BackingField;
	}
}
