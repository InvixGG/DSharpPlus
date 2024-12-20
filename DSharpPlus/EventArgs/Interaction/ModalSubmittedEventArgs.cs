using System.Collections.Generic;

using DSharpPlus.Entities;

using Newtonsoft.Json;

namespace DSharpPlus.EventArgs;

/// <summary>
/// Fired when a modal is submitted. Note that this event is fired only if the modal is submitted by the user, and not if the modal is closed.
/// </summary>
public class ModalSubmittedEventArgs : InteractionCreatedEventArgs
{
    /// <summary>
    /// A dictionary of submitted fields, keyed on the custom id of the input component.
    /// </summary>
    [JsonIgnore]
    public IReadOnlyDictionary<string, string> Values { get; }

    /// <summary>
    /// The custom ID this modal was sent with.
    /// </summary>
    [JsonIgnore]
    public string Id => this.Interaction.Data.CustomId;

    internal ModalSubmittedEventArgs(DiscordInteraction interaction)
    {
        this.Interaction = interaction;

        Dictionary<string, string> dict = [];

        foreach (DiscordActionRowComponent component in interaction.Data.components)
        {
            if (component.Components[0] is DiscordTextInputComponent input)
            {
                dict.Add(input.CustomId, input.Value);
            }
        }

        this.Values = dict;
    }
}
