using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies that a type contains an unmanaged array that might potentially overflow. This class cannot be inherited.</summary>
	// Token: 0x0200080B RID: 2059
	[AttributeUsage(AttributeTargets.Struct)]
	[Serializable]
	public sealed class UnsafeValueTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.UnsafeValueTypeAttribute" /> class.</summary>
		// Token: 0x0600462A RID: 17962 RVA: 0x00002050 File Offset: 0x00000250
		public UnsafeValueTypeAttribute()
		{
		}
	}
}
