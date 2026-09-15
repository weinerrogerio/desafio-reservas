using FluentValidation;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Base;

namespace WorkspaceReservas.Utils
{ 
    //public class BaseRequestValidator<T> : AbstractValidator<T> where T : BaseEntity
    //{
    //    public BaseRequestValidator()
    //    {
    //        RuleFor(x => x.Id).GreaterThan(0).WithMessage("ID must be greater than zero.");
    //    }
    //}

    // Validador específico herdando do validador base
    public class SalaRequestValidator : AbstractValidator<SalaDTO>
    {
        public SalaRequestValidator()
        {
            // Já possui a validação do ID vinda do BaseRequestValidator!
            RuleFor(x => x.Nome)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required.");

            RuleFor(x => x.Capacidade)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than zero.");

            RuleFor(x => x.PrecoPorHora)
                .GreaterThan(0)
                .WithMessage("Price per hour must be greater than zero.");
        }
    }

    public class SalaUpdateDTOValidator : AbstractValidator<SalaUpdateDTO>
    {
        public SalaUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("ID must be greater than zero.");

            // Só valida Nome se foi fornecido (não é nulo)
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Name cannot be empty.")
                .When(x => x.Nome != null);

            // Só valida Capacidade se foi fornecido (não é nulo)
            RuleFor(x => x.Capacidade)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than zero.")
                .When(x => x.Capacidade.HasValue);

            // Só valida PrecoPorHora se foi fornecido (não é nulo)
            RuleFor(x => x.PrecoPorHora)
                .GreaterThan(0)
                .WithMessage("Price per hour must be greater than zero.")
                .When(x => x.PrecoPorHora.HasValue);
        }
    }

    public class ReservaRequestValidator : AbstractValidator<ReservaDTO>
    {
        public ReservaRequestValidator()
        {

            RuleFor(x => x.IdSalaFk)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull()
                .WithMessage("Sala ID must be imformed.");

            RuleFor(x => x.DataHoraInicio)
                .LessThan(x => x.DataHoraFim)
                .WithMessage("end time must be greater than start time.");

            RuleFor(x => x.DataHoraInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage("Start time must be in the future or today.")
                .NotEmpty()
                .NotNull()
                .WithMessage("End time is required.");

            RuleFor(x => x)
                .Must(r => {
                    var duracao = r.DataHoraFim - r.DataHoraInicio;
                    return duracao >= TimeSpan.FromMinutes(30) && duracao <= TimeSpan.FromHours(8);
                })
                .WithMessage("Duration must be between 30 minutes and 8 hours.");
        }
    }
}