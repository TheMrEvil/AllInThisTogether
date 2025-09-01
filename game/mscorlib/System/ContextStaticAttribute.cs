using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Indicates that the value of a static field is unique for a particular context.</summary>
	// Token: 0x020001EF RID: 495
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public class ContextStaticAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ContextStaticAttribute" /> class.</summary>
		// Token: 0x06001552 RID: 5458 RVA: 0x00002050 File Offset: 0x00000250
		public ContextStaticAttribute()
		{
		}
	}
}
