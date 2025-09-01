using System;

namespace System
{
	/// <summary>Indicates that a method will allow a variable number of arguments in its invocation. This class cannot be inherited.</summary>
	// Token: 0x0200016C RID: 364
	[AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class ParamArrayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ParamArrayAttribute" /> class with default properties.</summary>
		// Token: 0x06000E72 RID: 3698 RVA: 0x00002050 File Offset: 0x00000250
		public ParamArrayAttribute()
		{
		}
	}
}
