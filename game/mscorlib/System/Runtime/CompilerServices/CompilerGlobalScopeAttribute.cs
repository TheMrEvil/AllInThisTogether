using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a class should be treated as if it has global scope.</summary>
	// Token: 0x020007E3 RID: 2019
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class CompilerGlobalScopeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CompilerGlobalScopeAttribute" /> class.</summary>
		// Token: 0x060045D8 RID: 17880 RVA: 0x00002050 File Offset: 0x00000250
		public CompilerGlobalScopeAttribute()
		{
		}
	}
}
