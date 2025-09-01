using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Manages the path context for the current application. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	public sealed class ExeContext
	{
		// Token: 0x0600023C RID: 572 RVA: 0x000079D7 File Offset: 0x00005BD7
		internal ExeContext(string path, ConfigurationUserLevel level)
		{
			this.path = path;
			this.level = level;
		}

		/// <summary>Gets the current path for the application.</summary>
		/// <returns>A string value containing the current path.</returns>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000079ED File Offset: 0x00005BED
		public string ExePath
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets an object representing the path level of the current application.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationUserLevel" /> object representing the path level of the current application.</returns>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000079F5 File Offset: 0x00005BF5
		public ConfigurationUserLevel UserLevel
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00003518 File Offset: 0x00001718
		internal ExeContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000E8 RID: 232
		private string path;

		// Token: 0x040000E9 RID: 233
		private ConfigurationUserLevel level;
	}
}
