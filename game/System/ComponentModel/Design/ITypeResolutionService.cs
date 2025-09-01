using System;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to retrieve an assembly or type by name.</summary>
	// Token: 0x0200046E RID: 1134
	public interface ITypeResolutionService
	{
		/// <summary>Gets the requested assembly.</summary>
		/// <param name="name">The name of the assembly to retrieve.</param>
		/// <returns>An instance of the requested assembly, or <see langword="null" /> if no assembly can be located.</returns>
		// Token: 0x06002480 RID: 9344
		Assembly GetAssembly(AssemblyName name);

		/// <summary>Gets the requested assembly.</summary>
		/// <param name="name">The name of the assembly to retrieve.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> if this method should throw an exception if the assembly cannot be located; otherwise, <see langword="false" />, and this method returns <see langword="null" /> if the assembly cannot be located.</param>
		/// <returns>An instance of the requested assembly, or <see langword="null" /> if no assembly can be located.</returns>
		// Token: 0x06002481 RID: 9345
		Assembly GetAssembly(AssemblyName name, bool throwOnError);

		/// <summary>Loads a type with the specified name.</summary>
		/// <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies.</param>
		/// <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or <see langword="null" /> if no type can be found.</returns>
		// Token: 0x06002482 RID: 9346
		Type GetType(string name);

		/// <summary>Loads a type with the specified name.</summary>
		/// <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> if this method should throw an exception if the assembly cannot be located; otherwise, <see langword="false" />, and this method returns <see langword="null" /> if the assembly cannot be located.</param>
		/// <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or <see langword="null" /> if no type can be found.</returns>
		// Token: 0x06002483 RID: 9347
		Type GetType(string name, bool throwOnError);

		/// <summary>Loads a type with the specified name.</summary>
		/// <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> if this method should throw an exception if the assembly cannot be located; otherwise, <see langword="false" />, and this method returns <see langword="null" /> if the assembly cannot be located.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore case when searching for types; otherwise, <see langword="false" />.</param>
		/// <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or <see langword="null" /> if no type can be found.</returns>
		// Token: 0x06002484 RID: 9348
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		/// <summary>Adds a reference to the specified assembly.</summary>
		/// <param name="name">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly to reference.</param>
		// Token: 0x06002485 RID: 9349
		void ReferenceAssembly(AssemblyName name);

		/// <summary>Gets the path to the file from which the assembly was loaded.</summary>
		/// <param name="name">The name of the assembly.</param>
		/// <returns>The path to the file from which the assembly was loaded.</returns>
		// Token: 0x06002486 RID: 9350
		string GetPathOfAssembly(AssemblyName name);
	}
}
