namespace DomainModels
{
    public abstract class BaseClass
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime ModifiedOn { get; set; }
    }
}
