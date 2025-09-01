using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Encapsulates the context information that is associated with a <see cref="T:System.Configuration.ConfigurationElement" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200003B RID: 59
	public sealed class ContextInformation
	{
		// Token: 0x06000212 RID: 530 RVA: 0x000076AE File Offset: 0x000058AE
		internal ContextInformation(Configuration config, object ctx)
		{
			this.ctx = ctx;
			this.config = config;
		}

		/// <summary>Provides an object containing configuration-section information based on the specified section name.</summary>
		/// <param name="sectionName">The name of the configuration section.</param>
		/// <returns>An object containing the specified section within the configuration.</returns>
		// Token: 0x06000213 RID: 531 RVA: 0x000076C4 File Offset: 0x000058C4
		public object GetSection(string sectionName)
		{
			return this.config.GetSection(sectionName);
		}

		/// <summary>Gets the context of the environment where the configuration property is being evaluated.</summary>
		/// <returns>An object specifying the environment where the configuration property is being evaluated.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000076D2 File Offset: 0x000058D2
		public object HostingContext
		{
			get
			{
				return this.ctx;
			}
		}

		/// <summary>Gets a value specifying whether the configuration property is being evaluated at the machine configuration level.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration property is being evaluated at the machine configuration level; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000076DA File Offset: 0x000058DA
		[MonoInternalNote("should this use HostingContext instead?")]
		public bool IsMachineLevel
		{
			get
			{
				return this.config.ConfigPath == "machine";
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00003518 File Offset: 0x00001718
		internal ContextInformation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000DD RID: 221
		private object ctx;

		// Token: 0x040000DE RID: 222
		private Configuration config;
	}
}
