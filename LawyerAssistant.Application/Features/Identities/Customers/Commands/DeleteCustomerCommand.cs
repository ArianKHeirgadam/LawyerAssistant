using LawyerAssistant.Application.Objects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LawyerAssistant.Application.Features.Identities.Customers.Commands;

public class DeleteCustomerCommand : IRequest<SysResult>
{
    /// <summary>
    /// شناسه مشتری
    /// </summary>
    [Display(Name = "شناسه مشتری")]
    [Required(ErrorMessage = ValidationCommonMessages.Required)]
    public int Id { get; set; }
}