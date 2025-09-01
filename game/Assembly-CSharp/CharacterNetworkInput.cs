using System;
using CMF;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class CharacterNetworkInput : CharacterInput
{
	// Token: 0x06000195 RID: 405 RVA: 0x0000F747 File Offset: 0x0000D947
	private void Awake()
	{
		this.mover = base.GetComponent<Mover>();
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000F755 File Offset: 0x0000D955
	public override float GetHorizontalMovementInput()
	{
		return this.overrideInput.x;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000F762 File Offset: 0x0000D962
	public override float GetVerticalMovementInput()
	{
		return this.overrideInput.y;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000F76F File Offset: 0x0000D96F
	private void FixedUpdate()
	{
		this.mover.SetVelocity(this.overrideVelocity);
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000F782 File Offset: 0x0000D982
	public override bool IsJumpKeyPressed()
	{
		return false;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000F785 File Offset: 0x0000D985
	public CharacterNetworkInput()
	{
	}

	// Token: 0x040001E0 RID: 480
	public Vector3 wantPosition;

	// Token: 0x040001E1 RID: 481
	public Vector3 overrideVelocity;

	// Token: 0x040001E2 RID: 482
	public Vector2 overrideInput;

	// Token: 0x040001E3 RID: 483
	private Mover mover;
}
