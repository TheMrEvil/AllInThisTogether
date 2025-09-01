using System;

namespace System.Reflection
{
	/// <summary>Represents an object that provides a custom type.</summary>
	// Token: 0x0200087B RID: 2171
	public interface ICustomTypeProvider
	{
		/// <summary>Gets the custom type provided by this object.</summary>
		/// <returns>The custom type.</returns>
		// Token: 0x060044C2 RID: 17602
		Type GetCustomType();
	}
}
