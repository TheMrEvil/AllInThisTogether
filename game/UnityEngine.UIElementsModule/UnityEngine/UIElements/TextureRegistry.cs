using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000267 RID: 615
	internal class TextureRegistry
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x0004ADFD File Offset: 0x00048FFD
		public static TextureRegistry instance
		{
			[CompilerGenerated]
			get
			{
				return TextureRegistry.<instance>k__BackingField;
			}
		} = new TextureRegistry();

		// Token: 0x060012B7 RID: 4791 RVA: 0x0004AE04 File Offset: 0x00049004
		public Texture GetTexture(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			Texture result;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to get an invalid texture (index={0}).", id.index));
				result = null;
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to get a texture (index={0}) that is not allocated.", id.index));
					result = null;
				}
				else
				{
					result = textureInfo.texture;
				}
			}
			return result;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004AEA8 File Offset: 0x000490A8
		public TextureId AllocAndAcquireDynamic()
		{
			return this.AllocAndAcquire(null, true);
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004AEC4 File Offset: 0x000490C4
		public void UpdateDynamic(TextureId id, Texture texture)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to update an invalid dynamic texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = !textureInfo.dynamic;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to update a texture (index={0}) that is not dynamic.", id.index));
				}
				else
				{
					bool flag3 = textureInfo.refCount < 1;
					if (flag3)
					{
						Debug.LogError(string.Format("Attempted to update a dynamic texture (index={0}) that is not allocated.", id.index));
					}
					else
					{
						textureInfo.texture = texture;
						this.m_Textures[id.index] = textureInfo;
					}
				}
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004AFA4 File Offset: 0x000491A4
		private TextureId AllocAndAcquire(Texture texture, bool dynamic)
		{
			TextureRegistry.TextureInfo textureInfo = new TextureRegistry.TextureInfo
			{
				texture = texture,
				dynamic = dynamic,
				refCount = 1
			};
			bool flag = this.m_FreeIds.Count > 0;
			TextureId textureId;
			if (flag)
			{
				textureId = this.m_FreeIds.Pop();
				this.m_Textures[textureId.index] = textureInfo;
			}
			else
			{
				bool flag2 = this.m_Textures.Count == 2048;
				if (flag2)
				{
					Debug.LogError(string.Format("Failed to allocate a {0} because the limit of {1} textures is reached.", "TextureId", 2048));
					return TextureId.invalid;
				}
				textureId = new TextureId(this.m_Textures.Count);
				this.m_Textures.Add(textureInfo);
			}
			bool flag3 = !dynamic;
			if (flag3)
			{
				this.m_TextureToId[texture] = textureId;
			}
			return textureId;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0004B08C File Offset: 0x0004928C
		public TextureId Acquire(Texture tex)
		{
			TextureId textureId;
			bool flag = this.m_TextureToId.TryGetValue(tex, out textureId);
			TextureId result;
			if (flag)
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[textureId.index];
				Debug.Assert(textureInfo.refCount > 0);
				Debug.Assert(!textureInfo.dynamic);
				textureInfo.refCount++;
				this.m_Textures[textureId.index] = textureInfo;
				result = textureId;
			}
			else
			{
				result = this.AllocAndAcquire(tex, false);
			}
			return result;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004B110 File Offset: 0x00049310
		public void Acquire(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to acquire an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to acquire a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					textureInfo.refCount++;
					this.m_Textures[id.index] = textureInfo;
				}
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0004B1C4 File Offset: 0x000493C4
		public void Release(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to release an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to release a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					textureInfo.refCount--;
					bool flag3 = textureInfo.refCount == 0;
					if (flag3)
					{
						bool flag4 = !textureInfo.dynamic;
						if (flag4)
						{
							this.m_TextureToId.Remove(textureInfo.texture);
						}
						textureInfo.texture = null;
						textureInfo.dynamic = false;
						this.m_FreeIds.Push(id);
					}
					this.m_Textures[id.index] = textureInfo;
				}
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004B2C8 File Offset: 0x000494C8
		public TextureId TextureToId(Texture texture)
		{
			TextureId textureId;
			bool flag = this.m_TextureToId.TryGetValue(texture, out textureId);
			TextureId result;
			if (flag)
			{
				result = textureId;
			}
			else
			{
				result = TextureId.invalid;
			}
			return result;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0004B2F8 File Offset: 0x000494F8
		public TextureRegistry.Statistics GatherStatistics()
		{
			TextureRegistry.Statistics statistics = default(TextureRegistry.Statistics);
			statistics.freeIdsCount = this.m_FreeIds.Count;
			statistics.createdIdsCount = this.m_Textures.Count;
			statistics.allocatedIdsTotalCount = this.m_Textures.Count - this.m_FreeIds.Count;
			statistics.allocatedIdsDynamicCount = statistics.allocatedIdsTotalCount - this.m_TextureToId.Count;
			statistics.allocatedIdsStaticCount = statistics.allocatedIdsTotalCount - statistics.allocatedIdsDynamicCount;
			statistics.availableIdsCount = 2048 - statistics.allocatedIdsTotalCount;
			return statistics;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004B395 File Offset: 0x00049595
		public TextureRegistry()
		{
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004B3C9 File Offset: 0x000495C9
		// Note: this type is marked as 'beforefieldinit'.
		static TextureRegistry()
		{
		}

		// Token: 0x040008AF RID: 2223
		private List<TextureRegistry.TextureInfo> m_Textures = new List<TextureRegistry.TextureInfo>(128);

		// Token: 0x040008B0 RID: 2224
		private Dictionary<Texture, TextureId> m_TextureToId = new Dictionary<Texture, TextureId>(128);

		// Token: 0x040008B1 RID: 2225
		private Stack<TextureId> m_FreeIds = new Stack<TextureId>();

		// Token: 0x040008B2 RID: 2226
		internal const int maxTextures = 2048;

		// Token: 0x040008B3 RID: 2227
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static readonly TextureRegistry <instance>k__BackingField;

		// Token: 0x02000268 RID: 616
		private struct TextureInfo
		{
			// Token: 0x040008B4 RID: 2228
			public Texture texture;

			// Token: 0x040008B5 RID: 2229
			public bool dynamic;

			// Token: 0x040008B6 RID: 2230
			public int refCount;
		}

		// Token: 0x02000269 RID: 617
		public struct Statistics
		{
			// Token: 0x040008B7 RID: 2231
			public int freeIdsCount;

			// Token: 0x040008B8 RID: 2232
			public int createdIdsCount;

			// Token: 0x040008B9 RID: 2233
			public int allocatedIdsTotalCount;

			// Token: 0x040008BA RID: 2234
			public int allocatedIdsDynamicCount;

			// Token: 0x040008BB RID: 2235
			public int allocatedIdsStaticCount;

			// Token: 0x040008BC RID: 2236
			public int availableIdsCount;
		}
	}
}
