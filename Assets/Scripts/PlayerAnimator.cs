using UnityEngine;

namespace TarodevController
{
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator _anim;
        [SerializeField] private SpriteRenderer _sprite;

        [Header("Audio Setup")]
        [SerializeField] private AudioSource _walkingSource;
        [SerializeField] private AudioSource _jumpSource;
        [SerializeField] private AudioClip _jumpSound;

        [Header("Settings")]
        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxTilt = 5;
        [SerializeField] private float _tiltSpeed = 20;

        [Header("Particles")]
        [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles;
        [SerializeField] private ParticleSystem _landParticles;

        private IPlayerController _player;
        private bool _grounded;
        private ParticleSystem.MinMaxGradient _currentGradient;

        // This is the new variable to fix the "Start Jump"
        private float _timeSinceStart;

        private void Awake()
        {
            _player = GetComponentInParent<IPlayerController>();

            AudioSource[] sources = GetComponents<AudioSource>();
            if (_walkingSource == null && sources.Length > 0) _walkingSource = sources[0];
            if (_jumpSource == null && sources.Length > 1) _jumpSource = sources[1];
        }

        private void OnEnable()
        {
            if (_player == null) return;
            _player.Jumped += OnJumped;
            _player.GroundedChanged += OnGroundedChanged;
        }

        private void OnDisable()
        {
            if (_player == null) return;
            _player.Jumped -= OnJumped;
            _player.GroundedChanged -= OnGroundedChanged;
        }

        private void Update()
        {
            if (_player == null) return;

            // Update the timer every frame
            _timeSinceStart += Time.deltaTime;

            DetectGroundColor();
            HandleSpriteFlip();
            HandleIdleSpeed();
            HandleCharacterTilt();

            HandleFootstepsLoop();
        }

        private void HandleFootstepsLoop()
        {
            if (_walkingSource == null || _walkingSource.clip == null) return;

            bool isMoving = _grounded && Mathf.Abs(_player.FrameInput.x) > 0.01f;

            if (isMoving)
            {
                if (!_walkingSource.isPlaying) _walkingSource.Play();
            }
            else
            {
                if (_walkingSource.isPlaying && _grounded) _walkingSource.Stop();
            }
        }

        private void HandleSpriteFlip()
        {
            if (_player.FrameInput.x != 0) _sprite.flipX = _player.FrameInput.x < 0;
        }

        private void HandleIdleSpeed()
        {
            var inputStrength = Mathf.Abs(_player.FrameInput.x);
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));
            _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);
        }

        private void HandleCharacterTilt()
        {
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;
            _anim.transform.up = Vector3.RotateTowards(_anim.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
        }

        private void OnJumped()
        {
            // FIX: If game has been running for less than 0.2 seconds, ignore the jump
            if (_timeSinceStart < 0.2f) return;

            _anim.SetTrigger(JumpKey);
            _anim.ResetTrigger(GroundedKey);

            if (_walkingSource != null) _walkingSource.Stop();

            if (_jumpSound != null && _jumpSource != null)
            {
                _jumpSource.PlayOneShot(_jumpSound);
            }

            if (_grounded)
            {
                SetColor(_jumpParticles);
                SetColor(_launchParticles);
                _jumpParticles.Play();
            }
        }

        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;

            // Apply the same fix to landing sounds/particles if needed
            if (grounded && _timeSinceStart > 0.2f)
            {
                DetectGroundColor();
                SetColor(_landParticles);
                _anim.SetTrigger(GroundedKey);
                _moveParticles.Play();
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                _landParticles.Play();
            }
            else
            {
                _moveParticles.Stop();
                if (_walkingSource != null && _walkingSource.isPlaying) _walkingSource.Stop();
            }
        }

        private void DetectGroundColor()
        {
            var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);
            if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) return;
            var color = r.color;
            _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
            SetColor(_moveParticles);
        }

        private void SetColor(ParticleSystem ps)
        {
            var main = ps.main;
            main.startColor = _currentGradient;
        }

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
    }
}