using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000044 RID: 68
	public class UnityMouseProvider : IMouseProvider
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0000B5B9 File Offset: 0x000097B9
		public void Setup()
		{
			this.ClearState();
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000B5C1 File Offset: 0x000097C1
		public void Reset()
		{
			this.ClearState();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000B5CC File Offset: 0x000097CC
		public void Update()
		{
			if (Input.mousePresent)
			{
				Array.Copy(this.buttonPressed, this.lastButtonPressed, this.buttonPressed.Length);
				this.buttonPressed[1] = UnityMouseProvider.SafeGetMouseButton(0);
				this.buttonPressed[2] = UnityMouseProvider.SafeGetMouseButton(1);
				this.buttonPressed[3] = UnityMouseProvider.SafeGetMouseButton(2);
				this.buttonPressed[10] = UnityMouseProvider.SafeGetMouseButton(3);
				this.buttonPressed[11] = UnityMouseProvider.SafeGetMouseButton(4);
				this.buttonPressed[12] = UnityMouseProvider.SafeGetMouseButton(5);
				this.buttonPressed[13] = UnityMouseProvider.SafeGetMouseButton(6);
				this.lastPosition = this.position;
				this.position = Input.mousePosition;
				this.delta = new Vector2(Input.GetAxisRaw("mouse x"), Input.GetAxisRaw("mouse y"));
				this.scroll = Input.mouseScrollDelta;
				return;
			}
			this.ClearState();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000B6B0 File Offset: 0x000098B0
		private static bool SafeGetMouseButton(int button)
		{
			if (button < UnityMouseProvider.maxSafeMouseButton)
			{
				try
				{
					return Input.GetMouseButton(button);
				}
				catch (ArgumentException)
				{
					UnityMouseProvider.maxSafeMouseButton = Mathf.Min(button, UnityMouseProvider.maxSafeMouseButton);
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000B6F4 File Offset: 0x000098F4
		private void ClearState()
		{
			Array.Clear(this.lastButtonPressed, 0, this.lastButtonPressed.Length);
			Array.Clear(this.buttonPressed, 0, this.buttonPressed.Length);
			this.lastPosition = Vector2.zero;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.scroll = Vector2.zero;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000B755 File Offset: 0x00009955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 GetPosition()
		{
			return this.position;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B75D File Offset: 0x0000995D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetDeltaX()
		{
			return this.delta.x;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000B76A File Offset: 0x0000996A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetDeltaY()
		{
			return this.delta.y;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000B777 File Offset: 0x00009977
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetDeltaScroll()
		{
			if (Utility.Abs(this.scroll.x) <= Utility.Abs(this.scroll.y))
			{
				return this.scroll.y;
			}
			return this.scroll.x;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000B7B2 File Offset: 0x000099B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool GetButtonIsPressed(Mouse control)
		{
			return this.buttonPressed[(int)control];
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000B7BC File Offset: 0x000099BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool GetButtonWasPressed(Mouse control)
		{
			return this.buttonPressed[(int)control] && !this.lastButtonPressed[(int)control];
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000B7D5 File Offset: 0x000099D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool GetButtonWasReleased(Mouse control)
		{
			return !this.buttonPressed[(int)control] && this.lastButtonPressed[(int)control];
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000B7EB File Offset: 0x000099EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasMousePresent()
		{
			return Input.mousePresent;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000B7F2 File Offset: 0x000099F2
		public UnityMouseProvider()
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000B814 File Offset: 0x00009A14
		// Note: this type is marked as 'beforefieldinit'.
		static UnityMouseProvider()
		{
		}

		// Token: 0x04000315 RID: 789
		private const string mouseXAxis = "mouse x";

		// Token: 0x04000316 RID: 790
		private const string mouseYAxis = "mouse y";

		// Token: 0x04000317 RID: 791
		private readonly bool[] lastButtonPressed = new bool[16];

		// Token: 0x04000318 RID: 792
		private readonly bool[] buttonPressed = new bool[16];

		// Token: 0x04000319 RID: 793
		private Vector2 lastPosition;

		// Token: 0x0400031A RID: 794
		private Vector2 position;

		// Token: 0x0400031B RID: 795
		private Vector2 delta;

		// Token: 0x0400031C RID: 796
		private Vector2 scroll;

		// Token: 0x0400031D RID: 797
		private static int maxSafeMouseButton = int.MaxValue;
	}
}
