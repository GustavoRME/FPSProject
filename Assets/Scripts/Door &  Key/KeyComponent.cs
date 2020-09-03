using UnityEngine;

public class KeyComponent : MonoBehaviour
{
    [Tooltip("Unique ID to match the with lock.")]
    [SerializeField] private int _keyID = 1;

    private Animator _anim;
    private KeychainComponent _keychain;
    private bool _isTaked;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _keychain = FindObjectOfType<KeychainComponent>();

        _isTaked = false;
    }

    //Called when player try take take. 
    public void Take()
    {
        if(!_isTaked)
        {
            //Start take animation
            _anim.SetTrigger("Take");

            _isTaked = true;            
        }        
    }

    //Notify player that key was taked. It's called when the animation is end up.
    public void NotifyPlayer()
    {
        //Notify player
        _keychain.NotifyPlayer(_keyID, name);

        //Destroy this object
        Destroy(gameObject);
    }

}
