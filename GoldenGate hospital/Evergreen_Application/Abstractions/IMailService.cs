using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain.Helper;

namespace Evergreen_Application.Abstractions
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
