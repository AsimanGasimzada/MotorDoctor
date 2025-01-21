namespace MotorDoctor.Business.Services.Abstractions;

public interface IPaymentService
{
    Task<PaymentResponseDto> CreateAsync(PaymentCreateDto dto);
    Task<bool> CheckPaymentAsync(PaymentCheckDto dto);
}
