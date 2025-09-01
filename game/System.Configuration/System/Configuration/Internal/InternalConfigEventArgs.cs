using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines a class that allows the .NET Framework infrastructure to specify event arguments for configuration events.</summary>
	// Token: 0x02000086 RID: 134
	public sealed class InternalConfigEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Internal.InternalConfigEventArgs" /> class.</summary>
		/// <param name="configPath">A configuration path.</param>
		// Token: 0x06000481 RID: 1153 RVA: 0x0000B3F3 File Offset: 0x000095F3
		public InternalConfigEventArgs(string configPath)
		{
			this.configPath = configPath;
		}

		/// <summary>Gets or sets the configuration path related to the <see cref="T:System.Configuration.Internal.InternalConfigEventArgs" /> object.</summary>
		/// <returns>A string value specifying the configuration path.</returns>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000B402 File Offset: 0x00009602
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0000B40A File Offset: 0x0000960A
		public string ConfigPath
		{
			get
			{
				return this.configPath;
			}
			set
			{
				this.configPath = value;
			}
		}

		// Token: 0x04000167 RID: 359
		private string configPath;
	}
}
