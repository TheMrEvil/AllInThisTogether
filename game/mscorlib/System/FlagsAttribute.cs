using System;

namespace System
{
	/// <summary>Indicates that an enumeration can be treated as a bit field; that is, a set of flags.</summary>
	// Token: 0x0200011A RID: 282
	[AttributeUsage(AttributeTargets.Enum, Inherited = false)]
	[Serializable]
	public class FlagsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FlagsAttribute" /> class.</summary>
		// Token: 0x06000AE1 RID: 2785 RVA: 0x00002050 File Offset: 0x00000250
		public FlagsAttribute()
		{
		}
	}
}
