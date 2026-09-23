using FluentValidation;
using WorkspaceReservas.Data.Dto;
using WorkspaceReservas.Data.Dto.UpdateDTO;
using WorkspaceReservas.Models;
using WorkspaceReservas.Models.Base;


// -------------------------------------- UTILIZADO PARA REGRAS COM FLUENT VALIDATION!!!
//obs ao criar nova classe de regras NovaClasse: AbstractValidator<NovoObjeto> não esquecer de registrar no Program.cs com builder.Services.AddValidatorsFromAssemblyContaining<NovaClasse>();

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

    public class SalaUpdateValidator : AbstractValidator<SalaUpdateDTO>
    {
        public SalaUpdateValidator()
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

    public class MedicoRequestValidator : AbstractValidator<MedicoDTO>
    {
        public MedicoRequestValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.CRM)
                .NotEmpty()
                .WithMessage("CRM is required.")
                .MinimumLength(4)
                .WithMessage("CRM must be at least 4 characters long.");
        }
    }

    public class ConsultaRequestValidator : AbstractValidator<ConsultaDTO>
    {
        public ConsultaRequestValidator()
        {
            RuleFor(x => x.NomePaciente)
                .NotEmpty()
                .WithMessage("Patient name is required.");

            RuleFor(x => x.MedicoIdFk)
                .GreaterThan(0)
                .WithMessage("Doctor ID must be greater than zero.");

            RuleFor(x => x.DataHoraInicio)
                .GreaterThan(DateTime.Now)
                .WithMessage("Start time must be in the future or today.");

            RuleFor(x => x.DataHoraFim)
                .GreaterThan(x => x.DataHoraInicio)
                .WithMessage("End time must be greater than start time.");

            RuleFor(x => x.DataHoraFim)
                .Must((consulta, fim) => ( fim - consulta.DataHoraInicio ) >= TimeSpan.FromMinutes(20))
                .WithMessage("Duration must be at least 20 minutes.");
        }
    }

    //rever se precisa
    //public class ConsultaRequestUpdateValidator : AbstractValidator<ConsultaUpdateDTO>
    //{
    //    public ConsultaRequestUpdateValidator()
    //    {
    //        RuleFor(x => x.NomePaciente)
    //            .NotEmpty()
    //            .WithMessage("Patient name is required.");

    //        RuleFor(x => x.MedicoIdFk)
    //            .GreaterThan(0)
    //            .WithMessage("Doctor ID must be greater than zero.");

    //        RuleFor(x => x.DataHoraInicio)
    //            .GreaterThan(DateTime.Now)
    //            .WithMessage("Start time must be in the future or today.");

    //        RuleFor(x => x.DataHoraFim)
    //            .GreaterThan(x => x.DataHoraInicio)
    //            .WithMessage("End time must be greater than start time.");

    //        RuleFor(x => x.DataHoraFim)
    //            .Must((consulta, fim) => ( fim - consulta.DataHoraInicio ) >= TimeSpan.FromMinutes(20))
    //            .WithMessage("Duration must be at least 20 minutes.");
    //    }
    //}

}