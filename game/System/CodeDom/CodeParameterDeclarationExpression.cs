using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a parameter declaration for a method, property, or constructor.</summary>
	// Token: 0x02000321 RID: 801
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class.</summary>
		// Token: 0x0600198A RID: 6538 RVA: 0x0005EE88 File Offset: 0x0005D088
		public CodeParameterDeclarationExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">An object that indicates the type of the parameter to declare.</param>
		/// <param name="name">The name of the parameter to declare.</param>
		// Token: 0x0600198B RID: 6539 RVA: 0x00060A1B File Offset: 0x0005EC1B
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare.</param>
		/// <param name="name">The name of the parameter to declare.</param>
		// Token: 0x0600198C RID: 6540 RVA: 0x00060A31 File Offset: 0x0005EC31
		public CodeParameterDeclarationExpression(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare.</param>
		/// <param name="name">The name of the parameter to declare.</param>
		// Token: 0x0600198D RID: 6541 RVA: 0x00060A4C File Offset: 0x0005EC4C
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Gets or sets the custom attributes for the parameter declaration.</summary>
		/// <returns>An object that indicates the custom attributes.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x00060A68 File Offset: 0x0005EC68
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x00060A8D File Offset: 0x0005EC8D
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection result;
				if ((result = this._customAttributes) == null)
				{
					result = (this._customAttributes = new CodeAttributeDeclarationCollection());
				}
				return result;
			}
			set
			{
				this._customAttributes = value;
			}
		}

		/// <summary>Gets or sets the direction of the field.</summary>
		/// <returns>An object that indicates the direction of the field.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x00060A96 File Offset: 0x0005EC96
		// (set) Token: 0x06001991 RID: 6545 RVA: 0x00060A9E File Offset: 0x0005EC9E
		public FieldDirection Direction
		{
			[CompilerGenerated]
			get
			{
				return this.<Direction>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Direction>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the parameter.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x00060AA8 File Offset: 0x0005ECA8
		// (set) Token: 0x06001993 RID: 6547 RVA: 0x00060AD2 File Offset: 0x0005ECD2
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

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001994 RID: 6548 RVA: 0x00060ADB File Offset: 0x0005ECDB
		// (set) Token: 0x06001995 RID: 6549 RVA: 0x00060AEC File Offset: 0x0005ECEC
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x04000DC7 RID: 3527
		private CodeTypeReference _type;

		// Token: 0x04000DC8 RID: 3528
		private string _name;

		// Token: 0x04000DC9 RID: 3529
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000DCA RID: 3530
		[CompilerGenerated]
		private FieldDirection <Direction>k__BackingField;
	}
}
