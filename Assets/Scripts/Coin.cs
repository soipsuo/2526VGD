using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Settings")]
    public int coinValue = 1;
    [SerializeField] private AudioClip _coinSound;

    [Header("Animations")]
    public float rotationSpeed = 180f;
    public float bobHeight = 0.2f;
    public float bobSpeed = 3f;

    private Vector3 _startPos;

    private void Start() { _startPos = transform.position; }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        float newY = _startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(_startPos.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource playerSource = other.GetComponentInChildren<AudioSource>();
            if (playerSource != null && _coinSound != null)
            {
                playerSource.PlayOneShot(_coinSound);
            }

            // This line matches the AddCoins function we just fixed
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.AddCoins(coinValue);
            }

            Destroy(gameObject);
        }
    }
}