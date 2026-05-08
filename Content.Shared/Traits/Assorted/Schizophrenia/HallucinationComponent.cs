using Robust.Shared.GameStates;

namespace Content.Shared.Traits.Assorted.Schizophrenia;

/// <summary>
/// This component is used for hallucinations, which are caused by the schizophrenia trait
/// </summary>
[RegisterComponent, NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class HallucinationComponent : Component
{
    /// <summary>
    /// The minimum remaining time for the attached entity to exist
    /// </summary>
    [DataField("minTimeLeft", required: true), ViewVariables(VVAccess.ReadWrite)]
    [AutoNetworkedField]
    public float MinTimeLeft = 5f;
}
