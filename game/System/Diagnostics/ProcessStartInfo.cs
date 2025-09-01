using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	/// <summary>Specifies a set of values that are used when you start a process.</summary>
	// Token: 0x02000245 RID: 581
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, SelfAffectingProcessMgmt = true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProcessStartInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class without specifying a file name with which to start the process.</summary>
		// Token: 0x060011D3 RID: 4563 RVA: 0x0004DDAC File Offset: 0x0004BFAC
		public ProcessStartInfo()
		{
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0004DDBB File Offset: 0x0004BFBB
		internal ProcessStartInfo(Process parent)
		{
			this.weakParentProcess = new WeakReference(parent);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class and specifies a file name such as an application or document with which to start the process.</summary>
		/// <param name="fileName">An application or document with which to start a process.</param>
		// Token: 0x060011D5 RID: 4565 RVA: 0x0004DDD6 File Offset: 0x0004BFD6
		public ProcessStartInfo(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ProcessStartInfo" /> class, specifies an application file name with which to start the process, and specifies a set of command-line arguments to pass to the application.</summary>
		/// <param name="fileName">An application with which to start a process.</param>
		/// <param name="arguments">Command-line arguments to pass to the application when the process starts.</param>
		// Token: 0x060011D6 RID: 4566 RVA: 0x0004DDEC File Offset: 0x0004BFEC
		public ProcessStartInfo(string fileName, string arguments)
		{
			this.fileName = fileName;
			this.arguments = arguments;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0004DE09 File Offset: 0x0004C009
		public Collection<string> ArgumentList
		{
			get
			{
				if (this._argumentList == null)
				{
					this._argumentList = new Collection<string>();
				}
				return this._argumentList;
			}
		}

		/// <summary>Gets or sets the verb to use when opening the application or document specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The action to take with the file that the process opens. The default is an empty string (""), which signifies no action.</returns>
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x0004DE24 File Offset: 0x0004C024
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x0004DE3A File Offset: 0x0004C03A
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.VerbConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[MonitoringDescription("The verb to apply to the document specified by the FileName property.")]
		public string Verb
		{
			get
			{
				if (this.verb == null)
				{
					return string.Empty;
				}
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		/// <summary>Gets or sets the set of command-line arguments to use when starting the application.</summary>
		/// <returns>A single string containing the arguments to pass to the target application specified in the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property. The default is an empty string (""). On Windows Vista and earlier versions of the Windows operating system, the length of the arguments added to the length of the full path to the process must be less than 2080. On Windows 7 and later versions, the length must be less than 32699.  
		///  Arguments are parsed and interpreted by the target application, so must align with the expectations of that application. For.NET applications as demonstrated in the Examples below, spaces are interpreted as a separator between multiple arguments. A single argument that includes spaces must be surrounded by quotation marks, but those quotation marks are not carried through to the target application. In include quotation marks in the final parsed argument, triple-escape each mark.</returns>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x0004DE43 File Offset: 0x0004C043
		// (set) Token: 0x060011DB RID: 4571 RVA: 0x0004DE59 File Offset: 0x0004C059
		[SettingsBindable(true)]
		[MonitoringDescription("Command line arguments that will be passed to the application specified by the FileName property.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Arguments
		{
			get
			{
				if (this.arguments == null)
				{
					return string.Empty;
				}
				return this.arguments;
			}
			set
			{
				this.arguments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to start the process in a new window.</summary>
		/// <returns>
		///   <see langword="true" /> if the process should be started without creating a new window to contain it; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x0004DE62 File Offset: 0x0004C062
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0004DE6A File Offset: 0x0004C06A
		[DefaultValue(false)]
		[MonitoringDescription("Whether to start the process without creating a new window to contain it.")]
		[NotifyParentProperty(true)]
		public bool CreateNoWindow
		{
			get
			{
				return this.createNoWindow;
			}
			set
			{
				this.createNoWindow = value;
			}
		}

		/// <summary>Gets search paths for files, directories for temporary files, application-specific options, and other similar information.</summary>
		/// <returns>A string dictionary that provides environment variables that apply to this process and child processes. The default is <see langword="null" />.</returns>
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0004DE74 File Offset: 0x0004C074
		[MonitoringDescription("Set of environment variables that apply to this process and child processes.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Diagnostics.Design.StringDictionaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public StringDictionary EnvironmentVariables
		{
			get
			{
				if (this.environmentVariables == null)
				{
					this.environmentVariables = new CaseSensitiveStringDictionary();
					if (this.weakParentProcess == null || !this.weakParentProcess.IsAlive || ((Component)this.weakParentProcess.Target).Site == null || !((Component)this.weakParentProcess.Target).Site.DesignMode)
					{
						foreach (object obj in System.Environment.GetEnvironmentVariables())
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
							this.environmentVariables.Add((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
						}
					}
				}
				return this.environmentVariables;
			}
		}

		/// <summary>Gets the environment variables that apply to this process and its child processes.</summary>
		/// <returns>A generic dictionary containing the environment variables that apply to this process and its child processes. The default is <see langword="null" />.</returns>
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0004DF4C File Offset: 0x0004C14C
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDictionary<string, string> Environment
		{
			get
			{
				if (this.environment == null)
				{
					this.environment = this.EnvironmentVariables.AsGenericDictionary();
				}
				return this.environment;
			}
		}

		/// <summary>Gets or sets a value indicating whether the input for an application is read from the <see cref="P:System.Diagnostics.Process.StandardInput" /> stream.</summary>
		/// <returns>
		///   <see langword="true" /> if input should be read from <see cref="P:System.Diagnostics.Process.StandardInput" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0004DF6D File Offset: 0x0004C16D
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x0004DF75 File Offset: 0x0004C175
		[MonitoringDescription("Whether the process command input is read from the Process instance's StandardInput member.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool RedirectStandardInput
		{
			get
			{
				return this.redirectStandardInput;
			}
			set
			{
				this.redirectStandardInput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the textual output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <returns>
		///   <see langword="true" /> if output should be written to <see cref="P:System.Diagnostics.Process.StandardOutput" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0004DF7E File Offset: 0x0004C17E
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x0004DF86 File Offset: 0x0004C186
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether the process output is written to the Process instance's StandardOutput member.")]
		public bool RedirectStandardOutput
		{
			get
			{
				return this.redirectStandardOutput;
			}
			set
			{
				this.redirectStandardOutput = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error output of an application is written to the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <returns>
		///   <see langword="true" /> if error output should be written to <see cref="P:System.Diagnostics.Process.StandardError" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x0004DF8F File Offset: 0x0004C18F
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x0004DF97 File Offset: 0x0004C197
		[DefaultValue(false)]
		[MonitoringDescription("Whether the process's error output is written to the Process instance's StandardError member.")]
		[NotifyParentProperty(true)]
		public bool RedirectStandardError
		{
			get
			{
				return this.redirectStandardError;
			}
			set
			{
				this.redirectStandardError = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for error output.</summary>
		/// <returns>An object that represents the preferred encoding for error output. The default is <see langword="null" />.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x0004DFA8 File Offset: 0x0004C1A8
		public Encoding StandardErrorEncoding
		{
			get
			{
				return this.standardErrorEncoding;
			}
			set
			{
				this.standardErrorEncoding = value;
			}
		}

		/// <summary>Gets or sets the preferred encoding for standard output.</summary>
		/// <returns>An object that represents the preferred encoding for standard output. The default is <see langword="null" />.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x0004DFB1 File Offset: 0x0004C1B1
		// (set) Token: 0x060011E9 RID: 4585 RVA: 0x0004DFB9 File Offset: 0x0004C1B9
		public Encoding StandardOutputEncoding
		{
			get
			{
				return this.standardOutputEncoding;
			}
			set
			{
				this.standardOutputEncoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the operating system shell to start the process.</summary>
		/// <returns>
		///   <see langword="true" /> if the shell should be used when starting the process; <see langword="false" /> if the process should be created directly from the executable file. The default is <see langword="true" /> on .NET Framework apps and <see langword="false" /> on .NET Core apps.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to set the value to <see langword="true" /> on Universal Windows Platform (UWP) apps occurs.</exception>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x0004DFC2 File Offset: 0x0004C1C2
		// (set) Token: 0x060011EB RID: 4587 RVA: 0x0004DFCA File Offset: 0x0004C1CA
		[NotifyParentProperty(true)]
		[MonitoringDescription("Whether to use the operating system shell to start the process.")]
		[DefaultValue(true)]
		public bool UseShellExecute
		{
			get
			{
				return this.useShellExecute;
			}
			set
			{
				this.useShellExecute = value;
			}
		}

		/// <summary>Gets or sets the user name to use when starting the process. If you use the UPN format, <paramref name="user" />@<paramref name="DNS_domain_name" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.Domain" /> property must be <see langword="null" />.</summary>
		/// <returns>The user name to use when starting the process. If you use the UPN format, <paramref name="user" />@<paramref name="DNS_domain_name" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.Domain" /> property must be <see langword="null" />.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0004DFD3 File Offset: 0x0004C1D3
		// (set) Token: 0x060011ED RID: 4589 RVA: 0x0004DFE9 File Offset: 0x0004C1E9
		[NotifyParentProperty(true)]
		public string UserName
		{
			get
			{
				if (this.userName == null)
				{
					return string.Empty;
				}
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		/// <summary>Gets or sets a secure string that contains the user password to use when starting the process.</summary>
		/// <returns>The user password to use when starting the process.</returns>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0004DFF2 File Offset: 0x0004C1F2
		// (set) Token: 0x060011EF RID: 4591 RVA: 0x0004DFFA File Offset: 0x0004C1FA
		public SecureString Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		/// <summary>Gets or sets the user password in clear text to use when starting the process.</summary>
		/// <returns>The user password in clear text.</returns>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0004E003 File Offset: 0x0004C203
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x0004E00B File Offset: 0x0004C20B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string PasswordInClearText
		{
			get
			{
				return this.passwordInClearText;
			}
			set
			{
				this.passwordInClearText = value;
			}
		}

		/// <summary>Gets or sets a value that identifies the domain to use when starting the process. If this value is <see langword="null" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property must be specified in UPN format.</summary>
		/// <returns>The Active Directory domain to use when starting the process. If this value is <see langword="null" />, the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property must be specified in UPN format.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0004E014 File Offset: 0x0004C214
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x0004E02A File Offset: 0x0004C22A
		[NotifyParentProperty(true)]
		public string Domain
		{
			get
			{
				if (this.domain == null)
				{
					return string.Empty;
				}
				return this.domain;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the Windows user profile is to be loaded from the registry.</summary>
		/// <returns>
		///   <see langword="true" /> if the Windows user profile should be loaded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0004E033 File Offset: 0x0004C233
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0004E03B File Offset: 0x0004C23B
		[NotifyParentProperty(true)]
		public bool LoadUserProfile
		{
			get
			{
				return this.loadUserProfile;
			}
			set
			{
				this.loadUserProfile = value;
			}
		}

		/// <summary>Gets or sets the application or document to start.</summary>
		/// <returns>The name of the application to start, or the name of a document of a file type that is associated with an application and that has a default open action available to it. The default is an empty string ("").</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0004E044 File Offset: 0x0004C244
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x0004E05A File Offset: 0x0004C25A
		[SettingsBindable(true)]
		[NotifyParentProperty(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[MonitoringDescription("The name of the application, document or URL to start.")]
		[Editor("System.Diagnostics.Design.StartFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string FileName
		{
			get
			{
				if (this.fileName == null)
				{
					return string.Empty;
				}
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is <see langword="false" />, gets or sets the working directory for the process to be started. When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is <see langword="true" />, gets or sets the directory that contains the process to be started.</summary>
		/// <returns>When <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> is <see langword="true" />, the fully qualified name of the directory that contains the process to be started. When the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property is <see langword="false" />, the working directory for the process to be started. The default is an empty string ("").</returns>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0004E063 File Offset: 0x0004C263
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0004E079 File Offset: 0x0004C279
		[SettingsBindable(true)]
		[MonitoringDescription("The initial working directory for the process.")]
		[DefaultValue("")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[NotifyParentProperty(true)]
		[Editor("System.Diagnostics.Design.WorkingDirectoryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string WorkingDirectory
		{
			get
			{
				if (this.directory == null)
				{
					return string.Empty;
				}
				return this.directory;
			}
			set
			{
				this.directory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether an error dialog box is displayed to the user if the process cannot be started.</summary>
		/// <returns>
		///   <see langword="true" /> if an error dialog box should be displayed on the screen if the process cannot be started; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0004E082 File Offset: 0x0004C282
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0004E08A File Offset: 0x0004C28A
		[MonitoringDescription("Whether to show an error dialog to the user if there is an error.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public bool ErrorDialog
		{
			get
			{
				return this.errorDialog;
			}
			set
			{
				this.errorDialog = value;
			}
		}

		/// <summary>Gets or sets the window handle to use when an error dialog box is shown for a process that cannot be started.</summary>
		/// <returns>A pointer to the handle of the error dialog box that results from a process start failure.</returns>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0004E093 File Offset: 0x0004C293
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x0004E09B File Offset: 0x0004C29B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IntPtr ErrorDialogParentHandle
		{
			get
			{
				return this.errorDialogParentHandle;
			}
			set
			{
				this.errorDialogParentHandle = value;
			}
		}

		/// <summary>Gets or sets the window state to use when the process is started.</summary>
		/// <returns>One of the enumeration values that indicates whether the process is started in a window that is maximized, minimized, normal (neither maximized nor minimized), or not visible. The default is <see langword="Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The window style is not one of the <see cref="T:System.Diagnostics.ProcessWindowStyle" /> enumeration members.</exception>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0004E0AC File Offset: 0x0004C2AC
		[DefaultValue(ProcessWindowStyle.Normal)]
		[MonitoringDescription("How the main window should be created when the process starts.")]
		[NotifyParentProperty(true)]
		public ProcessWindowStyle WindowStyle
		{
			get
			{
				return this.windowStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessWindowStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessWindowStyle));
				}
				this.windowStyle = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0004E0E2 File Offset: 0x0004C2E2
		internal bool HaveEnvVars
		{
			get
			{
				return this.environmentVariables != null;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0004E0ED File Offset: 0x0004C2ED
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x0004E0F5 File Offset: 0x0004C2F5
		public Encoding StandardInputEncoding
		{
			[CompilerGenerated]
			get
			{
				return this.<StandardInputEncoding>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<StandardInputEncoding>k__BackingField = value;
			}
		}

		/// <summary>Gets the set of verbs associated with the type of file specified by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</summary>
		/// <returns>The actions that the system can apply to the file indicated by the <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.</returns>
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0004E100 File Offset: 0x0004C300
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] Verbs
		{
			get
			{
				PlatformID platform = System.Environment.OSVersion.Platform;
				if (platform == PlatformID.Unix || platform == PlatformID.MacOSX || platform == (PlatformID)128)
				{
					return ProcessStartInfo.empty;
				}
				string text = string.IsNullOrEmpty(this.fileName) ? null : Path.GetExtension(this.fileName);
				if (text == null)
				{
					return ProcessStartInfo.empty;
				}
				RegistryKey registryKey = null;
				RegistryKey registryKey2 = null;
				RegistryKey registryKey3 = null;
				string[] result;
				try
				{
					registryKey = Registry.ClassesRoot.OpenSubKey(text);
					string text2 = (registryKey != null) ? (registryKey.GetValue(null) as string) : null;
					registryKey2 = ((text2 != null) ? Registry.ClassesRoot.OpenSubKey(text2) : null);
					registryKey3 = ((registryKey2 != null) ? registryKey2.OpenSubKey("shell") : null);
					result = ((registryKey3 != null) ? registryKey3.GetSubKeyNames() : null);
				}
				finally
				{
					if (registryKey3 != null)
					{
						registryKey3.Close();
					}
					if (registryKey2 != null)
					{
						registryKey2.Close();
					}
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
				return result;
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0004E1E4 File Offset: 0x0004C3E4
		// Note: this type is marked as 'beforefieldinit'.
		static ProcessStartInfo()
		{
		}

		// Token: 0x04000A72 RID: 2674
		private string fileName;

		// Token: 0x04000A73 RID: 2675
		private string arguments;

		// Token: 0x04000A74 RID: 2676
		private string directory;

		// Token: 0x04000A75 RID: 2677
		private string verb;

		// Token: 0x04000A76 RID: 2678
		private ProcessWindowStyle windowStyle;

		// Token: 0x04000A77 RID: 2679
		private bool errorDialog;

		// Token: 0x04000A78 RID: 2680
		private IntPtr errorDialogParentHandle;

		// Token: 0x04000A79 RID: 2681
		private bool useShellExecute = true;

		// Token: 0x04000A7A RID: 2682
		private string userName;

		// Token: 0x04000A7B RID: 2683
		private string domain;

		// Token: 0x04000A7C RID: 2684
		private SecureString password;

		// Token: 0x04000A7D RID: 2685
		private string passwordInClearText;

		// Token: 0x04000A7E RID: 2686
		private bool loadUserProfile;

		// Token: 0x04000A7F RID: 2687
		private bool redirectStandardInput;

		// Token: 0x04000A80 RID: 2688
		private bool redirectStandardOutput;

		// Token: 0x04000A81 RID: 2689
		private bool redirectStandardError;

		// Token: 0x04000A82 RID: 2690
		private Encoding standardOutputEncoding;

		// Token: 0x04000A83 RID: 2691
		private Encoding standardErrorEncoding;

		// Token: 0x04000A84 RID: 2692
		private bool createNoWindow;

		// Token: 0x04000A85 RID: 2693
		private WeakReference weakParentProcess;

		// Token: 0x04000A86 RID: 2694
		internal StringDictionary environmentVariables;

		// Token: 0x04000A87 RID: 2695
		private static readonly string[] empty = new string[0];

		// Token: 0x04000A88 RID: 2696
		private Collection<string> _argumentList;

		// Token: 0x04000A89 RID: 2697
		private IDictionary<string, string> environment;

		// Token: 0x04000A8A RID: 2698
		[CompilerGenerated]
		private Encoding <StandardInputEncoding>k__BackingField;
	}
}
