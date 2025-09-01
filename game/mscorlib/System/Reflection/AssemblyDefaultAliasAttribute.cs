using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Defines a friendly default alias for an assembly manifest.</summary>
	// Token: 0x02000883 RID: 2179
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyDefaultAliasAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyDefaultAliasAttribute" /> class.</summary>
		/// <param name="defaultAlias">The assembly default alias information.</param>
		// Token: 0x06004859 RID: 18521 RVA: 0x000EE048 File Offset: 0x000EC248
		public AssemblyDefaultAliasAttribute(string defaultAlias)
		{
			this.DefaultAlias = defaultAlias;
		}

		/// <summary>Gets default alias information.</summary>
		/// <returns>A string containing the default alias information.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x000EE057 File Offset: 0x000EC257
		public string DefaultAlias
		{
			[CompilerGenerated]
			get
			{
				return this.<DefaultAlias>k__BackingField;
			}
		}

		// Token: 0x04002E51 RID: 11857
		[CompilerGenerated]
		private readonly string <DefaultAlias>k__BackingField;
	}
}
