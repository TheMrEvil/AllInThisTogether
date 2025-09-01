using System;
using System.Configuration.Internal;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Configuration
{
	// Token: 0x0200004E RID: 78
	internal class InternalConfigurationRoot : IInternalConfigRoot
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00002050 File Offset: 0x00000250
		public InternalConfigurationRoot()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		public IInternalConfigRecord GetConfigRecord(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000820B File Offset: 0x0000640B
		public object GetSection(string section, string configPath)
		{
			return this.GetConfigRecord(configPath).GetSection(section);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007CF9 File Offset: 0x00005EF9
		[MonoTODO]
		public string GetUniqueConfigPath(string configPath)
		{
			return configPath;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000821A File Offset: 0x0000641A
		[MonoTODO]
		public IInternalConfigRecord GetUniqueConfigRecord(string configPath)
		{
			return this.GetConfigRecord(this.GetUniqueConfigPath(configPath));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00008229 File Offset: 0x00006429
		public void Init(IInternalConfigHost host, bool isDesignTime)
		{
			this.host = host;
			this.isDesignTime = isDesignTime;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00008239 File Offset: 0x00006439
		[MonoTODO]
		public void RemoveConfig(string configPath)
		{
			this.host.DeleteStream(configPath);
			if (this.ConfigRemoved != null)
			{
				this.ConfigRemoved(this, new InternalConfigEventArgs(configPath));
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008261 File Offset: 0x00006461
		public bool IsDesignTime
		{
			get
			{
				return this.isDesignTime;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002A6 RID: 678 RVA: 0x0000826C File Offset: 0x0000646C
		// (remove) Token: 0x060002A7 RID: 679 RVA: 0x000082A4 File Offset: 0x000064A4
		public event InternalConfigEventHandler ConfigChanged
		{
			[CompilerGenerated]
			add
			{
				InternalConfigEventHandler internalConfigEventHandler = this.ConfigChanged;
				InternalConfigEventHandler internalConfigEventHandler2;
				do
				{
					internalConfigEventHandler2 = internalConfigEventHandler;
					InternalConfigEventHandler value2 = (InternalConfigEventHandler)Delegate.Combine(internalConfigEventHandler2, value);
					internalConfigEventHandler = Interlocked.CompareExchange<InternalConfigEventHandler>(ref this.ConfigChanged, value2, internalConfigEventHandler2);
				}
				while (internalConfigEventHandler != internalConfigEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				InternalConfigEventHandler internalConfigEventHandler = this.ConfigChanged;
				InternalConfigEventHandler internalConfigEventHandler2;
				do
				{
					internalConfigEventHandler2 = internalConfigEventHandler;
					InternalConfigEventHandler value2 = (InternalConfigEventHandler)Delegate.Remove(internalConfigEventHandler2, value);
					internalConfigEventHandler = Interlocked.CompareExchange<InternalConfigEventHandler>(ref this.ConfigChanged, value2, internalConfigEventHandler2);
				}
				while (internalConfigEventHandler != internalConfigEventHandler2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060002A8 RID: 680 RVA: 0x000082DC File Offset: 0x000064DC
		// (remove) Token: 0x060002A9 RID: 681 RVA: 0x00008314 File Offset: 0x00006514
		public event InternalConfigEventHandler ConfigRemoved
		{
			[CompilerGenerated]
			add
			{
				InternalConfigEventHandler internalConfigEventHandler = this.ConfigRemoved;
				InternalConfigEventHandler internalConfigEventHandler2;
				do
				{
					internalConfigEventHandler2 = internalConfigEventHandler;
					InternalConfigEventHandler value2 = (InternalConfigEventHandler)Delegate.Combine(internalConfigEventHandler2, value);
					internalConfigEventHandler = Interlocked.CompareExchange<InternalConfigEventHandler>(ref this.ConfigRemoved, value2, internalConfigEventHandler2);
				}
				while (internalConfigEventHandler != internalConfigEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				InternalConfigEventHandler internalConfigEventHandler = this.ConfigRemoved;
				InternalConfigEventHandler internalConfigEventHandler2;
				do
				{
					internalConfigEventHandler2 = internalConfigEventHandler;
					InternalConfigEventHandler value2 = (InternalConfigEventHandler)Delegate.Remove(internalConfigEventHandler2, value);
					internalConfigEventHandler = Interlocked.CompareExchange<InternalConfigEventHandler>(ref this.ConfigRemoved, value2, internalConfigEventHandler2);
				}
				while (internalConfigEventHandler != internalConfigEventHandler2);
			}
		}

		// Token: 0x040000FB RID: 251
		private IInternalConfigHost host;

		// Token: 0x040000FC RID: 252
		private bool isDesignTime;

		// Token: 0x040000FD RID: 253
		[CompilerGenerated]
		private InternalConfigEventHandler ConfigChanged;

		// Token: 0x040000FE RID: 254
		[CompilerGenerated]
		private InternalConfigEventHandler ConfigRemoved;
	}
}
