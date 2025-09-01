using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x02000310 RID: 784
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x060018F0 RID: 6384 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeFieldReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field.</param>
		/// <param name="fieldName">The name of the field.</param>
		// Token: 0x060018F1 RID: 6385 RVA: 0x0005FC78 File Offset: 0x0005DE78
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0005FC8E File Offset: 0x0005DE8E
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0005FC96 File Offset: 0x0005DE96
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

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005FC9F File Offset: 0x0005DE9F
		// (set) Token: 0x060018F5 RID: 6389 RVA: 0x0005FCB0 File Offset: 0x0005DEB0
		public string FieldName
		{
			get
			{
				return this._fieldName ?? string.Empty;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x04000D8B RID: 3467
		private string _fieldName;

		// Token: 0x04000D8C RID: 3468
		[CompilerGenerated]
		private CodeExpression <TargetObject>k__BackingField;
	}
}
