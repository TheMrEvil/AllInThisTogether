using System;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	/// <summary>When applied to a <see cref="T:System.Reflection.Assembly" /> object, enables an <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> object to discover custom <see cref="T:System.Reflection.ReflectionContext" /> objects.</summary>
	// Token: 0x02000022 RID: 34
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = true)]
	public class CatalogReflectionContextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AssemblyCatalog" /> class with the specified <see cref="T:System.Reflection.ReflectionContext" /> type.</summary>
		/// <param name="reflectionContextType">The type of the reflection context.</param>
		// Token: 0x0600012A RID: 298 RVA: 0x00004300 File Offset: 0x00002500
		public CatalogReflectionContextAttribute(Type reflectionContextType)
		{
			Requires.NotNull<Type>(reflectionContextType, "reflectionContextType");
			this._reflectionContextType = reflectionContextType;
		}

		/// <summary>Creates an instance of the custom <see cref="T:System.Reflection.ReflectionContext" /> object.</summary>
		/// <returns>An instance of the custom reflection context.</returns>
		// Token: 0x0600012B RID: 299 RVA: 0x0000431C File Offset: 0x0000251C
		public ReflectionContext CreateReflectionContext()
		{
			Assumes.NotNull<Type>(this._reflectionContextType);
			ReflectionContext result = null;
			try
			{
				result = (ReflectionContext)Activator.CreateInstance(this._reflectionContextType);
			}
			catch (InvalidCastException innerException)
			{
				throw new InvalidOperationException(Strings.ReflectionContext_Type_Required, innerException);
			}
			catch (MissingMethodException inner)
			{
				throw new MissingMethodException(Strings.ReflectionContext_Requires_DefaultConstructor, inner);
			}
			return result;
		}

		// Token: 0x0400006B RID: 107
		private Type _reflectionContextType;
	}
}
