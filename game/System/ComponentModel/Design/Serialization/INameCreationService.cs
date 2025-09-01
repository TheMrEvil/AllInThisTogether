using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides a service that can generate unique names for objects.</summary>
	// Token: 0x0200048B RID: 1163
	public interface INameCreationService
	{
		/// <summary>Creates a new name that is unique to all components in the specified container.</summary>
		/// <param name="container">The container where the new object is added.</param>
		/// <param name="dataType">The data type of the object that receives the name.</param>
		/// <returns>A unique name for the data type.</returns>
		// Token: 0x06002530 RID: 9520
		string CreateName(IContainer container, Type dataType);

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <param name="name">The name to validate.</param>
		/// <returns>
		///   <see langword="true" /> if the name is valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002531 RID: 9521
		bool IsValidName(string name);

		/// <summary>Gets a value indicating whether the specified name is valid.</summary>
		/// <param name="name">The name to validate.</param>
		// Token: 0x06002532 RID: 9522
		void ValidateName(string name);
	}
}
