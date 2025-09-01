using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents an argument used in a metadata attribute declaration.</summary>
	// Token: 0x020002F4 RID: 756
	[Serializable]
	public class CodeAttributeArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class.</summary>
		// Token: 0x0600182E RID: 6190 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeAttributeArgument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified value.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument.</param>
		// Token: 0x0600182F RID: 6191 RVA: 0x0005F17B File Offset: 0x0005D37B
		public CodeAttributeArgument(CodeExpression value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified name and value.</summary>
		/// <param name="name">The name of the attribute property the argument applies to.</param>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument.</param>
		// Token: 0x06001830 RID: 6192 RVA: 0x0005F18A File Offset: 0x0005D38A
		public CodeAttributeArgument(string name, CodeExpression value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute property the argument is for.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0005F1A0 File Offset: 0x0005D3A0
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x0005F1B1 File Offset: 0x0005D3B1
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

		/// <summary>Gets or sets the value for the attribute argument.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value for the attribute argument.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005F1BA File Offset: 0x0005D3BA
		// (set) Token: 0x06001834 RID: 6196 RVA: 0x0005F1C2 File Offset: 0x0005D3C2
		public CodeExpression Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x04000D51 RID: 3409
		private string _name;

		// Token: 0x04000D52 RID: 3410
		[CompilerGenerated]
		private CodeExpression <Value>k__BackingField;
	}
}
