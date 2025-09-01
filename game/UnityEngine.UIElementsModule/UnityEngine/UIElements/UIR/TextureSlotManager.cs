using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000327 RID: 807
	internal class TextureSlotManager
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x00072424 File Offset: 0x00070624
		static TextureSlotManager()
		{
			TextureSlotManager.k_SlotCount = (UIRenderDevice.shaderModelIs35 ? 8 : 4);
			TextureSlotManager.slotIds = new int[TextureSlotManager.k_SlotCount];
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				TextureSlotManager.slotIds[i] = Shader.PropertyToID(string.Format("_Texture{0}", i));
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00072490 File Offset: 0x00070690
		public TextureSlotManager()
		{
			this.m_Textures = new TextureId[TextureSlotManager.k_SlotCount];
			this.m_Tickets = new int[TextureSlotManager.k_SlotCount];
			this.m_GpuTextures = new Vector4[TextureSlotManager.k_SlotCount];
			this.Reset();
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000724F4 File Offset: 0x000706F4
		public void Reset()
		{
			this.m_CurrentTicket = 0;
			this.m_FirstUsedTicket = 0;
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				this.m_Textures[i] = TextureId.invalid;
				this.m_Tickets[i] = -1;
				this.m_GpuTextures[i] = new Vector4(-1f, 1f, 1f, 0f);
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00072568 File Offset: 0x00070768
		public void StartNewBatch()
		{
			int num = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num;
			this.m_FirstUsedTicket = num;
			this.FreeSlots = TextureSlotManager.k_SlotCount;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0007259C File Offset: 0x0007079C
		public int IndexOf(TextureId id)
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Textures[i].index == id.index;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000725E8 File Offset: 0x000707E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MarkUsed(int slotIndex)
		{
			int num = this.m_Tickets[slotIndex];
			bool flag = num < this.m_FirstUsedTicket;
			int num2;
			if (flag)
			{
				num2 = this.FreeSlots - 1;
				this.FreeSlots = num2;
			}
			int[] tickets = this.m_Tickets;
			num2 = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num2;
			tickets[slotIndex] = num2;
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00072636 File Offset: 0x00070836
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x0007263E File Offset: 0x0007083E
		public int FreeSlots
		{
			[CompilerGenerated]
			get
			{
				return this.<FreeSlots>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FreeSlots>k__BackingField = value;
			}
		} = TextureSlotManager.k_SlotCount;

		// Token: 0x06001A5D RID: 6749 RVA: 0x00072648 File Offset: 0x00070848
		public int FindOldestSlot()
		{
			int num = this.m_Tickets[0];
			int result = 0;
			for (int i = 1; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Tickets[i] < num;
				if (flag)
				{
					num = this.m_Tickets[i];
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0007269C File Offset: 0x0007089C
		public void Bind(TextureId id, int slot, MaterialPropertyBlock mat)
		{
			Texture texture = this.textureRegistry.GetTexture(id);
			bool flag = texture == null;
			if (flag)
			{
				texture = Texture2D.whiteTexture;
			}
			this.m_Textures[slot] = id;
			this.MarkUsed(slot);
			this.m_GpuTextures[slot] = new Vector4(id.ConvertToGpu(), 1f / (float)texture.width, 1f / (float)texture.height, 0f);
			mat.SetTexture(TextureSlotManager.slotIds[slot], texture);
			mat.SetVectorArray(TextureSlotManager.textureTableId, this.m_GpuTextures);
		}

		// Token: 0x04000C0F RID: 3087
		private static readonly int k_SlotCount;

		// Token: 0x04000C10 RID: 3088
		internal static readonly int[] slotIds;

		// Token: 0x04000C11 RID: 3089
		internal static readonly int textureTableId = Shader.PropertyToID("_TextureInfo");

		// Token: 0x04000C12 RID: 3090
		private TextureId[] m_Textures;

		// Token: 0x04000C13 RID: 3091
		private int[] m_Tickets;

		// Token: 0x04000C14 RID: 3092
		private int m_CurrentTicket;

		// Token: 0x04000C15 RID: 3093
		private int m_FirstUsedTicket;

		// Token: 0x04000C16 RID: 3094
		private Vector4[] m_GpuTextures;

		// Token: 0x04000C17 RID: 3095
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <FreeSlots>k__BackingField;

		// Token: 0x04000C18 RID: 3096
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}
