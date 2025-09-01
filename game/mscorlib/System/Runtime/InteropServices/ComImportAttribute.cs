using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed type was previously defined in COM.</summary>
	// Token: 0x020006FE RID: 1790
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class ComImportAttribute : Attribute
	{
		// Token: 0x0600408B RID: 16523 RVA: 0x000E1172 File Offset: 0x000DF372
		internal static Attribute GetCustomAttribute(RuntimeType type)
		{
			if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.NotPublic)
			{
				return null;
			}
			return new ComImportAttribute();
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x000E1189 File Offset: 0x000DF389
		internal static bool IsDefined(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />.</summary>
		// Token: 0x0600408D RID: 16525 RVA: 0x00002050 File Offset: 0x00000250
		public ComImportAttribute()
		{
		}
	}
}
