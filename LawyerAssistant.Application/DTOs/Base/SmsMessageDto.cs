using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerAssistant.Application.DTOs.Base;


public class SmsMessageDto
{
    public Return _return { get; set; }
    public Entry[] entries { get; set; }
}

public class Return
{
    public int status { get; set; }
    public string message { get; set; }
}

public class Entry
{
    public int messageid { get; set; }
    public string message { get; set; }
    public int status { get; set; }
    public string statustext { get; set; }
    public string sender { get; set; }
    public string receptor { get; set; }
    public int date { get; set; }
    public int cost { get; set; }
}
