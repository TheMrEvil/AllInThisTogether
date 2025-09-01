using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to support creation of new configuration records.</summary>
	// Token: 0x02000082 RID: 130
	[ComVisible(false)]
	public interface IInternalConfigRecord
	{
		/// <summary>Returns an object representing a section of a configuration from the last-known-good (LKG) configuration.</summary>
		/// <param name="configKey">A string representing a key to a configuration section.</param>
		/// <returns>An <see cref="T:System.Object" /> instance representing the section of the last-known-good configuration specified by <paramref name="configKey" />.</returns>
		// Token: 0x06000469 RID: 1129
		object GetLkgSection(string configKey);

		/// <summary>Returns an <see cref="T:System.Object" /> instance representing a section of a configuration file.</summary>
		/// <param name="configKey">A string representing a key to a configuration section.</param>
		/// <returns>An <see cref="T:System.Object" /> instance representing a section of a configuration file.</returns>
		// Token: 0x0600046A RID: 1130
		object GetSection(string configKey);

		/// <summary>Causes a specified section of the configuration object to be reinitialized.</summary>
		/// <param name="configKey">A string representing a key to a configuration section that is to be refreshed.</param>
		// Token: 0x0600046B RID: 1131
		void RefreshSection(string configKey);

		/// <summary>Removes a configuration record.</summary>
		// Token: 0x0600046C RID: 1132
		void Remove();

		/// <summary>Grants the configuration object the permission to throw an exception if an error occurs during initialization.</summary>
		// Token: 0x0600046D RID: 1133
		void ThrowIfInitErrors();

		/// <summary>Gets a string representing a configuration file path.</summary>
		/// <returns>A string representing a configuration file path.</returns>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600046E RID: 1134
		string ConfigPath { get; }

		/// <summary>Returns a value indicating whether an error occurred during initialization of a configuration object.</summary>
		/// <returns>
		///   <see langword="true" /> if an error occurred during initialization of a configuration object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600046F RID: 1135
		bool HasInitErrors { get; }

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</summary>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</returns>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000470 RID: 1136
		string StreamName { get; }
	}
}
