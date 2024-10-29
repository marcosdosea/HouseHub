using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace HouseHubWeb.Helpers
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        private readonly string _from;
        private readonly string _webRootPath;

        public EmailSender(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _from = configuration["Smtp:From"];
            _webRootPath = environment.WebRootPath; // Obtém o caminho físico do wwwroot
            _client = new SmtpClient
            {
                Host = configuration["Smtp:Host"],
                Port = int.Parse(configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
                EnableSsl = true
            };
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Caminho físico para a imagem no servidor, dentro do wwwroot
            var imagePath = Path.Combine(_webRootPath, "img", "houseHub__b.svg");

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_from),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            // Cria o conteúdo HTML com a imagem embutida
            var htmlWithHeaderAndFooter = $@"
            <html>
                <body>
                    <div style='text-align: center; margin-bottom: 20px;'>
                        <img src='cid:HeaderImage' alt='Househub' style='max-width: 30%; height: auto;'/>
                    </div>

                    <div style='margin-bottom: 20px; font-size: 14px'>
                        {htmlMessage}
                    </div>

                    <div style='text-align: center; font-size: 14px; color: #888888; margin-top: 20px; width: 100%; padding: 20px 0; background-color: #f1f1f1;'>
                        <p>&copy; 2024 - Househub - Todos os direitos reservados.</p>
                    </div>
                </body>
            </html>";

            // Cria a visualização alternativa com o HTML e a imagem inline
            var altView = AlternateView.CreateAlternateViewFromString(htmlWithHeaderAndFooter, null, MediaTypeNames.Text.Html);

            // Adiciona a imagem como um recurso inline
            var inlineLogo = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg)
            {
                ContentId = "HeaderImage"
            };
            altView.LinkedResources.Add(inlineLogo);

            // Adiciona a visualização alternativa (HTML com a imagem inline) ao email
            mailMessage.AlternateViews.Add(altView);

            return _client.SendMailAsync(mailMessage);
        }
    }
}
