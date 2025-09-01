using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides the context for waiting when asynchronously switching into a target environment.</summary>
	// Token: 0x02000823 RID: 2083
	public readonly struct YieldAwaitable
	{
		/// <summary>Retrieves a <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> object  for this instance of the class.</summary>
		/// <returns>The object that is used to monitor the completion of an asynchronous operation.</returns>
		// Token: 0x0600469C RID: 18076 RVA: 0x000E6FF0 File Offset: 0x000E51F0
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		/// <summary>Provides an awaiter for switching into a target environment.</summary>
		// Token: 0x02000824 RID: 2084
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public readonly struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			/// <summary>Gets a value that indicates whether a yield is not required.</summary>
			/// <returns>Always <see langword="false" />, which indicates that a yield is always required for <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" />.</returns>
			// Token: 0x17000ADA RID: 2778
			// (get) Token: 0x0600469D RID: 18077 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			/// <summary>Sets the continuation to invoke.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="continuation" /> is <see langword="null" />.</exception>
			// Token: 0x0600469E RID: 18078 RVA: 0x000E7006 File Offset: 0x000E5206
			[SecuritySafeCritical]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			/// <summary>Posts the <paramref name="continuation" /> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is <see langword="null" />.</exception>
			// Token: 0x0600469F RID: 18079 RVA: 0x000E700F File Offset: 0x000E520F
			[SecurityCritical]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x060046A0 RID: 18080 RVA: 0x000E7018 File Offset: 0x000E5218
			[SecurityCritical]
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x060046A1 RID: 18081 RVA: 0x00082EDA File Offset: 0x000810DA
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			/// <summary>Ends the await operation.</summary>
			// Token: 0x060046A2 RID: 18082 RVA: 0x00004BF9 File Offset: 0x00002DF9
			public void GetResult()
			{
			}

			// Token: 0x060046A3 RID: 18083 RVA: 0x000E70A2 File Offset: 0x000E52A2
			// Note: this type is marked as 'beforefieldinit'.
			static YieldAwaiter()
			{
			}

			// Token: 0x04002D70 RID: 11632
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x04002D71 RID: 11633
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
