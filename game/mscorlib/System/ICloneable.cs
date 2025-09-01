using System;

namespace System
{
	/// <summary>Supports cloning, which creates a new instance of a class with the same value as an existing instance.</summary>
	// Token: 0x02000138 RID: 312
	public interface ICloneable
	{
		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		// Token: 0x06000BE8 RID: 3048
		object Clone();
	}
}
