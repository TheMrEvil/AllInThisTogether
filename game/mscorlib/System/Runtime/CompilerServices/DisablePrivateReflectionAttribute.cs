using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that any private members contained in an assembly's types are not available to reflection.</summary>
	// Token: 0x020007EE RID: 2030
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class DisablePrivateReflectionAttribute : Attribute
	{
		/// <summary>Initializes a new instances of the <see cref="T:System.Runtime.CompilerServices.DisablePrivateReflectionAttribute" /> class.</summary>
		// Token: 0x060045F8 RID: 17912 RVA: 0x00002050 File Offset: 0x00000250
		public DisablePrivateReflectionAttribute()
		{
		}
	}
}
