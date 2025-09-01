using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200005A RID: 90
	[Obsolete("Use [RequiredIn(PrefabKind.PrefabAsset)] instead.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class RequiredInPrefabAssetsAttribute : Attribute
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000356F File Offset: 0x0000176F
		public RequiredInPrefabAssetsAttribute()
		{
			this.MessageType = InfoMessageType.Error;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000357E File Offset: 0x0000177E
		public RequiredInPrefabAssetsAttribute(string errorMessage, InfoMessageType messageType)
		{
			this.ErrorMessage = errorMessage;
			this.MessageType = messageType;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003594 File Offset: 0x00001794
		public RequiredInPrefabAssetsAttribute(string errorMessage)
		{
			this.ErrorMessage = errorMessage;
			this.MessageType = InfoMessageType.Error;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000035AA File Offset: 0x000017AA
		public RequiredInPrefabAssetsAttribute(InfoMessageType messageType)
		{
			this.MessageType = messageType;
		}

		// Token: 0x040000F8 RID: 248
		public string ErrorMessage;

		// Token: 0x040000F9 RID: 249
		public InfoMessageType MessageType;
	}
}
