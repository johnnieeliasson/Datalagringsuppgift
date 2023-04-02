
namespace ConsoleApp1.Models.Entities;

    internal class CaseEntity
    {
        public string CaseId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string? UserPhoneNumber { get; set; } = null!;
        public string? UserCompany { get; set; }
        public string Description { get; set; } = null!;   
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public int StatusId { get; set; } = 1;
        public StatusEntity Status { get; set; } = null!;
        public int UserId { get; set; }
    
        public UserEntity User { get; set; } = null!;
        public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    }

