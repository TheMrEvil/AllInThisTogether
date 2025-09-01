using System;
using System.Runtime.InteropServices;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200003F RID: 63
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public readonly struct ProfilerCategory
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00002669 File Offset: 0x00000869
		public ProfilerCategory(string categoryName)
		{
			this.m_CategoryId = ProfilerUnsafeUtility.CreateCategory(categoryName, ProfilerCategoryColor.Scripts);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002679 File Offset: 0x00000879
		public ProfilerCategory(string categoryName, ProfilerCategoryColor color)
		{
			this.m_CategoryId = ProfilerUnsafeUtility.CreateCategory(categoryName, color);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002689 File Offset: 0x00000889
		internal ProfilerCategory(ushort category)
		{
			this.m_CategoryId = category;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002694 File Offset: 0x00000894
		public string Name
		{
			get
			{
				ProfilerCategoryDescription categoryDescription = ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId);
				return ProfilerUnsafeUtility.Utf8ToString(categoryDescription.NameUtf8, categoryDescription.NameUtf8Len);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000026C3 File Offset: 0x000008C3
		public Color32 Color
		{
			get
			{
				return ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId).Color;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000026D8 File Offset: 0x000008D8
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000026F0 File Offset: 0x000008F0
		public static ProfilerCategory Render
		{
			get
			{
				return new ProfilerCategory(0);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000026F8 File Offset: 0x000008F8
		public static ProfilerCategory Scripts
		{
			get
			{
				return new ProfilerCategory(1);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002700 File Offset: 0x00000900
		public static ProfilerCategory Gui
		{
			get
			{
				return new ProfilerCategory(4);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002708 File Offset: 0x00000908
		public static ProfilerCategory Physics
		{
			get
			{
				return new ProfilerCategory(5);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002710 File Offset: 0x00000910
		public static ProfilerCategory Animation
		{
			get
			{
				return new ProfilerCategory(6);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002718 File Offset: 0x00000918
		public static ProfilerCategory Ai
		{
			get
			{
				return new ProfilerCategory(7);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002720 File Offset: 0x00000920
		public static ProfilerCategory Audio
		{
			get
			{
				return new ProfilerCategory(8);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002728 File Offset: 0x00000928
		public static ProfilerCategory Video
		{
			get
			{
				return new ProfilerCategory(11);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002731 File Offset: 0x00000931
		public static ProfilerCategory Particles
		{
			get
			{
				return new ProfilerCategory(12);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000273A File Offset: 0x0000093A
		public static ProfilerCategory Lighting
		{
			get
			{
				return new ProfilerCategory(13);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002743 File Offset: 0x00000943
		public static ProfilerCategory Network
		{
			get
			{
				return new ProfilerCategory(14);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000274C File Offset: 0x0000094C
		public static ProfilerCategory Loading
		{
			get
			{
				return new ProfilerCategory(15);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002755 File Offset: 0x00000955
		public static ProfilerCategory Vr
		{
			get
			{
				return new ProfilerCategory(22);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000275E File Offset: 0x0000095E
		public static ProfilerCategory Input
		{
			get
			{
				return new ProfilerCategory(30);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002767 File Offset: 0x00000967
		public static ProfilerCategory Memory
		{
			get
			{
				return new ProfilerCategory(23);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002770 File Offset: 0x00000970
		public static ProfilerCategory VirtualTexturing
		{
			get
			{
				return new ProfilerCategory(31);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002779 File Offset: 0x00000979
		public static ProfilerCategory FileIO
		{
			get
			{
				return new ProfilerCategory(25);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002782 File Offset: 0x00000982
		public static ProfilerCategory Internal
		{
			get
			{
				return new ProfilerCategory(24);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000278B File Offset: 0x0000098B
		internal static ProfilerCategory Any
		{
			get
			{
				return new ProfilerCategory(ushort.MaxValue);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002797 File Offset: 0x00000997
		internal static ProfilerCategory GPU
		{
			get
			{
				return new ProfilerCategory(32);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000027A0 File Offset: 0x000009A0
		internal static ProfilerCategory Physics2D
		{
			get
			{
				return new ProfilerCategory(33);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000027AC File Offset: 0x000009AC
		public static implicit operator ushort(ProfilerCategory category)
		{
			return category.m_CategoryId;
		}

		// Token: 0x040000F7 RID: 247
		[FieldOffset(0)]
		private readonly ushort m_CategoryId;
	}
}
