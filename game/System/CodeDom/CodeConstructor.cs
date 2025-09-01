using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x02000304 RID: 772
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x060018AE RID: 6318 RVA: 0x0005F92E File Offset: 0x0005DB2E
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0005F957 File Offset: 0x0005DB57
		public CodeExpressionCollection BaseConstructorArgs
		{
			[CompilerGenerated]
			get
			{
				return this.<BaseConstructorArgs>k__BackingField;
			}
		} = new CodeExpressionCollection();

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0005F95F File Offset: 0x0005DB5F
		public CodeExpressionCollection ChainedConstructorArgs
		{
			[CompilerGenerated]
			get
			{
				return this.<ChainedConstructorArgs>k__BackingField;
			}
		} = new CodeExpressionCollection();

		// Token: 0x04000D7E RID: 3454
		[CompilerGenerated]
		private readonly CodeExpressionCollection <BaseConstructorArgs>k__BackingField;

		// Token: 0x04000D7F RID: 3455
		[CompilerGenerated]
		private readonly CodeExpressionCollection <ChainedConstructorArgs>k__BackingField;
	}
}
