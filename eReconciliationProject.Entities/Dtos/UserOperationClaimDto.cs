namespace eReconciliationProject.Entities.Dtos;
public class UserOperationClaimDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
    public int CompanyId { get; set; }
    public string OperationClaimDescription { get; set; }
    public string OperationClaimName { get; set; }

}
