using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005CF RID: 1487
	[Serializable]
	internal class ContextLevelActivator : IActivator
	{
		// Token: 0x060038D2 RID: 14546 RVA: 0x000CAD3F File Offset: 0x000C8F3F
		public ContextLevelActivator(IActivator next)
		{
			this.m_NextActivator = next;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x00047F71 File Offset: 0x00046171
		public ActivatorLevel Level
		{
			get
			{
				return ActivatorLevel.Context;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000CAD4E File Offset: 0x000C8F4E
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000CAD56 File Offset: 0x000C8F56
		public IActivator NextActivator
		{
			get
			{
				return this.m_NextActivator;
			}
			set
			{
				this.m_NextActivator = value;
			}
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000CAD60 File Offset: 0x000C8F60
		public IConstructionReturnMessage Activate(IConstructionCallMessage ctorCall)
		{
			ServerIdentity serverIdentity = RemotingServices.CreateContextBoundObjectIdentity(ctorCall.ActivationType);
			RemotingServices.SetMessageTargetIdentity(ctorCall, serverIdentity);
			ConstructionCall constructionCall = ctorCall as ConstructionCall;
			if (constructionCall == null || !constructionCall.IsContextOk)
			{
				serverIdentity.Context = Context.CreateNewContext(ctorCall);
				Context newContext = Context.SwitchToContext(serverIdentity.Context);
				try
				{
					return this.m_NextActivator.Activate(ctorCall);
				}
				finally
				{
					Context.SwitchToContext(newContext);
				}
			}
			return this.m_NextActivator.Activate(ctorCall);
		}

		// Token: 0x040025FB RID: 9723
		private IActivator m_NextActivator;
	}
}
