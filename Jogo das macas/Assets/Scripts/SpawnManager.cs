using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Interface para gerenciar o spawn
public interface ISpawnManager
{
    void Spawn(); // M�todo de spawn
    void SetCooldown(float newCooldown); // Define o cooldown do spawn
    void AddPrefab(GameObject prefab); // Adiciona um novo prefab � lista
    void ClearPrefabs(); // Limpa a lista de prefabs
}
public class SpawnManager : MonoBehaviour, ISpawnManager
{
    [SerializeField] private List<GameObject> applePrefabs = new List<GameObject>(); // Lista de prefabs
    private float timer;
    private float cooldown = 1f; // Cooldown padr�o
    private void Update()
    {
        Spawn();
    }
    // Implementa��o do m�todo de spawn
    public void Spawn()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && applePrefabs.Count > 0)
        {
            GameObject appleSelected = GetRandomApple();
            Vector3 spawnPosition = new Vector3(
                Random.Range(-GameManager.instance.ScreenBounds.x, GameManager.instance.ScreenBounds.x),
                GameManager.instance.ScreenBounds.y,
                0
            );
            Instantiate(appleSelected, spawnPosition, Quaternion.identity);
            timer = cooldown;
        }
    }
    // M�todo para definir o cooldown do spawn
    public void SetCooldown(float newCooldown)
    {
        cooldown = Mathf.Max(0.1f, newCooldown); // Garante que o cooldown n�o seja menor que 0.1
    }
    // M�todo para adicionar um novo prefab � lista
    public void AddPrefab(GameObject prefab)
    {
        if (prefab != null && !applePrefabs.Contains(prefab))
        {
            applePrefabs.Add(prefab);
        }
    }
    // M�todo para limpar todos os prefabs da lista
    public void ClearPrefabs()
    {
        applePrefabs.Clear();
    }