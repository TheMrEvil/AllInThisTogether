using System;
using System.Threading;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000053 RID: 83
	internal sealed class DisposableReflectionComposablePart : ReflectionComposablePart, IDisposable
	{
		// Token: 0x0600022A RID: 554 RVA: 0x00006A24 File Offset: 0x00004C24
		public DisposableReflectionComposablePart(ReflectionComposablePartDefinition definition) : base(definition)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006A30 File Offset: 0x00004C30
		protected override void ReleaseInstanceIfNecessary(object instance)
		{
			IDisposable disposable = instance as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006A4D File Offset: 0x00004C4D
		protected override void EnsureRunning()
		{
			base.EnsureRunning();
			if (this._isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006A67 File Offset: 0x00004C67
		void IDisposable.Dispose()
		{
			if (Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
			{
				this.ReleaseInstanceIfNecessary(base.CachedInstance);
			}
		}

		// Token: 0x040000E8 RID: 232
		private volatile int _isDisposed;
	}
}
