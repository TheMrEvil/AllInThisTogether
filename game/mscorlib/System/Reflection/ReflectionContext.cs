using System;

namespace System.Reflection
{
	/// <summary>Represents a context that can provide reflection objects.</summary>
	// Token: 0x020008BD RID: 2237
	public abstract class ReflectionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionContext" /> class.</summary>
		// Token: 0x06004A08 RID: 18952 RVA: 0x0000259F File Offset: 0x0000079F
		protected ReflectionContext()
		{
		}

		/// <summary>Gets the representation, in this reflection context, of an assembly that is represented by an object from another reflection context.</summary>
		/// <param name="assembly">The external representation of the assembly to represent in this context.</param>
		/// <returns>The representation of the assembly in this reflection context.</returns>
		// Token: 0x06004A09 RID: 18953
		public abstract Assembly MapAssembly(Assembly assembly);

		/// <summary>Gets the representation, in this reflection context, of a type represented by an object from another reflection context.</summary>
		/// <param name="type">The external representation of the type to represent in this context.</param>
		/// <returns>The representation of the type in this reflection context.</returns>
		// Token: 0x06004A0A RID: 18954
		public abstract TypeInfo MapType(TypeInfo type);

		/// <summary>Gets the representation of the type of the specified object in this reflection context.</summary>
		/// <param name="value">The object to represent.</param>
		/// <returns>An object that represents the type of the specified object.</returns>
		// Token: 0x06004A0B RID: 18955 RVA: 0x000EF524 File Offset: 0x000ED724
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}
