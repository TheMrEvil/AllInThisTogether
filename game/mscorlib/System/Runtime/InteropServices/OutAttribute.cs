using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that data should be marshaled from callee back to caller.</summary>
	// Token: 0x02000702 RID: 1794
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x06004096 RID: 16534 RVA: 0x000E11F2 File Offset: 0x000DF3F2
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x000E1203 File Offset: 0x000DF403
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOut;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.OutAttribute" /> class.</summary>
		// Token: 0x06004098 RID: 16536 RVA: 0x00002050 File Offset: 0x00000250
		public OutAttribute()
		{
		}
	}
}
