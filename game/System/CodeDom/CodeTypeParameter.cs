using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x02000339 RID: 825
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class.</summary>
		// Token: 0x06001A32 RID: 6706 RVA: 0x0005F685 File Offset: 0x0005D885
		public CodeTypeParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class with the specified type parameter name.</summary>
		/// <param name="name">The name of the type parameter.</param>
		// Token: 0x06001A33 RID: 6707 RVA: 0x000615AF File Offset: 0x0005F7AF
		public CodeTypeParameter(string name)
		{
			this._name = name;
		}

		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x000615BE File Offset: 0x0005F7BE
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x000615CF File Offset: 0x0005F7CF
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

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x000615D8 File Offset: 0x0005F7D8
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				CodeTypeReferenceCollection result;
				if ((result = this._constraints) == null)
				{
					result = (this._constraints = new CodeTypeReferenceCollection());
				}
				return result;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is <see langword="null" />.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00061600 File Offset: 0x0005F800
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
		}

		/// <summary>Gets or sets a value indicating whether the type parameter has a constructor constraint.</summary>
		/// <returns>
		///   <see langword="true" /> if the type parameter has a constructor constraint; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00061625 File Offset: 0x0005F825
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x0006162D File Offset: 0x0005F82D
		public bool HasConstructorConstraint
		{
			[CompilerGenerated]
			get
			{
				return this.<HasConstructorConstraint>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasConstructorConstraint>k__BackingField = value;
			}
		}

		// Token: 0x04000DF8 RID: 3576
		private string _name;

		// Token: 0x04000DF9 RID: 3577
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000DFA RID: 3578
		private CodeTypeReferenceCollection _constraints;

		// Token: 0x04000DFB RID: 3579
		[CompilerGenerated]
		private bool <HasConstructorConstraint>k__BackingField;
	}
}
