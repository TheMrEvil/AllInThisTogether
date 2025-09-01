using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000017 RID: 23
	public class MouseBindingSource : BindingSource
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002FFF File Offset: 0x000011FF
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003007 File Offset: 0x00001207
		public Mouse Control
		{
			[CompilerGenerated]
			get
			{
				return this.<Control>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Control>k__BackingField = value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003010 File Offset: 0x00001210
		internal MouseBindingSource()
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003018 File Offset: 0x00001218
		public MouseBindingSource(Mouse mouseControl)
		{
			this.Control = mouseControl;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003027 File Offset: 0x00001227
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool ButtonIsPressed(Mouse control)
		{
			return InputManager.MouseProvider.GetButtonIsPressed(control);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003034 File Offset: 0x00001234
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool NegativeScrollWheelIsActive(float threshold)
		{
			return Mathf.Min(InputManager.MouseProvider.GetDeltaScroll() * MouseBindingSource.ScaleZ, 0f) < -threshold;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003054 File Offset: 0x00001254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool PositiveScrollWheelIsActive(float threshold)
		{
			return Mathf.Max(0f, InputManager.MouseProvider.GetDeltaScroll() * MouseBindingSource.ScaleZ) > threshold;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003074 File Offset: 0x00001274
		internal static float GetValue(Mouse mouseControl)
		{
			switch (mouseControl)
			{
			case Mouse.None:
				return 0f;
			case Mouse.NegativeX:
				return -Mathf.Min(InputManager.MouseProvider.GetDeltaX() * MouseBindingSource.ScaleX, 0f);
			case Mouse.PositiveX:
				return Mathf.Max(0f, InputManager.MouseProvider.GetDeltaX() * MouseBindingSource.ScaleX);
			case Mouse.NegativeY:
				return -Mathf.Min(InputManager.MouseProvider.GetDeltaY() * MouseBindingSource.ScaleY, 0f);
			case Mouse.PositiveY:
				return Mathf.Max(0f, InputManager.MouseProvider.GetDeltaY() * MouseBindingSource.ScaleY);
			case Mouse.PositiveScrollWheel:
				return Mathf.Max(0f, InputManager.MouseProvider.GetDeltaScroll() * MouseBindingSource.ScaleZ);
			case Mouse.NegativeScrollWheel:
				return -Mathf.Min(InputManager.MouseProvider.GetDeltaScroll() * MouseBindingSource.ScaleZ, 0f);
			}
			if (!InputManager.MouseProvider.GetButtonIsPressed(mouseControl))
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003177 File Offset: 0x00001377
		public override float GetValue(InputDevice inputDevice)
		{
			return MouseBindingSource.GetValue(this.Control);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003184 File Offset: 0x00001384
		public override bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003194 File Offset: 0x00001394
		public override string Name
		{
			get
			{
				return this.Control.ToString();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000031B5 File Offset: 0x000013B5
		public override string DeviceName
		{
			get
			{
				return "Mouse";
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000031BC File Offset: 0x000013BC
		public override InputDeviceClass DeviceClass
		{
			get
			{
				return InputDeviceClass.Mouse;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000031BF File Offset: 0x000013BF
		public override InputDeviceStyle DeviceStyle
		{
			get
			{
				return InputDeviceStyle.Unknown;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000031C4 File Offset: 0x000013C4
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			MouseBindingSource mouseBindingSource = other as MouseBindingSource;
			return mouseBindingSource != null && this.Control == mouseBindingSource.Control;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000031FC File Offset: 0x000013FC
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			MouseBindingSource mouseBindingSource = other as MouseBindingSource;
			return mouseBindingSource != null && this.Control == mouseBindingSource.Control;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003230 File Offset: 0x00001430
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003251 File Offset: 0x00001451
		public override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.MouseBindingSource;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003254 File Offset: 0x00001454
		public override void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003262 File Offset: 0x00001462
		public override void Load(BinaryReader reader, ushort dataFormatVersion)
		{
			this.Control = (Mouse)reader.ReadInt32();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003270 File Offset: 0x00001470
		// Note: this type is marked as 'beforefieldinit'.
		static MouseBindingSource()
		{
		}

		// Token: 0x040000E0 RID: 224
		[CompilerGenerated]
		private Mouse <Control>k__BackingField;

		// Token: 0x040000E1 RID: 225
		public static float ScaleX = 0.05f;

		// Token: 0x040000E2 RID: 226
		public static float ScaleY = 0.05f;

		// Token: 0x040000E3 RID: 227
		public static float ScaleZ = 0.05f;

		// Token: 0x040000E4 RID: 228
		public static float JitterThreshold = 0.05f;
	}
}
