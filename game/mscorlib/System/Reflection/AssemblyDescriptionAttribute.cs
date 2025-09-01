using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Provides a text description for an assembly.</summary>
	// Token: 0x02000885 RID: 2181
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDescriptionAttribute" /> class.</summary>
		/// <param name="description">The assembly description.</param>
		// Token: 0x0600485D RID: 18525 RVA: 0x000EE076 File Offset: 0x000EC276
		public AssemblyDescriptionAttribute(string description)
		{
			this.Description = description;
		}

		/// <summary>Gets assembly description information.</summary>
		/// <returns>A string containing the assembly description.</returns>
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x000EE085 File Offset: 0x000EC285
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
		}

		// Token: 0x04002E53 RID: 11859
		[CompilerGenerated]
		private readonly string <Description>k__BackingField;
	}
}
