namespace Korbitec.Licensing.FunctionApplication.Dtos
{
    public class ValidateKeyDeactivationResponseDto
    {
        public ValidateKeyDeactivationResponseDto(bool keyValidAndDeactivated)
        {
            KeyValidAndDeactivated = keyValidAndDeactivated;
        }
        public bool KeyValidAndDeactivated { get; set; }
    }
}
