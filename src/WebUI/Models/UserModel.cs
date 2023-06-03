namespace WebUI.Models;

public class UserModel
{
    public string userId {  get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> listRoles { get; set; }
}
