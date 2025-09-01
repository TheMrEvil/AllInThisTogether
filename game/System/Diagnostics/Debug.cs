using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that help debug your code.</summary>
	// Token: 0x02000215 RID: 533
	public static class Debug
	{
		/// <summary>Gets the collection of listeners that is monitoring the debug output.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceListenerCollection" /> representing a collection of type <see cref="T:System.Diagnostics.TraceListener" /> that monitors the debug output.</returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00044C16 File Offset: 0x00042E16
		public static TraceListenerCollection Listeners
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return TraceInternal.Listeners;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Diagnostics.Debug.Flush" /> should be called on the <see cref="P:System.Diagnostics.Debug.Listeners" /> after every write.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Diagnostics.Debug.Flush" /> is called on the <see cref="P:System.Diagnostics.Debug.Listeners" /> after every write; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00044C1D File Offset: 0x00042E1D
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x00044C24 File Offset: 0x00042E24
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

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is 0.</returns>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00044C2C File Offset: 0x00042E2C
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00044C33 File Offset: 0x00042E33
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
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00044C3B File Offset: 0x00042E3B
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00044C42 File Offset: 0x00042E42
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

		/// <summary>Flushes the output buffer and causes buffered data to write to the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		// Token: 0x06000F5E RID: 3934 RVA: 0x00044C4A File Offset: 0x00042E4A
		[Conditional("DEBUG")]
		public static void Flush()
		{
			TraceInternal.Flush();
		}

		/// <summary>Flushes the output buffer and then calls the <see langword="Close" /> method on each of the <see cref="P:System.Diagnostics.Debug.Listeners" />.</summary>
		// Token: 0x06000F5F RID: 3935 RVA: 0x00044C51 File Offset: 0x00042E51
		[Conditional("DEBUG")]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static void Close()
		{
			TraceInternal.Close();
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, a failure message is not sent and the message box is not displayed.</param>
		// Token: 0x06000F60 RID: 3936 RVA: 0x00044C58 File Offset: 0x00042E58
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
			TraceInternal.Assert(condition);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs a specified message and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified message is not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06000F61 RID: 3937 RVA: 0x00044C60 File Offset: 0x00042E60
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			TraceInternal.Assert(condition, message);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs two specified messages and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified messages are not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		/// <param name="detailMessage">The detailed message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		// Token: 0x06000F62 RID: 3938 RVA: 0x00044C69 File Offset: 0x00042E69
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessage)
		{
			TraceInternal.Assert(condition, message, detailMessage);
		}

		/// <summary>Checks for a condition; if the condition is <see langword="false" />, outputs two messages (simple and formatted) and displays a message box that shows the call stack.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the specified messages are not sent and the message box is not displayed.</param>
		/// <param name="message">The message to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection.</param>
		/// <param name="detailMessageFormat">The composite format string to send to the <see cref="P:System.Diagnostics.Trace.Listeners" /> collection. This message contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x06000F63 RID: 3939 RVA: 0x00044C73 File Offset: 0x00042E73
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message, string detailMessageFormat, params object[] args)
		{
			TraceInternal.Assert(condition, message, string.Format(CultureInfo.InvariantCulture, detailMessageFormat, args));
		}

		/// <summary>Emits the specified error message.</summary>
		/// <param name="message">A message to emit.</param>
		// Token: 0x06000F64 RID: 3940 RVA: 0x00044C88 File Offset: 0x00042E88
		[Conditional("DEBUG")]
		public static void Fail(string message)
		{
			TraceInternal.Fail(message);
		}

		/// <summary>Emits an error message and a detailed error message.</summary>
		/// <param name="message">A message to emit.</param>
		/// <param name="detailMessage">A detailed message to emit.</param>
		// Token: 0x06000F65 RID: 3941 RVA: 0x00044C90 File Offset: 0x00042E90
		[Conditional("DEBUG")]
		public static void Fail(string message, string detailMessage)
		{
			TraceInternal.Fail(message, detailMessage);
		}

		/// <summary>Writes a message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x06000F66 RID: 3942 RVA: 0x00044C99 File Offset: 0x00042E99
		[Conditional("DEBUG")]
		public static void Print(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes a formatted string followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array containing zero or more objects to format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.  
		/// -or-  
		/// The number that indicates an argument to format is less than zero, or greater than or equal to the number of specified objects to format.</exception>
		// Token: 0x06000F67 RID: 3943 RVA: 0x00044CA1 File Offset: 0x00042EA1
		[Conditional("DEBUG")]
		public static void Print(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06000F68 RID: 3944 RVA: 0x00044CB4 File Offset: 0x00042EB4
		[Conditional("DEBUG")]
		public static void Write(string message)
		{
			TraceInternal.Write(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06000F69 RID: 3945 RVA: 0x00044CBC File Offset: 0x00042EBC
		[Conditional("DEBUG")]
		public static void Write(object value)
		{
			TraceInternal.Write(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F6A RID: 3946 RVA: 0x00044CC4 File Offset: 0x00042EC4
		[Conditional("DEBUG")]
		public static void Write(string message, string category)
		{
			TraceInternal.Write(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F6B RID: 3947 RVA: 0x00044CCD File Offset: 0x00042ECD
		[Conditional("DEBUG")]
		public static void Write(object value, string category)
		{
			TraceInternal.Write(value, category);
		}

		/// <summary>Writes a message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		// Token: 0x06000F6C RID: 3948 RVA: 0x00044C99 File Offset: 0x00042E99
		[Conditional("DEBUG")]
		public static void WriteLine(string message)
		{
			TraceInternal.WriteLine(message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06000F6D RID: 3949 RVA: 0x00044CD6 File Offset: 0x00042ED6
		[Conditional("DEBUG")]
		public static void WriteLine(object value)
		{
			TraceInternal.WriteLine(value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F6E RID: 3950 RVA: 0x00044CDE File Offset: 0x00042EDE
		[Conditional("DEBUG")]
		public static void WriteLine(string message, string category)
		{
			TraceInternal.WriteLine(message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F6F RID: 3951 RVA: 0x00044CE7 File Offset: 0x00042EE7
		[Conditional("DEBUG")]
		public static void WriteLine(object value, string category)
		{
			TraceInternal.WriteLine(value, category);
		}

		/// <summary>Writes a formatted message followed by a line terminator to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.</summary>
		/// <param name="format">A composite format string that contains text intermixed with zero or more format items, which correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An object array that contains zero or more objects to format.</param>
		// Token: 0x06000F70 RID: 3952 RVA: 0x00044CA1 File Offset: 0x00042EA1
		[Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] args)
		{
			TraceInternal.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the message is written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06000F71 RID: 3953 RVA: 0x00044CF0 File Offset: 0x00042EF0
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message)
		{
			TraceInternal.WriteIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the value is written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06000F72 RID: 3954 RVA: 0x00044CF9 File Offset: 0x00042EF9
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value)
		{
			TraceInternal.WriteIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the category name and message are written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F73 RID: 3955 RVA: 0x00044D02 File Offset: 0x00042F02
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, string message, string category)
		{
			TraceInternal.WriteIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the category name and value are written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F74 RID: 3956 RVA: 0x00044D0C File Offset: 0x00042F0C
		[Conditional("DEBUG")]
		public static void WriteIf(bool condition, object value, string category)
		{
			TraceInternal.WriteIf(condition, value, category);
		}

		/// <summary>Writes a message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the message is written to the trace listeners in the collection.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06000F75 RID: 3957 RVA: 0x00044D16 File Offset: 0x00042F16
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message)
		{
			TraceInternal.WriteLineIf(condition, message);
		}

		/// <summary>Writes the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the value is written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		// Token: 0x06000F76 RID: 3958 RVA: 0x00044D1F File Offset: 0x00042F1F
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value)
		{
			TraceInternal.WriteLineIf(condition, value);
		}

		/// <summary>Writes a category name and message to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">
		///   <see langword="true" /> to cause a message to be written; otherwise, <see langword="false" />.</param>
		/// <param name="message">A message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F77 RID: 3959 RVA: 0x00044D28 File Offset: 0x00042F28
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, string message, string category)
		{
			TraceInternal.WriteLineIf(condition, message, category);
		}

		/// <summary>Writes a category name and the value of the object's <see cref="M:System.Object.ToString" /> method to the trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection if a condition is <see langword="true" />.</summary>
		/// <param name="condition">The conditional expression to evaluate. If the condition is <see langword="true" />, the category name and value are written to the trace listeners in the collection.</param>
		/// <param name="value">An object whose name is sent to the <see cref="P:System.Diagnostics.Debug.Listeners" />.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000F78 RID: 3960 RVA: 0x00044D32 File Offset: 0x00042F32
		[Conditional("DEBUG")]
		public static void WriteLineIf(bool condition, object value, string category)
		{
			TraceInternal.WriteLineIf(condition, value, category);
		}

		/// <summary>Increases the current <see cref="P:System.Diagnostics.Debug.IndentLevel" /> by one.</summary>
		// Token: 0x06000F79 RID: 3961 RVA: 0x00044D3C File Offset: 0x00042F3C
		[Conditional("DEBUG")]
		public static void Indent()
		{
			TraceInternal.Indent();
		}

		/// <summary>Decreases the current <see cref="P:System.Diagnostics.Debug.IndentLevel" /> by one.</summary>
		// Token: 0x06000F7A RID: 3962 RVA: 0x00044D43 File Offset: 0x00042F43
		[Conditional("DEBUG")]
		public static void Unindent()
		{
			TraceInternal.Unindent();
		}
	}
}
