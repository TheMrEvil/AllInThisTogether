using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	/// <summary>Propagates notification that operations should be canceled.</summary>
	// Token: 0x0200029E RID: 670
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	public readonly struct CancellationToken
	{
		/// <summary>Returns an empty <see cref="T:System.Threading.CancellationToken" /> value.</summary>
		/// <returns>An empty cancellation token.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x0006EB4C File Offset: 0x0006CD4C
		public static CancellationToken None
		{
			get
			{
				return default(CancellationToken);
			}
		}

		/// <summary>Gets whether cancellation has been requested for this token.</summary>
		/// <returns>
		///   <see langword="true" /> if cancellation has been requested for this token; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x0006EB62 File Offset: 0x0006CD62
		public bool IsCancellationRequested
		{
			get
			{
				return this._source != null && this._source.IsCancellationRequested;
			}
		}

		/// <summary>Gets whether this token is capable of being in the canceled state.</summary>
		/// <returns>
		///   <see langword="true" /> if this token is capable of being in the canceled state; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0006EB79 File Offset: 0x0006CD79
		public bool CanBeCanceled
		{
			get
			{
				return this._source != null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is signaled when the token is canceled.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x0006EB84 File Offset: 0x0006CD84
		public WaitHandle WaitHandle
		{
			get
			{
				return (this._source ?? CancellationTokenSource.s_neverCanceledSource).WaitHandle;
			}
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0006EB9A File Offset: 0x0006CD9A
		internal CancellationToken(CancellationTokenSource source)
		{
			this._source = source;
		}

		/// <summary>Initializes the <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="canceled">The canceled state for the token.</param>
		// Token: 0x06001DC6 RID: 7622 RVA: 0x0006EBA3 File Offset: 0x0006CDA3
		public CancellationToken(bool canceled)
		{
			this = new CancellationToken(canceled ? CancellationTokenSource.s_canceledSource : null);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06001DC7 RID: 7623 RVA: 0x0006EBB6 File Offset: 0x0006CDB6
		public CancellationTokenRegistration Register(Action callback)
		{
			Action<object> callback2 = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback2, callback, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="useSynchronizationContext">A value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06001DC8 RID: 7624 RVA: 0x0006EBD5 File Offset: 0x0006CDD5
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			Action<object> callback2 = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback2, callback, useSynchronizationContext, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06001DC9 RID: 7625 RVA: 0x0006EBF4 File Offset: 0x0006CDF4
		public CancellationTokenRegistration Register(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, true);
		}

		/// <summary>Registers a delegate that will be called when this <see cref="T:System.Threading.CancellationToken" /> is canceled.</summary>
		/// <param name="callback">The delegate to be executed when the <see cref="T:System.Threading.CancellationToken" /> is canceled.</param>
		/// <param name="state">The state to pass to the <paramref name="callback" /> when the delegate is invoked. This may be null.</param>
		/// <param name="useSynchronizationContext">A Boolean value that indicates whether to capture the current <see cref="T:System.Threading.SynchronizationContext" /> and use it when invoking the <paramref name="callback" />.</param>
		/// <returns>The <see cref="T:System.Threading.CancellationTokenRegistration" /> instance that can be used to unregister the callback.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callback" /> is null.</exception>
		// Token: 0x06001DCA RID: 7626 RVA: 0x0006EC00 File Offset: 0x0006CE00
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0006EC0C File Offset: 0x0006CE0C
		internal CancellationTokenRegistration InternalRegisterWithoutEC(Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0006EC18 File Offset: 0x0006CE18
		[MethodImpl(MethodImplOptions.NoInlining)]
		public CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			CancellationTokenSource source = this._source;
			if (source == null)
			{
				return default(CancellationTokenRegistration);
			}
			return source.InternalRegister(callback, state, useSynchronizationContext ? SynchronizationContext.Current : null, useExecutionContext ? ExecutionContext.Capture() : null);
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified token.</summary>
		/// <param name="other">The other <see cref="T:System.Threading.CancellationToken" /> to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the instances are equal; otherwise, <see langword="false" />. See the Remarks section for more information.</returns>
		// Token: 0x06001DCD RID: 7629 RVA: 0x0006EC66 File Offset: 0x0006CE66
		public bool Equals(CancellationToken other)
		{
			return this._source == other._source;
		}

		/// <summary>Determines whether the current <see cref="T:System.Threading.CancellationToken" /> instance is equal to the specified <see cref="T:System.Object" />.</summary>
		/// <param name="other">The other object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is a <see cref="T:System.Threading.CancellationToken" /> and if the two instances are equal; otherwise, <see langword="false" />. See the Remarks section for more information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06001DCE RID: 7630 RVA: 0x0006EC76 File Offset: 0x0006CE76
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		/// <summary>Serves as a hash function for a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.CancellationToken" /> instance.</returns>
		// Token: 0x06001DCF RID: 7631 RVA: 0x0006EC8E File Offset: 0x0006CE8E
		public override int GetHashCode()
		{
			return (this._source ?? CancellationTokenSource.s_neverCanceledSource).GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are equal.</summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>
		///   <see langword="true" /> if the instances are equal; otherwise, <see langword="false" /> See the Remarks section for more information.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06001DD0 RID: 7632 RVA: 0x0006ECA4 File Offset: 0x0006CEA4
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Threading.CancellationToken" /> instances are not equal.</summary>
		/// <param name="left">The first instance.</param>
		/// <param name="right">The second instance.</param>
		/// <returns>
		///   <see langword="true" /> if the instances are not equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06001DD1 RID: 7633 RVA: 0x0006ECAE File Offset: 0x0006CEAE
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		/// <summary>Throws a <see cref="T:System.OperationCanceledException" /> if this token has had cancellation requested.</summary>
		/// <exception cref="T:System.OperationCanceledException">The token has had cancellation requested.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The associated <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		// Token: 0x06001DD2 RID: 7634 RVA: 0x0006ECBB File Offset: 0x0006CEBB
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0006ECCB File Offset: 0x0006CECB
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException("The operation was canceled.", this);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0006ECDD File Offset: 0x0006CEDD
		// Note: this type is marked as 'beforefieldinit'.
		static CancellationToken()
		{
		}

		// Token: 0x04001A56 RID: 6742
		private readonly CancellationTokenSource _source;

		// Token: 0x04001A57 RID: 6743
		private static readonly Action<object> s_actionToActionObjShunt = delegate(object obj)
		{
			((Action)obj)();
		};

		// Token: 0x0200029F RID: 671
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001DD5 RID: 7637 RVA: 0x0006ECF4 File Offset: 0x0006CEF4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001DD6 RID: 7638 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06001DD7 RID: 7639 RVA: 0x0006ED00 File Offset: 0x0006CF00
			internal void <.cctor>b__26_0(object obj)
			{
				((Action)obj)();
			}

			// Token: 0x04001A58 RID: 6744
			public static readonly CancellationToken.<>c <>9 = new CancellationToken.<>c();
		}
	}
}
