namespace API.Models.UserRequests.UserRequestModels;

public class UpdateUserInfoRequestModel
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    /// <summary>
    /// send as a byte array . . . .
    /// </summary>
    public byte[] Image { get; set; }
    public string Bio { get; set; }
}