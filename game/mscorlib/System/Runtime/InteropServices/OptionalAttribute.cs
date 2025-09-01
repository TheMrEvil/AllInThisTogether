using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that a parameter is optional.</summary>
	// Token: 0x02000703 RID: 1795
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x06004099 RID: 16537 RVA: 0x000E120B File Offset: 0x000DF40B
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x000E121C File Offset: 0x000DF41C
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOptional;
		}

		/// <summary>Initializes a new instance of the <see langword="OptionalAttribute" /> class with default values.</summary>
		// Token: 0x0600409B RID: 16539 RVA: 0x00002050 File Offset: 0x00000250
		public OptionalAttribute()
		{
		}
	}
}
