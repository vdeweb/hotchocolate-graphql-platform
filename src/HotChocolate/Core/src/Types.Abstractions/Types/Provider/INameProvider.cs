namespace HotChocolate.Types;

/// <summary>
/// A type system member that has a name.
/// </summary>
public interface INameProvider : ITypeSystemMember
{
    /// <summary>
    /// Gets the name of the type system member.
    /// </summary>
    /// <value>
    /// The name of the type system member.
    /// </value>
    string Name { get; }
}
