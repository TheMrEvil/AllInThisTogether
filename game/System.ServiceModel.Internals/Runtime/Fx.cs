using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200001B RID: 27
	internal static class Fx
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003A11 File Offset: 0x00001C11
		public static ExceptionTrace Exception
		{
			get
			{
				if (Fx.exceptionTrace == null)
				{
					Fx.exceptionTrace = new ExceptionTrace("System.Runtime", Fx.Trace);
				}
				return Fx.exceptionTrace;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003A33 File Offset: 0x00001C33
		public static EtwDiagnosticTrace Trace
		{
			get
			{
				if (Fx.diagnosticTrace == null)
				{
					Fx.diagnosticTrace = Fx.InitializeTracing();
				}
				return Fx.diagnosticTrace;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A4C File Offset: 0x00001C4C
		[SecuritySafeCritical]
		private static EtwDiagnosticTrace InitializeTracing()
		{
			EtwDiagnosticTrace etwDiagnosticTrace = new EtwDiagnosticTrace("System.Runtime", EtwDiagnosticTrace.DefaultEtwProviderId);
			if (etwDiagnosticTrace.EtwProvider != null)
			{
				EtwDiagnosticTrace etwDiagnosticTrace2 = etwDiagnosticTrace;
				etwDiagnosticTrace2.RefreshState = (Action)Delegate.Combine(etwDiagnosticTrace2.RefreshState, new Action(delegate()
				{
					Fx.UpdateLevel();
				}));
			}
			Fx.UpdateLevel(etwDiagnosticTrace);
			return etwDiagnosticTrace;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003AAD File Offset: 0x00001CAD
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public static Fx.ExceptionHandler AsynchronousThreadExceptionHandler
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return Fx.asynchronousThreadExceptionHandler;
			}
			[SecurityCritical]
			set
			{
				Fx.asynchronousThreadExceptionHandler = value;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003ABC File Offset: 0x00001CBC
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string description)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003AC0 File Offset: 0x00001CC0
		[Conditional("DEBUG")]
		public static void Assert(string description)
		{
			AssertHelper.FireAssert(description);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public static void AssertAndThrow(bool condition, string description)
		{
			if (!condition)
			{
				Fx.AssertAndThrow(description);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndThrow(string description)
		{
			TraceCore.ShipAssertExceptionMessage(Fx.Trace, description);
			throw new Fx.InternalException(description);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003AE7 File Offset: 0x00001CE7
		public static void AssertAndThrowFatal(bool condition, string description)
		{
			if (!condition)
			{
				Fx.AssertAndThrowFatal(description);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003AF3 File Offset: 0x00001CF3
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndThrowFatal(string description)
		{
			TraceCore.ShipAssertExceptionMessage(Fx.Trace, description);
			throw new Fx.FatalInternalException(description);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003B06 File Offset: 0x00001D06
		public static void AssertAndFailFast(bool condition, string description)
		{
			if (!condition)
			{
				Fx.AssertAndFailFast(description);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003B14 File Offset: 0x00001D14
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Exception AssertAndFailFast(string description)
		{
			string message = InternalSR.FailFastMessage(description);
			try
			{
				try
				{
					Fx.Exception.TraceFailFast(message);
				}
				finally
				{
					Environment.FailFast(message);
				}
			}
			catch
			{
				throw;
			}
			return null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B60 File Offset: 0x00001D60
		public static bool IsFatal(Exception exception)
		{
			while (exception != null)
			{
				if (exception is FatalException || (exception is OutOfMemoryException && !(exception is InsufficientMemoryException)) || exception is ThreadAbortException || exception is Fx.FatalInternalException)
				{
					return true;
				}
				if (exception is TypeInitializationException || exception is TargetInvocationException)
				{
					exception = exception.InnerException;
				}
				else
				{
					if (exception is AggregateException)
					{
						using (IEnumerator<Exception> enumerator = ((AggregateException)exception).InnerExceptions.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (Fx.IsFatal(enumerator.Current))
								{
									return true;
								}
							}
							break;
						}
						continue;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003C10 File Offset: 0x00001E10
		internal static bool AssertsFailFast
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003C13 File Offset: 0x00001E13
		internal static Type[] BreakOnExceptionTypes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003C16 File Offset: 0x00001E16
		internal static bool FastDebug
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003C19 File Offset: 0x00001E19
		internal static bool StealthDebugger
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static Action<T1> ThunkCallback<T1>(Action<T1> callback)
		{
			return new Fx.ActionThunk<T1>(callback).ThunkFrame;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C29 File Offset: 0x00001E29
		public static AsyncCallback ThunkCallback(AsyncCallback callback)
		{
			return new Fx.AsyncThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C36 File Offset: 0x00001E36
		public static WaitCallback ThunkCallback(WaitCallback callback)
		{
			return new Fx.WaitThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003C43 File Offset: 0x00001E43
		public static TimerCallback ThunkCallback(TimerCallback callback)
		{
			return new Fx.TimerThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003C50 File Offset: 0x00001E50
		public static WaitOrTimerCallback ThunkCallback(WaitOrTimerCallback callback)
		{
			return new Fx.WaitOrTimerThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003C5D File Offset: 0x00001E5D
		public static SendOrPostCallback ThunkCallback(SendOrPostCallback callback)
		{
			return new Fx.SendOrPostThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003C6A File Offset: 0x00001E6A
		[SecurityCritical]
		public static IOCompletionCallback ThunkCallback(IOCompletionCallback callback)
		{
			return new Fx.IOCompletionThunk(callback).ThunkFrame;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003C78 File Offset: 0x00001E78
		public static Guid CreateGuid(string guidString)
		{
			bool flag = false;
			Guid result = Guid.Empty;
			try
			{
				result = new Guid(guidString);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					Fx.AssertAndThrow("Creation of the Guid failed.");
				}
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public static bool TryCreateGuid(string guidString, out Guid result)
		{
			bool result2 = false;
			result = Guid.Empty;
			try
			{
				result = new Guid(guidString);
				result2 = true;
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			catch (OverflowException)
			{
			}
			return result2;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003D14 File Offset: 0x00001F14
		public static byte[] AllocateByteArray(int size)
		{
			byte[] result;
			try
			{
				result = new byte[size];
			}
			catch (OutOfMemoryException innerException)
			{
				throw Fx.Exception.AsError(new InsufficientMemoryException(InternalSR.BufferAllocationFailed(size), innerException));
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003D54 File Offset: 0x00001F54
		public static char[] AllocateCharArray(int size)
		{
			char[] result;
			try
			{
				result = new char[size];
			}
			catch (OutOfMemoryException innerException)
			{
				throw Fx.Exception.AsError(new InsufficientMemoryException(InternalSR.BufferAllocationFailed(size * 2), innerException));
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003D98 File Offset: 0x00001F98
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static void TraceExceptionNoThrow(Exception exception)
		{
			try
			{
				Fx.Exception.TraceUnhandledException(exception);
			}
			catch
			{
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003DC8 File Offset: 0x00001FC8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private static bool HandleAtThreadBase(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			Fx.TraceExceptionNoThrow(exception);
			try
			{
				Fx.ExceptionHandler exceptionHandler = Fx.AsynchronousThreadExceptionHandler;
				return exceptionHandler != null && exceptionHandler.HandleException(exception);
			}
			catch (Exception exception2)
			{
				Fx.TraceExceptionNoThrow(exception2);
			}
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003E10 File Offset: 0x00002010
		private static void UpdateLevel(EtwDiagnosticTrace trace)
		{
			if (trace == null)
			{
				return;
			}
			if (TraceCore.ActionItemCallbackInvokedIsEnabled(trace) || TraceCore.ActionItemScheduledIsEnabled(trace))
			{
				trace.SetEnd2EndActivityTracingEnabled(true);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003E2D File Offset: 0x0000202D
		private static void UpdateLevel()
		{
			Fx.UpdateLevel(Fx.Trace);
		}

		// Token: 0x04000099 RID: 153
		private const string defaultEventSource = "System.Runtime";

		// Token: 0x0400009A RID: 154
		private static ExceptionTrace exceptionTrace;

		// Token: 0x0400009B RID: 155
		private static EtwDiagnosticTrace diagnosticTrace;

		// Token: 0x0400009C RID: 156
		[SecurityCritical]
		private static Fx.ExceptionHandler asynchronousThreadExceptionHandler;

		// Token: 0x0200005E RID: 94
		public abstract class ExceptionHandler
		{
			// Token: 0x0600035F RID: 863
			public abstract bool HandleException(Exception exception);

			// Token: 0x06000360 RID: 864 RVA: 0x00010FA7 File Offset: 0x0000F1A7
			protected ExceptionHandler()
			{
			}
		}

		// Token: 0x0200005F RID: 95
		public static class Tag
		{
			// Token: 0x0200009C RID: 156
			public enum CacheAttrition
			{
				// Token: 0x0400030B RID: 779
				None,
				// Token: 0x0400030C RID: 780
				ElementOnTimer,
				// Token: 0x0400030D RID: 781
				ElementOnGC,
				// Token: 0x0400030E RID: 782
				ElementOnCallback,
				// Token: 0x0400030F RID: 783
				FullPurgeOnTimer,
				// Token: 0x04000310 RID: 784
				FullPurgeOnEachAccess,
				// Token: 0x04000311 RID: 785
				PartialPurgeOnTimer,
				// Token: 0x04000312 RID: 786
				PartialPurgeOnEachAccess
			}

			// Token: 0x0200009D RID: 157
			public enum ThrottleAction
			{
				// Token: 0x04000314 RID: 788
				Reject,
				// Token: 0x04000315 RID: 789
				Pause
			}

			// Token: 0x0200009E RID: 158
			public enum ThrottleMetric
			{
				// Token: 0x04000317 RID: 791
				Count,
				// Token: 0x04000318 RID: 792
				Rate,
				// Token: 0x04000319 RID: 793
				Other
			}

			// Token: 0x0200009F RID: 159
			public enum Location
			{
				// Token: 0x0400031B RID: 795
				InProcess,
				// Token: 0x0400031C RID: 796
				OutOfProcess,
				// Token: 0x0400031D RID: 797
				LocalSystem,
				// Token: 0x0400031E RID: 798
				LocalOrRemoteSystem,
				// Token: 0x0400031F RID: 799
				RemoteSystem
			}

			// Token: 0x020000A0 RID: 160
			public enum SynchronizationKind
			{
				// Token: 0x04000321 RID: 801
				LockStatement,
				// Token: 0x04000322 RID: 802
				MonitorWait,
				// Token: 0x04000323 RID: 803
				MonitorExplicit,
				// Token: 0x04000324 RID: 804
				InterlockedNoSpin,
				// Token: 0x04000325 RID: 805
				InterlockedWithSpin,
				// Token: 0x04000326 RID: 806
				FromFieldType
			}

			// Token: 0x020000A1 RID: 161
			[Flags]
			public enum BlocksUsing
			{
				// Token: 0x04000328 RID: 808
				MonitorEnter = 0,
				// Token: 0x04000329 RID: 809
				MonitorWait = 1,
				// Token: 0x0400032A RID: 810
				ManualResetEvent = 2,
				// Token: 0x0400032B RID: 811
				AutoResetEvent = 3,
				// Token: 0x0400032C RID: 812
				AsyncResult = 4,
				// Token: 0x0400032D RID: 813
				IAsyncResult = 5,
				// Token: 0x0400032E RID: 814
				PInvoke = 6,
				// Token: 0x0400032F RID: 815
				InputQueue = 7,
				// Token: 0x04000330 RID: 816
				ThreadNeutralSemaphore = 8,
				// Token: 0x04000331 RID: 817
				PrivatePrimitive = 9,
				// Token: 0x04000332 RID: 818
				OtherInternalPrimitive = 10,
				// Token: 0x04000333 RID: 819
				OtherFrameworkPrimitive = 11,
				// Token: 0x04000334 RID: 820
				OtherInterop = 12,
				// Token: 0x04000335 RID: 821
				Other = 13,
				// Token: 0x04000336 RID: 822
				NonBlocking = 14
			}

			// Token: 0x020000A2 RID: 162
			public static class Strings
			{
				// Token: 0x04000337 RID: 823
				internal const string ExternallyManaged = "externally managed";

				// Token: 0x04000338 RID: 824
				internal const string AppDomain = "AppDomain";

				// Token: 0x04000339 RID: 825
				internal const string DeclaringInstance = "instance of declaring class";

				// Token: 0x0400033A RID: 826
				internal const string Unbounded = "unbounded";

				// Token: 0x0400033B RID: 827
				internal const string Infinite = "infinite";
			}

			// Token: 0x020000A3 RID: 163
			[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
			[Conditional("DEBUG")]
			public sealed class FriendAccessAllowedAttribute : Attribute
			{
				// Token: 0x06000446 RID: 1094 RVA: 0x000136F5 File Offset: 0x000118F5
				public FriendAccessAllowedAttribute(string assemblyName)
				{
					this.AssemblyName = assemblyName;
				}

				// Token: 0x170000AE RID: 174
				// (get) Token: 0x06000447 RID: 1095 RVA: 0x00013704 File Offset: 0x00011904
				// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001370C File Offset: 0x0001190C
				public string AssemblyName
				{
					[CompilerGenerated]
					get
					{
						return this.<AssemblyName>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<AssemblyName>k__BackingField = value;
					}
				}

				// Token: 0x0400033C RID: 828
				[CompilerGenerated]
				private string <AssemblyName>k__BackingField;
			}

			// Token: 0x020000A4 RID: 164
			public static class Throws
			{
				// Token: 0x020000B7 RID: 183
				[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
				[Conditional("CODE_ANALYSIS_CDF")]
				public sealed class TimeoutAttribute : Fx.Tag.ThrowsAttribute
				{
					// Token: 0x060004AE RID: 1198 RVA: 0x00013EAC File Offset: 0x000120AC
					public TimeoutAttribute() : this("The operation timed out.")
					{
					}

					// Token: 0x060004AF RID: 1199 RVA: 0x00013EB9 File Offset: 0x000120B9
					public TimeoutAttribute(string diagnosis) : base(typeof(TimeoutException), diagnosis)
					{
					}
				}
			}

			// Token: 0x020000A5 RID: 165
			[AttributeUsage(AttributeTargets.Field)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class CacheAttribute : Attribute
			{
				// Token: 0x06000449 RID: 1097 RVA: 0x00013718 File Offset: 0x00011918
				public CacheAttribute(Type elementType, Fx.Tag.CacheAttrition cacheAttrition)
				{
					this.Scope = "instance of declaring class";
					this.SizeLimit = "unbounded";
					this.Timeout = "infinite";
					if (elementType == null)
					{
						throw Fx.Exception.ArgumentNull("elementType");
					}
					this.elementType = elementType;
					this.cacheAttrition = cacheAttrition;
				}

				// Token: 0x170000AF RID: 175
				// (get) Token: 0x0600044A RID: 1098 RVA: 0x00013773 File Offset: 0x00011973
				public Type ElementType
				{
					get
					{
						return this.elementType;
					}
				}

				// Token: 0x170000B0 RID: 176
				// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001377B File Offset: 0x0001197B
				public Fx.Tag.CacheAttrition CacheAttrition
				{
					get
					{
						return this.cacheAttrition;
					}
				}

				// Token: 0x170000B1 RID: 177
				// (get) Token: 0x0600044C RID: 1100 RVA: 0x00013783 File Offset: 0x00011983
				// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001378B File Offset: 0x0001198B
				public string Scope
				{
					[CompilerGenerated]
					get
					{
						return this.<Scope>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Scope>k__BackingField = value;
					}
				}

				// Token: 0x170000B2 RID: 178
				// (get) Token: 0x0600044E RID: 1102 RVA: 0x00013794 File Offset: 0x00011994
				// (set) Token: 0x0600044F RID: 1103 RVA: 0x0001379C File Offset: 0x0001199C
				public string SizeLimit
				{
					[CompilerGenerated]
					get
					{
						return this.<SizeLimit>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<SizeLimit>k__BackingField = value;
					}
				}

				// Token: 0x170000B3 RID: 179
				// (get) Token: 0x06000450 RID: 1104 RVA: 0x000137A5 File Offset: 0x000119A5
				// (set) Token: 0x06000451 RID: 1105 RVA: 0x000137AD File Offset: 0x000119AD
				public string Timeout
				{
					[CompilerGenerated]
					get
					{
						return this.<Timeout>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Timeout>k__BackingField = value;
					}
				}

				// Token: 0x0400033D RID: 829
				private readonly Type elementType;

				// Token: 0x0400033E RID: 830
				private readonly Fx.Tag.CacheAttrition cacheAttrition;

				// Token: 0x0400033F RID: 831
				[CompilerGenerated]
				private string <Scope>k__BackingField;

				// Token: 0x04000340 RID: 832
				[CompilerGenerated]
				private string <SizeLimit>k__BackingField;

				// Token: 0x04000341 RID: 833
				[CompilerGenerated]
				private string <Timeout>k__BackingField;
			}

			// Token: 0x020000A6 RID: 166
			[AttributeUsage(AttributeTargets.Field)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class QueueAttribute : Attribute
			{
				// Token: 0x06000452 RID: 1106 RVA: 0x000137B6 File Offset: 0x000119B6
				public QueueAttribute(Type elementType)
				{
					this.Scope = "instance of declaring class";
					this.SizeLimit = "unbounded";
					if (elementType == null)
					{
						throw Fx.Exception.ArgumentNull("elementType");
					}
					this.elementType = elementType;
				}

				// Token: 0x170000B4 RID: 180
				// (get) Token: 0x06000453 RID: 1107 RVA: 0x000137F4 File Offset: 0x000119F4
				public Type ElementType
				{
					get
					{
						return this.elementType;
					}
				}

				// Token: 0x170000B5 RID: 181
				// (get) Token: 0x06000454 RID: 1108 RVA: 0x000137FC File Offset: 0x000119FC
				// (set) Token: 0x06000455 RID: 1109 RVA: 0x00013804 File Offset: 0x00011A04
				public string Scope
				{
					[CompilerGenerated]
					get
					{
						return this.<Scope>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Scope>k__BackingField = value;
					}
				}

				// Token: 0x170000B6 RID: 182
				// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001380D File Offset: 0x00011A0D
				// (set) Token: 0x06000457 RID: 1111 RVA: 0x00013815 File Offset: 0x00011A15
				public string SizeLimit
				{
					[CompilerGenerated]
					get
					{
						return this.<SizeLimit>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<SizeLimit>k__BackingField = value;
					}
				}

				// Token: 0x170000B7 RID: 183
				// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001381E File Offset: 0x00011A1E
				// (set) Token: 0x06000459 RID: 1113 RVA: 0x00013826 File Offset: 0x00011A26
				public bool StaleElementsRemovedImmediately
				{
					[CompilerGenerated]
					get
					{
						return this.<StaleElementsRemovedImmediately>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<StaleElementsRemovedImmediately>k__BackingField = value;
					}
				}

				// Token: 0x170000B8 RID: 184
				// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001382F File Offset: 0x00011A2F
				// (set) Token: 0x0600045B RID: 1115 RVA: 0x00013837 File Offset: 0x00011A37
				public bool EnqueueThrowsIfFull
				{
					[CompilerGenerated]
					get
					{
						return this.<EnqueueThrowsIfFull>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<EnqueueThrowsIfFull>k__BackingField = value;
					}
				}

				// Token: 0x04000342 RID: 834
				private readonly Type elementType;

				// Token: 0x04000343 RID: 835
				[CompilerGenerated]
				private string <Scope>k__BackingField;

				// Token: 0x04000344 RID: 836
				[CompilerGenerated]
				private string <SizeLimit>k__BackingField;

				// Token: 0x04000345 RID: 837
				[CompilerGenerated]
				private bool <StaleElementsRemovedImmediately>k__BackingField;

				// Token: 0x04000346 RID: 838
				[CompilerGenerated]
				private bool <EnqueueThrowsIfFull>k__BackingField;
			}

			// Token: 0x020000A7 RID: 167
			[AttributeUsage(AttributeTargets.Field)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class ThrottleAttribute : Attribute
			{
				// Token: 0x0600045C RID: 1116 RVA: 0x00013840 File Offset: 0x00011A40
				public ThrottleAttribute(Fx.Tag.ThrottleAction throttleAction, Fx.Tag.ThrottleMetric throttleMetric, string limit)
				{
					this.Scope = "AppDomain";
					if (string.IsNullOrEmpty(limit))
					{
						throw Fx.Exception.ArgumentNullOrEmpty("limit");
					}
					this.throttleAction = throttleAction;
					this.throttleMetric = throttleMetric;
					this.limit = limit;
				}

				// Token: 0x170000B9 RID: 185
				// (get) Token: 0x0600045D RID: 1117 RVA: 0x00013880 File Offset: 0x00011A80
				public Fx.Tag.ThrottleAction ThrottleAction
				{
					get
					{
						return this.throttleAction;
					}
				}

				// Token: 0x170000BA RID: 186
				// (get) Token: 0x0600045E RID: 1118 RVA: 0x00013888 File Offset: 0x00011A88
				public Fx.Tag.ThrottleMetric ThrottleMetric
				{
					get
					{
						return this.throttleMetric;
					}
				}

				// Token: 0x170000BB RID: 187
				// (get) Token: 0x0600045F RID: 1119 RVA: 0x00013890 File Offset: 0x00011A90
				public string Limit
				{
					get
					{
						return this.limit;
					}
				}

				// Token: 0x170000BC RID: 188
				// (get) Token: 0x06000460 RID: 1120 RVA: 0x00013898 File Offset: 0x00011A98
				// (set) Token: 0x06000461 RID: 1121 RVA: 0x000138A0 File Offset: 0x00011AA0
				public string Scope
				{
					[CompilerGenerated]
					get
					{
						return this.<Scope>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Scope>k__BackingField = value;
					}
				}

				// Token: 0x04000347 RID: 839
				private readonly Fx.Tag.ThrottleAction throttleAction;

				// Token: 0x04000348 RID: 840
				private readonly Fx.Tag.ThrottleMetric throttleMetric;

				// Token: 0x04000349 RID: 841
				private readonly string limit;

				// Token: 0x0400034A RID: 842
				[CompilerGenerated]
				private string <Scope>k__BackingField;
			}

			// Token: 0x020000A8 RID: 168
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class ExternalResourceAttribute : Attribute
			{
				// Token: 0x06000462 RID: 1122 RVA: 0x000138A9 File Offset: 0x00011AA9
				public ExternalResourceAttribute(Fx.Tag.Location location, string description)
				{
					this.location = location;
					this.description = description;
				}

				// Token: 0x170000BD RID: 189
				// (get) Token: 0x06000463 RID: 1123 RVA: 0x000138BF File Offset: 0x00011ABF
				public Fx.Tag.Location Location
				{
					get
					{
						return this.location;
					}
				}

				// Token: 0x170000BE RID: 190
				// (get) Token: 0x06000464 RID: 1124 RVA: 0x000138C7 File Offset: 0x00011AC7
				public string Description
				{
					get
					{
						return this.description;
					}
				}

				// Token: 0x0400034B RID: 843
				private readonly Fx.Tag.Location location;

				// Token: 0x0400034C RID: 844
				private readonly string description;
			}

			// Token: 0x020000A9 RID: 169
			[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class SynchronizationObjectAttribute : Attribute
			{
				// Token: 0x06000465 RID: 1125 RVA: 0x000138CF File Offset: 0x00011ACF
				public SynchronizationObjectAttribute()
				{
					this.Blocking = true;
					this.Scope = "instance of declaring class";
					this.Kind = Fx.Tag.SynchronizationKind.FromFieldType;
				}

				// Token: 0x170000BF RID: 191
				// (get) Token: 0x06000466 RID: 1126 RVA: 0x000138F0 File Offset: 0x00011AF0
				// (set) Token: 0x06000467 RID: 1127 RVA: 0x000138F8 File Offset: 0x00011AF8
				public bool Blocking
				{
					[CompilerGenerated]
					get
					{
						return this.<Blocking>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Blocking>k__BackingField = value;
					}
				}

				// Token: 0x170000C0 RID: 192
				// (get) Token: 0x06000468 RID: 1128 RVA: 0x00013901 File Offset: 0x00011B01
				// (set) Token: 0x06000469 RID: 1129 RVA: 0x00013909 File Offset: 0x00011B09
				public string Scope
				{
					[CompilerGenerated]
					get
					{
						return this.<Scope>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Scope>k__BackingField = value;
					}
				}

				// Token: 0x170000C1 RID: 193
				// (get) Token: 0x0600046A RID: 1130 RVA: 0x00013912 File Offset: 0x00011B12
				// (set) Token: 0x0600046B RID: 1131 RVA: 0x0001391A File Offset: 0x00011B1A
				public Fx.Tag.SynchronizationKind Kind
				{
					[CompilerGenerated]
					get
					{
						return this.<Kind>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Kind>k__BackingField = value;
					}
				}

				// Token: 0x0400034D RID: 845
				[CompilerGenerated]
				private bool <Blocking>k__BackingField;

				// Token: 0x0400034E RID: 846
				[CompilerGenerated]
				private string <Scope>k__BackingField;

				// Token: 0x0400034F RID: 847
				[CompilerGenerated]
				private Fx.Tag.SynchronizationKind <Kind>k__BackingField;
			}

			// Token: 0x020000AA RID: 170
			[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class SynchronizationPrimitiveAttribute : Attribute
			{
				// Token: 0x0600046C RID: 1132 RVA: 0x00013923 File Offset: 0x00011B23
				public SynchronizationPrimitiveAttribute(Fx.Tag.BlocksUsing blocksUsing)
				{
					this.blocksUsing = blocksUsing;
				}

				// Token: 0x170000C2 RID: 194
				// (get) Token: 0x0600046D RID: 1133 RVA: 0x00013932 File Offset: 0x00011B32
				public Fx.Tag.BlocksUsing BlocksUsing
				{
					get
					{
						return this.blocksUsing;
					}
				}

				// Token: 0x170000C3 RID: 195
				// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001393A File Offset: 0x00011B3A
				// (set) Token: 0x0600046F RID: 1135 RVA: 0x00013942 File Offset: 0x00011B42
				public bool SupportsAsync
				{
					[CompilerGenerated]
					get
					{
						return this.<SupportsAsync>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<SupportsAsync>k__BackingField = value;
					}
				}

				// Token: 0x170000C4 RID: 196
				// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001394B File Offset: 0x00011B4B
				// (set) Token: 0x06000471 RID: 1137 RVA: 0x00013953 File Offset: 0x00011B53
				public bool Spins
				{
					[CompilerGenerated]
					get
					{
						return this.<Spins>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Spins>k__BackingField = value;
					}
				}

				// Token: 0x170000C5 RID: 197
				// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001395C File Offset: 0x00011B5C
				// (set) Token: 0x06000473 RID: 1139 RVA: 0x00013964 File Offset: 0x00011B64
				public string ReleaseMethod
				{
					[CompilerGenerated]
					get
					{
						return this.<ReleaseMethod>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<ReleaseMethod>k__BackingField = value;
					}
				}

				// Token: 0x04000350 RID: 848
				private readonly Fx.Tag.BlocksUsing blocksUsing;

				// Token: 0x04000351 RID: 849
				[CompilerGenerated]
				private bool <SupportsAsync>k__BackingField;

				// Token: 0x04000352 RID: 850
				[CompilerGenerated]
				private bool <Spins>k__BackingField;

				// Token: 0x04000353 RID: 851
				[CompilerGenerated]
				private string <ReleaseMethod>k__BackingField;
			}

			// Token: 0x020000AB RID: 171
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class BlockingAttribute : Attribute
			{
				// Token: 0x06000474 RID: 1140 RVA: 0x0001396D File Offset: 0x00011B6D
				public BlockingAttribute()
				{
				}

				// Token: 0x170000C6 RID: 198
				// (get) Token: 0x06000475 RID: 1141 RVA: 0x00013975 File Offset: 0x00011B75
				// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001397D File Offset: 0x00011B7D
				public string CancelMethod
				{
					[CompilerGenerated]
					get
					{
						return this.<CancelMethod>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<CancelMethod>k__BackingField = value;
					}
				}

				// Token: 0x170000C7 RID: 199
				// (get) Token: 0x06000477 RID: 1143 RVA: 0x00013986 File Offset: 0x00011B86
				// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001398E File Offset: 0x00011B8E
				public Type CancelDeclaringType
				{
					[CompilerGenerated]
					get
					{
						return this.<CancelDeclaringType>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<CancelDeclaringType>k__BackingField = value;
					}
				}

				// Token: 0x170000C8 RID: 200
				// (get) Token: 0x06000479 RID: 1145 RVA: 0x00013997 File Offset: 0x00011B97
				// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001399F File Offset: 0x00011B9F
				public string Conditional
				{
					[CompilerGenerated]
					get
					{
						return this.<Conditional>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Conditional>k__BackingField = value;
					}
				}

				// Token: 0x04000354 RID: 852
				[CompilerGenerated]
				private string <CancelMethod>k__BackingField;

				// Token: 0x04000355 RID: 853
				[CompilerGenerated]
				private Type <CancelDeclaringType>k__BackingField;

				// Token: 0x04000356 RID: 854
				[CompilerGenerated]
				private string <Conditional>k__BackingField;
			}

			// Token: 0x020000AC RID: 172
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class GuaranteeNonBlockingAttribute : Attribute
			{
				// Token: 0x0600047B RID: 1147 RVA: 0x000139A8 File Offset: 0x00011BA8
				public GuaranteeNonBlockingAttribute()
				{
				}
			}

			// Token: 0x020000AD RID: 173
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class NonThrowingAttribute : Attribute
			{
				// Token: 0x0600047C RID: 1148 RVA: 0x000139B0 File Offset: 0x00011BB0
				public NonThrowingAttribute()
				{
				}
			}

			// Token: 0x020000AE RID: 174
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public class ThrowsAttribute : Attribute
			{
				// Token: 0x0600047D RID: 1149 RVA: 0x000139B8 File Offset: 0x00011BB8
				public ThrowsAttribute(Type exceptionType, string diagnosis)
				{
					if (exceptionType == null)
					{
						throw Fx.Exception.ArgumentNull("exceptionType");
					}
					if (string.IsNullOrEmpty(diagnosis))
					{
						throw Fx.Exception.ArgumentNullOrEmpty("diagnosis");
					}
					this.exceptionType = exceptionType;
					this.diagnosis = diagnosis;
				}

				// Token: 0x170000C9 RID: 201
				// (get) Token: 0x0600047E RID: 1150 RVA: 0x00013A0A File Offset: 0x00011C0A
				public Type ExceptionType
				{
					get
					{
						return this.exceptionType;
					}
				}

				// Token: 0x170000CA RID: 202
				// (get) Token: 0x0600047F RID: 1151 RVA: 0x00013A12 File Offset: 0x00011C12
				public string Diagnosis
				{
					get
					{
						return this.diagnosis;
					}
				}

				// Token: 0x04000357 RID: 855
				private readonly Type exceptionType;

				// Token: 0x04000358 RID: 856
				private readonly string diagnosis;
			}

			// Token: 0x020000AF RID: 175
			[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class InheritThrowsAttribute : Attribute
			{
				// Token: 0x06000480 RID: 1152 RVA: 0x00013A1A File Offset: 0x00011C1A
				public InheritThrowsAttribute()
				{
				}

				// Token: 0x170000CB RID: 203
				// (get) Token: 0x06000481 RID: 1153 RVA: 0x00013A22 File Offset: 0x00011C22
				// (set) Token: 0x06000482 RID: 1154 RVA: 0x00013A2A File Offset: 0x00011C2A
				public Type FromDeclaringType
				{
					[CompilerGenerated]
					get
					{
						return this.<FromDeclaringType>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<FromDeclaringType>k__BackingField = value;
					}
				}

				// Token: 0x170000CC RID: 204
				// (get) Token: 0x06000483 RID: 1155 RVA: 0x00013A33 File Offset: 0x00011C33
				// (set) Token: 0x06000484 RID: 1156 RVA: 0x00013A3B File Offset: 0x00011C3B
				public string From
				{
					[CompilerGenerated]
					get
					{
						return this.<From>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<From>k__BackingField = value;
					}
				}

				// Token: 0x04000359 RID: 857
				[CompilerGenerated]
				private Type <FromDeclaringType>k__BackingField;

				// Token: 0x0400035A RID: 858
				[CompilerGenerated]
				private string <From>k__BackingField;
			}

			// Token: 0x020000B0 RID: 176
			[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class KnownXamlExternalAttribute : Attribute
			{
				// Token: 0x06000485 RID: 1157 RVA: 0x00013A44 File Offset: 0x00011C44
				public KnownXamlExternalAttribute()
				{
				}
			}

			// Token: 0x020000B1 RID: 177
			[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class XamlVisibleAttribute : Attribute
			{
				// Token: 0x06000486 RID: 1158 RVA: 0x00013A4C File Offset: 0x00011C4C
				public XamlVisibleAttribute() : this(true)
				{
				}

				// Token: 0x06000487 RID: 1159 RVA: 0x00013A55 File Offset: 0x00011C55
				public XamlVisibleAttribute(bool visible)
				{
					this.Visible = visible;
				}

				// Token: 0x170000CD RID: 205
				// (get) Token: 0x06000488 RID: 1160 RVA: 0x00013A64 File Offset: 0x00011C64
				// (set) Token: 0x06000489 RID: 1161 RVA: 0x00013A6C File Offset: 0x00011C6C
				public bool Visible
				{
					[CompilerGenerated]
					get
					{
						return this.<Visible>k__BackingField;
					}
					[CompilerGenerated]
					private set
					{
						this.<Visible>k__BackingField = value;
					}
				}

				// Token: 0x0400035B RID: 859
				[CompilerGenerated]
				private bool <Visible>k__BackingField;
			}

			// Token: 0x020000B2 RID: 178
			[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
			[Conditional("CODE_ANALYSIS_CDF")]
			public sealed class SecurityNoteAttribute : Attribute
			{
				// Token: 0x0600048A RID: 1162 RVA: 0x00013A75 File Offset: 0x00011C75
				public SecurityNoteAttribute()
				{
				}

				// Token: 0x170000CE RID: 206
				// (get) Token: 0x0600048B RID: 1163 RVA: 0x00013A7D File Offset: 0x00011C7D
				// (set) Token: 0x0600048C RID: 1164 RVA: 0x00013A85 File Offset: 0x00011C85
				public string Critical
				{
					[CompilerGenerated]
					get
					{
						return this.<Critical>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Critical>k__BackingField = value;
					}
				}

				// Token: 0x170000CF RID: 207
				// (get) Token: 0x0600048D RID: 1165 RVA: 0x00013A8E File Offset: 0x00011C8E
				// (set) Token: 0x0600048E RID: 1166 RVA: 0x00013A96 File Offset: 0x00011C96
				public string Safe
				{
					[CompilerGenerated]
					get
					{
						return this.<Safe>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Safe>k__BackingField = value;
					}
				}

				// Token: 0x170000D0 RID: 208
				// (get) Token: 0x0600048F RID: 1167 RVA: 0x00013A9F File Offset: 0x00011C9F
				// (set) Token: 0x06000490 RID: 1168 RVA: 0x00013AA7 File Offset: 0x00011CA7
				public string Miscellaneous
				{
					[CompilerGenerated]
					get
					{
						return this.<Miscellaneous>k__BackingField;
					}
					[CompilerGenerated]
					set
					{
						this.<Miscellaneous>k__BackingField = value;
					}
				}

				// Token: 0x0400035C RID: 860
				[CompilerGenerated]
				private string <Critical>k__BackingField;

				// Token: 0x0400035D RID: 861
				[CompilerGenerated]
				private string <Safe>k__BackingField;

				// Token: 0x0400035E RID: 862
				[CompilerGenerated]
				private string <Miscellaneous>k__BackingField;
			}
		}

		// Token: 0x02000060 RID: 96
		private abstract class Thunk<T> where T : class
		{
			// Token: 0x06000361 RID: 865 RVA: 0x00010FAF File Offset: 0x0000F1AF
			[SecuritySafeCritical]
			protected Thunk(T callback)
			{
				this.callback = callback;
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x06000362 RID: 866 RVA: 0x00010FBE File Offset: 0x0000F1BE
			internal T Callback
			{
				[SecuritySafeCritical]
				get
				{
					return this.callback;
				}
			}

			// Token: 0x0400021C RID: 540
			[SecurityCritical]
			private T callback;
		}

		// Token: 0x02000061 RID: 97
		private sealed class ActionThunk<T1> : Fx.Thunk<Action<T1>>
		{
			// Token: 0x06000363 RID: 867 RVA: 0x00010FC6 File Offset: 0x0000F1C6
			public ActionThunk(Action<T1> callback) : base(callback)
			{
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000364 RID: 868 RVA: 0x00010FCF File Offset: 0x0000F1CF
			public Action<T1> ThunkFrame
			{
				get
				{
					return new Action<T1>(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000365 RID: 869 RVA: 0x00010FE0 File Offset: 0x0000F1E0
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(T1 result)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(result);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000062 RID: 98
		private sealed class AsyncThunk : Fx.Thunk<AsyncCallback>
		{
			// Token: 0x06000366 RID: 870 RVA: 0x0001101C File Offset: 0x0000F21C
			public AsyncThunk(AsyncCallback callback) : base(callback)
			{
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000367 RID: 871 RVA: 0x00011025 File Offset: 0x0000F225
			public AsyncCallback ThunkFrame
			{
				get
				{
					return new AsyncCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000368 RID: 872 RVA: 0x00011034 File Offset: 0x0000F234
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(IAsyncResult result)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(result);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000063 RID: 99
		private sealed class WaitThunk : Fx.Thunk<WaitCallback>
		{
			// Token: 0x06000369 RID: 873 RVA: 0x00011070 File Offset: 0x0000F270
			public WaitThunk(WaitCallback callback) : base(callback)
			{
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600036A RID: 874 RVA: 0x00011079 File Offset: 0x0000F279
			public WaitCallback ThunkFrame
			{
				get
				{
					return new WaitCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x0600036B RID: 875 RVA: 0x00011088 File Offset: 0x0000F288
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000064 RID: 100
		private sealed class TimerThunk : Fx.Thunk<TimerCallback>
		{
			// Token: 0x0600036C RID: 876 RVA: 0x000110C4 File Offset: 0x0000F2C4
			public TimerThunk(TimerCallback callback) : base(callback)
			{
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x0600036D RID: 877 RVA: 0x000110CD File Offset: 0x0000F2CD
			public TimerCallback ThunkFrame
			{
				get
				{
					return new TimerCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x0600036E RID: 878 RVA: 0x000110DC File Offset: 0x0000F2DC
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000065 RID: 101
		private sealed class WaitOrTimerThunk : Fx.Thunk<WaitOrTimerCallback>
		{
			// Token: 0x0600036F RID: 879 RVA: 0x00011118 File Offset: 0x0000F318
			public WaitOrTimerThunk(WaitOrTimerCallback callback) : base(callback)
			{
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000370 RID: 880 RVA: 0x00011121 File Offset: 0x0000F321
			public WaitOrTimerCallback ThunkFrame
			{
				get
				{
					return new WaitOrTimerCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000371 RID: 881 RVA: 0x00011130 File Offset: 0x0000F330
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state, bool timedOut)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state, timedOut);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000066 RID: 102
		private sealed class SendOrPostThunk : Fx.Thunk<SendOrPostCallback>
		{
			// Token: 0x06000372 RID: 882 RVA: 0x0001116C File Offset: 0x0000F36C
			public SendOrPostThunk(SendOrPostCallback callback) : base(callback)
			{
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000373 RID: 883 RVA: 0x00011175 File Offset: 0x0000F375
			public SendOrPostCallback ThunkFrame
			{
				get
				{
					return new SendOrPostCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000374 RID: 884 RVA: 0x00011184 File Offset: 0x0000F384
			[SecuritySafeCritical]
			private void UnhandledExceptionFrame(object state)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					base.Callback(state);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x02000067 RID: 103
		[SecurityCritical]
		private sealed class IOCompletionThunk
		{
			// Token: 0x06000375 RID: 885 RVA: 0x000111C0 File Offset: 0x0000F3C0
			public IOCompletionThunk(IOCompletionCallback callback)
			{
				this.callback = callback;
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000376 RID: 886 RVA: 0x000111CF File Offset: 0x0000F3CF
			public IOCompletionCallback ThunkFrame
			{
				get
				{
					return new IOCompletionCallback(this.UnhandledExceptionFrame);
				}
			}

			// Token: 0x06000377 RID: 887 RVA: 0x000111E0 File Offset: 0x0000F3E0
			private unsafe void UnhandledExceptionFrame(uint error, uint bytesRead, NativeOverlapped* nativeOverlapped)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this.callback(error, bytesRead, nativeOverlapped);
				}
				catch (Exception exception)
				{
					if (!Fx.HandleAtThreadBase(exception))
					{
						throw;
					}
				}
			}

			// Token: 0x0400021D RID: 541
			private IOCompletionCallback callback;
		}

		// Token: 0x02000068 RID: 104
		[Serializable]
		private class InternalException : SystemException
		{
			// Token: 0x06000378 RID: 888 RVA: 0x00011220 File Offset: 0x0000F420
			public InternalException(string description) : base(InternalSR.ShipAssertExceptionMessage(description))
			{
			}

			// Token: 0x06000379 RID: 889 RVA: 0x0001122E File Offset: 0x0000F42E
			protected InternalException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}

		// Token: 0x02000069 RID: 105
		[Serializable]
		private class FatalInternalException : Fx.InternalException
		{
			// Token: 0x0600037A RID: 890 RVA: 0x00011238 File Offset: 0x0000F438
			public FatalInternalException(string description) : base(description)
			{
			}

			// Token: 0x0600037B RID: 891 RVA: 0x00011241 File Offset: 0x0000F441
			protected FatalInternalException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}

		// Token: 0x0200006A RID: 106
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600037C RID: 892 RVA: 0x0001124B File Offset: 0x0000F44B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600037D RID: 893 RVA: 0x00011257 File Offset: 0x0000F457
			public <>c()
			{
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0001125F File Offset: 0x0000F45F
			internal void <InitializeTracing>b__8_0()
			{
				Fx.UpdateLevel();
			}

			// Token: 0x0400021E RID: 542
			public static readonly Fx.<>c <>9 = new Fx.<>c();

			// Token: 0x0400021F RID: 543
			public static Action <>9__8_0;
		}
	}
}
