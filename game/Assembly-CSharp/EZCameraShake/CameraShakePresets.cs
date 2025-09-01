using System;
using UnityEngine;

namespace EZCameraShake
{
	// Token: 0x020003BA RID: 954
	public static class CameraShakePresets
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x000BBDAA File Offset: 0x000B9FAA
		public static CameraShakeInstance Bump
		{
			get
			{
				return new CameraShakeInstance(2.5f, 4f, 0.1f, 0.75f)
				{
					PositionInfluence = Vector3.one * 0.15f,
					RotationInfluence = Vector3.one
				};
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000BBDE8 File Offset: 0x000B9FE8
		public static CameraShakeInstance Explosion
		{
			get
			{
				return new CameraShakeInstance(5f, 10f, 0f, 1.5f)
				{
					PositionInfluence = Vector3.one * 0.25f,
					RotationInfluence = new Vector3(4f, 1f, 1f)
				};
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x000BBE40 File Offset: 0x000BA040
		public static CameraShakeInstance Earthquake
		{
			get
			{
				return new CameraShakeInstance(0.6f, 3.5f, 2f, 10f)
				{
					PositionInfluence = Vector3.one * 0.25f,
					RotationInfluence = new Vector3(1f, 1f, 4f)
				};
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x000BBE98 File Offset: 0x000BA098
		public static CameraShakeInstance BadTrip
		{
			get
			{
				return new CameraShakeInstance(10f, 0.15f, 5f, 10f)
				{
					PositionInfluence = new Vector3(0f, 0f, 0.15f),
					RotationInfluence = new Vector3(2f, 1f, 4f)
				};
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x000BBEF2 File Offset: 0x000BA0F2
		public static CameraShakeInstance HandheldCamera
		{
			get
			{
				return new CameraShakeInstance(1f, 0.25f, 5f, 10f)
				{
					PositionInfluence = Vector3.zero,
					RotationInfluence = new Vector3(1f, 0.5f, 0.5f)
				};
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x000BBF34 File Offset: 0x000BA134
		public static CameraShakeInstance Vibration
		{
			get
			{
				return new CameraShakeInstance(0.4f, 20f, 2f, 2f)
				{
					PositionInfluence = new Vector3(0f, 0.15f, 0f),
					RotationInfluence = new Vector3(1.25f, 0f, 4f)
				};
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000BBF8E File Offset: 0x000BA18E
		public static CameraShakeInstance RoughDriving
		{
			get
			{
				return new CameraShakeInstance(1f, 2f, 1f, 1f)
				{
					PositionInfluence = Vector3.zero,
					RotationInfluence = Vector3.one
				};
			}
		}
	}
}
