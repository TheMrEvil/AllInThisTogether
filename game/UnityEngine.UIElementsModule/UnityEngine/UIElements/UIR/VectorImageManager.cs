using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032E RID: 814
	internal class VectorImageManager : IDisposable
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0007287C File Offset: 0x00070A7C
		public Texture2D atlas
		{
			get
			{
				GradientSettingsAtlas gradientSettingsAtlas = this.m_GradientSettingsAtlas;
				return (gradientSettingsAtlas != null) ? gradientSettingsAtlas.atlas : null;
			}
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000728A0 File Offset: 0x00070AA0
		public VectorImageManager(AtlasBase atlas)
		{
			VectorImageManager.instances.Add(this);
			this.m_Atlas = atlas;
			this.m_Registered = new Dictionary<VectorImage, VectorImageRenderInfo>(32);
			this.m_RenderInfoPool = new VectorImageRenderInfoPool();
			this.m_GradientRemapPool = new GradientRemapPool();
			this.m_GradientSettingsAtlas = new GradientSettingsAtlas(4096);
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000728FB File Offset: 0x00070AFB
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00072903 File Offset: 0x00070B03
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

		// Token: 0x06001A71 RID: 6769 RVA: 0x0007290C File Offset: 0x00070B0C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00072920 File Offset: 0x00070B20
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Registered.Clear();
					this.m_RenderInfoPool.Clear();
					this.m_GradientRemapPool.Clear();
					this.m_GradientSettingsAtlas.Dispose();
					VectorImageManager.instances.Remove(this);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00072988 File Offset: 0x00070B88
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Registered.Clear();
				this.m_RenderInfoPool.Clear();
				this.m_GradientRemapPool.Clear();
				this.m_GradientSettingsAtlas.Reset();
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000729DC File Offset: 0x00070BDC
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_GradientSettingsAtlas.Commit();
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00072A0C File Offset: 0x00070C0C
		public GradientRemap AddUser(VectorImage vi, VisualElement context)
		{
			bool disposed = this.disposed;
			GradientRemap result;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				result = null;
			}
			else
			{
				bool flag = vi == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount++;
					}
					else
					{
						vectorImageRenderInfo = this.Register(vi, context);
					}
					result = vectorImageRenderInfo.firstGradientRemap;
				}
			}
			return result;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00072A78 File Offset: 0x00070C78
		public void RemoveUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = vi == null;
				if (!flag)
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount--;
						bool flag3 = vectorImageRenderInfo.useCount == 0;
						if (flag3)
						{
							this.Unregister(vi, vectorImageRenderInfo);
						}
					}
				}
			}
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00072AE4 File Offset: 0x00070CE4
		private VectorImageRenderInfo Register(VectorImage vi, VisualElement context)
		{
			VectorImageManager.s_MarkerRegister.Begin();
			VectorImageRenderInfo vectorImageRenderInfo = this.m_RenderInfoPool.Get();
			vectorImageRenderInfo.useCount = 1;
			this.m_Registered[vi] = vectorImageRenderInfo;
			GradientSettings[] settings = vi.settings;
			bool flag = settings != null && settings.Length != 0;
			if (flag)
			{
				int num = vi.settings.Length;
				Alloc alloc = this.m_GradientSettingsAtlas.Add(num);
				bool flag2 = alloc.size > 0U;
				if (flag2)
				{
					TextureId atlas;
					RectInt rectInt;
					bool flag3 = this.m_Atlas.TryGetAtlas(context, vi.atlas, out atlas, out rectInt);
					if (flag3)
					{
						GradientRemap gradientRemap = null;
						for (int i = 0; i < num; i++)
						{
							GradientRemap gradientRemap2 = this.m_GradientRemapPool.Get();
							bool flag4 = i > 0;
							if (flag4)
							{
								gradientRemap.next = gradientRemap2;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap2;
							}
							gradientRemap = gradientRemap2;
							gradientRemap2.origIndex = i;
							gradientRemap2.destIndex = (int)(alloc.start + (uint)i);
							GradientSettings gradientSettings = vi.settings[i];
							RectInt location = gradientSettings.location;
							location.x += rectInt.x;
							location.y += rectInt.y;
							gradientRemap2.location = location;
							gradientRemap2.atlas = atlas;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, vectorImageRenderInfo.firstGradientRemap);
					}
					else
					{
						GradientRemap gradientRemap3 = null;
						for (int j = 0; j < num; j++)
						{
							GradientRemap gradientRemap4 = this.m_GradientRemapPool.Get();
							bool flag5 = j > 0;
							if (flag5)
							{
								gradientRemap3.next = gradientRemap4;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap4;
							}
							gradientRemap3 = gradientRemap4;
							gradientRemap4.origIndex = j;
							gradientRemap4.destIndex = (int)(alloc.start + (uint)j);
							gradientRemap4.atlas = TextureId.invalid;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, null);
					}
				}
				else
				{
					bool flag6 = !this.m_LoggedExhaustedSettingsAtlas;
					if (flag6)
					{
						string str = "Exhausted max gradient settings (";
						string str2 = this.m_GradientSettingsAtlas.length.ToString();
						string str3 = ") for atlas: ";
						Texture2D atlas2 = this.m_GradientSettingsAtlas.atlas;
						Debug.LogError(str + str2 + str3 + ((atlas2 != null) ? atlas2.name : null));
						this.m_LoggedExhaustedSettingsAtlas = true;
					}
				}
			}
			VectorImageManager.s_MarkerRegister.End();
			return vectorImageRenderInfo;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00072D54 File Offset: 0x00070F54
		private void Unregister(VectorImage vi, VectorImageRenderInfo renderInfo)
		{
			VectorImageManager.s_MarkerUnregister.Begin();
			bool flag = renderInfo.gradientSettingsAlloc.size > 0U;
			if (flag)
			{
				this.m_GradientSettingsAtlas.Remove(renderInfo.gradientSettingsAlloc);
			}
			GradientRemap next;
			for (GradientRemap gradientRemap = renderInfo.firstGradientRemap; gradientRemap != null; gradientRemap = next)
			{
				next = gradientRemap.next;
				this.m_GradientRemapPool.Return(gradientRemap);
			}
			this.m_Registered.Remove(vi);
			this.m_RenderInfoPool.Return(renderInfo);
			VectorImageManager.s_MarkerUnregister.End();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00072DDF File Offset: 0x00070FDF
		// Note: this type is marked as 'beforefieldinit'.
		static VectorImageManager()
		{
		}

		// Token: 0x04000C27 RID: 3111
		public static List<VectorImageManager> instances = new List<VectorImageManager>(16);

		// Token: 0x04000C28 RID: 3112
		private static ProfilerMarker s_MarkerRegister = new ProfilerMarker("UIR.VectorImageManager.Register");

		// Token: 0x04000C29 RID: 3113
		private static ProfilerMarker s_MarkerUnregister = new ProfilerMarker("UIR.VectorImageManager.Unregister");

		// Token: 0x04000C2A RID: 3114
		private readonly AtlasBase m_Atlas;

		// Token: 0x04000C2B RID: 3115
		private Dictionary<VectorImage, VectorImageRenderInfo> m_Registered;

		// Token: 0x04000C2C RID: 3116
		private VectorImageRenderInfoPool m_RenderInfoPool;

		// Token: 0x04000C2D RID: 3117
		private GradientRemapPool m_GradientRemapPool;

		// Token: 0x04000C2E RID: 3118
		private GradientSettingsAtlas m_GradientSettingsAtlas;

		// Token: 0x04000C2F RID: 3119
		private bool m_LoggedExhaustedSettingsAtlas;

		// Token: 0x04000C30 RID: 3120
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <disposed>k__BackingField;
	}
}
