using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the HRESULT or <see langword="retval" /> signature transformation that takes place during COM interop calls should be suppressed.</summary>
	// Token: 0x02000700 RID: 1792
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06004090 RID: 16528 RVA: 0x000E11B1 File Offset: 0x000DF3B1
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x000E11C8 File Offset: 0x000DF3C8
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.PreserveSigAttribute" /> class.</summary>
		// Token: 0x06004092 RID: 16530 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveSigAttribute()
		{
		}
	}
}
