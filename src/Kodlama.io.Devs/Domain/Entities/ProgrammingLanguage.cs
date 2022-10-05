using Core.Domain.Entities;

namespace Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; }

    public virtual ICollection<Technology> Technologies { get; set; }
    public ProgrammingLanguage()
    {

    }

    public ProgrammingLanguage(int id, string name)
        => (Id, Name) = (id, name);
}
