using System;

namespace System.Reflection
{
	/// <summary>Represents a type that you can reflect over.</summary>
	// Token: 0x020008A3 RID: 2211
	public interface IReflectableType
	{
		/// <summary>Retrieves an object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x06004902 RID: 18690
		TypeInfo GetTypeInfo();
	}
}
