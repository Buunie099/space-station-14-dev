using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Utility;

namespace Content.Shared.Traits.Assorted.Schizophrenia;

/// <summary>
/// This component is used for schizophrenia, which causes visual hallucinations.
/// </summary>
[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class SchizophreniaComponent : Component
{
    /// <summary>
    /// The maximum time between incidents in seconds
    /// </summary>
    [DataField("maxTimeBetweenIncidents", required: true), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
    public float MaxTimeBetweenIncidents = 60f;

    /// <summary>
    /// The minimum time between incidents in seconds
    /// </summary>
    [DataField("minTimeBetweenIncidents", required: true), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
    public float MinTimeBetweenIncidents = 30f;

    /// <summary>
    /// The maximum distance to display a hallucination at
    /// </summary>
    [DataField("maxHallucinationDistance", required: true), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
    public float MaxHallucinationDistance = 10f;

    /// <summary>
    /// The hallucination entities
    /// </summary>
    [DataField("hallucinations", required: true), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
    public List<string> Hallucinations = [];

    [DataField("timeBetweenIncidents", customTypeSerializer: typeof(TimeOffsetSerializer)), ViewVariables(VVAccess.ReadWrite)]
    public TimeSpan NextIncidentTime;

    public EntityUid? Hallucination;
}
