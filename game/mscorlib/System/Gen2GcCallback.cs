using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x0200011D RID: 285
	internal sealed class Gen2GcCallback : CriticalFinalizerObject
	{
		// Token: 0x06000AEF RID: 2799 RVA: 0x00028917 File Offset: 0x00026B17
		private Gen2GcCallback()
		{
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002891F File Offset: 0x00026B1F
		public static void Register(Func<object, bool> callback, object targetObj)
		{
			new Gen2GcCallback().Setup(callback, targetObj);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002892D File Offset: 0x00026B2D
		private void Setup(Func<object, bool> callback, object targetObj)
		{
			this._callback = callback;
			this._weakTargetObj = GCHandle.Alloc(targetObj, GCHandleType.Weak);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00028944 File Offset: 0x00026B44
		protected override void Finalize()
		{
			try
			{
				object target = this._weakTargetObj.Target;
				if (target == null)
				{
					this._weakTargetObj.Free();
				}
				else
				{
					try
					{
						if (!this._callback(target))
						{
							return;
						}
					}
					catch
					{
					}
					if (!Environment.HasShutdownStarted)
					{
						GC.ReRegisterForFinalize(this);
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x040010E3 RID: 4323
		private Func<object, bool> _callback;

		// Token: 0x040010E4 RID: 4324
		private GCHandle _weakTargetObj;
	}
}
