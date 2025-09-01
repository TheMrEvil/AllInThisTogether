using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Parse
{
	// Token: 0x02000019 RID: 25
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00007790 File Offset: 0x00005990
		internal Resources()
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007798 File Offset: 0x00005998
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Parse.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000077C4 File Offset: 0x000059C4
		// (set) Token: 0x06000189 RID: 393 RVA: 0x000077CB File Offset: 0x000059CB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000077D3 File Offset: 0x000059D3
		internal static string FileBackedCacheSynchronousMutationNotSupportedMessage
		{
			get
			{
				return Resources.ResourceManager.GetString("FileBackedCacheSynchronousMutationNotSupportedMessage", Resources.resourceCulture);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000077E9 File Offset: 0x000059E9
		internal static string TransientCacheControllerDiskFileOperationNotSupportedMessage
		{
			get
			{
				return Resources.ResourceManager.GetString("TransientCacheControllerDiskFileOperationNotSupportedMessage", Resources.resourceCulture);
			}
		}

		// Token: 0x04000038 RID: 56
		private static ResourceManager resourceMan;

		// Token: 0x04000039 RID: 57
		private static CultureInfo resourceCulture;
	}
}
