using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Specifies a description for an assembly.</summary>
	// Token: 0x0200088F RID: 2191
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTitleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyTitleAttribute" /> class.</summary>
		/// <param name="title">The assembly title.</param>
		// Token: 0x06004874 RID: 18548 RVA: 0x000EE171 File Offset: 0x000EC371
		public AssemblyTitleAttribute(string title)
		{
			this.Title = title;
		}

		/// <summary>Gets assembly title information.</summary>
		/// <returns>The assembly title.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x000EE180 File Offset: 0x000EC380
		public string Title
		{
			[CompilerGenerated]
			get
			{
				return this.<Title>k__BackingField;
			}
		}

		// Token: 0x04002E64 RID: 11876
		[CompilerGenerated]
		private readonly string <Title>k__BackingField;
	}
}
