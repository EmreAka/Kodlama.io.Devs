using Core.Domain.Entities;
namespace Domain.Entities;

public class Technology : Entity
{
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }

    public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
    public Technology()
    {

    }

    public Technology(int id, int programmingLanguageId, string name) : this()
        => (Id, ProgrammingLanguageId, Name) = (id, programmingLanguageId, name);
}