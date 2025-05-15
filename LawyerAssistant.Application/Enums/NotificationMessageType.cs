using System.ComponentModel.DataAnnotations;

namespace Application.Enums
{
    public enum NotificationMessageType
    {
        [Display(Name = "ثبت رزرو جدید"  , Description= "/reserve/reservationDetail/")]
        RegisterNewOrder = 1,

        [Display(Name = "ثبت کارت به کارت جدید" , Description = "/peyment/CardToCardDetail/")]
        PaddingCardToCardPayment = 2,

        [Display(Name = "ثبت تیکت جدید توسط مشتری" , Description = "/ticketsNotifications/ticketDetail/")]
        CustomerRegisterTicket = 3,
    }
}
