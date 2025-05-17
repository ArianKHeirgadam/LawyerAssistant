using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.DTOs.Identities;

public class LoginDTO
{
    //======================================================================
    /// <summary>
    /// نام کاربری
    /// </summary>
    [Required(ErrorMessage = "نام کاربری  نبایستی خالی باشد")]
    public string Username { get; set; }

    //======================================================================
    /// <summary>
    /// کلمه عبور
    /// </summary>
    [Required(ErrorMessage = "کلمه عبور نباید خالی باشد")]
    public string Password { get; set; }

}

