using System;
using System.Runtime.CompilerServices;

namespace System.CodeDom
{
	/// <summary>Provides a base class for a member of a type. Type members include fields, methods, properties, constructors and nested types.</summary>
	// Token: 0x02000336 RID: 822
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		/// <summary>Gets or sets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000613C3 File Offset: 0x0005F5C3
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x000613D4 File Offset: 0x0005F5D4
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

		/// <summary>Gets or sets the attributes of the member.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.MemberAttributes" /> values used to indicate the attributes of the member. The default value is <see cref="F:System.CodeDom.MemberAttributes.Private" /> | <see cref="F:System.CodeDom.MemberAttributes.Final" />.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000613DD File Offset: 0x0005F5DD
		// (set) Token: 0x06001A16 RID: 6678 RVA: 0x000613E5 File Offset: 0x0005F5E5
		public MemberAttributes Attributes
		{
			[CompilerGenerated]
			get
			{
				return this.<Attributes>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Attributes>k__BackingField = value;
			}
		} = (MemberAttributes)20482;

		/// <summary>Gets or sets the custom attributes of the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the member.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000613F0 File Offset: 0x0005F5F0
		// (set) Token: 0x06001A18 RID: 6680 RVA: 0x00061415 File Offset: 0x0005F615
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

		/// <summary>Gets or sets the line on which the type member statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the location of the type member declaration.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0006141E File Offset: 0x0005F61E
		// (set) Token: 0x06001A1A RID: 6682 RVA: 0x00061426 File Offset: 0x0005F626
		public CodeLinePragma LinePragma
		{
			[CompilerGenerated]
			get
			{
				return this.<LinePragma>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LinePragma>k__BackingField = value;
			}
		}

		/// <summary>Gets the collection of comments for the type member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the member.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0006142F File Offset: 0x0005F62F
		public CodeCommentStatementCollection Comments
		{
			[CompilerGenerated]
			get
			{
				return this.<Comments>k__BackingField;
			}
		} = new CodeCommentStatementCollection();

		/// <summary>Gets the start directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00061438 File Offset: 0x0005F638
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection result;
				if ((result = this._startDirectives) == null)
				{
					result = (this._startDirectives = new CodeDirectiveCollection());
				}
				return result;
			}
		}

		/// <summary>Gets the end directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00061460 File Offset: 0x0005F660
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection result;
				if ((result = this._endDirectives) == null)
				{
					result = (this._endDirectives = new CodeDirectiveCollection());
				}
				return result;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeMember" /> class.</summary>
		// Token: 0x06001A1E RID: 6686 RVA: 0x00061485 File Offset: 0x0005F685
		public CodeTypeMember()
		{
		}

		// Token: 0x04000DF0 RID: 3568
		private string _name;

		// Token: 0x04000DF1 RID: 3569
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000DF2 RID: 3570
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000DF3 RID: 3571
		private CodeDirectiveCollection _endDirectives;

		// Token: 0x04000DF4 RID: 3572
		[CompilerGenerated]
		private MemberAttributes <Attributes>k__BackingField;

		// Token: 0x04000DF5 RID: 3573
		[CompilerGenerated]
		private CodeLinePragma <LinePragma>k__BackingField;

		// Token: 0x04000DF6 RID: 3574
		[CompilerGenerated]
		private readonly CodeCommentStatementCollection <Comments>k__BackingField;
	}
}
