using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005D3 RID: 1491
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x060038E1 RID: 14561 RVA: 0x00002050 File Offset: 0x00000250
		public RemoteActivationAttribute()
		{
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000CADE0 File Offset: 0x000C8FE0
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000CADF0 File Offset: 0x000C8FF0
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object value in this._contextProperties)
				{
					ctor.ContextProperties.Add(value);
				}
			}
		}

		// Token: 0x040025FC RID: 9724
		private IList _contextProperties;
	}
}
