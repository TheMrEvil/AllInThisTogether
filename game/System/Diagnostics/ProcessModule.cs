using System;
using System.ComponentModel;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Represents a.dll or .exe file that is loaded into a particular process.</summary>
	// Token: 0x0200027C RID: 636
	[Designer("System.Diagnostics.Design.ProcessModuleDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ProcessModule : Component
	{
		// Token: 0x0600143F RID: 5183 RVA: 0x000530B7 File Offset: 0x000512B7
		internal ProcessModule(IntPtr baseaddr, IntPtr entryaddr, string filename, FileVersionInfo version_info, int memory_size, string modulename)
		{
			this.baseaddr = baseaddr;
			this.entryaddr = entryaddr;
			this.filename = filename;
			this.version_info = version_info;
			this.memory_size = memory_size;
			this.modulename = modulename;
		}

		/// <summary>Gets the memory address where the module was loaded.</summary>
		/// <returns>The load address of the module.</returns>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000530EC File Offset: 0x000512EC
		[MonitoringDescription("The base memory address of this module")]
		public IntPtr BaseAddress
		{
			get
			{
				return this.baseaddr;
			}
		}

		/// <summary>Gets the memory address for the function that runs when the system loads and runs the module.</summary>
		/// <returns>The entry point of the module.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x000530F4 File Offset: 0x000512F4
		[MonitoringDescription("The base memory address of the entry point of this module")]
		public IntPtr EntryPointAddress
		{
			get
			{
				return this.entryaddr;
			}
		}

		/// <summary>Gets the full path to the module.</summary>
		/// <returns>The fully qualified path that defines the location of the module.</returns>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x000530FC File Offset: 0x000512FC
		[MonitoringDescription("The file name of this module")]
		public string FileName
		{
			get
			{
				return this.filename;
			}
		}

		/// <summary>Gets version information about the module.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.FileVersionInfo" /> that contains the module's version information.</returns>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00053104 File Offset: 0x00051304
		[Browsable(false)]
		public FileVersionInfo FileVersionInfo
		{
			get
			{
				return this.version_info;
			}
		}

		/// <summary>Gets the amount of memory that is required to load the module.</summary>
		/// <returns>The size, in bytes, of the memory that the module occupies.</returns>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0005310C File Offset: 0x0005130C
		[MonitoringDescription("The memory needed by this module")]
		public int ModuleMemorySize
		{
			get
			{
				return this.memory_size;
			}
		}

		/// <summary>Gets the name of the process module.</summary>
		/// <returns>The name of the module.</returns>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00053114 File Offset: 0x00051314
		[MonitoringDescription("The name of this module")]
		public string ModuleName
		{
			get
			{
				return this.modulename;
			}
		}

		/// <summary>Converts the name of the module to a string.</summary>
		/// <returns>The value of the <see cref="P:System.Diagnostics.ProcessModule.ModuleName" /> property.</returns>
		// Token: 0x06001446 RID: 5190 RVA: 0x0005311C File Offset: 0x0005131C
		public override string ToString()
		{
			return this.ModuleName;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00013BCA File Offset: 0x00011DCA
		internal ProcessModule()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000B50 RID: 2896
		private IntPtr baseaddr;

		// Token: 0x04000B51 RID: 2897
		private IntPtr entryaddr;

		// Token: 0x04000B52 RID: 2898
		private string filename;

		// Token: 0x04000B53 RID: 2899
		private FileVersionInfo version_info;

		// Token: 0x04000B54 RID: 2900
		private int memory_size;

		// Token: 0x04000B55 RID: 2901
		private string modulename;
	}
}
