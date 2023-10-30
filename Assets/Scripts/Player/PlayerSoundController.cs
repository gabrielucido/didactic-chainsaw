using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundController : MonoBehaviour
{
    private IPlayerController _player;
    public AudioClip gunShot;

    private AudioSource _audioSource;

    void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _player.Jumped += OnJumped;
        _player.Attacked += OnAttacked;
        // _player.GroundedChanged += OnGroundedChanged;
        //
        // _moveParticles.Play();
    }

    private void OnDisable()
    {
        _player.Jumped -= OnJumped;
        _player.Attacked -= OnAttacked;
        // _player.GroundedChanged -= OnGroundedChanged;

        // _moveParticles.Stop();
    }

    private void OnJumped()
    {
        Debug.Log("Jumped Sound Controller");
    }

    private void OnAttacked()
    {
        _audioSource.clip = gunShot;
        _audioSource.Play();
        // var facingRight = _player.IsFacingRight();
        // var auxSpawnPosition = transform.position;
        // auxSpawnPosition.x = auxSpawnPosition.x + (facingRight ? 1 : -1);
        // var instance =
        //     Instantiate(projectile, auxSpawnPosition, transform.rotation); //, auxSpawnPosition, Quaternion.identity);
        // instance.GetComponent<ProjectileController>().goingLeft = !facingRight;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        if (gunShot == null)
            Debug.LogWarning("Please assign a GunShot Audio Clip to the Player Sound Controller in the Gun Shot slot",
                this);
    }
#endif
}