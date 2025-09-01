using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal sealed class ColorPalette : ScriptableObject, IHasDefault
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000042D8 File Offset: 0x000024D8
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000042E0 File Offset: 0x000024E0
		public Color current
		{
			[CompilerGenerated]
			get
			{
				return this.<current>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<current>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000042E9 File Offset: 0x000024E9
		public ReadOnlyCollection<Color> colors
		{
			get
			{
				return new ReadOnlyCollection<Color>(this.m_Colors);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000042F6 File Offset: 0x000024F6
		public void SetColors(IEnumerable<Color> colors)
		{
			if (colors == null)
			{
				throw new ArgumentNullException("colors");
			}
			this.m_Colors = colors.ToList<Color>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004314 File Offset: 0x00002514
		public void SetDefaultValues()
		{
			this.m_Colors = new List<Color>
			{
				new Color(0f, 0.122f, 0.247f, 1f),
				new Color(0f, 0.455f, 0.851f, 1f),
				new Color(0.498f, 0.859f, 1f, 1f),
				new Color(0.224f, 0.8f, 0.8f, 1f),
				new Color(0.239f, 0.6f, 0.439f, 1f),
				new Color(0.18f, 0.8f, 0.251f, 1f),
				new Color(0.004f, 1f, 0.439f, 1f),
				new Color(1f, 0.863f, 0f, 1f),
				new Color(1f, 0.522f, 0.106f, 1f),
				new Color(1f, 0.255f, 0.212f, 1f),
				new Color(0.522f, 0.078f, 0.294f, 1f),
				new Color(0.941f, 0.071f, 0.745f, 1f),
				new Color(0.694f, 0.051f, 0.788f, 1f),
				new Color(0.067f, 0.067f, 0.067f, 1f),
				new Color(0.667f, 0.667f, 0.667f, 1f),
				new Color(0.867f, 0.867f, 0.867f, 1f)
			};
		}

		// Token: 0x17000028 RID: 40
		public Color this[int i]
		{
			get
			{
				return this.m_Colors[i];
			}
			set
			{
				this.m_Colors[i] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004539 File Offset: 0x00002739
		public int Count
		{
			get
			{
				return this.m_Colors.Count;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004546 File Offset: 0x00002746
		public ColorPalette()
		{
		}

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		private Color <current>k__BackingField;

		// Token: 0x04000040 RID: 64
		[FormerlySerializedAs("colors")]
		[SerializeField]
		private List<Color> m_Colors;
	}
}
