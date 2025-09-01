using System;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Specifies that the attributed code should be excluded from code coverage information.</summary>
	// Token: 0x0200028C RID: 652
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false, AllowMultiple = false)]
	public sealed class ExcludeFromCodeCoverageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute" /> class.</summary>
		// Token: 0x060014A9 RID: 5289 RVA: 0x00003D9F File Offset: 0x00001F9F
		public ExcludeFromCodeCoverageAttribute()
		{
		}
	}
}
