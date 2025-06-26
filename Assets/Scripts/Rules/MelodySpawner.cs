using UnityEngine;

public class MelodySpawner : MonoBehaviour
{
    public MelodyController melodyController;
    public PlayerStatus playerStatus;

    public float tempoMin = 40f;
    public float tempoMax = 80f;

    private bool cooldown = false;

    void Start()
    {
        Debug.Log("Ativo");
        AgendarProximaTentativa();
    }

    void AgendarProximaTentativa()
    {
        float delay = Random.Range(tempoMin, tempoMax);
        Invoke(nameof(TentarTocarMelodia), delay);
    }

    void TentarTocarMelodia()
    {
        if (cooldown || melodyController == null || playerStatus == null)
        {
            AgendarProximaTentativa();
            return;
        }

        float chanceBase = 0.25f;
        float escalonamentoPorSanidade = (100f - playerStatus.GetCurrentSanity()) / 100f; // 0 a 1
        float chanceFinal = chanceBase + (escalonamentoPorSanidade * 0.35f); // até 60% máx

        if (Random.value < chanceFinal)
        {
            melodyController.TocarMelodia();
            cooldown = true;
            GameEvents.OnMelodyEnd += LiberarCooldown;
        }

        AgendarProximaTentativa(); // sempre agenda a próxima
    }

    void LiberarCooldown()
    {
        cooldown = false;
        GameEvents.OnMelodyEnd -= LiberarCooldown;
    }
}
