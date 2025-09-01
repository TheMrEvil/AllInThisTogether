using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031B RID: 795
	internal abstract class BaseShaderInfoStorage : IDisposable
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001A0D RID: 6669
		public abstract Texture2D texture { get; }

		// Token: 0x06001A0E RID: 6670
		public abstract bool AllocateRect(int width, int height, out RectInt uvs);

		// Token: 0x06001A0F RID: 6671
		public abstract void SetTexel(int x, int y, Color color);

		// Token: 0x06001A10 RID: 6672
		public abstract void UpdateTexture();

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0006E770 File Offset: 0x0006C970
		// (set) Token: 0x06001A12 RID: 6674 RVA: 0x0006E778 File Offset: 0x0006C978
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x0006E781 File Offset: 0x0006C981
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x0006E794 File Offset: 0x0006C994
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				bool flag = !disposing;
				if (flag)
				{
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000020C2 File Offset: 0x000002C2
		protected BaseShaderInfoStorage()
		{
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0006E7BE File Offset: 0x0006C9BE
		// Note: this type is marked as 'beforefieldinit'.
		static BaseShaderInfoStorage()
		{
		}

		// Token: 0x04000BD7 RID: 3031
		protected static int s_TextureCounter;

		// Token: 0x04000BD8 RID: 3032
		internal static ProfilerMarker s_MarkerCopyTexture = new ProfilerMarker("UIR.ShaderInfoStorage.CopyTexture");

		// Token: 0x04000BD9 RID: 3033
		internal static ProfilerMarker s_MarkerGetTextureData = new ProfilerMarker("UIR.ShaderInfoStorage.GetTextureData");

		// Token: 0x04000BDA RID: 3034
		internal static ProfilerMarker s_MarkerUpdateTexture = new ProfilerMarker("UIR.ShaderInfoStorage.UpdateTexture");

		// Token: 0x04000BDB RID: 3035
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;
	}
}
