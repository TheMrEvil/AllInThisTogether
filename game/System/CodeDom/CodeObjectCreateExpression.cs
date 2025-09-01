using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a new instance of a type.</summary>
	// Token: 0x02000320 RID: 800
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class.</summary>
		// Token: 0x06001983 RID: 6531 RVA: 0x00060950 File Offset: 0x0005EB50
		public CodeObjectCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the object to create.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object.</param>
		// Token: 0x06001984 RID: 6532 RVA: 0x00060963 File Offset: 0x0005EB63
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.CreateType = createType;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The name of the data type of object to create.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object.</param>
		// Token: 0x06001985 RID: 6533 RVA: 0x00060989 File Offset: 0x0005EB89
		public CodeObjectCreateExpression(string createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The data type of the object to create.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object.</param>
		// Token: 0x06001986 RID: 6534 RVA: 0x000609B4 File Offset: 0x0005EBB4
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the data type of the object to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> to the data type of the object to create.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x000609E0 File Offset: 0x0005EBE0
		// (set) Token: 0x06001988 RID: 6536 RVA: 0x00060A0A File Offset: 0x0005EC0A
		public CodeTypeReference CreateType
		{
			get
			{
				CodeTypeReference result;
				if ((result = this._createType) == null)
				{
					result = (this._createType = new CodeTypeReference(""));
				}
				return result;
			}
			set
			{
				this._createType = value;
			}
		}

		/// <summary>Gets or sets the parameters to use in creating the object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to use when creating the object.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001989 RID: 6537 RVA: 0x00060A13 File Offset: 0x0005EC13
		public CodeExpressionCollection Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
		} = new CodeExpressionCollection();

		// Token: 0x04000DC5 RID: 3525
		private CodeTypeReference _createType;

		// Token: 0x04000DC6 RID: 3526
		[CompilerGenerated]
		private readonly CodeExpressionCollection <Parameters>k__BackingField;
	}
}
