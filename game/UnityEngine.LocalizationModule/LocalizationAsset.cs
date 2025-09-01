using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[MovedFrom("UnityEditor")]
	[ExcludeFromPreset]
	[NativeClass("LocalizationAsset")]
	[NativeHeader("Modules/Localization/Public/LocalizationAsset.bindings.h")]
	[NativeHeader("Modules/Localization/Public/LocalizationAsset.h")]
	public sealed class LocalizationAsset : Object
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public LocalizationAsset()
		{
			LocalizationAsset.Internal_CreateInstance(this);
		}

		// Token: 0x06000002 RID: 2
		[FreeFunction("Internal_CreateInstance")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateInstance([Writable] LocalizationAsset locAsset);

		// Token: 0x06000003 RID: 3
		[NativeMethod("StoreLocalizedString")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLocalizedString(string original, string localized);

		// Token: 0x06000004 RID: 4
		[NativeMethod("GetLocalized")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLocalizedString(string original);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		public extern string localeIsoCode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		public extern bool isEditorAsset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
