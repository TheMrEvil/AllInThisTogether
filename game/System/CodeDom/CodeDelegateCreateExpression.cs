using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		// Token: 0x060018B5 RID: 6325 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeDelegateCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		/// <param name="delegateType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</param>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</param>
		/// <param name="methodName">The name of the event-handler method.</param>
		// Token: 0x060018B6 RID: 6326 RVA: 0x0005F9AB File Offset: 0x0005DBAB
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this._delegateType = delegateType;
			this.TargetObject = targetObject;
			this._methodName = methodName;
		}

		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005F9C8 File Offset: 0x0005DBC8
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x0005F9F2 File Offset: 0x0005DBF2
		public CodeTypeReference DelegateType
		{
			get
			{
				CodeTypeReference result;
				if ((result = this._delegateType) == null)
				{
					result = (this._delegateType = new CodeTypeReference(""));
				}
				return result;
			}
			set
			{
				this._delegateType = value;
			}
		}

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0005F9FB File Offset: 0x0005DBFB
		// (set) Token: 0x060018BA RID: 6330 RVA: 0x0005FA03 File Offset: 0x0005DC03
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

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0005FA0C File Offset: 0x0005DC0C
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x0005FA1D File Offset: 0x0005DC1D
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
			set
			{
				this._methodName = value;
			}
		}

		// Token: 0x04000D81 RID: 3457
		private CodeTypeReference _delegateType;

		// Token: 0x04000D82 RID: 3458
		private string _methodName;

		// Token: 0x04000D83 RID: 3459
		[CompilerGenerated]
		private CodeExpression <TargetObject>k__BackingField;
	}
}
