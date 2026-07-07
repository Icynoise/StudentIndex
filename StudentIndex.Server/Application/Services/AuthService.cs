using Microsoft.Extensions.Logging;
using StudentIndex.Server.Application.DTOs;
using StudentIndex.Server.Application.Exceptions;
using StudentIndex.Server.Application.Interfaces;
using StudentIndex.Server.Domain;
using StudentIndex.Server.Domain.Constants;

namespace StudentIndex.Server.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IIdentityService identityService,
            ITokenGenerator tokenGenerator,
            IStudentRepository studentRepository,
            IUnitOfWork unitOfWork,
            ILogger<AuthService> logger)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<AuthResultDto> RegisterAsync(RegisterStudentRequest request)
        {
            try
            {
                // Upis studenta i kreiranje Identity korisnika moraju biti atomarni —
                // ako kreiranje korisnika ne uspije, upis studenta se vraća nazad.
                await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {
                    var student = new Studenti
                    {
                        Ime = request.Ime,
                        Prezime = request.Prezime,
                        BrojIndexa = request.BrojIndexa,
                        Email = request.EmailStudent,
                        Telefon = request.Telefon,
                        DatumRođenja = request.DatumRodjenja,
                        Status = request.Status ?? StatusStudenta.Aktivan
                    };

                    await _studentRepository.AddAsync(student);

                    var result = await _identityService.CreateStudentUserAsync(
                        request.Email, request.Password, student.StudentId);

                    if (!result.Succeeded)
                        throw new RegistrationFailedException(result.Errors);
                });
            }
            catch (RegistrationFailedException ex)
            {
                return new AuthResultDto { Succeeded = false, Errors = ex.Errors };
            }

            _logger.LogInformation("Registrovan novi student: {Email}", request.Email);
            return new AuthResultDto { Succeeded = true };
        }

        public async Task<AuthResultDto> LoginAsync(string email, string password)
        {
            var user = await _identityService.ValidateCredentialsAsync(email, password);
            if (user == null)
            {
                _logger.LogWarning("Neuspješna prijava za {Email}", email);
                return new AuthResultDto
                {
                    Succeeded = false,
                    Errors = new[] { "Pogrešan email ili lozinka." }
                };
            }

            return await IssueTokensAsync(user);
        }

        public async Task<AuthResultDto> RefreshTokenAsync(string refreshToken)
        {
            var user = await _identityService.ValidateAndRevokeRefreshTokenAsync(refreshToken);
            if (user == null)
                throw new UnauthorizedException("Refresh token je nevažeći ili je istekao.");

            return await IssueTokensAsync(user);
        }

        public Task LogoutAsync(string refreshToken)
            => _identityService.RevokeRefreshTokenAsync(refreshToken);

        private async Task<AuthResultDto> IssueTokensAsync(UserInfoDto user)
        {
            var accessToken = _tokenGenerator.GenerateAccessToken(user);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();
            await _identityService.StoreRefreshTokenAsync(user.UserId, refreshToken);

            return new AuthResultDto
            {
                Succeeded = true,
                Token = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
