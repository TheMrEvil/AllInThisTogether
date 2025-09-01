using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a field of a type.</summary>
	// Token: 0x02000317 RID: 791
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class.</summary>
		// Token: 0x0600191B RID: 6427 RVA: 0x0005FE51 File Offset: 0x0005E051
		public CodeMemberField()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">An object that indicates the type of the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x0600191C RID: 6428 RVA: 0x0005FEC5 File Offset: 0x0005E0C5
		public CodeMemberField(CodeTypeReference type, string name)
		{
			this.Type = type;
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x0600191D RID: 6429 RVA: 0x0005FEDB File Offset: 0x0005E0DB
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field.</param>
		/// <param name="name">The name of the field.</param>
		// Token: 0x0600191E RID: 6430 RVA: 0x0005FEF6 File Offset: 0x0005E0F6
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Gets or sets the type of the field.</summary>
		/// <returns>The type of the field.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600191F RID: 6431 RVA: 0x0005FF14 File Offset: 0x0005E114
		// (set) Token: 0x06001920 RID: 6432 RVA: 0x0005FF3E File Offset: 0x0005E13E
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

		/// <summary>Gets or sets the initialization expression for the field.</summary>
		/// <returns>The initialization expression for the field.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001921 RID: 6433 RVA: 0x0005FF47 File Offset: 0x0005E147
		// (set) Token: 0x06001922 RID: 6434 RVA: 0x0005FF4F File Offset: 0x0005E14F
		public CodeExpression InitExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<InitExpression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InitExpression>k__BackingField = value;
			}
		}

		// Token: 0x04000D9B RID: 3483
		private CodeTypeReference _type;

		// Token: 0x04000D9C RID: 3484
		[CompilerGenerated]
		private CodeExpression <InitExpression>k__BackingField;
	}
}
