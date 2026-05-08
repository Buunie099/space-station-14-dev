using System.Numerics;
using Content.Shared.Traits.Assorted.Schizophrenia;
using Robust.Client.Player;
using Robust.Shared.Player;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Client.Traits.Schizophrenia;

public sealed class SchizophreniaSystem : SharedSchizophreniaSystem
{
    [Dependency] private readonly IPlayerManager _player = default!;
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<SchizophreniaComponent, ComponentStartup>(OnComponentStartup);
        SubscribeLocalEvent<SchizophreniaComponent, LocalPlayerDetachedEvent>(OnComponentDetached);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        if (!_timing.IsFirstTimePredicted)
            return;

        if (_player.LocalEntity is not EntityUid localPlayer)
            return;

        ShowHallucination(localPlayer);
    }

    private void OnComponentStartup(EntityUid uid, SchizophreniaComponent component, ComponentStartup args)
    {
        component.NextIncidentTime = _timing.CurTime + TimeSpan.FromSeconds(_random.NextFloat(component.MinTimeBetweenIncidents, component.MaxTimeBetweenIncidents));
    }

    private void OnComponentDetached(EntityUid uid, SchizophreniaComponent component, LocalPlayerDetachedEvent args)
    {
        QueueDel(component.Hallucination);
    }

    private void ShowHallucination(EntityUid uid)
    {
        if (!TryComp<SchizophreniaComponent>(uid, out var schizophrenia))
            return;

        if (_timing.CurTime <= schizophrenia.NextIncidentTime)
            return;

        // Set the new time.
        var timeInterval = _random.NextFloat(schizophrenia.MinTimeBetweenIncidents, schizophrenia.MaxTimeBetweenIncidents);
        schizophrenia.NextIncidentTime += TimeSpan.FromSeconds(timeInterval);

        // Offset position where the hallucination occurs
        var randomOffset =
            new Vector2 (
                _random.NextFloat(-schizophrenia.MaxHallucinationDistance, schizophrenia.MaxHallucinationDistance),
                _random.NextFloat(-schizophrenia.MaxHallucinationDistance, schizophrenia.MaxHallucinationDistance)
            );

        var newCoords = Transform(uid).Coordinates.Offset(randomOffset);

        var hallucinationPrototype = _random.Pick(schizophrenia.Hallucinations);

        // Show the hallucination
        schizophrenia.Hallucination = SpawnAtPosition(hallucinationPrototype, newCoords);
    }
}
