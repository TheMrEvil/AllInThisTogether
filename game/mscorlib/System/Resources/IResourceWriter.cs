using System;

namespace System.Resources
{
	/// <summary>Provides the base functionality for writing resources to an output file or stream.</summary>
	// Token: 0x02000863 RID: 2147
	public interface IResourceWriter : IDisposable
	{
		/// <summary>Adds a named resource of type <see cref="T:System.String" /> to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The value of the resource.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600475B RID: 18267
		void AddResource(string name, string value);

		/// <summary>Adds a named resource of type <see cref="T:System.Object" /> to the list of resources to be written.</summary>
		/// <param name="name">The name of the resource.</param>
		/// <param name="value">The value of the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600475C RID: 18268
		void AddResource(string name, object value);

		/// <summary>Adds an 8-bit unsigned integer array as a named resource to the list of resources to be written.</summary>
		/// <param name="name">Name of a resource.</param>
		/// <param name="value">Value of a resource as an 8-bit unsigned integer array.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600475D RID: 18269
		void AddResource(string name, byte[] value);

		/// <summary>Closes the underlying resource file or stream, ensuring all the data has been written to the file.</summary>
		// Token: 0x0600475E RID: 18270
		void Close();

		/// <summary>Writes all the resources added by the <see cref="M:System.Resources.IResourceWriter.AddResource(System.String,System.String)" /> method to the output file or stream.</summary>
		// Token: 0x0600475F RID: 18271
		void Generate();
	}
}
