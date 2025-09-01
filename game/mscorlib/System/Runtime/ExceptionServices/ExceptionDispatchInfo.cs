using System;
using System.Diagnostics;
using Unity;

namespace System.Runtime.ExceptionServices
{
	/// <summary>Represents an exception whose state is captured at a certain point in code.</summary>
	// Token: 0x020007D2 RID: 2002
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x060045AB RID: 17835 RVA: 0x000E5060 File Offset: 0x000E3260
		private ExceptionDispatchInfo(Exception exception)
		{
			this.m_Exception = exception;
			StackTrace[] captured_traces = exception.captured_traces;
			int num = (captured_traces == null) ? 0 : captured_traces.Length;
			StackTrace[] array = new StackTrace[num + 1];
			if (num != 0)
			{
				Array.Copy(captured_traces, 0, array, 0, num);
			}
			array[num] = new StackTrace(exception, 0, true);
			this.m_stackTrace = array;
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x000E50B3 File Offset: 0x000E32B3
		internal object BinaryStackTraceArray
		{
			get
			{
				return this.m_stackTrace;
			}
		}

		/// <summary>Creates an <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object that represents the specified exception at the current point in code.</summary>
		/// <param name="source">The exception whose state is captured, and which is represented by the returned object.</param>
		/// <returns>An object that represents the specified exception at the current point in code.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060045AD RID: 17837 RVA: 0x000E50BB File Offset: 0x000E32BB
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", Environment.GetResourceString("Object cannot be null."));
			}
			return new ExceptionDispatchInfo(source);
		}

		/// <summary>Gets the exception that is represented by the current instance.</summary>
		/// <returns>The exception that is represented by the current instance.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060045AE RID: 17838 RVA: 0x000E50DB File Offset: 0x000E32DB
		public Exception SourceException
		{
			get
			{
				return this.m_Exception;
			}
		}

		/// <summary>Throws the exception that is represented by the current <see cref="T:System.Runtime.ExceptionServices.ExceptionDispatchInfo" /> object, after restoring the state that was saved when the exception was captured.</summary>
		// Token: 0x060045AF RID: 17839 RVA: 0x000E50E3 File Offset: 0x000E32E3
		[StackTraceHidden]
		public void Throw()
		{
			this.m_Exception.RestoreExceptionDispatchInfo(this);
			throw this.m_Exception;
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x00017829 File Offset: 0x00015A29
		[StackTraceHidden]
		public static void Throw(Exception source)
		{
			ExceptionDispatchInfo.Capture(source).Throw();
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x000173AD File Offset: 0x000155AD
		internal ExceptionDispatchInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002D14 RID: 11540
		private Exception m_Exception;

		// Token: 0x04002D15 RID: 11541
		private object m_stackTrace;
	}
}
