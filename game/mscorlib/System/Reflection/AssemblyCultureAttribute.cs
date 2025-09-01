using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Specifies which culture the assembly supports.</summary>
	// Token: 0x02000882 RID: 2178
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCultureAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCultureAttribute" /> class with the culture supported by the assembly being attributed.</summary>
		/// <param name="culture">The culture supported by the attributed assembly.</param>
		// Token: 0x06004857 RID: 18519 RVA: 0x000EE031 File Offset: 0x000EC231
		public AssemblyCultureAttribute(string culture)
		{
			this.Culture = culture;
		}

		/// <summary>Gets the supported culture of the attributed assembly.</summary>
		/// <returns>A string containing the name of the supported culture.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x000EE040 File Offset: 0x000EC240
		public string Culture
		{
			[CompilerGenerated]
			get
			{
				return this.<Culture>k__BackingField;
			}
		}

		// Token: 0x04002E50 RID: 11856
		[CompilerGenerated]
		private readonly string <Culture>k__BackingField;
	}
}
