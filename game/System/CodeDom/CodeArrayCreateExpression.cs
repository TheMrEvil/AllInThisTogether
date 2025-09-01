using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x0600180B RID: 6155 RVA: 0x0005EEB9 File Offset: 0x0005D0B9
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create.</param>
		/// <param name="initializers">An array of expressions to use to initialize the array.</param>
		// Token: 0x0600180C RID: 6156 RVA: 0x0005EECC File Offset: 0x0005D0CC
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this._createType = createType;
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and initializers.</summary>
		/// <param name="createType">The name of the data type of the array to create.</param>
		/// <param name="initializers">An array of expressions to use to initialize the array.</param>
		// Token: 0x0600180D RID: 6157 RVA: 0x0005EEF2 File Offset: 0x0005D0F2
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initializers.</summary>
		/// <param name="createType">The data type of the array to create.</param>
		/// <param name="initializers">An array of expressions to use to initialize the array.</param>
		// Token: 0x0600180E RID: 6158 RVA: 0x0005EF1D File Offset: 0x0005D11D
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create.</param>
		/// <param name="size">The number of indexes of the array to create.</param>
		// Token: 0x0600180F RID: 6159 RVA: 0x0005EF48 File Offset: 0x0005D148
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this._createType = createType;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create.</param>
		/// <param name="size">The number of indexes of the array to create.</param>
		// Token: 0x06001810 RID: 6160 RVA: 0x0005EF69 File Offset: 0x0005D169
		public CodeArrayCreateExpression(string createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create.</param>
		/// <param name="size">The number of indexes of the array to create.</param>
		// Token: 0x06001811 RID: 6161 RVA: 0x0005EF8F File Offset: 0x0005D18F
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create.</param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create.</param>
		// Token: 0x06001812 RID: 6162 RVA: 0x0005EFB5 File Offset: 0x0005D1B5
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this._createType = createType;
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create.</param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create.</param>
		// Token: 0x06001813 RID: 6163 RVA: 0x0005EFD6 File Offset: 0x0005D1D6
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create.</param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create.</param>
		// Token: 0x06001814 RID: 6164 RVA: 0x0005EFFC File Offset: 0x0005D1FC
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0005F024 File Offset: 0x0005D224
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x0005F04E File Offset: 0x0005D24E
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

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0005F057 File Offset: 0x0005D257
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this._initializers;
			}
		}

		/// <summary>Gets or sets the number of indexes in the array.</summary>
		/// <returns>The number of indexes in the array.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x0005F05F File Offset: 0x0005D25F
		// (set) Token: 0x06001819 RID: 6169 RVA: 0x0005F067 File Offset: 0x0005D267
		public int Size
		{
			[CompilerGenerated]
			get
			{
				return this.<Size>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Size>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005F070 File Offset: 0x0005D270
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x0005F078 File Offset: 0x0005D278
		public CodeExpression SizeExpression
		{
			[CompilerGenerated]
			get
			{
				return this.<SizeExpression>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SizeExpression>k__BackingField = value;
			}
		}

		// Token: 0x04000D47 RID: 3399
		private readonly CodeExpressionCollection _initializers = new CodeExpressionCollection();

		// Token: 0x04000D48 RID: 3400
		private CodeTypeReference _createType;

		// Token: 0x04000D49 RID: 3401
		[CompilerGenerated]
		private int <Size>k__BackingField;

		// Token: 0x04000D4A RID: 3402
		[CompilerGenerated]
		private CodeExpression <SizeExpression>k__BackingField;
	}
}
