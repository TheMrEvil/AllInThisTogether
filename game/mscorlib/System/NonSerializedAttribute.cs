using System;

namespace System
{
	/// <summary>Indicates that a field of a serializable class should not be serialized. This class cannot be inherited.</summary>
	// Token: 0x0200015E RID: 350
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class NonSerializedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.NonSerializedAttribute" /> class.</summary>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00002050 File Offset: 0x00000250
		public NonSerializedAttribute()
		{
		}
	}
}
