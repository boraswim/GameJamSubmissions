using DG.Tweening;
using UnityEngine;

public class EssentialPhysics : MonoBehaviour
{
    public static void SetFacingDirection(Transform transformFPS, Transform transformTPS, Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        //transformFPS.DORotateQuaternion(Quaternion.LookRotation(direction), 0f);
        transformTPS.DORotate(new Vector3(0, targetAngle, 0), 0.3f);
    }

    public static void GroundCheck(Transform transform, PlayerData data)
    {
        if (Physics.SphereCast(transform.position + data.Physics.grondCheckPosition, 0.40f, Vector3.down, out RaycastHit _hit, 0.3f, data.Physics.groundLayerMask))
        {
            if ((data.Physics.groundLayerMask.value & (1 << _hit.transform.gameObject.layer)) > 0)
            {
                data.Physics.isGrounded = true;
            }
        }
        else
        {
            data.Physics.isGrounded = false;
        }
    }

    public static void MovingPlatformCheck(Rigidbody playerRb, Transform transform, PlayerData data)
    {
        if (Physics.SphereCast(transform.position + data.Physics.grondCheckPosition, 0.40f, Vector3.down, out RaycastHit _hit, 0.3f, data.Physics.movingPlatformLayerMask))
        {
            if ((data.Physics.movingPlatformLayerMask.value & (1 << _hit.transform.gameObject.layer)) > 0)
            {
                data.Physics.isOnMovablePlatform = true;
                transform.parent = _hit.transform;
                playerRb.interpolation = RigidbodyInterpolation.None;
            }
        }
        else
        {
            data.Physics.isOnMovablePlatform = false;
            transform.parent = null;
                playerRb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }
    /*
    public static void SetWorldVelocity(PlayerData data)
    {
        if (data.Physics.isOnMovablePlatform)
        {
            data.Physics.worldVelocity = data.Physics.movingPlatform.velocity;
        }
        else
        {
            data.Physics.worldVelocity = Vector3.zero;
        }
    }
    */
    public static float SetCurveTimeByValue(AnimationCurve curve, float value, float maxTime = 1f, bool greaterValues = true)
    {
        float _curveTime = 0f;
        while ((greaterValues && curve.Evaluate(_curveTime) <= value) || (!greaterValues && curve.Evaluate(_curveTime) >= value))
        {
            _curveTime += Time.fixedDeltaTime;
            if (_curveTime >= maxTime)
            {
                break;
            }
        }
        return _curveTime;
    }

    public static void UpdateTimersAndChecks(PlayerData data)
    {
        data.Actions.Jump.JumpBufferTimer -= data.Actions.Jump.JumpBufferTimer == 0 ? 0f : Time.deltaTime;
        data.Actions.Jump.CoyoteTimeTimer -= data.Actions.Jump.CoyoteTimeTimer == 0 ? 0f : Time.deltaTime;

        data.Actions.Dash.dashCooldownTimer -= data.Actions.Dash.dashCooldownTimer == 0 ? 0f : Time.deltaTime;

        if (data.Physics.isGrounded)
        {
            data.Actions.Jump.CoyoteTimeTimer = data.Actions.Jump.CoyoteTimeMaxTime;
        }

        data.Physics.canJump = data.Actions.Jump.JumpBufferTimer > 0f && data.Actions.Jump.CoyoteTimeTimer > 0f;
        data.Physics.canDash = data.Actions.Dash.dashCooldownTimer <= 0f;
    }

    public static void UpdateSecondaryActions(PlayerData data)
    {
        if (data.secondaryAction == PlayerData.SpearAction.Idle)
        {
            data.Physics.readyToThrowSpear = false;
        }
        else if (data.secondaryAction == PlayerData.SpearAction.Aim)
        {
            data.Actions.Spear.spearAimTimer -= Time.deltaTime;
            if (data.Actions.Spear.spearAimTimer <= 0f)
            {
                data.Physics.readyToThrowSpear = true;
            }
        }
    }

    public static void ApplySecondaryActions(PlayerData data)
    {
        if (data.secondaryAction == PlayerData.SpearAction.Attack1 || data.secondaryAction == PlayerData.SpearAction.Attack2)
        {
            data.Actions.Spear.spearAnimator.SetTrigger(data.secondaryAction.ToString());

            if (data.CurrentAction == PlayerData.ModeAction.Run)
            {
                data.secondaryAction = PlayerData.SpearAction.Run;
                data.Actions.Spear.spearAnimator.SetBool("Run", true);
                data.Actions.Spear.spearAnimator.SetBool("Idle", false);
            }
            else
            {
                data.secondaryAction = PlayerData.SpearAction.Idle;
                data.Actions.Spear.spearAnimator.SetBool("Run", false);
                data.Actions.Spear.spearAnimator.SetBool("Idle", true);
            }
        }

        if (data.secondaryAction == PlayerData.SpearAction.Aim)
        {
            data.Actions.Spear.spearAnimator.SetBool("Aim", true);
        }

        if (data.secondaryAction == PlayerData.SpearAction.ThrowSpear)
        {
            data.Actions.Spear.spear.gameObject.SetActive(false);
            data.Actions.Spear.GetSpearTimer -= Time.deltaTime;

            if (data.Actions.Spear.GetSpearTimer <= 0f)
            {
                data.Actions.Spear.spear.gameObject.SetActive(true);
                data.Actions.Spear.spearAnimator.SetTrigger("GetSpear");
                if (data.CurrentAction == PlayerData.ModeAction.Run)
                {
                    data.secondaryAction = PlayerData.SpearAction.Run;
                    data.Actions.Spear.spearAnimator.SetBool("Run", true);
                    data.Actions.Spear.spearAnimator.SetBool("Idle", false);
                }
                else
                {
                    data.secondaryAction = PlayerData.SpearAction.Idle;
                    data.Actions.Spear.spearAnimator.SetBool("Run", false);
                    data.Actions.Spear.spearAnimator.SetBool("Idle", true);
                }
            }
        }
    }

    public static void ThrowSpearAction(PlayerData data)
    {
        GameObject _spear = Instantiate(data.Actions.Spear.spearPrefab, data.Actions.Spear.spear.position, data.Actions.Spear.spear.rotation);
        MoveSpear _moveSpear = _spear.GetComponent<MoveSpear>();
        _moveSpear.Force = data.Actions.Spear.ThrowForce;
        _moveSpear.Drag = data.Actions.Spear.ThrowDrag;
        
        data.Actions.Spear.GetSpearTimer = data.Actions.Spear.GetSpearMaxTime;
    }
}
