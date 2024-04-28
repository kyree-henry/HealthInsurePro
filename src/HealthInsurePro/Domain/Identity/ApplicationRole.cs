namespace HealthInsurePro.Domain.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        { }

        public ApplicationRole(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Description { get; set; }
    }
}