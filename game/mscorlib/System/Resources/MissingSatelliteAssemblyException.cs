using System;
using System.Runtime.Serialization;

namespace System.Resources
{
	/// <summary>The exception that is thrown when the satellite assembly for the resources of the default culture is missing.</summary>
	// Token: 0x0200085B RID: 2139
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with default properties.</summary>
		// Token: 0x0600473A RID: 18234 RVA: 0x000E8088 File Offset: 0x000E6288
		public MissingSatelliteAssemblyException() : base("Resource lookup fell back to the ultimate fallback resources in a satellite assembly, but that satellite either was not found or could not be loaded. Please consider reinstalling or repairing the application.")
		{
			base.HResult = -2146233034;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with the specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x0600473B RID: 18235 RVA: 0x000E80A0 File Offset: 0x000E62A0
		public MissingSatelliteAssemblyException(string message) : base(message)
		{
			base.HResult = -2146233034;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and the name of a neutral culture.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="cultureName">The name of the neutral culture.</param>
		// Token: 0x0600473C RID: 18236 RVA: 0x000E80B4 File Offset: 0x000E62B4
		public MissingSatelliteAssemblyException(string message, string cultureName) : base(message)
		{
			base.HResult = -2146233034;
			this._cultureName = cultureName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600473D RID: 18237 RVA: 0x000E80CF File Offset: 0x000E62CF
		public MissingSatelliteAssemblyException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233034;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.MissingSatelliteAssemblyException" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		// Token: 0x0600473E RID: 18238 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets the name of the default culture.</summary>
		/// <returns>The name of the default culture.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x000E80E4 File Offset: 0x000E62E4
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x04002DA1 RID: 11681
		private string _cultureName;
	}
}
