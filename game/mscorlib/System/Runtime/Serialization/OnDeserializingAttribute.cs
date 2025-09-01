using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a method, specifies that the method is called during deserialization of an object in an object graph. The order of deserialization relative to other objects in the graph is non-deterministic.</summary>
	// Token: 0x0200066F RID: 1647
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	public sealed class OnDeserializingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.OnDeserializingAttribute" /> class.</summary>
		// Token: 0x06003D69 RID: 15721 RVA: 0x00002050 File Offset: 0x00000250
		public OnDeserializingAttribute()
		{
		}
	}
}
