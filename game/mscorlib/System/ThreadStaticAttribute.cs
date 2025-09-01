using System;

namespace System
{
	/// <summary>Indicates that the value of a static field is unique for each thread.</summary>
	// Token: 0x02000191 RID: 401
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	[Serializable]
	public class ThreadStaticAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ThreadStaticAttribute" /> class.</summary>
		// Token: 0x06000FCE RID: 4046 RVA: 0x00002050 File Offset: 0x00000250
		public ThreadStaticAttribute()
		{
		}
	}
}
