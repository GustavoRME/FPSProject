using UnityEngine;

public class WeaponAnimatorComponent : MonoBehaviour
{
    private Animator _weaponAnimator;
    
    private AnimationClip _fireAnimationClip;
    private AnimationClip _reloadAnimationClip;

    private readonly int _fireHash = Animator.StringToHash("Base Layer.fire");
    private readonly int _reloadHash = Animator.StringToHash("Base Layer.reload");

    public void TakeAnimation(Animator animator, AnimationClip fireAnimation, AnimationClip reloadAnimation, float fireSpeed, float reloadSpeed)
    {
        _weaponAnimator = animator;
        _weaponAnimator.SetTrigger("selected");
        _weaponAnimator.SetBool("grounded", true);

        _fireAnimationClip = fireAnimation;
        _reloadAnimationClip = reloadAnimation;

        _weaponAnimator.SetFloat("fireSpeed", _fireAnimationClip.length / fireSpeed);
        _weaponAnimator.SetFloat("reloadSpeed", _reloadAnimationClip.length / reloadSpeed);       
    }

    //Actions
    public void MoveAnimation(float speed) => _weaponAnimator.SetFloat("speed", speed);
    public void FireAnimation() => _weaponAnimator.SetTrigger("fire");
    public void ReloadAnimation() => _weaponAnimator.SetTrigger("reload");
    
    public bool CanReloadOrFire() => _weaponAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != _fireHash 
        && _weaponAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash != _reloadHash;
}
