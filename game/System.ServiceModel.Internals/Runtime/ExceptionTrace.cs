using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.Runtime
{
	// Token: 0x02000018 RID: 24
	internal class ExceptionTrace
	{
		// Token: 0x0600007F RID: 127 RVA: 0x000034E8 File Offset: 0x000016E8
		public ExceptionTrace(string eventSourceName, EtwDiagnosticTrace diagnosticTrace)
		{
			this.eventSourceName = eventSourceName;
			this.diagnosticTrace = diagnosticTrace;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034FE File Offset: 0x000016FE
		public void AsInformation(Exception exception)
		{
			TraceCore.HandledException(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000351C File Offset: 0x0000171C
		public void AsWarning(Exception exception)
		{
			TraceCore.HandledExceptionWarning(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000353C File Offset: 0x0000173C
		public Exception AsError(Exception exception)
		{
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				return this.AsError<Exception>(ex);
			}
			TargetInvocationException ex2 = exception as TargetInvocationException;
			if (ex2 != null && ex2.InnerException != null)
			{
				return this.AsError(ex2.InnerException);
			}
			return this.TraceException<Exception>(exception);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003584 File Offset: 0x00001784
		public Exception AsError(Exception exception, string eventSource)
		{
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				return this.AsError<Exception>(ex, eventSource);
			}
			TargetInvocationException ex2 = exception as TargetInvocationException;
			if (ex2 != null && ex2.InnerException != null)
			{
				return this.AsError(ex2.InnerException, eventSource);
			}
			return this.TraceException<Exception>(exception, eventSource);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035CC File Offset: 0x000017CC
		public Exception AsError(TargetInvocationException targetInvocationException, string eventSource)
		{
			if (Fx.IsFatal(targetInvocationException))
			{
				return targetInvocationException;
			}
			Exception innerException = targetInvocationException.InnerException;
			if (innerException != null)
			{
				return this.AsError(innerException, eventSource);
			}
			return this.TraceException<Exception>(targetInvocationException, eventSource);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035FE File Offset: 0x000017FE
		public Exception AsError<TPreferredException>(AggregateException aggregateException)
		{
			return this.AsError<TPreferredException>(aggregateException, this.eventSourceName);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003610 File Offset: 0x00001810
		public Exception AsError<TPreferredException>(AggregateException aggregateException, string eventSource)
		{
			if (Fx.IsFatal(aggregateException))
			{
				return aggregateException;
			}
			ReadOnlyCollection<Exception> innerExceptions = aggregateException.Flatten().InnerExceptions;
			if (innerExceptions.Count == 0)
			{
				return this.TraceException<AggregateException>(aggregateException, eventSource);
			}
			Exception ex = null;
			foreach (Exception ex2 in innerExceptions)
			{
				TargetInvocationException ex3 = ex2 as TargetInvocationException;
				Exception ex4 = (ex3 != null && ex3.InnerException != null) ? ex3.InnerException : ex2;
				if (ex4 is TPreferredException && ex == null)
				{
					ex = ex4;
				}
				this.TraceException<Exception>(ex4, eventSource);
			}
			if (ex == null)
			{
				ex = innerExceptions[0];
			}
			return ex;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036C0 File Offset: 0x000018C0
		public ArgumentException Argument(string paramName, string message)
		{
			return this.TraceException<ArgumentException>(new ArgumentException(message, paramName));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000036CF File Offset: 0x000018CF
		public ArgumentNullException ArgumentNull(string paramName)
		{
			return this.TraceException<ArgumentNullException>(new ArgumentNullException(paramName));
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000036DD File Offset: 0x000018DD
		public ArgumentNullException ArgumentNull(string paramName, string message)
		{
			return this.TraceException<ArgumentNullException>(new ArgumentNullException(paramName, message));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000036EC File Offset: 0x000018EC
		public ArgumentException ArgumentNullOrEmpty(string paramName)
		{
			return this.Argument(paramName, InternalSR.ArgumentNullOrEmpty(paramName));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036FB File Offset: 0x000018FB
		public ArgumentOutOfRangeException ArgumentOutOfRange(string paramName, object actualValue, string message)
		{
			return this.TraceException<ArgumentOutOfRangeException>(new ArgumentOutOfRangeException(paramName, actualValue, message));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000370B File Offset: 0x0000190B
		public ObjectDisposedException ObjectDisposed(string message)
		{
			return this.TraceException<ObjectDisposedException>(new ObjectDisposedException(null, message));
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000371A File Offset: 0x0000191A
		public void TraceUnhandledException(Exception exception)
		{
			TraceCore.UnhandledException(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003738 File Offset: 0x00001938
		public void TraceHandledException(Exception exception, TraceEventType traceEventType)
		{
			if (traceEventType != TraceEventType.Error)
			{
				if (traceEventType != TraceEventType.Warning)
				{
					if (traceEventType != TraceEventType.Verbose)
					{
						if (TraceCore.HandledExceptionIsEnabled(this.diagnosticTrace))
						{
							TraceCore.HandledException(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
						}
					}
					else if (TraceCore.HandledExceptionVerboseIsEnabled(this.diagnosticTrace))
					{
						TraceCore.HandledExceptionVerbose(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
						return;
					}
				}
				else if (TraceCore.HandledExceptionWarningIsEnabled(this.diagnosticTrace))
				{
					TraceCore.HandledExceptionWarning(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
					return;
				}
			}
			else if (TraceCore.HandledExceptionErrorIsEnabled(this.diagnosticTrace))
			{
				TraceCore.HandledExceptionError(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
				return;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003804 File Offset: 0x00001A04
		public void TraceEtwException(Exception exception, TraceEventType eventType)
		{
			switch (eventType)
			{
			case TraceEventType.Critical:
				if (TraceCore.EtwUnhandledExceptionIsEnabled(this.diagnosticTrace))
				{
					TraceCore.EtwUnhandledException(this.diagnosticTrace, (exception != null) ? exception.ToString() : string.Empty, exception);
					return;
				}
				return;
			case TraceEventType.Error:
			case TraceEventType.Warning:
				if (TraceCore.ThrowingEtwExceptionIsEnabled(this.diagnosticTrace))
				{
					TraceCore.ThrowingEtwException(this.diagnosticTrace, this.eventSourceName, (exception != null) ? exception.ToString() : string.Empty, exception);
					return;
				}
				return;
			}
			if (TraceCore.ThrowingEtwExceptionVerboseIsEnabled(this.diagnosticTrace))
			{
				TraceCore.ThrowingEtwExceptionVerbose(this.diagnosticTrace, this.eventSourceName, (exception != null) ? exception.ToString() : string.Empty, exception);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000038B4 File Offset: 0x00001AB4
		private TException TraceException<TException>(TException exception) where TException : Exception
		{
			return this.TraceException<TException>(exception, this.eventSourceName);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000038C4 File Offset: 0x00001AC4
		[SecuritySafeCritical]
		private TException TraceException<TException>(TException exception, string eventSource) where TException : Exception
		{
			if (TraceCore.ThrowingExceptionIsEnabled(this.diagnosticTrace))
			{
				TraceCore.ThrowingException(this.diagnosticTrace, eventSource, (exception != null) ? exception.ToString() : string.Empty, exception);
			}
			this.BreakOnException(exception);
			return exception;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003917 File Offset: 0x00001B17
		[SecuritySafeCritical]
		private void BreakOnException(Exception exception)
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000391C File Offset: 0x00001B1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void TraceFailFast(string message)
		{
			EventLogger logger = new EventLogger(this.eventSourceName, this.diagnosticTrace);
			this.TraceFailFast(message, logger);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003948 File Offset: 0x00001B48
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal void TraceFailFast(string message, EventLogger logger)
		{
			if (logger != null)
			{
				try
				{
					string text = null;
					try
					{
						text = new StackTrace().ToString();
					}
					catch (Exception ex)
					{
						text = ex.Message;
						if (Fx.IsFatal(ex))
						{
							throw;
						}
					}
					finally
					{
						logger.LogEvent(TraceEventType.Critical, 6, 3221291110U, new string[]
						{
							message,
							text
						});
					}
				}
				catch (Exception ex2)
				{
					logger.LogEvent(TraceEventType.Critical, 6, 3221291111U, new string[]
					{
						ex2.ToString()
					});
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x04000096 RID: 150
		private const ushort FailFastEventLogCategory = 6;

		// Token: 0x04000097 RID: 151
		private string eventSourceName;

		// Token: 0x04000098 RID: 152
		private readonly EtwDiagnosticTrace diagnosticTrace;
	}
}
