using AA2ApiNET6._3_Infrastructure.Infrastructure.Impl.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AA2ApiNET6._1_Presentation.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly DataContext _dataContext;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            DataContext dataContext
            ) : base(options, logger, encoder, clock)
        {
            _dataContext = dataContext;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization headers");
            }

            try
            {
                var uthenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(uthenticationHeaderValue.Parameter);
                var credentials = Encoding.UTF8.GetString(bytes).Split(":");
                string email = credentials[0];
                var password = credentials[1];

                if(email == "admin@admin.com")
                {
                    var userAdmin = _dataContext.Specialists.Where(user => user.Email == email && user.Password == password).FirstOrDefault();

                    if (userAdmin == null)
                    {
                        return AuthenticateResult.Fail("Invalidad username or passoword");
                    }
                    else
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, userAdmin.Name.ToString()) };
                        var identity = new ClaimsIdentity(claims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);

                        return AuthenticateResult.Success(ticket);
                    }
                } else if(email.Contains("@specialist.com"))
                {
                    var specialist = _dataContext.Specialists.Where(user => user.Email == email && user.Password == password).FirstOrDefault(); //BUSCAR EN EL DATACONETXT DE PACIENTES Y COMPARAR AMBOS
                    if (specialist == null)
                    {
                        return AuthenticateResult.Fail("Invalidad username or passoword");
                    }
                    else
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, specialist.Id.ToString()) };
                        var identity = new ClaimsIdentity(claims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);

                        return AuthenticateResult.Success(ticket);
                    }
                } else
                {
                    var user = _dataContext.Patients.Where(user => user.Email == email && user.Password == password).FirstOrDefault(); //BUSCAR EN EL DATACONETXT DE PACIENTES Y COMPARAR AMBOS
                    if (user == null)
                    {
                        return AuthenticateResult.Fail("Invalidad username or passoword");
                    }
                    else
                    {
                        var claims = new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) };
                        var identity = new ClaimsIdentity(claims, Scheme.Name);
                        var principal = new ClaimsPrincipal(identity);
                        var ticket = new AuthenticationTicket(principal, Scheme.Name);

                        return AuthenticateResult.Success(ticket);
                    }
                }
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Fail");
            }
        }
    }
}
