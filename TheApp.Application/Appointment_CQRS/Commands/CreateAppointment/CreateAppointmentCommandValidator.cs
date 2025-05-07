using FluentValidation;
using TheApp.Application.AppointmentDTO.Commands.CreateAppointment;
using TheApp.Domain.Interfaces;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    private readonly IDentalStudioServiceRepository _serviceRepository;

    public CreateAppointmentCommandValidator(IDentalStudioServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;

        RuleFor(a => a.StartTime)
            .NotEmpty().WithMessage("Start time is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("Start time must be in the future.");

        RuleFor(a => a.DentalStudioServiceId)
            .NotEmpty().WithMessage("Dental Studio Service ID is required.")
            .MustAsync(ServiceExists).WithMessage("Selected service does not exist.");

        RuleFor(a => a)
            .MustAsync(NoOverlappingAppointments).WithMessage("The selected time slot is already booked.");
    }

    private async Task<bool> ServiceExists(int serviceId, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetServiceById(serviceId);
        return service != null;
    }

    private async Task<bool> NoOverlappingAppointments(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetServiceById(command.DentalStudioServiceId);
        if (service == null || service.Duration == null)
            return false;

        var endTime = command.StartTime.Add(service.Duration.Value);

        var existingAppointments = await _serviceRepository.GetFutureAppointmentsForService(command.DentalStudioServiceId, command.StartTime.Date);
        return !existingAppointments.Any(a => command.StartTime < a.EndTime && endTime > a.StartTime);
    }
}