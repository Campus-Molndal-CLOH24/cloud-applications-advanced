namespace MerchStore.Domain.Common;

public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        if (EqualityComparer<TId>.Default.Equals(id, default))
        {
            throw new ArgumentException("The entity ID cannot be the default value.", nameof(id));
        }

        Id = id;
    }

    // Required for EF Core, even if using private setters elsewhere
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Entity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.


    // Överskuggar Object.Equals metoden för att jämföra två objekt
    // Denna metod anropas när du använder object.Equals() eller när .NET behöver jämföra två objekt
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    // Implementerar IEquatable<T> gränssnittet för mer effektiv jämförelse
    // Denna metod används när samlingen vet att den jämför två Entity<TId>-objekt
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    // Överlagrar == operatorn så att du kan skriva: entity1 == entity2
    // Detta ger en mer intuitiv syntax för jämförelser
    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    // Överlagrar != operatorn för motsatt jämförelse
    // Detta är alltid motsatsen till == operatorn
    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    // Överskuggar GetHashCode för att entiteter ska fungera korrekt i Dictionary<>, HashSet<> etc.
    // HashCode måste vara konsekvent med Equals: om två objekt är lika måste de ha samma hash-kod
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}