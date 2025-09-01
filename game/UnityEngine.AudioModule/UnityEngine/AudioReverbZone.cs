using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Audio/Public/AudioReverbZone.h")]
	public sealed class AudioReverbZone : Behaviour
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		public extern float minDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C1 RID: 193
		// (set) Token: 0x060000C2 RID: 194
		public extern float maxDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C3 RID: 195
		// (set) Token: 0x060000C4 RID: 196
		public extern AudioReverbPreset reverbPreset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		public extern int room { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C7 RID: 199
		// (set) Token: 0x060000C8 RID: 200
		public extern int roomHF { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C9 RID: 201
		// (set) Token: 0x060000CA RID: 202
		public extern int roomLF { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CB RID: 203
		// (set) Token: 0x060000CC RID: 204
		public extern float decayTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CD RID: 205
		// (set) Token: 0x060000CE RID: 206
		public extern float decayHFRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CF RID: 207
		// (set) Token: 0x060000D0 RID: 208
		public extern int reflections { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D1 RID: 209
		// (set) Token: 0x060000D2 RID: 210
		public extern float reflectionsDelay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D3 RID: 211
		// (set) Token: 0x060000D4 RID: 212
		public extern int reverb { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D5 RID: 213
		// (set) Token: 0x060000D6 RID: 214
		public extern float reverbDelay { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D7 RID: 215
		// (set) Token: 0x060000D8 RID: 216
		public extern float HFReference { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D9 RID: 217
		// (set) Token: 0x060000DA RID: 218
		public extern float LFReference { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002A50 File Offset: 0x00000C50
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002A72 File Offset: 0x00000C72
		[Obsolete("Warning! roomRolloffFactor is no longer supported.")]
		public float roomRolloffFactor
		{
			get
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
				return 10f;
			}
			set
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DD RID: 221
		// (set) Token: 0x060000DE RID: 222
		public extern float diffusion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DF RID: 223
		// (set) Token: 0x060000E0 RID: 224
		public extern float density { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060000E1 RID: 225 RVA: 0x00002760 File Offset: 0x00000960
		public AudioReverbZone()
		{
		}
	}
}
