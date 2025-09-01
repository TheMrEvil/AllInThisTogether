using System;

namespace System
{
	/// <summary>Indicates that a class can be serialized. This class cannot be inherited.</summary>
	// Token: 0x0200017B RID: 379
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
	public sealed class SerializableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.SerializableAttribute" /> class.</summary>
		// Token: 0x06000F0F RID: 3855 RVA: 0x00002050 File Offset: 0x00000250
		public SerializableAttribute()
		{
		}
	}
}
