using System;

namespace System.Reflection
{
	/// <summary>Contains methods for converting <see cref="T:System.Type" /> objects.</summary>
	// Token: 0x020008A6 RID: 2214
	public static class IntrospectionExtensions
	{
		/// <summary>Returns the <see cref="T:System.Reflection.TypeInfo" /> representation of the specified type.</summary>
		/// <param name="type">The type to convert.</param>
		/// <returns>The converted object.</returns>
		// Token: 0x06004903 RID: 18691 RVA: 0x000EE8B4 File Offset: 0x000ECAB4
		public static TypeInfo GetTypeInfo(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IReflectableType reflectableType = type as IReflectableType;
			if (reflectableType != null)
			{
				return reflectableType.GetTypeInfo();
			}
			return new TypeDelegator(type);
		}
	}
}
