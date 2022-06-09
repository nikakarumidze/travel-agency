namespace API.Models.UserRequests;

public class UpdateUserInfoRequestModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public byte[] Image { get; set; }
}