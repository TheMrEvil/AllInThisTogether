using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Distinguishes a compiler-generated element from a user-generated element. This class cannot be inherited.</summary>
	// Token: 0x020007E2 RID: 2018
	[AttributeUsage(AttributeTargets.All, Inherited = true)]
	[Serializable]
	public sealed class CompilerGeneratedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CompilerGeneratedAttribute" /> class.</summary>
		// Token: 0x060045D7 RID: 17879 RVA: 0x00002050 File Offset: 0x00000250
		public CompilerGeneratedAttribute()
		{
		}
	}
}
