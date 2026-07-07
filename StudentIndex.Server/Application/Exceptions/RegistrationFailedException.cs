namespace StudentIndex.Server.Application.Exceptions;

/// <summary>
/// Baca se unutar transakcije registracije kada kreiranje Identity korisnika ne uspije,
/// kako bi se upis studenta vratio nazad (rollback).
/// </summary>
public class RegistrationFailedException : Exception
{
    public IEnumerable<string> Errors { get; }

    public RegistrationFailedException(IEnumerable<string> errors)
        : base("Registracija korisnika nije uspjela.")
    {
        Errors = errors;
    }
}
