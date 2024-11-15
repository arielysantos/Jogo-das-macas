using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Interface para gerenciar o spawn
public interface ISpawnManager
{
    void Spawn(); // Método de spawn
    void SetCooldown(float newCooldown); // Define o cooldown do spawn
    void AddPrefab(GameObject prefab); // Adiciona um novo prefab à lista
    void ClearPrefabs(); // Limpa a lista de prefabs
}
public class SpawnManager : MonoBehaviour, ISpawnManager
{
    [SerializeField] private List<GameObject> applePrefabs = new List<GameObject>(); // Lista de prefabs
    private float timer;
    private float cooldown = 1f; // Cooldown padrão
    private void Update()
    {
        Spawn();
    }
    // Implementação do método de spawn
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
    // Método para definir o cooldown do spawn
    public void SetCooldown(float newCooldown)
    {
        cooldown = Mathf.Max(0.1f, newCooldown); // Garante que o cooldown não seja menor que 0.1
    }
    // Método para adicionar um novo prefab à lista
    public void AddPrefab(GameObject prefab)
    {
        if (prefab != null && !applePrefabs.Contains(prefab))
        {
            applePrefabs.Add(prefab);
        }
    }
    // Método para limpar todos os prefabs da lista
    public void ClearPrefabs()
    {
        applePrefabs.Clear();
    }