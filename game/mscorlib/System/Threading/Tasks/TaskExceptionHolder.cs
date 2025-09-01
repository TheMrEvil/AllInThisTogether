using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000374 RID: 884
	internal class TaskExceptionHolder
	{
		// Token: 0x0600249D RID: 9373 RVA: 0x00082FF5 File Offset: 0x000811F5
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		private static bool ShouldFailFastOnUnobservedException()
		{
			return false;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00083004 File Offset: 0x00081204
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled && !Environment.HasShutdownStarted)
				{
					AggregateException ex = new AggregateException("A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread.", this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs = new UnobservedTaskExceptionEventArgs(ex);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, unobservedTaskExceptionEventArgs);
					if (TaskExceptionHolder.s_failFastOnUnobservedException && !unobservedTaskExceptionEventArgs.m_observed)
					{
						throw ex;
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x0008307C File Offset: 0x0008127C
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00083089 File Offset: 0x00081289
		internal void Add(object exceptionObject)
		{
			this.Add(exceptionObject, false);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00083093 File Offset: 0x00081293
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000830A8 File Offset: 0x000812A8
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo cancellationException = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = cancellationException;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000830E4 File Offset: 0x000812E4
		private void AddFaultException(object exceptionObject)
		{
			LowLevelListWithIList<ExceptionDispatchInfo> lowLevelListWithIList = this.m_faultExceptions;
			if (lowLevelListWithIList == null)
			{
				lowLevelListWithIList = (this.m_faultExceptions = new LowLevelListWithIList<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				lowLevelListWithIList.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					lowLevelListWithIList.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception source = enumerator.Current;
								lowLevelListWithIList.Add(ExceptionDispatchInfo.Capture(source));
							}
							goto IL_AE;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException("(Internal)Expected an Exception or an IEnumerable<Exception>", "exceptionObject");
					}
					lowLevelListWithIList.AddRange(enumerable2);
				}
			}
			IL_AE:
			if (lowLevelListWithIList.Count > 0)
			{
				this.MarkAsUnhandled();
			}
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000831C0 File Offset: 0x000813C0
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000831DB File Offset: 0x000813DB
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000831FC File Offset: 0x000813FC
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			LowLevelListWithIList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			array[array.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0008325E File Offset: 0x0008145E
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			IList<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00083274 File Offset: 0x00081474
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0008327C File Offset: 0x0008147C
		// Note: this type is marked as 'beforefieldinit'.
		static TaskExceptionHolder()
		{
		}

		// Token: 0x04001D3F RID: 7487
		private static readonly bool s_failFastOnUnobservedException = TaskExceptionHolder.ShouldFailFastOnUnobservedException();

		// Token: 0x04001D40 RID: 7488
		private readonly Task m_task;

		// Token: 0x04001D41 RID: 7489
		private volatile LowLevelListWithIList<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04001D42 RID: 7490
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04001D43 RID: 7491
		private volatile bool m_isHandled;
	}
}
