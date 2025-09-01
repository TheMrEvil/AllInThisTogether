using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Suppresses reporting of a specific static analysis tool rule violation, allowing multiple suppressions on a single code artifact.</summary>
	// Token: 0x02000A02 RID: 2562
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> class, specifying the category of the static analysis tool and the identifier for an analysis rule.</summary>
		/// <param name="category">The category for the attribute.</param>
		/// <param name="checkId">The identifier of the analysis tool rule the attribute applies to.</param>
		// Token: 0x06005B4E RID: 23374 RVA: 0x00134925 File Offset: 0x00132B25
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		/// <summary>Gets the category identifying the classification of the attribute.</summary>
		/// <returns>The category identifying the attribute.</returns>
		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x0013493B File Offset: 0x00132B3B
		public string Category
		{
			[CompilerGenerated]
			get
			{
				return this.<Category>k__BackingField;
			}
		}

		/// <summary>Gets the identifier of the static analysis tool rule to be suppressed.</summary>
		/// <returns>The identifier of the static analysis tool rule to be suppressed.</returns>
		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005B50 RID: 23376 RVA: 0x00134943 File Offset: 0x00132B43
		public string CheckId
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckId>k__BackingField;
			}
		}

		/// <summary>Gets or sets the scope of the code that is relevant for the attribute.</summary>
		/// <returns>The scope of the code that is relevant for the attribute.</returns>
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x0013494B File Offset: 0x00132B4B
		// (set) Token: 0x06005B52 RID: 23378 RVA: 0x00134953 File Offset: 0x00132B53
		public string Scope
		{
			[CompilerGenerated]
			get
			{
				return this.<Scope>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Scope>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a fully qualified path that represents the target of the attribute.</summary>
		/// <returns>A fully qualified path that represents the target of the attribute.</returns>
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x0013495C File Offset: 0x00132B5C
		// (set) Token: 0x06005B54 RID: 23380 RVA: 0x00134964 File Offset: 0x00132B64
		public string Target
		{
			[CompilerGenerated]
			get
			{
				return this.<Target>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Target>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets an optional argument expanding on exclusion criteria.</summary>
		/// <returns>A string containing the expanded exclusion criteria.</returns>
		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005B55 RID: 23381 RVA: 0x0013496D File Offset: 0x00132B6D
		// (set) Token: 0x06005B56 RID: 23382 RVA: 0x00134975 File Offset: 0x00132B75
		public string MessageId
		{
			[CompilerGenerated]
			get
			{
				return this.<MessageId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MessageId>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the justification for suppressing the code analysis message.</summary>
		/// <returns>The justification for suppressing the message.</returns>
		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005B57 RID: 23383 RVA: 0x0013497E File Offset: 0x00132B7E
		// (set) Token: 0x06005B58 RID: 23384 RVA: 0x00134986 File Offset: 0x00132B86
		public string Justification
		{
			[CompilerGenerated]
			get
			{
				return this.<Justification>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Justification>k__BackingField = value;
			}
		}

		// Token: 0x0400384C RID: 14412
		[CompilerGenerated]
		private readonly string <Category>k__BackingField;

		// Token: 0x0400384D RID: 14413
		[CompilerGenerated]
		private readonly string <CheckId>k__BackingField;

		// Token: 0x0400384E RID: 14414
		[CompilerGenerated]
		private string <Scope>k__BackingField;

		// Token: 0x0400384F RID: 14415
		[CompilerGenerated]
		private string <Target>k__BackingField;

		// Token: 0x04003850 RID: 14416
		[CompilerGenerated]
		private string <MessageId>k__BackingField;

		// Token: 0x04003851 RID: 14417
		[CompilerGenerated]
		private string <Justification>k__BackingField;
	}
}
