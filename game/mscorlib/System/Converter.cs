using System;

namespace System
{
	/// <summary>Represents a method that converts an object from one type to another type.</summary>
	/// <param name="input">The object to convert.</param>
	/// <typeparam name="TInput">The type of object that is to be converted.</typeparam>
	/// <typeparam name="TOutput">The type the input object is to be converted to.</typeparam>
	/// <returns>The <typeparamref name="TOutput" /> that represents the converted <typeparamref name="TInput" />.</returns>
	// Token: 0x020000EF RID: 239
	// (Invoke) Token: 0x060006DF RID: 1759
	public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
}
