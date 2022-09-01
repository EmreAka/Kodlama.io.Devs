using Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; }

    public ProgrammingLanguage()
    {

    }

    public ProgrammingLanguage(int id, string name)
        => (Id, Name) = (id, name);
}
