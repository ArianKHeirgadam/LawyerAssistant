using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerAssistant.Application.DTOs.Base;

public class SmsMessageDto
{
    public long MessageId { get; set; } // شناسه یکتای این پیامک
    public string Message { get; set; } // متن پیام ارسال شده
    public int Status { get; set; } // وضعیت عددی این پیامک
    public string StatusText { get; set; } // متن توضیح وضعیت عددی این پیامک
    public string Sender { get; set; } // شماره فرستنده
    public string Receptor { get; set; } // شماره گیرنده
    public long Date { get; set; } // زمان ارسال این پیامک به فرمت UnixTime
    public int Cost { get; set; } // هزینه این پیامک (ریال)
}