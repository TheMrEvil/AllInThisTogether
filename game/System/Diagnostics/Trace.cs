using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help you trace the execution of your code. This class cannot be inherited.</summary>
	// Token: 0x0200022B RID: 555
	public sealed class Trace
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x0000219B File Offset: 0x0000039B
		private Trace()
		{
		}

		/// <summary>Gets the collection of listeners that is monitoring the trace output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> that represents a collection of type <see cref="T:System.Diagnostics.TraceListener" /> monitoring the trace output.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x00044C16 File Offset: 0x00042E16
		public static TraceListenerCollection Listeners
		{
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets whether <see cref="M:System.Diagnostics.Trace.Flush" /> should be called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Diagnostics.Trace.Flush" /> is called on the <see cref="P:System.Diagnostics.Trace.Listeners" /> after every write; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00044C1D File Offset: 0x00042E1D
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x00044C24 File Offset: 0x00042E24
		public static bool AutoFlush
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.AutoFlush;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.AutoFlush = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the global lock should be used.</summary>
		/// <returns>
		///   <see langword="true" /> if the global lock is to be used; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000472A8 File Offset: 0x000454A8
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x000472AF File Offset: 0x000454AF
		public static bool UseGlobalLock
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return TraceInternal.UseGlobalLock;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				TraceInternal.UseGlobalLock = value;
			}
		}

		/// <summary>Gets the correlation manager for the thread for this trace.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.CorrelationManager" /> object associated with the thread for this trace.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x000472B7 File Offset: 0x000454B7
		public static CorrelationManager CorrelationManager
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (Trace.correlationManager == null)
				{
					Trace.correlationManager = new CorrelationManager();
				}
				return Trace.correlationManager;
			}
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x00044C2C File Offset: 0x00042E2C
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x00044C33 File Offset: 0x00042E33
		public static int IndentLevel
		{
			get
			{
				return TraceInternal.IndentLevel;
			}
			set
			{
				TraceInternal.IndentLevel = value;
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four.</returns>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00044C3B File Offset: 0x00042E3B
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x00044C42 File Offset: 0x00042E42
		public static int IndentSize
		{
			get
			{
				return TraceInternal.IndentSize;
			}
			set
			{
				TraceInternal.IndentSize = value;
			}
		}

		/// <summary>Flushes the output buffer, and causes buffered data to be written to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</summary>
		// Token: 0x0600103C RID: 4156 RVA: 0x00044C4A File Offset: 0x00042E4A
		[Conditional("TRACE")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		/// <summary>Flushes the output buffer, and then closes the <see cref="P:System.Diagnostics.Trace.Listeners" />.</summary>
		// Token: 0x0600103D RID: 4157 RVA: 0x00044C51 File Offset: 0x00042E51
		[Conditional("TRACE")]
		public static void Close()
		{
			TraceInternal.Close();
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, a failure message is not sent and the message box is not displayed.</param>
		// Token: 0x0600103E RID: 4158 RVA: 0x00044C58 File Offset: 0x00042E58
		[Conditional("TRACE")]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified message is not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x0600103F RID: 4159 RVA: 0x00044C60 File Offset: 0x00042E60
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs two specified messages and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified messages are not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		/// <param name="detailMessage">The detailed message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06001040 RID: 4160 RVA: 0x00044C69 File Offset: 0x00042E69
		[Conditional("TRACE")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		/// <summary>Emits the specified error message.</summary>
		/// <param name="message">A message to emit.</param>
		// Token: 0x06001041 RID: 4161 RVA: 0x00044C88 File Offset: 0x00042E88
		[Conditional("TRACE")]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		/// <summary>Emits an error message, and a detailed error message.</summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		// Token: 0x06001042 RID: 4162 RVA: 0x00044C90 File Offset: 0x00042E90
		[Conditional("TRACE")]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		/// <summary>Refreshes the trace configuration data.</summary>
		// Token: 0x06001043 RID: 4163 RVA: 0x000472D5 File Offset: 0x000454D5
		public static void Refresh()
		{
			DiagnosticsConfiguration.Refresh();
			Switch.RefreshAll();
			TraceSource.RefreshAll();
			TraceInternal.Refresh();
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		// Token: 0x06001044 RID: 4164 RVA: 0x000472EB File Offset: 0x000454EB
		[Conditional("TRACE")]
		public static void TraceInformation(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, message, null);
		}

		/// <summary>Writes an informational message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06001045 RID: 4165 RVA: 0x000472F6 File Offset: 0x000454F6
		[Conditional("TRACE")]
		public static void TraceInformation(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Information, 0, format, args);
		}

		/// <summary>Writes a warning message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		// Token: 0x06001046 RID: 4166 RVA: 0x00047301 File Offset: 0x00045501
		[Conditional("TRACE")]
		public static void TraceWarning(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, message, null);
		}

		/// <summary>Writes a warning message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06001047 RID: 4167 RVA: 0x0004730C File Offset: 0x0004550C
		[Conditional("TRACE")]
		public static void TraceWarning(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Warning, 0, format, args);
		}

		/// <summary>Writes an error message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified message.</summary>
		/// <param name="message">The informative message to write.</param>
		// Token: 0x06001048 RID: 4168 RVA: 0x00047317 File Offset: 0x00045517
		[Conditional("TRACE")]
		public static void TraceError(string message)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, message, null);
		}

		/// <summary>Writes an error message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection using the specified array of objects and formatting information.</summary>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An <see langword="object" /> array containing zero or more objects to format.</param>
		// Token: 0x06001049 RID: 4169 RVA: 0x00047322 File Offset: 0x00045522
		[Conditional("TRACE")]
		public static void TraceError(string format, params object[] args)
		{
			TraceInternal.TraceEvent(TraceEventType.Error, 0, format, args);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x0600104A RID: 4170 RVA: 0x00044CB4 File Offset: 0x00042EB4
		[Conditional("TRACE")]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x0600104B RID: 4171 RVA: 0x00044CBC File Offset: 0x00042EBC
		[Conditional("TRACE")]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		/// <summary>Writes a category name and a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x0600104C RID: 4172 RVA: 0x00044CC4 File Offset: 0x00042EC4
		[Conditional("TRACE")]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x0600104D RID: 4173 RVA: 0x00044CCD File Offset: 0x00042ECD
		[Conditional("TRACE")]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x0600104E RID: 4174 RVA: 0x00044C99 File Offset: 0x00042E99
		[Conditional("TRACE")]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x0600104F RID: 4175 RVA: 0x00044CD6 File Offset: 0x00042ED6
		[Conditional("TRACE")]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06001050 RID: 4176 RVA: 0x00044CDE File Offset: 0x00042EDE
		[Conditional("TRACE")]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06001051 RID: 4177 RVA: 0x00044CE7 File Offset: 0x00042EE7
		[Conditional("TRACE")]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06001052 RID: 4178 RVA: 0x00044CF0 File Offset: 0x00042EF0
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x06001053 RID: 4179 RVA: 0x00044CF9 File Offset: 0x00042EF9
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06001054 RID: 4180 RVA: 0x00044D02 File Offset: 0x00042F02
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06001055 RID: 4181 RVA: 0x00044D0C File Offset: 0x00042F0C
		[Conditional("TRACE")]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06001056 RID: 4182 RVA: 0x00044D16 File Offset: 0x00042F16
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		// Token: 0x06001057 RID: 4183 RVA: 0x00044D1F File Offset: 0x00042F1F
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06001058 RID: 4184 RVA: 0x00044D28 File Offset: 0x00042F28
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="value">An <see cref="T:System.Object" /> whose name is sent to the <see cref="P:System.Diagnostics.Trace.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06001059 RID: 4185 RVA: 0x00044D32 File Offset: 0x00042F32
		[Conditional("TRACE")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		/// <summary>Increases the current <see cref="P:System.Diagnostics.Trace.IndentLevel" /> by one.</summary>
		// Token: 0x0600105A RID: 4186 RVA: 0x00044D3C File Offset: 0x00042F3C
		[Conditional("TRACE")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		/// <summary>Decreases the current <see cref="P:System.Diagnostics.Trace.IndentLevel" /> by one.</summary>
		// Token: 0x0600105B RID: 4187 RVA: 0x00044D43 File Offset: 0x00042F43
		[Conditional("TRACE")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}

		// Token: 0x040009D2 RID: 2514
		private static volatile CorrelationManager correlationManager;
	}
}
