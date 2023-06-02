using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Domain.IdentityModel;
using MailKit.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using Org.BouncyCastle.Crypto.Macs;

namespace hrOT.Application.Auth.Queries
{
    public class ResetPassword : IRequest<string>
    {
        public string Email { get; set; }
    }

    public class ResetPasswordHandler : IRequestHandler<ResetPassword, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(ResetPassword request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), request.Email);

            var newPassword = GenerateRandomPassword(); // Tạo một mật khẩu ngẫu nhiên mới

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, await _userManager.GeneratePasswordResetTokenAsync(user), newPassword);
            if (!resetPasswordResult.Succeeded)
            {
                return("Đặt lại mật khẩu thất bại");
            }

            await SendEmailAsync(user.Email, "Đặt lại mật khẩu", $"Mật khẩu mới của bạn là: {newPassword}");

            return "Đặt lại mật khẩu thành công. Vui lòng kiểm tra email của bạn để biết mật khẩu mới.";
        }

        private string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string specialChars = "!@#$%^&*()";
            var random = new Random();
            var password = new StringBuilder();

            // Tạo phần đầu tiên của mật khẩu
            for (int i = 0; i < 5; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            // Tạo ký tự đặc biệt
            password.Append(specialChars[random.Next(specialChars.Length)]);

            // Xáo trộn các ký tự trong mật khẩu
            for (int i = password.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                char temp = password[i];
                password[i] = password[j];
                password[j] = temp;
            }

            return password.ToString();
        }


        private async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("LogOT", "bishamond1108@gmail.com"));// Tài khoản gmail
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("bishamond1108@gmail.com", "mznixcvlkvssfdco"); //Tài khoản gmail và mạt khẩu của ứng dụng
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

    }
}
