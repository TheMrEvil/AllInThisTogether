using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides the default output methods and behavior for tracing.</summary>
	// Token: 0x02000252 RID: 594
	public class DefaultTraceListener : TraceListener
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x0004EA6C File Offset: 0x0004CC6C
		static DefaultTraceListener()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_TRACE_LISTENER");
				if (environmentVariable != null)
				{
					string monoTraceFile;
					string monoTracePrefix;
					if (environmentVariable.StartsWith("Console.Out"))
					{
						monoTraceFile = "Console.Out";
						monoTracePrefix = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Out");
					}
					else if (environmentVariable.StartsWith("Console.Error"))
					{
						monoTraceFile = "Console.Error";
						monoTracePrefix = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Error");
					}
					else
					{
						monoTraceFile = environmentVariable;
						monoTracePrefix = "";
					}
					DefaultTraceListener.MonoTraceFile = monoTraceFile;
					DefaultTraceListener.MonoTracePrefix = monoTracePrefix;
				}
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004EAF6 File Offset: 0x0004CCF6
		private static string GetPrefix(string var, string target)
		{
			if (var.Length > target.Length)
			{
				return var.Substring(target.Length + 1);
			}
			return "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DefaultTraceListener" /> class with "Default" as its <see cref="P:System.Diagnostics.TraceListener.Name" /> property value.</summary>
		// Token: 0x06001248 RID: 4680 RVA: 0x0004EB1A File Offset: 0x0004CD1A
		public DefaultTraceListener() : base("Default")
		{
		}

		/// <summary>Gets or sets a value indicating whether the application is running in user-interface mode.</summary>
		/// <returns>
		///   <see langword="true" /> if user-interface mode is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0004EB27 File Offset: 0x0004CD27
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x0004EB2F File Offset: 0x0004CD2F
		[MonoTODO("AssertUiEnabled defaults to False; should follow Environment.UserInteractive.")]
		public bool AssertUiEnabled
		{
			get
			{
				return this.assertUiEnabled;
			}
			set
			{
				this.assertUiEnabled = value;
			}
		}

		/// <summary>Gets or sets the name of a log file to write trace or debug messages to.</summary>
		/// <returns>The name of a log file to write trace or debug messages to.</returns>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0004EB38 File Offset: 0x0004CD38
		// (set) Token: 0x0600124C RID: 4684 RVA: 0x0004EB40 File Offset: 0x0004CD40
		[MonoTODO]
		public string LogFileName
		{
			get
			{
				return this.logFileName;
			}
			set
			{
				this.logFileName = value;
			}
		}

		/// <summary>Emits or displays a message and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display.</param>
		// Token: 0x0600124D RID: 4685 RVA: 0x0004EB49 File Offset: 0x0004CD49
		public override void Fail(string message)
		{
			base.Fail(message);
		}

		/// <summary>Emits or displays detailed messages and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display.</param>
		/// <param name="detailMessage">The detailed message to emit or display.</param>
		// Token: 0x0600124E RID: 4686 RVA: 0x0004EB52 File Offset: 0x0004CD52
		public override void Fail(string message, string detailMessage)
		{
			base.Fail(message, detailMessage);
			if (this.ProcessUI(message, detailMessage) == DefaultTraceListener.DialogResult.Abort)
			{
				Thread.CurrentThread.Abort();
			}
			this.WriteLine(new StackTrace().ToString());
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0004EB84 File Offset: 0x0004CD84
		private DefaultTraceListener.DialogResult ProcessUI(string message, string detailMessage)
		{
			if (!this.AssertUiEnabled)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			object obj;
			MethodInfo method;
			try
			{
				Assembly assembly = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				if (assembly == null)
				{
					return DefaultTraceListener.DialogResult.None;
				}
				Type type = assembly.GetType("System.Windows.Forms.MessageBoxButtons");
				obj = Enum.Parse(type, "AbortRetryIgnore");
				method = assembly.GetType("System.Windows.Forms.MessageBox").GetMethod("Show", new Type[]
				{
					typeof(string),
					typeof(string),
					type
				});
			}
			catch
			{
				return DefaultTraceListener.DialogResult.None;
			}
			if (method == null || obj == null)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			string text = string.Format("Assertion Failed: {0} to quit, {1} to debug, {2} to continue", "Abort", "Retry", "Ignore");
			string text2 = string.Format("{0}{1}{2}{1}{1}{3}", new object[]
			{
				message,
				Environment.NewLine,
				detailMessage,
				new StackTrace()
			});
			string a = method.Invoke(null, new object[]
			{
				text2,
				text,
				obj
			}).ToString();
			if (a == "Ignore")
			{
				return DefaultTraceListener.DialogResult.Ignore;
			}
			if (!(a == "Abort"))
			{
				return DefaultTraceListener.DialogResult.Retry;
			}
			return DefaultTraceListener.DialogResult.Abort;
		}

		// Token: 0x06001250 RID: 4688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteWindowsDebugString(char* message);

		// Token: 0x06001251 RID: 4689 RVA: 0x0004ECC4 File Offset: 0x0004CEC4
		private unsafe void WriteDebugString(string message)
		{
			if (DefaultTraceListener.OnWin32)
			{
				fixed (string text = message)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					DefaultTraceListener.WriteWindowsDebugString(ptr);
				}
				return;
			}
			this.WriteMonoTrace(message);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004ECF8 File Offset: 0x0004CEF8
		private void WriteMonoTrace(string message)
		{
			string monoTraceFile = DefaultTraceListener.MonoTraceFile;
			if (monoTraceFile == "Console.Out")
			{
				Console.Out.Write(message);
				return;
			}
			if (!(monoTraceFile == "Console.Error"))
			{
				this.WriteLogFile(message, DefaultTraceListener.MonoTraceFile);
				return;
			}
			Console.Error.Write(message);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004ED4B File Offset: 0x0004CF4B
		private void WritePrefix()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				this.WriteMonoTrace(DefaultTraceListener.MonoTracePrefix);
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004ED5F File Offset: 0x0004CF5F
		private void WriteImpl(string message)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
				this.WritePrefix();
			}
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
			}
			else
			{
				this.WriteDebugString(message);
			}
			this.WriteLogFile(message, this.LogFileName);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0004ED9C File Offset: 0x0004CF9C
		private void WriteLogFile(string message, string logFile)
		{
			if (logFile != null && logFile.Length != 0)
			{
				FileInfo fileInfo = new FileInfo(logFile);
				StreamWriter streamWriter = null;
				try
				{
					if (fileInfo.Exists)
					{
						streamWriter = fileInfo.AppendText();
					}
					else
					{
						streamWriter = fileInfo.CreateText();
					}
				}
				catch
				{
					return;
				}
				using (streamWriter)
				{
					streamWriter.Write(message);
					streamWriter.Flush();
				}
			}
		}

		/// <summary>Writes the output to the <see langword="OutputDebugString" /> function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method.</summary>
		/// <param name="message">The message to write to <see langword="OutputDebugString" /> and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />.</param>
		// Token: 0x06001256 RID: 4694 RVA: 0x0004EE14 File Offset: 0x0004D014
		public override void Write(string message)
		{
			this.WriteImpl(message);
		}

		/// <summary>Writes the output to the <see langword="OutputDebugString" /> function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method, followed by a carriage return and line feed (\r\n).</summary>
		/// <param name="message">The message to write to <see langword="OutputDebugString" /> and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />.</param>
		// Token: 0x06001257 RID: 4695 RVA: 0x0004EE20 File Offset: 0x0004D020
		public override void WriteLine(string message)
		{
			string message2 = message + Environment.NewLine;
			this.WriteImpl(message2);
			base.NeedIndent = true;
		}

		// Token: 0x04000A99 RID: 2713
		private static readonly bool OnWin32 = Path.DirectorySeparatorChar == '\\';

		// Token: 0x04000A9A RID: 2714
		private const string ConsoleOutTrace = "Console.Out";

		// Token: 0x04000A9B RID: 2715
		private const string ConsoleErrorTrace = "Console.Error";

		// Token: 0x04000A9C RID: 2716
		private static readonly string MonoTracePrefix;

		// Token: 0x04000A9D RID: 2717
		private static readonly string MonoTraceFile;

		// Token: 0x04000A9E RID: 2718
		private string logFileName;

		// Token: 0x04000A9F RID: 2719
		private bool assertUiEnabled;

		// Token: 0x02000253 RID: 595
		private enum DialogResult
		{
			// Token: 0x04000AA1 RID: 2721
			None,
			// Token: 0x04000AA2 RID: 2722
			Retry,
			// Token: 0x04000AA3 RID: 2723
			Ignore,
			// Token: 0x04000AA4 RID: 2724
			Abort
		}
	}
}
