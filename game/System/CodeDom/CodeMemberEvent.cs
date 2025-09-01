using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x02000316 RID: 790
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberEvent" /> class.</summary>
		// Token: 0x06001915 RID: 6421 RVA: 0x0005FE51 File Offset: 0x0005E051
		public CodeMemberEvent()
		{
		}

		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x0005FE5C File Offset: 0x0005E05C
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x0005FE86 File Offset: 0x0005E086
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

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x0005FE8F File Offset: 0x0005E08F
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x0005FE97 File Offset: 0x0005E097
		public CodeTypeReference PrivateImplementationType
		{
			[CompilerGenerated]
			get
			{
				return this.<PrivateImplementationType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrivateImplementationType>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x0005FEA0 File Offset: 0x0005E0A0
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection result;
				if ((result = this._implementationTypes) == null)
				{
					result = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return result;
			}
		}

		// Token: 0x04000D98 RID: 3480
		private CodeTypeReference _type;

		// Token: 0x04000D99 RID: 3481
		private CodeTypeReferenceCollection _implementationTypes;

		// Token: 0x04000D9A RID: 3482
		[CompilerGenerated]
		private CodeTypeReference <PrivateImplementationType>k__BackingField;
	}
}
