using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x02000340 RID: 832
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06001A5F RID: 6751 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target.</param>
		/// <param name="methodName">The name of the method to call.</param>
		// Token: 0x06001A60 RID: 6752 RVA: 0x00061888 File Offset: 0x0005FA88
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object, method name, and generic type arguments.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target.</param>
		/// <param name="methodName">The name of the method to call.</param>
		/// <param name="typeParameters">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values that specify the <see cref="P:System.CodeDom.CodeMethodReferenceExpression.TypeArguments" /> for this <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</param>
		// Token: 0x06001A61 RID: 6753 RVA: 0x0006189E File Offset: 0x0005FA9E
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
			if (typeParameters != null && typeParameters.Length != 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x000618C7 File Offset: 0x0005FAC7
		// (set) Token: 0x06001A63 RID: 6755 RVA: 0x000618CF File Offset: 0x0005FACF
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

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x000618D8 File Offset: 0x0005FAD8
		// (set) Token: 0x06001A65 RID: 6757 RVA: 0x000618E9 File Offset: 0x0005FAE9
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

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x000618F4 File Offset: 0x0005FAF4
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				CodeTypeReferenceCollection result;
				if ((result = this._typeArguments) == null)
				{
					result = (this._typeArguments = new CodeTypeReferenceCollection());
				}
				return result;
			}
		}

		// Token: 0x04000E16 RID: 3606
		private string _methodName;

		// Token: 0x04000E17 RID: 3607
		private CodeTypeReferenceCollection _typeArguments;

		// Token: 0x04000E18 RID: 3608
		[CompilerGenerated]
		private CodeExpression <TargetObject>k__BackingField;
	}
}
