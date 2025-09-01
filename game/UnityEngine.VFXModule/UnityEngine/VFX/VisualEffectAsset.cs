using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/VFX/Public/VisualEffectAsset.h")]
	[NativeHeader("VFXScriptingClasses.h")]
	[UsedByNativeCode]
	public class VisualEffectAsset : VisualEffectObject
	{
		// Token: 0x060000A2 RID: 162
		[FreeFunction(Name = "VisualEffectAssetBindings::GetTextureDimension", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TextureDimension GetTextureDimension(int nameID);

		// Token: 0x060000A3 RID: 163
		[FreeFunction(Name = "VisualEffectAssetBindings::GetExposedProperties", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetExposedProperties([NotNull("ArgumentNullException")] List<VFXExposedProperty> exposedProperties);

		// Token: 0x060000A4 RID: 164
		[FreeFunction(Name = "VisualEffectAssetBindings::GetEvents", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetEvents([NotNull("ArgumentNullException")] List<string> names);

		// Token: 0x060000A5 RID: 165 RVA: 0x000029E8 File Offset: 0x00000BE8
		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002A06 File Offset: 0x00000C06
		public VisualEffectAsset()
		{
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002A0F File Offset: 0x00000C0F
		// Note: this type is marked as 'beforefieldinit'.
		static VisualEffectAsset()
		{
		}

		// Token: 0x040000F6 RID: 246
		public const string PlayEventName = "OnPlay";

		// Token: 0x040000F7 RID: 247
		public const string StopEventName = "OnStop";

		// Token: 0x040000F8 RID: 248
		public static readonly int PlayEventID = Shader.PropertyToID("OnPlay");

		// Token: 0x040000F9 RID: 249
		public static readonly int StopEventID = Shader.PropertyToID("OnStop");
	}
}
